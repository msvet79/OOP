using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_Solution
{
    class Daisy_Perfect_SKL_Command
    {
        public static byte[] Command(byte seq, byte command, byte[] STX, byte[] ENQ, byte[] ETX)
        {


            byte[] Lenght_seq_and_command = new byte[] { 36, seq, command };
            IEnumerable<byte> SKL_Status = Lenght_seq_and_command.Concat(ENQ);
            byte[] SKL_Status_array = SKL_Status.ToArray();// 
            byte[] SKL_Status_BCC = Contol_Amount_Calculation.BCC(SKL_Status_array);// Изчисление на BBC посредством метода
            IEnumerable<byte> SKL_Status_print = STX.Concat(SKL_Status_array).Concat(SKL_Status_BCC).Concat(ETX);
            byte[] SKL_Status_print_ready = SKL_Status_print.ToArray();// Готовият масив за пращане
            return SKL_Status_print_ready;


        }

        public static byte[] nachalo_na_fiscalen_bon(byte seq, int p, byte[] ClerkNum, byte[] Password, byte[] Others)
        {
            //1, 49, 36, 48, 49, 44, 48, 48, 48, 48, 48, 49, 44, 48, 48, 48,
            //49, 5, 48, 50, 63, 53, 3

            byte[] fiscalen_bon_seq = new byte[] { seq };
            byte[] command = new byte[] { (byte)p };

            IEnumerable<byte> fiscalen_bon = fiscalen_bon_seq.Concat(command).Concat(ClerkNum).Concat(Password).Concat(Others).Concat(Global_Variables.ENQ);//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.
            Byte fiscalen_bon_lenght = (byte)len_calc.len_c(fiscalen_bon.ToArray());

            byte[] fiscalen_bon_lenght_array = new byte[] { fiscalen_bon_lenght };//

            IEnumerable<byte> fiscalen_bon_for_control_amount = fiscalen_bon_lenght_array.Concat(fiscalen_bon.ToArray().ToArray());

            byte[] fiscalen_bon_for_control_amount_array = fiscalen_bon_for_control_amount.ToArray();// Горния ред в масив от байтове
            byte[] fiscalen_bon_BCC = Contol_Amount_Calculation.BCC(fiscalen_bon_for_control_amount_array);// Изчисление на BBC посредством метода 
            IEnumerable<byte> fiscalen_bon_print = Global_Variables.STX.Concat(fiscalen_bon_for_control_amount_array).Concat(fiscalen_bon_BCC).Concat(Global_Variables.ETX);
            byte[] fiscalen_bon_print_ready = fiscalen_bon_print.ToArray();// Готовият масив за пращане
            return fiscalen_bon_print_ready;
        }

        public static byte[] Otchet_Z_s_nulirane(byte seq, int p, int q)
        {

            byte[] otchet_seq = new byte[] { seq };
            byte[] command = new byte[] { (byte)p };
            byte[] command_info = new byte[] { (byte)q };

            IEnumerable<byte> otchet = otchet_seq.Concat(command).Concat(command_info).Concat(Global_Variables.ENQ);//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.
            Byte otchet_lenght = (byte)len_calc.len_c(otchet.ToArray());
            byte[] otchet_lenght_array = new byte[] { otchet_lenght };//

            IEnumerable<byte> otchet_for_control_amount = otchet_lenght_array.Concat(otchet.ToArray().ToArray());
            byte[] otchet_for_control_amount_array = otchet_for_control_amount.ToArray();// Горния ред в масив от байтове

            byte[] otchet_BCC = Contol_Amount_Calculation.BCC(otchet_for_control_amount_array);// Изчисление на BBC посредством метода 

            IEnumerable<byte> otchet_print = Global_Variables.STX.Concat(otchet_for_control_amount_array).Concat(otchet_BCC).Concat(Global_Variables.ETX);
            byte[] otchet_print_ready = otchet_print.ToArray();// Готовият масив за пращане
            return otchet_print_ready;


        }
        public static byte[] pechat_ot_klen(byte seq, int p, string PrnType, string Bgn, string End, bool Send)
        {

            byte[] pechat_ot_klen_seq = new byte[] { seq };
            byte[] command = new byte[] { (byte)p };
            
            byte[] pechat_ot_klen_type_and_period = new byte[] { };
            if (Send)
            {
                pechat_ot_klen_type_and_period = Cyrilic_hex_array.Cyrilic_hex(PrnType + "," + Bgn + "," + End + "," + "Send");
            }
            else
            {
                pechat_ot_klen_type_and_period = Cyrilic_hex_array.Cyrilic_hex(PrnType+","+Bgn+","+End);
            }
            IEnumerable<byte> pechat_ot_klen_type_and_period_len = pechat_ot_klen_seq.Concat(command).Concat(pechat_ot_klen_type_and_period).Concat(Global_Variables.ENQ);//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.

            Byte pechat_ot_klen_type_and_period_lenght = (byte)len_calc.len_c(pechat_ot_klen_type_and_period_len.ToArray());

            byte[] pechat_ot_klen_type_and_period_lenght_array = new byte[] { pechat_ot_klen_type_and_period_lenght };//

            IEnumerable<byte> pechat_ot_klen_type_and_period_control_amount = pechat_ot_klen_type_and_period_lenght_array.Concat(pechat_ot_klen_type_and_period_len.ToArray());

            byte[] pechat_ot_klen_type_and_period_for_control_amount_array = pechat_ot_klen_type_and_period_control_amount.ToArray();// Горния ред в масив от байтове

            byte[] pechat_ot_klen_type_and_period_BCC = Contol_Amount_Calculation.BCC(pechat_ot_klen_type_and_period_for_control_amount_array);// Изчисление на BBC посредством метода

            IEnumerable<byte> pechat_ot_klen_type_and_period_print = Global_Variables.STX.Concat(pechat_ot_klen_type_and_period_for_control_amount_array).Concat(pechat_ot_klen_type_and_period_BCC).Concat(Global_Variables.ETX);
            byte[] pechat_ot_klen_type_and_period_ready = pechat_ot_klen_type_and_period_print.ToArray();// Готовият масив за пращане

            return pechat_ot_klen_type_and_period_ready;//pechat_ot_klen_type_and_period_ready;
        
        }
    }
}
