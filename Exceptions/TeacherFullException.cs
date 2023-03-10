using System;

namespace aeronology_tech_exam_no2.Exceptions
{
    public class TeacherFullException : Exception
    {
        public TeacherFullException() : base("Can't add more teachers.")
        {

        }
    }
}
