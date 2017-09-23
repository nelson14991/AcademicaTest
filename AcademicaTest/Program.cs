using AcademicaTest.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcademicaTest
{
    class Program
    {
        static void Main()
        {
            try
            {
                ApiService service = new ApiService();
                IEnumerable<Enrollment> enrollments = service.getListEnrollments(" ").Result;
                IEnumerable<GradeBookItem> gradeBookItems = service.getEntityGradeBook("", "").Result;
                List<TeacherResponse> teacherResponseList = new List<TeacherResponse>();
                //Loop through each enrollment
                foreach (var enrollment in enrollments)
                {
                    //find gradeBookItem that matches enrollment CourseID
                    var gradeBookItem = gradeBookItems.Where(x => x.ItemID == enrollment.CourseID).FirstOrDefault();
                    int isPastDue = DateTime.Compare(gradeBookItem.DueDate, DateTime.Now);
                    //check if assignment is pastdue or not submitted, update grade to 0
                    if (isPastDue > 0 || gradeBookItem.Status == 0)
                    {
                        TeacherResponse teacherResponse = new TeacherResponse();
                        teacherResponse.EnrollmentID = enrollment.EnrollmentID;
                        teacherResponse.ItemID = gradeBookItem.ItemID;
                        teacherResponse.PointsAssigned = 0;
                        teacherResponseList.Add(teacherResponse);
                    }
                    else {
                        //get existing TeacherResponse from api and update grade accordingly
                    }
                }

                service.putTeacherResponses(teacherResponseList).Wait();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.InnerException.Message.ToString());
            }
            

        }



    }
}
