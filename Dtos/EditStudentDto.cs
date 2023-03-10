using System;
using System.ComponentModel.DataAnnotations;

namespace aeronology_tech_exam_no2.Dtos
{
    public class EditStudentDto
    {
        [Required]
        public int StudentIDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? Age { get; set; }
        public double? OldGPA { get; set; }
    }
}
