using System;

namespace aeronology_tech_exam_no2.Utils
{
    public static class Rand
    {
        private static readonly Random rnd = new();

        public static int GetRandomNumber(int max)
        {
            lock (rnd)
            {
                return rnd.Next(max);
            }
        }
    }
}
