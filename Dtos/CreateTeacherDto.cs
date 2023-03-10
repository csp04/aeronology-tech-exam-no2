using System;
using System.ComponentModel.DataAnnotations;

namespace aeronology_tech_exam_no2.Dtos
{
    public class CreateTeacherDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [Range(18, 99)]
        public int Age { get; set; }
        public bool IsStarSectionAdviser { get; set; }
    }
}
