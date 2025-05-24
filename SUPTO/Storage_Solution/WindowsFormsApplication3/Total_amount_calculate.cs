using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class Total_amount_calculate
    {
        public static byte[] Total_amount_cal(byte seq, Double total_amount)
        {
            byte[] prodazhba = new byte[] { seq, 49, 206, 225, 249, 224, 32, 241, 243, 236, 224, 32, 226, 32, 
227, 240, 243, 239, 224, 32,  193, 9, 193, 43};

            IEnumerable<byte> prodazhba_seq = prodazhba.Concat(Cyrilic_hex_array.Cyrilic_hex(total_amount.ToString() + "*1").Concat(Global_Variables.ENQ));//Към продажба добавяме общата сума като число и *1 и добавяме “ENQ”

            Byte len = (byte)len_calc.len_c(prodazhba_seq.ToArray());//общ брой байтове от позиция 2 (вкл.) до позиция 8 (вкл.) плюс фиксирано отместване от 20h(например дължина 0х01 се предава 21h).

            byte[] prodazhb_02_to_05 = new byte[] { len };//

            IEnumerable<byte> prodazhba_for_control_amount = prodazhb_02_to_05.Concat(prodazhba_seq.ToArray());//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.


            byte[] prodazhba_for_control_amount_array = prodazhba_for_control_amount.ToArray();// Горния ред в масив от байтове


            byte[] prodazhaba_BCC = Contol_Amount_Calculation.BCC(prodazhba_for_control_amount_array);// Изчисление на BBC посредством метода 

            IEnumerable<byte> prodazhba_print = Global_Variables.STX.Concat(prodazhba_for_control_amount_array).Concat(prodazhaba_BCC).Concat(Global_Variables.ETX);
            byte[] prodazhba_print_ready = prodazhba_print.ToArray();// Готовият масив за пращане


            return prodazhba_print_ready;

        }

    }
}
