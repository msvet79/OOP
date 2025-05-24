using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage_Solution
{
    public partial class Discount : Form
    {
        public Discount()
        {
            InitializeComponent();
        }

        string total_amount_before_discount;
        string items_ordered;

        public string _total_amount_before_discount
        {
            set { total_amount_before_discount = value; }
        }

        public string _items_ordered
        {

            set { items_ordered = value; }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !(Control.ModifierKeys == Keys.Shift))
               || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
               || (e.KeyCode == Keys.Decimal && !(tBox.Text.Contains('.'))
                   && !(tBox.Text.Length == 0)
                   && !((tBox.Text.Length == 1)
                      && (tBox.Text.Contains('-') || tBox.Text.Contains('+'))))
               || (e.KeyCode == Keys.OemPeriod && !(tBox.Text.Contains('.'))
                   && !(tBox.Text.Length == 0)
                   && !((tBox.Text.Length == 1)
                      && (tBox.Text.Contains('-') || tBox.Text.Contains('+'))))
                //  || (e.KeyCode == Keys.Subtract && ((tBox.Text.Length == 0) ||
                //    tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
                // || (e.KeyCode == Keys.OemMinus && ((tBox.Text.Length == 0) ||
                //  tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
                //  || (e.KeyCode == Keys.Add && ((tBox.Text.Length == 0) ||
                // tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
                // || (e.KeyCode == Keys.Oemplus && ((tBox.Text.Length == 0) ||
                // tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || e.KeyCode == Keys.Delete
               || e.KeyCode == Keys.Back
               || e.KeyCode == Keys.Left
               || e.KeyCode == Keys.Right
               || (e.KeyCode == Keys.E) && !(tBox.Text.Contains('e')) &&
                   (tBox.Text.Contains('.') && !tBox.Text.EndsWith("."))))
            {
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox1_Validating(sender, new CancelEventArgs());
                return;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs cancelEventArgs)
        {
            TextBox tBox = (TextBox)sender;
            double tstDbl;
            if (!double.TryParse(tBox.Text, out tstDbl))
            {
                //handle bad input
                return;
            }
            else
            {
                //double value OK
                //textBox4.Text = (decimal.Parse(textBox3.Text) * numericUpDown1.Value).ToString();
            }
        }

        private void Discount_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton3.Checked = true;
        }


        public delegate void delPassData(string total_amount_discount, string price_per_item_discount); 

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == ""))
            {
                Form1 frm = System.Windows.Forms.Application.OpenForms["Form1"] as Form1;
                delPassData del = new delPassData(frm.funData);
                //  frm.Show();

                if (radioButton2.Checked == true && radioButton3.Checked == true)
                {

                    if (double.Parse(textBox1.Text) < 10)
                    {
                        decimal total = decimal.Parse(total_amount_before_discount);
                        int total_itmes = int.Parse(items_ordered);
                        decimal total_amount_discount = Math.Round(total * (1 - decimal.Parse(textBox1.Text) / 100), 2);
                        decimal price_per_item_discount = total_amount_discount / total_itmes;
                        del(total_amount_discount.ToString(), price_per_item_discount.ToString());

                        this.Close();
                    }
                    else {

                        MessageBox.Show("Не можете да въведете отстъпка по-голяма от 10 %", "Отстъпка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                   
                }
                else if (radioButton1.Checked == true && radioButton3.Checked == true)
                {
                    decimal total = decimal.Parse(total_amount_before_discount);

                    int total_itmes = int.Parse(items_ordered);
                    decimal total_amount_discount = Math.Round(total * (1 + decimal.Parse(textBox1.Text) / 100), 2);
                    decimal price_per_item_discount = total_amount_discount / total_itmes;


                    del(total_amount_discount.ToString(), price_per_item_discount.ToString());

                    this.Close();

                }
                else if (radioButton2.Checked == true && radioButton4.Checked == true)
                {

                    decimal total = decimal.Parse(total_amount_before_discount);

                    int total_itmes = int.Parse(items_ordered);
                    
                    if (double.Parse(textBox1.Text) <= (double)(total / total_itmes)*0.1)
                    {


                        
                        decimal total_amount_discount = Math.Round((total / total_itmes - decimal.Parse(textBox1.Text)) * total_itmes, 2);
                        decimal price_per_item_discount = total_amount_discount / total_itmes;


                        del(total_amount_discount.ToString(), price_per_item_discount.ToString());

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не можете да въведете отстъпка по-голяма от "+((double)(total / total_itmes)*0.1).ToString()+" лв. (10% от продажбената цена) за този продукт", "Отстъпка", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                }
                else
                {

                    decimal total = decimal.Parse(total_amount_before_discount);

                    int total_itmes = int.Parse(items_ordered);
                    decimal total_amount_discount = Math.Round((total / total_itmes + decimal.Parse(textBox1.Text)) * total_itmes, 2);
                    decimal price_per_item_discount = total_amount_discount / total_itmes;


                    del(total_amount_discount.ToString(), price_per_item_discount.ToString());
                    this.Close();

                }



            }
            
       
        
        
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
              && !char.IsDigit(e.KeyChar)
              && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
