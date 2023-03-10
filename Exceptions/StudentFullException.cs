using System;

namespace aeronology_tech_exam_no2.Exceptions
{
    public class StudentFullException : Exception
    {
        public StudentFullException() : base("Can't add more students.")
        {

        }
    }
}
