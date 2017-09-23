using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicaTest.model
{
    class GradeBookItem
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
    }
}
