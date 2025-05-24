using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class Cyrilic_hex_array
    {
        public static byte[] Cyrilic_hex(string input)
        {

            Encoding encod = Encoding.GetEncoding(1251);
            char[] myChars = input.ToCharArray();
            byte[] conversion = new byte[myChars.Length];
            for (int j = 0; j <= myChars.Length - 1; j++)
            {

                byte code = encod.GetBytes(new[] { myChars[j] })[0];
                conversion[j] = encod.GetBytes(new[] { myChars[j] })[0];

            }

            return conversion;


        }
    }
}
