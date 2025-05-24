using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Storage_Solution
{
    class Item_For_Sale
    {
        public Item_For_Sale(int quantity, double price, string desctiption, byte seq)
        {


            Encoding encod = Encoding.GetEncoding(1251);
            this.seq = seq;
            this.quantity = quantity;
            this.price = price;
            this.description = desctiption;
            total_amount = price.ToString() + "*" + "1";
            price1 = (quantity * price).ToString("0.00") + "Б";
            string[] foo = Chop(desctiption, 28);//разделя описанието на масиви от по 28 символа и тук 

            if (foo.Length >= 2)
            {//Проверяваме описанието на продукта дали се събира в 28 символа или е по-голямо

                foo[1] = Stringformat(foo[1], price1, quantity);//Ако е по-голямо интерува ни само вторите 28 символа, като ги орязваме, за да включим и цената или пък добаваме шпации надясно, това се прави от  Stringformat

                for (int z = 0; z <= 1; z++)//
                {
                    foo2[z] = Cyrilic_hex_array.Cyrilic_hex(foo[z]);//Конвертира кирилицата в HEX и сме готови с първото описание

                }


            }
            else
            {

                if (foo[0].Length + price1.Length > 27)// Ако елементът е един, но символите му заедно с цената са по-дълги от 27, разделяме на два реда
                {

                    string exept = (quantity.ToString() + " " + "x" + " " + price.ToString("0.00") + " " + price1);
                    string exept1 = "";
                    string exept2 = exept1.PadRight(28 - exept.Length, ' ') + exept;

                    //Вярно е, че разделяме на два реда, но и първия трябва да проверим колко е дълъг и да допълним с шпации до 28 foo2[1]

                    foo2[0] = Cyrilic_hex_array.Cyrilic_hex(exept2);
                    foo2[1] = Cyrilic_hex_array.Cyrilic_hex(foo[0].PadRight(28, ' '));

                }
                else // В случай, че елементът и цената са "къси", т.е. събират се на ред от 28 символа, всичко се събира на ред.
                {

                    foo[0] = Stringformat(foo[0], price1, quantity);
                    foo2[0] = Cyrilic_hex_array.Cyrilic_hex(foo[0]);//Само един ред(масив) 

                }

            }
            byte[] command_start_sales = new byte[] { seq, 54 };//Предхожда текста(команда) за продукта първи блок, 36h	54*	Печат на фискален текст

            IEnumerable<byte> opisanie_block_1 = command_start_sales.Concat(foo2[0]).Concat(Global_Variables.ENQ);//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.
            //Приготваме се да изчислим дължината, защото се оказа, че не е фиксирана 40, а може да е 3F, когато цената е под 10 лв., 0.01, да речем

            Byte len_opisanie_block_1 = (byte)len_calc.len_c(opisanie_block_1.ToArray());//общ брой байтове от позиция 2 (вкл.) до позиция 8 (вкл.) плюс фиксирано отместване от 20h(например дължина 0х01 се предава 21h).

            byte[] opisanie_block_1_02_to_05 = new byte[] { len_opisanie_block_1 };//

            IEnumerable<byte> opisanie_block_1_for_control_amount = opisanie_block_1_02_to_05.Concat(opisanie_block_1.ToArray());//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.

            byte[] opisanie_block_1_for_control_amount_array = opisanie_block_1_for_control_amount.ToArray();// Горния ред в масив от байтове

            byte[] opisanie_block_1_BCC = Contol_Amount_Calculation.BCC(opisanie_block_1_for_control_amount_array);// Изчисление на BBC посредством метода 

            IEnumerable<byte> opisanie_block_1_print = Global_Variables.STX.Concat(opisanie_block_1_for_control_amount_array).Concat(opisanie_block_1_BCC).Concat(Global_Variables.ETX);
            opisanie_block_1_print_ready = opisanie_block_1_print.ToArray();// Готовият масив за пращане
            seq++;
            NumCirles++;

            if (foo2[1] != null)
            {

                byte[] command_start_sales_2 = new byte[] { seq, 54 };//Предхожда текста(команда) за продукта във втроия блок
                byte[] command_end_sales2 = new byte[] { 5, 48, 56, 51, 59, 3 };//Край на текста(команда) за  описание на продукта във втория блок
                byte[] opisanie_block_2_array = new byte[] { };

                IEnumerable<byte> opisanie_block_2 = command_start_sales_2.Concat(foo2[1]).Concat(Global_Variables.ENQ);////общ брой байтове от позиция 2 (вкл.) до позиция 8 (вкл.) плюс фиксирано отместване от 20h(например дължина 0х01 се предава 21h).
                Byte len_opisanie_block_2 = (byte)len_calc.len_c(opisanie_block_2.ToArray());//общ брой байтове от позиция 2 (вкл.) до позиция 8 (вкл.) плюс фиксирано отместване от 20h(например дължина 0х01 се предава 21h).

                byte[] opisanie_block_2_02_to_05 = new byte[] { len_opisanie_block_2 };//

                IEnumerable<byte> opisanie_block_2_for_control_amount = opisanie_block_2_02_to_05.Concat(opisanie_block_2.ToArray());//приготвямебайтовете от позиция 2(LEN) до позиция 6(“ENQ”, 	Enquiry) вкл. и ще го подадем за изчисляване на контролната сума.

                byte[] opisanie_block_2_for_control_amount_array = opisanie_block_2_for_control_amount.ToArray();// Горния ред в масив от байтове

                byte[] opisanie_block_2_BCC = Contol_Amount_Calculation.BCC(opisanie_block_2_for_control_amount_array);// Изчисление на BBC посредством метода 

                IEnumerable<byte> opisanie_block_2_print = Global_Variables.STX.Concat(opisanie_block_2_for_control_amount_array).Concat(opisanie_block_2_BCC).Concat(Global_Variables.ETX);
                opisanie_block_2_print_ready = opisanie_block_2_print.ToArray();// Готовият масив за пращане
                seq++;
                NumCirles++;

            }


        }





        private string Stringformat(string p, string price1, int quantity)
        {

            int price1_lenght = (price1.Length < 6) ? 6 : price1.Length;//Прихващаме малоумен бъг, ако цената е по-малка от 10 лв,  5.49Б, 0.01Б, трябва да фиксираме дължината на 6 символа, а не на 5, за да се форматира коректно. Това води и до следното: Дължината на този стринг вече ще е 3F, а не 40 и за съжаление трябва да предвидим и тук пресматяне на второ поле LEN :) да му еба майката
            string price1_new = " " + (quantity * price).ToString("0.00") + "Б";
            string more_than_one_q_new = " " + (quantity.ToString() + " " + "x" + " " + price.ToString("0.00") + " " + price1);

            if (quantity > 1)
            {

                if ((more_than_one_q_new.Length + p.Length) > 28)
                    return p = p.Substring(0, 28 - more_than_one_q_new.Length) + more_than_one_q_new;
                else
                {
                    return p = p.PadRight(28 - more_than_one_q_new.Length, ' ') + more_than_one_q_new;

                }


            }
            else
            {

                if ((p.Length + price1_lenght) > 28)
                {


                    return p = p.Substring(0, 28 - price1_lenght) + price1_new;

                }

                else
                {

                    return p = p.PadRight(28 - price1_lenght, ' ') + price1_new;
                }

            }
        }






        private string[] Chop(string descrition, int length)
        {
            int strLength = descrition.Length;
            int strCount = (strLength + length - 1) / length;
            string[] result = new string[strCount];
            for (int i = 0; i < strCount; ++i)
            {
                result[i] = descrition.Substring(i * length, Math.Min(length, strLength));
                strLength -= length;
            }
            return result;

        }

        // public double price_print()
        //  {
        // return price;
        // }




        byte[] opisanie_block_1_print_ready = new byte[] { };

        public static int NumCirles = 0;

        public byte this[int i]
        {
            get { return this.opisanie_block_1_print_ready[i]; }

        }


        private byte seq;
        byte[] opisanie_block_2_print_ready = new byte[] { };
        private double price;

        public double Price
        {
            get { return this.price * quantity; }

        }
        private int quantity;
        private string description;
        private string total_amount;
        public string Total_amount
        {
            get { return this.total_amount; }

        }


        private string price1;
        public string Price1
        {
            get { return this.price1; }

        }

        private byte[][] foo2 = new byte[2][];

        // public byte[] this[int i] 
        // {
        //   get { return this.foo2[i]; }

        //  }

        public byte[] opisanie_block_1_print_ready1
        {
            get { return opisanie_block_1_print_ready; }

        }


        public byte[] opisanie_block_2_print_ready2
        {


            get { return opisanie_block_2_print_ready; }

        }

    }
}

