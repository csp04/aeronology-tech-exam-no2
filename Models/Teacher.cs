using System;
using System.Collections.Generic;

namespace aeronology_tech_exam_no2.Models
{
    public class Teacher : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public List<Student> Students { get; set; }
        public bool IsStarSectionAdviser { get; set; }
    }

}
