using AcademicaTest.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AcademicaTest
{
    class ApiService
    {
        static HttpClient client = new HttpClient();

        public ApiService() {
            client.BaseAddress = new Uri("http://localhost:55268/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Post method to putTeacherResponse into db
        public async Task<Boolean> putTeacherResponses(List<TeacherResponse> Responses)
        {
            //converts TeacherResponse to Json data and posts asynchronously
            HttpResponseMessage response = await client.PostAsJsonAsync("api/", Responses);
            response.EnsureSuccessStatusCode();
            return true;
        }

        //Method to send Get request for getting current enrollments
        public async Task<List<Enrollment>> getListEnrollments(string path)
        {
            List<Enrollment> enrollments = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                //Reads response data and converts to Enrollment object
                enrollments = await response.Content.ReadAsAsync<List<Enrollment>>();
            }
            return enrollments;
        }

        //Method to send Get request for getting current gradebook entries
        public async Task<List<GradeBookItem>> getEntityGradeBook(string courseID, string enrollmentID)
        {
            List<GradeBookItem> gradeBookItems = null;
            HttpResponseMessage response = await client.GetAsync(string.Format("api/courseID={0}&enrollmentID={1}", courseID, enrollmentID));
            if (response.IsSuccessStatusCode)
            {
                //Reads response data and converts to Enrollment object
                gradeBookItems = await response.Content.ReadAsAsync<List<GradeBookItem>>();
            }
            return gradeBookItems;
        }       
    }
}
