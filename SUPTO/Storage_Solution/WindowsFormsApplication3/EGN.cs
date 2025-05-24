using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class EGN
    {
   public static string checkEGN(string EGN, int pol)
   {
            int g;

            int m;

            int d;

            int sum;

            byte[] T = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

            if (EGN.Length != 10)

            {

                return "Greshna daljina!";

            }

            for (byte i = 0; i <= 9; i++)

            {

                if (!char.IsDigit(EGN[i]))

                {

                    return "Greshka  " + i + 1 + " pozicia";

                }

            }

            g = Convert.ToInt16(EGN.Substring(0, 2));

            m = Convert.ToInt16(EGN.Substring(2, 2));

            d = Convert.ToInt16(EGN.Substring(4, 2));

            if (m > 40)

            {

                m -= 40;

                g += 2000;

            }

            else if (m > 20)

            {

                m -= 20;

                g += 1800;

            }

            else

            {

                g += 1900;

            }

            if (m > 12 | m < 1)

            {

                return "Greshen mesec!";

            }

            if (d < 1 | d > System.DateTime.DaysInMonth(g, m))

            {

                return "Greshen  den";

            }

            if (pol == 0 ^ (Convert.ToInt16(EGN[8].ToString()) % 2 == 0))

            {

                return "Greshen pol!";

            }

            sum = 0;

            for (byte i = 0; i <= 8; i++)

            {

                sum += (Convert.ToInt16(EGN[i].ToString()) * Convert.ToInt16(T[i].ToString()));

            }

            sum = sum % 11;

            if (sum == 10) sum = 0;

            if (sum != Convert.ToInt16(EGN[9].ToString()))

            {

                return "Greshna kontrolna cifra";

            }

            return ("OK");


   }
    }
}
