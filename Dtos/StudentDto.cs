using System;

namespace aeronology_tech_exam_no2.Dtos
{
    public class StudentDto
    {
        public int StudentIDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public double OldGPA { get; set; }
        public bool IsStarSection { get; set; }
    }
}
