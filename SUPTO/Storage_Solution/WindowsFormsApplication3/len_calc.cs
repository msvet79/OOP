using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class len_calc
    {
        public static int len_c(byte[] prodazhba)
        {
            int dalzhina = prodazhba.Length + 1;//тази единица играе ролята на байта,който е за самото поле len

            return dalzhina + 32;//32 е фиксираното отместване на дължина 20hh според комуникационния протокол


        }
    }
}
