using System;

namespace aeronology_tech_exam_no2.Dtos
{
    public class TeacherDto
    {
        public int TeacherIDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public bool IsStarSectionAdviser { get; set; }
        public int HandledStudents { get; set; }
    }
}
