using System;

namespace aeronology_tech_exam_no2.Models
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public double OldGPA { get; set; }
        public bool IsStarSection => OldGPA > 95;
    }

}
