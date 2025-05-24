using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Storage_Solution
{
    public partial class Individual : Form
    {
        public delegate void AddressUpdaterHandler(object sender, AddressUpdateEventArgs e);
        public event AddressUpdaterHandler AddressUpdated;
        
        public Individual()
        {
            InitializeComponent();
            Autocomplete();
        
        }

       
        // add an event of the delegate type

        
        
        
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        
        private void Autocomplete()
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll3 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection coll4 = new AutoCompleteStringCollection();
            northwindDataSetTableAdapters.CustomersTableAdapter customersTA = new northwindDataSetTableAdapters.CustomersTableAdapter();
            northwindDataSet.CustomersDataTable Customers = new northwindDataSet.CustomersDataTable();
            customersTA.FillBy(Customers, true);

            foreach (DataRow row in Customers)
            {
                coll3.Add(row["CompanyName"].ToString());
                coll4.Add(row["EGN"].ToString());

            }

            textBox2.AutoCompleteCustomSource = coll4;
            textBox1.AutoCompleteCustomSource = coll3;

        }

        private void Individual_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Individual.ActiveForm.Close();
            string Name = textBox1.Text;

            AddressUpdateEventArgs args = new AddressUpdateEventArgs(Name);
            AddressUpdated(this, args); 

            // raise the event with the updated arguments

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            customersTableAdapter1.InsertQuery(textBox1.Text, textBox1.Text,"", textBox4.Text, textBox3.Text, textBox3.Text, textBox7.Text, "България", textBox5.Text, textBox5.Text, "", textBox6.Text, false, dateTimePicker1.Text, sqlFormattedDate,true,textBox2.Text);
            contactsTableAdapter1.InsertQuery("Customer", textBox1.Text, textBox1.Text, "", textBox4.Text, textBox3.Text, textBox3.Text, textBox7.Text, "България", textBox5.Text, textBox5.Text,dateTimePicker1.Text,sqlFormattedDate);
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            customersTableAdapter1.FillBy2(northwindDataSet1.Customers, textBox1.Text);


            if (northwindDataSet1.Customers.Rows.Count == 1)
            {

                foreach (DataRow row in northwindDataSet1.Customers)
                {

                    textBox2.TextChanged -= textBox2_TextChanged;
                    textBox2.Text = row["EGN"].ToString();

                    textBox3.Text = row["City"].ToString();
                    textBox4.Text = row["Address"].ToString();

                    textBox5.Text = row["Phone"].ToString();
                    textBox6.Text = row["Email"].ToString();


                    textBox2.TextChanged += textBox2_TextChanged;
                    foreach (Control c in this.Controls)
                    {

                        c.Enabled = false;

                    }
                    button2.Enabled = true;
                    button3.Enabled = true;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            customersTableAdapter1.FillBy3(northwindDataSet1.Customers, textBox2.Text);


            if (northwindDataSet1.Customers.Rows.Count == 1)
            {

                foreach (DataRow row in northwindDataSet1.Customers)
                {
                    textBox1.TextChanged -= textBox1_TextChanged;

                    textBox1.Text = row["CompanyName"].ToString();

                    textBox3.Text = row["City"].ToString();
                    textBox4.Text = row["Address"].ToString();

                    textBox5.Text = row["Phone"].ToString();
                    textBox6.Text = row["Email"].ToString();
                    textBox1.TextChanged += textBox1_TextChanged;
                    foreach (Control c in this.Controls)
                    {

                        c.Enabled = false;

                    }
                    button2.Enabled = true;
                    button3.Enabled = true;


                }
            
            
            
            
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_Validated(object sender, CancelEventArgs e)
        {
            if (isFormClosing)
                return;
            
            
            if (isValidEmail(textBox6.Text))
            {

                MessageBox.Show("The mail address is valid");
            }
            else
            {
                if (textBox6.TextLength > 0)
                {
                    textBox6.Clear();
                    MessageBox.Show("Please, eneter a valid e-mail address");
                    textBox6.Select();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !(Control.ModifierKeys == Keys.Shift))
               || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
               || (e.KeyCode == Keys.Subtract && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.OemMinus && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.Add && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.Oemplus && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
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
                textBox2_Validating(sender, new CancelEventArgs());
                return;
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {

            if (isFormClosing)
                return;

            string mil = EGN.checkEGN(textBox2.Text, 0);
            if (!(mil == "OK") && textBox2.TextLength > 0)
            {

                MessageBox.Show(mil);
                textBox2.Select();
            }



        }

        private void Individual_Leave(object sender, EventArgs e)
        {

        }

        private void Individual_FormClosing(object sender, FormClosingEventArgs e)
        {
            // textBox2.Validated -= textBox2_Validated;

            textBox2.CausesValidation = false;

          

        }



        //variable to hold true if the for is closing
        private bool isFormClosing = false;
        // Contant for the close message
        private const int WM_CLOSE = 16;
        //override the WndProc msg to trap the WM_CLOSE message
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLOSE)
                isFormClosing = true;
            base.WndProc(ref m);
        }

        private void textBox2_Validating_1(object sender, CancelEventArgs e)
        {
            if (isFormClosing)
                return;

            string mil = EGN.checkEGN(textBox2.Text, 0);
            if (!(mil == "OK") && textBox2.TextLength > 0)
            {

                MessageBox.Show(mil);
                textBox2.Select();
                e.Cancel = true;
      
                
            }

        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
               || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)

               || e.KeyCode == Keys.Delete
               || e.KeyCode == Keys.Back
               || e.KeyCode == Keys.Left
               || e.KeyCode == Keys.Right

                  ))
            {
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox7_Validating(sender, new CancelEventArgs());
                return;
            }
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            double tstDbl;
            if (!double.TryParse(tBox.Text, out tstDbl))
            {
                //handle bad input
            }
            else
            {
                //double value OK

            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Enabled = true;
            label4.Enabled = true;
            label5.Enabled = true;
            label6.Enabled = true;
            label7.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;

            AddressUpdateEventArgs args = new AddressUpdateEventArgs(Name);
            AddressUpdated(this, args);
            Individual.ActiveForm.Hide();
        
        }

        private void Individual_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Form1 frm = System.Windows.Forms.Application.OpenForms["Form1"] as Form1;
            frm.comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;

            AddressUpdateEventArgs args = new AddressUpdateEventArgs(Name);
            AddressUpdated(this, args);

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate2 = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            customersTableAdapter1.UpdateQuery1(textBox4.Text, textBox3.Text, textBox3.Text, textBox7.Text, textBox5.Text, textBox5.Text, textBox6.Text, sqlFormattedDate2, textBox1.Text);
            contactsTableAdapter1.UpdateQuery1(textBox4.Text, textBox3.Text, textBox3.Text, textBox7.Text, textBox5.Text, textBox5.Text, sqlFormattedDate2, textBox1.Text);
            Individual.ActiveForm.Hide();
        }

        


    }
}
