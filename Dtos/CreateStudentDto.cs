using System;
using System.ComponentModel.DataAnnotations;

namespace aeronology_tech_exam_no2.Dtos
{
    public class CreateStudentDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Range(4,99)]
        public int Age { get; set; }
        [Range(0.0,100.0)]
        public double OldGPA { get; set; }
    }
}
