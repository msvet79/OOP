using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class Global_Variables
    {

        public static byte[] STX = new byte[] { 1 };//“STX” – начало на пакетираното съобщение. 
        public static byte[] ENQ = new byte[] { 5 };//“ENQ”, 	Enquiry
        public static byte[] ETX = new byte[] { 3 };//“ETX” – край на пакетираното съобщение.

        private static byte _seq;
        public static byte seq{

            get
            {

                return _seq;
            }

            set 
            {

                _seq = value;
            }
        
        }
   
    }


}
