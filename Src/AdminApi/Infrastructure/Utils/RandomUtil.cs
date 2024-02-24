using System;

namespace Infrastructure
{


    public static class RandomUtil
    {
        public static string GenerateNumber (int Digit)
        {
            string Number=string.Empty;

            for (var i = 0; i < Digit; i++)
            {
                Random rd = new Random();
                int num=rd.Next(0,10);
                Number += num;
            }

            return Number;
        }
    }
}
