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
    public partial class Juridical : Form
    {
        public Juridical()
        {
            InitializeComponent();


            autocomplete();
        }
        public delegate void LegalUpdaterHandler(object sender, LegalUpdateEventArgs e);
        public event LegalUpdaterHandler LegalUpdated;
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
        private void autocomplete()
        {


            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll3 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection coll4 = new AutoCompleteStringCollection();
          // northwindDataSetTableAdapters.CustomersTableAdapter customersTA = new northwindDataSetTableAdapters.CustomersTableAdapter();
            //northwindDataSet.CustomersDataTable Customers = new northwindDataSet.CustomersDataTable();
            
           
            northwindDataSet1.EnforceConstraints = false;
            customersTableAdapter1.FillBy(northwindDataSet1.Customers,false);
            
           // customersTA.FillBy(Customers, false);
            button3.Enabled = false;
            button4.Enabled = false;
            foreach (DataRow row in northwindDataSet1.Customers)
            {
                coll3.Add(row["CompanyName"].ToString());
                coll4.Add(row["Bulstat"].ToString());

            }

            textBox2.AutoCompleteCustomSource = coll3;
            textBox1.AutoCompleteCustomSource = coll4;

        }

        private void Juridical_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string Firm = textBox2.Text;
            string MOL = textBox6.Text;
            LegalUpdateEventArgs args = new LegalUpdateEventArgs(Firm, MOL);
            LegalUpdated(this, args);
            customersTableAdapter1.FillBy1(northwindDataSet1.Customers, textBox1.Text);
            if (northwindDataSet1.Customers.Rows.Count != 1)
           {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
           customersTableAdapter1.InsertQuery(textBox2.Text, textBox6.Text,comboBox1.Text, textBox5.Text, textBox4.Text, textBox4.Text, textBox10.Text, textBox3.Text, textBox7.Text, textBox7.Text, textBox1.Text, textBox8.Text, checkBox2.Checked == true, dateTimePicker1.Text,sqlFormattedDate,false,"");
            contactsTableAdapter1.InsertQuery("Customer", textBox2.Text, textBox6.Text, comboBox1.Text, textBox5.Text, textBox4.Text, textBox4.Text, textBox10.Text, textBox3.Text, textBox7.Text, textBox7.Text, dateTimePicker1.Text, sqlFormattedDate);
            
            }
           else 
           {
                MessageBox.Show("The client already exists");
                button2.Enabled = true;
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox9.Enabled = false;
                textBox2.Enabled = false;
           }
            
          //   (@CustomerID,@CompanyName,@ContactName,@ContactTitle,@Address,@City,@Region,@PostalCode,@Country,@Phone,@Fax,@Bulstat); 

            Form1 frm = System.Windows.Forms.Application.OpenForms["Form1"] as Form1;
            frm.comboBox1.SelectedIndex = 2;
            Juridical.ActiveForm.Hide();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = Regex.Replace(textBox1.Text, "[^0-9.]", "");
            customersTableAdapter1.FillBy1(northwindDataSet1.Customers, textBox1.Text);


            if (northwindDataSet1.Customers.Rows.Count == 1)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                foreach (DataRow row in northwindDataSet1.Customers)
                {
                    textBox2.TextChanged -= textBox2_TextChanged;

                    textBox2.Text = row["CompanyName"].ToString();

                    textBox4.Text = row["City"].ToString();
                    textBox5.Text = row["Address"].ToString();
                    textBox6.Text = row["ContactName"].ToString();
                    textBox7.Text = row["Phone"].ToString();
                    textBox8.Text = row["Email"].ToString();
                    textBox10.Text = row["PostalCode"].ToString();
                    comboBox1.Text = row["ContactTitle"].ToString();
                    if (!(row["DDS"].ToString() == "true"))
                    {
                        checkBox2.Checked = true;
                        textBox9.Text = "BG" + textBox1.Text;


                    }

                    textBox2.TextChanged += textBox2_TextChanged;

                   
                   
                        foreach (Control c in this.Controls)
                        {

                            c.Enabled = false;

                        }
                        button2.Enabled = true;
                        button4.Enabled = true;

                    }

                
            }
            else
            {

                button1.Enabled = true;
                button2.Enabled = false;
                
            
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            customersTableAdapter1.FillBy2(northwindDataSet1.Customers, textBox2.Text);


            if (northwindDataSet1.Customers.Rows.Count == 1)
            {
                button1.Enabled = false;
                button2.Enabled = true;
               
                foreach (DataRow row in northwindDataSet1.Customers)
                {
                    textBox1.TextChanged -= textBox1_TextChanged;

                    textBox1.Text = row["Bulstat"].ToString();

                    textBox4.Text = row["City"].ToString();
                    textBox5.Text = row["Address"].ToString();
                    textBox6.Text = row["ContactName"].ToString();
                    textBox7.Text = row["Phone"].ToString();
                    textBox8.Text = row["Email"].ToString();
                    if (!(row["DDS"].ToString() == "true"))
                    {
                        checkBox2.Checked = true;
                        textBox9.Text = "BG" + row["Bulstat"].ToString();


                    }

                  
                    textBox1.TextChanged += textBox1_TextChanged;
                    foreach (Control c in this.Controls)
                    {

                        c.Enabled = false;

                    }
                    button2.Enabled = true;
                    button4.Enabled = true;


                }
            }
            else
            {

                button1.Enabled = true;
                button2.Enabled = false;

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            
         //  textBox9.Text =Regex.Replace(textBox9.Text, "^BG[0-9]{9,9}$|^BG[0-9]{10,10}$", "");
     
        
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (!(checkBox2.Checked == true))
            {
                textBox9.Clear();

            }
            else
            {

                textBox9.Text = "BG" + textBox1.Text;

            }
        }
       
        
        
        private void textBox8_Validated(object sender, EventArgs e)
        {



            if (isFormClosing)
                return;
            
            
            if (isValidEmail(textBox8.Text))
            {

                MessageBox.Show("The mail address is valid");
            }
            else
            {
                if (textBox8.TextLength > 0)
                {
                    
                    MessageBox.Show("Please, eneter a valid e-mail address");
                    textBox8.Select();
                }
            }
        }
        
        
        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            //Allow navigation keyboard arrows
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Delete:
                    e.SuppressKeyPress = false;
                    return;
                default:
                    break;
            }

            //Block non-number characters
            char currentKey = (char)e.KeyCode;
            bool modifier = e.Control || e.Alt || e.Shift;
            bool nonNumber = (char.IsLetter(currentKey) ||
                             char.IsSymbol(currentKey) ||
                            char.IsWhiteSpace(currentKey) ||
                            char.IsPunctuation(currentKey)) &&
                            !(e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);
            //bool nonNumber = (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)));
            if (!modifier && nonNumber)
                e.SuppressKeyPress = true;

            //Handle pasted Text
            if (e.Control && e.KeyCode == Keys.V)
            {
                //Preview paste data (removing non-number characters)
                string pasteText = Clipboard.GetText();
                string strippedText = "";
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (char.IsDigit(pasteText[i]))
                        strippedText += pasteText[i].ToString();
                }

                if (strippedText != pasteText)
                {
                    //There were non-numbers in the pasted text
                    e.SuppressKeyPress = true;

                    //OPTIONAL: Manually insert text stripped of non-numbers
                    TextBox me = (TextBox)sender;
                    int start = me.SelectionStart;
                    string newTxt = me.Text;
                    newTxt = newTxt.Remove(me.SelectionStart, me.SelectionLength); //remove highlighted text
                    newTxt = newTxt.Insert(me.SelectionStart, strippedText); //paste
                    me.Text = newTxt;
                    me.SelectionStart = start + strippedText.Length;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 && !(Control.ModifierKeys == Keys.Shift) && tBox.Text.Length>1)
               || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 && tBox.Text.Length > 1) 
               || (e.KeyCode == Keys.Subtract && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.OemMinus && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.Add && ((tBox.Text.Length == 0) || 
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.Oemplus && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.B && ((tBox.Text.Length == 0) && Control.ModifierKeys == Keys.Shift ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
                || (e.KeyCode == Keys.G  && ((tBox.Text.Length == 1) && Control.ModifierKeys == Keys.Shift ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || e.KeyCode == Keys.Delete
               ||(e.Control && e.KeyCode == Keys.V)
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
                textBox9_Validating(sender, new CancelEventArgs());
                return;
            }
       
        }

        private void textBox9_Validating(object sender, CancelEventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            tBox.Text = Regex.Replace(tBox.Text, "[^0-9]", "");

           // tBox.Text = Regex.Replace(tBox.Text, "^(BG)?[0-9]{9,10}$", "");
          //  tBox.Text = Regex.Replace(tBox.Text, "[^0-9.]", "");
           
        }
        
        private void textBox1_Validating(object sender, CancelEventArgs e)
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {

        }

        private void Juridical_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 frm = System.Windows.Forms.Application.OpenForms["Form1"] as Form1;
            frm.comboBox1.SelectedIndex = 0;

        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            //Allow navigation keyboard arrows
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Delete:
                    e.SuppressKeyPress = false;
                    return;
                default:
                    break;
            }

            //Block non-number characters
            char currentKey = (char)e.KeyCode;
            bool modifier = e.Control || e.Alt || e.Shift;
            bool nonNumber = (char.IsLetter(currentKey) ||
                             char.IsSymbol(currentKey) ||
                            char.IsWhiteSpace(currentKey) ||
                            char.IsPunctuation(currentKey)) &&
                            !(e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);
            //bool nonNumber = (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)));
            if (!modifier && nonNumber)
                e.SuppressKeyPress = true;

            //Handle pasted Text
            if (e.Control && e.KeyCode == Keys.V)
            {
                //Preview paste data (removing non-number characters)
                string pasteText = Clipboard.GetText();
                string strippedText = "";
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (char.IsDigit(pasteText[i]))
                        strippedText += pasteText[i].ToString();
                }

                if (strippedText != pasteText)
                {
                    //There were non-numbers in the pasted text
                    e.SuppressKeyPress = true;

                    //OPTIONAL: Manually insert text stripped of non-numbers
                    TextBox me = (TextBox)sender;
                    int start = me.SelectionStart;
                    string newTxt = me.Text;
                    newTxt = newTxt.Remove(me.SelectionStart, me.SelectionLength); //remove highlighted text
                    newTxt = newTxt.Insert(me.SelectionStart, strippedText); //paste
                    me.Text = newTxt;
                    me.SelectionStart = start + strippedText.Length;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            tBox.Text = Regex.Replace(tBox.Text, "[^0-9]", "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox8.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox10.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            checkBox2.Enabled = true;
            comboBox1.Enabled = true;
            label3.Enabled = true;
            label5.Enabled = true;
            label6.Enabled = true;
            label7.Enabled = true;
            label8.Enabled = true;
            label9.Enabled = true;
            label10.Enabled = true;
            label11.Enabled = true;
            button3.Enabled = true;

            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate1 = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            
            customersTableAdapter1.UpdateQuery(textBox6.Text,comboBox1.Text,textBox5.Text,textBox4.Text,textBox4.Text,textBox10.Text,textBox3.Text,textBox7.Text,textBox7.Text,textBox8.Text,checkBox2.Checked==true,sqlFormattedDate1,textBox2.Text);
            contactsTableAdapter1.UpdateQuery(textBox6.Text, comboBox1.Text, textBox5.Text, textBox4.Text, textBox4.Text,textBox10.Text, textBox3.Text, textBox7.Text, textBox7.Text, sqlFormattedDate1, textBox2.Text);
           // button4.Enabled = true;
            string Firm = textBox2.Text;
            string MOL = textBox6.Text;
            LegalUpdateEventArgs args = new LegalUpdateEventArgs(Firm, MOL);
            LegalUpdated(this, args);
            Juridical.ActiveForm.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Firm = textBox2.Text;
            string MOL = textBox6.Text;
            LegalUpdateEventArgs args = new LegalUpdateEventArgs(Firm, MOL);
            LegalUpdated(this, args);
            Juridical.ActiveForm.Hide();
        }

        private void Juridical_FormClosed(object sender, FormClosedEventArgs e)
        {

           
        }

       
    
    
    }
}


