using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class Total_all
    {
        public static byte[] Total_amount_final_calc(byte seq, byte Payment, Double total_amount)
        {

            byte[] obsta_suma_total_start = new byte[] { seq, 53, 9, Payment, };

            IEnumerable<byte> obsta_suma_total_all = obsta_suma_total_start.Concat(Cyrilic_hex_array.Cyrilic_hex(total_amount.ToString("0.00")).Concat(Global_Variables.ENQ));
            byte[] obsta_suma_total_all_byte_array = obsta_suma_total_all.ToArray();// от позиция 3 натам

            Byte len_53 = (byte)len_calc.len_c(obsta_suma_total_all.ToArray());

            byte[] obsta_suma_total_02_to_05 = new byte[] { len_53 };//

            IEnumerable<byte> obsta_suma_total_for_control_amount = obsta_suma_total_02_to_05.Concat(obsta_suma_total_all.ToArray());

            byte[] obsta_suma_total_for_control_amount_array = obsta_suma_total_for_control_amount.ToArray();// Горния ред в масив от байтове

            byte[] obsta_suma_total_BCC = Contol_Amount_Calculation.BCC(obsta_suma_total_for_control_amount_array);// Изчисление на BBC посредством метода

            IEnumerable<byte> obsta_suma_total_print = Global_Variables.STX.Concat(obsta_suma_total_for_control_amount_array).Concat(obsta_suma_total_BCC).Concat(Global_Variables.ETX);
            byte[] obsta_suma_total_ready = obsta_suma_total_print.ToArray();// Готовият масив за пращане

            return obsta_suma_total_ready;
        }


    }
}
