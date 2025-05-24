using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Storage_Solution
{
    public partial class Klen : Form
    {
        private PortReader myPort = new PortReader();
       
        public Klen(PortReader serialPort1)
        {
            
            InitializeComponent();
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "MMMM dd, yyyy - dddd";
            dateTimePicker1.CustomFormat = "ddMMyy";
            //  MessageBox.Show(dateTimePicker1.Value.Date.ToString("ddMMyy"));
          //  serialPort1.Open();
            AutoComplete();
            //byte[] klen = Daisy_Perfect_SKL_Command.pechat_ot_klen(Global_Variables.seq++, 195, "kur", "kur", "kur", "kur");

           // serialPort1.Write(klen, 0, klen.Length);

            myPort = serialPort1;
        }
       
   
     
        private void AutoComplete()
        {

          
           Dictionary<string, string> combo1 = new Dictionary<string, string>();
           combo1.Add("Печат на всички документи по дати","R3");
           combo1.Add("Печат само на Z отчети по дати", "R13");
           combo1.Add("Печат на фискални бележки по дати ", "R23");
           comboBox1.DataSource = new BindingSource(combo1,null);
           comboBox1.DisplayMember = "Key";
           comboBox1.ValueMember = "Value";
           Dictionary<string, string> combo2 = new Dictionary<string, string>();
           combo2.Add("Печат на всички документи по номера на документите", "R1");
           combo2.Add("Печат на всички документи по номера на Z отчети", "R2");
           combo2.Add("Печат само на Z отчети по номера на документите", "R11");
           combo2.Add("Печат само на Z отчети по номера", "R12");
           combo2.Add("Печат на фискални бележки по номера на документите", "R21");
           combo2.Add("Печат на фискални бележки по номера на Z", "R22");
           comboBox2.DataSource = new BindingSource(combo2, null);
           comboBox2.DisplayMember = "Key";
           comboBox2.ValueMember = "Value";
        }
       

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {

                MessageBox.Show("Началната дата не може да бъде след крайната", "Дата", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Крайната дата не може да бъде преди началната");
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            int tstDbl;
            if (!int.TryParse(tBox.Text, out tstDbl))
            {
                textBox1.Clear();
                //handle bad input
                return;
            }
            else
            {
              if (!(textBox2.Text=="") && (int.Parse(tBox.Text)>int.Parse(textBox2.Text)))
                {

                    MessageBox.Show("Опитахте да въведете номер на документ по-голям от " + textBox2.Text, "Документ по-голям от", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tBox.Text = textBox2.Text;
                } 
                
                //int value OK


            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !(Control.ModifierKeys == Keys.Shift))
               || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)

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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !(Control.ModifierKeys == Keys.Shift))
               || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)

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

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            int tstDbl;
            if (!int.TryParse(tBox.Text, out tstDbl))
            {
                textBox1.Clear();
                //handle bad input
                return;
            }
            else
            {
                if (!(textBox1.Text == "") && (int.Parse(tBox.Text) < int.Parse(textBox1.Text)))
                {

                    MessageBox.Show("Опитахте да въведете номер на документ по-малък от " + textBox1.Text, "Документ по-малък от", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tBox.Text = textBox1.Text;
                }

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] klen = Daisy_Perfect_SKL_Command.pechat_ot_klen(Global_Variables.seq++, 195, ((KeyValuePair<string, string>)comboBox2.SelectedItem).Value, textBox1.Text,textBox2.Text, checkBox2.Checked == true);
  
            myPort.DoWrite(klen,klen.Length);
        
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DateTime picker1 = dateTimePicker1.Value.AddDays(1);
            DateTime picker2 = dateTimePicker2.Value.AddDays(1);


            byte[] klen = Daisy_Perfect_SKL_Command.pechat_ot_klen(Global_Variables.seq++, 195, ((KeyValuePair<string, string>)comboBox1.SelectedItem).Value, picker1.Date.ToString("ddMMyy"),picker2.Date.ToString("ddMMyy"), checkBox1.Checked == true);

            myPort.DoWrite(klen, klen.Length);
        }

     
        private void Klen_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
