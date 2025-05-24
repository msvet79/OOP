using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class Contol_Amount_Calculation
    {
        public static byte[] BCC(byte[] c)
        {
            int sum1 = ComputeAdditionChecksum(c);
            string hexValue = sum1.ToString("X");
            switch (hexValue.Length)
            {
                case 1:
                    hexValue = "000" + hexValue;
                    break;
                case 2:
                    hexValue = "00" + hexValue;
                    break;
                case 3:
                    hexValue = "0" + hexValue;
                    break;

            }
            byte[] controll_amount = new byte[4];

            for (int i = 0; i <= 3; i++)
            {


                switch (hexValue[i])
                {

                    case '0': controll_amount[i] = 48;
                        break;

                    case '1': controll_amount[i] = 49;
                        break;
                    case '2': controll_amount[i] = 50;
                        break;
                    case '3': controll_amount[i] = 51;
                        break;
                    case '4': controll_amount[i] = 52;
                        break;
                    case '5': controll_amount[i] = 53;
                        break;
                    case '6': controll_amount[i] = 54;
                        break;
                    case '7': controll_amount[i] = 55;
                        break;
                    case '8': controll_amount[i] = 56;
                        break;
                    case '9': controll_amount[i] = 57;
                        break;
                    case 'A': controll_amount[i] = 58;
                        break;
                    case 'B': controll_amount[i] = 59;
                        break;
                    case 'C': controll_amount[i] = 60;
                        break;
                    case 'D': controll_amount[i] = 61;
                        break;
                    case 'E': controll_amount[i] = 62;
                        break;
                    case 'F': controll_amount[i] = 63;
                        break;

                }

            }

            return controll_amount;


        }

        private static int ComputeAdditionChecksum(byte[] test)
        {
            int longSum = test.Sum(x => (int)x);
            return unchecked((int)longSum);

        }
    }
}
