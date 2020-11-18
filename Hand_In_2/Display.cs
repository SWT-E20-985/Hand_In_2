using System;
using System.Collections.Generic;
using System.Text;

namespace Hand_In_2
{
    public class Display: IDisplay
    {
        public void print(string x) 
        {
            System.Console.WriteLine("DISPLAY: {0} \n",x);
        }

        public void CurrentCharge(double x) 
        {
            int y = 0;

            for (int i =0; i < 100; ++i)
            {
                y = ((((int)x)/5)-100)*(-1);

                System.Console.Write("\r Oplader...{0}%    ", y);
            }
        }


    }
}
