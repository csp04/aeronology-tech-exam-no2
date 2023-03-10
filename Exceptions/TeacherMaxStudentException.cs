using System;

namespace aeronology_tech_exam_no2.Exceptions
{
    public class TeacherMaxStudentException : Exception
    {
        public TeacherMaxStudentException() : base("Can't add more student for teachers.")
        {

        }
    }

    public class LowerSectionMaxStudentException : Exception
    {
        public LowerSectionMaxStudentException() : base("Can't add more student for lower section.")
        {

        }
    }
}
