using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Delivery_Module
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            suppliersTableAdapter1.Fill(northwindDataSet.Suppliers);
            Dictionary<string, string> combo1 = new Dictionary<string, string>();


            foreach (DataRow row in northwindDataSet.Suppliers)
            {
                // comboBox2.Items.Add(row["CompanyName"].ToString());
                combo1.Add(row["CompanyName"].ToString(), row["SupplierID"].ToString());

            }

            comboBox2.DataSource = new BindingSource(combo1, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";

            Dictionary<string, string> combo2 = new Dictionary<string, string>();
            combo2.Add("Карта", "1");
            combo2.Add("Банков път", "2");
            combo2.Add("Брой", "3");
            comboBox2.DataSource = new BindingSource(combo2, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";



        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            Figure_Only(sender, e);
        }

        private void Figure_Only(object sender, KeyEventArgs e)
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
                textBox_Validating(sender, new CancelEventArgs());
                return;
            }
        }

        private void textBox_Validating(object sender, CancelEventArgs cancelEventArgs)
        {
            throw new NotImplementedException();
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            Figure_Only(sender, e);
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            Figure_Only(sender, e);
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            Figure_Only(sender, e);
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
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
                            e.KeyCode == Keys.Oemcomma ||
                            e.KeyCode == Keys.OemPeriod ||
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet1.Delivery' table. You can move, or remove it, as needed.
            this.deliveryTableAdapter.Fill(this.northwindDataSet1.Delivery);
            // TODO: This line of code loads data into the 'northwindDataSet.Delivery' table. You can move, or remove it, as needed.
            this.deliveryTableAdapter.Fill(this.northwindDataSet.Delivery);
            // TODO: This line of code loads data into the 'northwindDataSet.Delivery' table. You can move, or remove it, as needed.
            this.deliveryTableAdapter.Fill(this.northwindDataSet.Delivery);
            LoadGrid();
        }

        private void LoadGrid()
        {
            DataGridViewLinkColumn links = new DataGridViewLinkColumn();
            links.UseColumnTextForLinkValue = true;
            links.HeaderText = "Download";
            links.DataPropertyName = "lnkColumn";
            links.ActiveLinkColor = Color.White;
            links.LinkBehavior = LinkBehavior.SystemDefault;
            links.LinkColor = Color.Blue;
            links.Text = "Click here";
            links.TrackVisitedState = true;
            links.VisitedLinkColor = Color.YellowGreen;
            dataGridView1.Columns.Add(links);
            dataGridView1.AutoResizeColumns();
           

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (DBNull.Value.Equals(row.Cells["fileSize"].Value))
                {
                    row.Cells[21] = new DataGridViewLinkCell();
                }
                else
                {
                    //row.ReadOnly = true;
                }
            }     
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            deliveryBindingSource.EndEdit();
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = dateTimePicker2.Value;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[15].Value = dateTimePicker3.Value;
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[16].Value = dateTimePicker4.Value;
           
            deliveryTableAdapter.Update(northwindDataSet.Delivery);
            dataGridView1.Refresh();
            // If you are not at the end of the list, move to the next item
            // in the BindingSource.
            if (deliveryBindingSource.Position + 1 < deliveryBindingSource.Count)
                deliveryBindingSource.MoveNext();

    // Otherwise, move back to the first item.
            else
                deliveryBindingSource.MoveFirst();

            // Force the form to repaint.
            this.Invalidate();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            deliveryBindingSource.CancelEdit();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            deliveryTableAdapter.Fill(northwindDataSet.Delivery);
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (!(textBox5.Text == null || textBox5.Text == string.Empty))
            {
                textBox7.Text = (textBox6.Text == null || textBox6.Text == string.Empty) ? textBox7.Text = (int.Parse(textBox4.Text) * decimal.Parse(textBox5.Text)).ToString() : textBox7.Text = (int.Parse(textBox4.Text) * decimal.Parse(textBox5.Text) * (1 - decimal.Parse(textBox6.Text) / 100)).ToString();

            }
            else
            {
                textBox7.Text = "";

            }

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (!(textBox6.Text == null || textBox6.Text == string.Empty))
            {
                textBox7.Text = !(textBox5.Text == null || textBox5.Text == string.Empty) ? textBox7.Text = (int.Parse(textBox4.Text) * decimal.Parse(textBox5.Text) * (1 - decimal.Parse(textBox6.Text) / 100)).ToString() : "";

            }
            else
            {

                textBox7.Text = !(textBox5.Text == null || textBox5.Text == string.Empty) ? (int.Parse(textBox4.Text) * decimal.Parse(textBox5.Text)).ToString() : "";



            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(textBox7.Text, out number))
            {
                textBox8.Text = (number * 1.2).ToString();
            }
            else
            {
                textBox8.Text = "";
            }

        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {

                label16.Enabled = true;
                label17.Enabled = true;
                label18.Enabled = true;
                label19.Enabled = true;
                comboBox3.Enabled = true;
                //comboBox3.SelectedIndex = 1;
                banksTableAdapter1.Fill(northwindDataSet.Banks);
                Dictionary<string, string> combo3 = new Dictionary<string, string>();
                combo3.Add("", "0");
                foreach (DataRow row in northwindDataSet.Banks)
                {

                    combo3.Add(row["IBAN"].ToString(), row["ID"].ToString());

                }
                comboBox3.DataSource = new BindingSource(combo3, null);
                comboBox3.DisplayMember = "Key";
                comboBox3.ValueMember = "Value";
                comboBox3.SelectedIndex = 1;
                textBox10.Text = northwindDataSet.Banks[0][2].ToString();
                textBox11.Text = northwindDataSet.Banks[0][3].ToString();
                textBox12.Text = northwindDataSet.Banks[0][4].ToString();
            }
            else
            {
                label16.Enabled = false;
                label17.Enabled = false;
                label18.Enabled = false;
                label19.Enabled = false;

                comboBox3.SelectedValue = "";
                comboBox3.Enabled = false;
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
            }
            {

            }
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((int.Parse(((KeyValuePair<string, string>)comboBox3.SelectedItem).Value)) > 0)
            {
                banksTableAdapter1.FillBy(northwindDataSet.Banks, ((KeyValuePair<string, string>)comboBox3.SelectedItem).Key);
                textBox10.Text = northwindDataSet.Banks.Rows[0][2].ToString();
                textBox11.Text = northwindDataSet.Banks.Rows[0][3].ToString();
                textBox12.Text = northwindDataSet.Banks.Rows[0][4].ToString();

            }
            else
            {
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
            }

        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Title = "Моля, изберете файл";
            openFileDialog1.Filter = "Pdf Files (.pdf)|*.pdf|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CreateAttachment(openFileDialog1.FileName, int.Parse(textBox1.Text));  //upload the attachment
            }
        }

        private void CreateAttachment(string strFile, int DeliveryID)
        {
            System.IO.FileStream objFileStream =
        new System.IO.FileStream(strFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            int intLength = Convert.ToInt32(objFileStream.Length);
            byte[] objData;
            objData = new byte[intLength];
            objFileStream.Read(objData, 0, intLength);
            objFileStream.Close();
            //deliveryTableAdapter.UpdateQueryPdf() textBox9.Text, dateTimePicker1.Value, DateTime.Now, intLength / 1024, objData

            billsTableAdapter1.InsertQuery(textBox9.Text, dateTimePicker1.Text, DateTime.Now, intLength / 1024, objData);
            deliveryTableAdapter.UpdateQueryFileSize(intLength / 1024, textBox9.Text);
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {

            string filepath = "D:\\temp.pdf";

            // deliveryTableAdapter.FillBy(northwindDataSet.Delivery, 8823);
            byte[] dbbyte = (byte[])northwindDataSet.Bills.Rows[0][5];

            //Assign File path create file
            System.IO.FileStream FS = new System.IO.FileStream(filepath, System.IO.FileMode.Create);

            //Write bytes to create file
            FS.Write(dbbyte, 0, dbbyte.Length);

            //Close FileStream instance
            FS.Close();


            // Open fila after write 

            //Create instance for process class
            System.Diagnostics.Process Proc = new System.Diagnostics.Process();

            //assign file path for process
            Proc.StartInfo.FileName = filepath;
            Proc.Start();
        }





        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            //MessageBox.Show(e.ColumnIndex.ToString());
            if (e.ColumnIndex == 21)
            {

                //Get selected file ID field
                string invoice = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
              
                SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\v11.0;Initial Catalog=northwind;Integrated Security=True");
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand();
                string strQuery_GetAttachmentById = "select attachment from [Bills] where [Invoice] = @Invoice";
                sqlcmd.CommandText = strQuery_GetAttachmentById;
                sqlcmd.Connection = sqlcon;
                sqlcmd.Parameters.AddWithValue("@Invoice", invoice);
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                adapter1.MissingSchemaAction = MissingSchemaAction.AddWithKey;  //grab schema
                adapter1.SelectCommand = sqlcmd;
                DataTable tbl1 = new DataTable();
                adapter1.Fill(tbl1);

                FileStream FS = null;
                byte[] dbbyte;

                try
                {



                    if (tbl1.Rows.Count > 0 && !DBNull.Value.Equals(tbl1.Rows[0]["attachment"]))
                    {
                        //Get a stored PDF bytes
                        dbbyte = (byte[])tbl1.Rows[0]["attachment"];
                        //store file Temporarily 
                        string filepath = "D:\\temp.pdf";


                        //Assign File path create file
                        FS = new FileStream(filepath, System.IO.FileMode.Create);

                        //Write bytes to create file
                        FS.Write(dbbyte, 0, dbbyte.Length);

                        //Close FileStream instance
                        FS.Close();


                        // Open file after write 

                        //Create instance for process class
                        Process Proc = new Process();

                        //assign file path for process
                        Proc.StartInfo.FileName = filepath;
                        Proc.Start();

                    }
                    else
                    {
                        dataGridView1[21, e.RowIndex] = new DataGridViewLinkCell();
                       
                        dataGridView1[21, e.RowIndex] = new DataGridViewTextBoxCell();

                        dataGridView1[21, e.RowIndex].ReadOnly = true;

                        dataGridView1[21, e.RowIndex].Style.BackColor = this.BackColor;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during File Read " + ex.ToString());
                }

                sqlcon.Close();

            }

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

