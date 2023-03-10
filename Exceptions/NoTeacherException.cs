using System;

namespace aeronology_tech_exam_no2.Exceptions
{
    public class NoTeacherException : Exception
    {
        public NoTeacherException() : base("No teacher found.")
        {

        }
    }
}
