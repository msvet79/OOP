using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.IO.Ports;
using System.Text.RegularExpressions;


namespace Storage_Solution
{
    public partial class Form1 : Form
    {

        PortReader myPort = new PortReader();

        public Form1()
        {

            InitializeComponent();
            Autocompete();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.LabelEdit = false;
            listView1.AllowColumnReorder = false;
            listView1.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;

            listView2.View = View.Details;
            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.LabelEdit = false;
            listView2.AllowColumnReorder = false;
            listView2.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            listView2.FullRowSelect = true;

            listView2.Columns.Add("Код на продукт", 100);
            listView2.Columns.Add("Доставчик", 80);
            listView2.Columns.Add("Количество", 80);
            listView2.Columns.Add("Цена с ДДС", 100);
            // Sort the items in the list in ascending order.
            //listView1.Sorting = SortOrder.Ascending;

            //Add column header
            myPort.Name = "COM12";
            myPort.DoOpen();


            listView1.Columns.Add("Продукт", 60);
            listView1.Columns.Add("Код на продукт", 100);
            listView1.Columns.Add("Количество общо", 120);
            listView1.Columns.Add("Цена за бр.", 80);
            listView1.Columns.Add("Общо сума", 70);
            listView1.Columns.Add("Общо сума с отстъпка/надбавка", 195);
            Dictionary<string, string> combo4 = new Dictionary<string, string>();
            combo4.Add("DY361368", "36458298");
            combo4.Add("DY380479", "36513019");
            comboBox4.DataSource = new BindingSource(combo4, null);
            comboBox4.DisplayMember = "Key";
            comboBox4.ValueMember = "Value";
            // БройКартаБанков път
            Dictionary<string, byte> combo5 = new Dictionary<string, byte>();
            combo5.Add("Брой", 80);
            combo5.Add("Чек", 78);
            combo5.Add("Карта", 67);
            combo5.Add("Банков път", 50);
            CustomerList.DataSource = new BindingSource(combo5, null);
            CustomerList.DisplayMember = "Key";
            CustomerList.ValueMember = "Value";
            Global_Variables.seq = 0x20;
            byte[] nomer_na_dokumet = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 113, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);
            myPort.DoWrite(nomer_na_dokumet, nomer_na_dokumet.Length);
            textBox9.Text = myPort.Message;
            myPort.Message = string.Empty;
            //byte[] nomer_na_dokumet = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 113, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//new byte[] { 1, 36, 41, 113, 5, 48, 48, 60, 51 };



            //serialPort1.Write(nomer_na_dokumet, 0, nomer_na_dokumet.Length);
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          //  textBox1.Text=textBox1.Text.Trim();
            northwindDataSet.EnforceConstraints = false;

            productsTableAdapter1.FillBy1(northwindDataSet.Products, textBox1.Text);

            if (northwindDataSet.Products.Rows.Count == 1)
            {
                textBox2.TextChanged -= textBox2_TextChanged_1;

                foreach (DataRow row in northwindDataSet.Products)
                {


                    textBox2.Text = row["SKU"].ToString();
                    textBox3.Text = row["UnitPrice"].ToString();
                    textBox4.Text = (decimal.Parse(textBox3.Text) * numericUpDown1.Value).ToString();
                    textBox2.TextChanged += textBox2_TextChanged_1;
                    comboBox3.SelectedIndex = int.Parse(row["SupplierID"].ToString());

                }
            }
            else
            {
            productsTableAdapter1.FillBy(northwindDataSet.Products, textBox2.Text);
            if (northwindDataSet.Products.Rows.Count == 1)
            {
                textBox2.Clear();
                comboBox3.SelectedIndex = 0;
                textBox3.Clear();
                textBox8.Clear();
                textBox4.Clear();
            }
            }
            
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                textBox8.Text = "";
            }
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                textBox8.Text = "";
            }
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
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
                textBox4.Clear();
                //handle bad input
                return;
            }
            else
            {
                //double value OK
                textBox4.Text = (decimal.Parse(textBox3.Text) * numericUpDown1.Value).ToString();

            }
        }



        private void textBox3_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                textBox1_Validating(sender, new CancelEventArgs());
                return;
            }
        }

        private void total_amount(object sender, EventArgs e)
        {
           // if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != ""))
           // {
                //textBox4.Text = (numericUpDown1.Value * decimal.Parse(textBox3.Text)).ToString();
           // }
          //  else
           // {
              //  textBox1.Text = "";
               // textBox2.Text = "";
               // textBox3.Text = "";
              //  textBox4.Text = "";
              //  textBox8.Text = "";
               // numericUpDown1.Value = 1;
           // }

                if ((textBox3.Text != ""))
                {
                    textBox4.Text = (numericUpDown1.Value * decimal.Parse(textBox3.Text)).ToString();
                }
                else
                {
                    numericUpDown1.Value = 1;
                }

            if (textBox8.Text != "")
            {
                textBox8.Text = "";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {

                    if (textBox3.Text != "")
                    {

                        if (comboBox3.Text != "")
                        {

                            if (textBox8.Text != "")
                            {

                                //textBox8.Text е отстъпката
                                string[] row = { textBox1.Text, textBox2.Text, numericUpDown1.Value.ToString(), textBox3.Text, textBox4.Text, textBox8.Text };
                                var txt = textBox2.Text;



                                if (!listView1.Items.ContainsKey(txt))
                                {

                                    var listViewItem = new ListViewItem(row);
                                    listViewItem.Name = textBox2.Text;
                                    listView1.Items.Add(listViewItem);
                                    button2.Enabled = true;
                                    button5.Enabled = true;
                                    // Тук ще го добави и в таблицата за доставчиците, за целта ще проверим дали не е добавен вече доставчик

                                    if ((listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text).Count() == 0))
                                    {
                                        string[] row_listview2 = { textBox2.Text, comboBox3.Text, numericUpDown1.Value.ToString(), textBox3.Text };

                                        var itme_listview2 = new ListViewItem(row_listview2);

                                        itme_listview2.Name = textBox2.Text;
                                        listView2.Items.Add(itme_listview2);
                                        calculate_kasov_bon_toatal();

                                    }
                                    else
                                    {//Ако вече ги имаме в доставчици, ъпдейт на количествата
                                        var listviewitem_product_supplier = listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text);
                                        var item500 = listviewitem_product_supplier.First();
                                        item500.SubItems[2].Text = (int.Parse(item500.SubItems[2].Text) + (int)numericUpDown1.Value).ToString();
                                    }

                                }
                                else
                                {

                                    var item3 = listView1.FindItemWithText(textBox2.Text); //Ако вече има продукт с такъв код пипаме само количествата и цените
                                    // ListViewItem item2 = listView1.Items 
                                    // .Cast<ListViewItem>()
                                    // .FirstOrDefault(x => x.Text == textBox1.Text);
                                    //listView1.Items.IndexOf(item3).ToString()

                                    item3.SubItems[2].Text = (int.Parse(item3.SubItems[2].Text) + (int)numericUpDown1.Value).ToString();//ъпдейт на количествата
                                    item3.SubItems[3].Text = textBox3.Text;//актуалната, последната цена за брой, по която ще продадем всички натрупани количества
                                    item3.SubItems[4].Text = (double.Parse(textBox3.Text) * int.Parse(item3.SubItems[2].Text)).ToString();//обща цена без отстъпка
                                    item3.SubItems[5].Text = (double.Parse(textBox8.Text) / (int)numericUpDown1.Value * int.Parse(item3.SubItems[2].Text)).ToString();//обща цена с отстъпка

                                    if ((listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text).Count() == 0))
                                    {
                                        string[] row_listview2 = { textBox2.Text, comboBox3.Text, numericUpDown1.Value.ToString(), textBox3.Text };

                                        var itme_listview2 = new ListViewItem(row_listview2);

                                        itme_listview2.Name = textBox2.Text;
                                        listView2.Items.Add(itme_listview2);
                                        calculate_kasov_bon_toatal();

                                    }
                                    else
                                    {//Ако вече ги имаме в доставчици, ъпдейт на количествата
                                        var listviewitem_product_supplier = listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text);
                                        var item500 = listviewitem_product_supplier.First();
                                        item500.SubItems[2].Text = (int.Parse(item500.SubItems[2].Text) + (int)numericUpDown1.Value).ToString();
                                    }

                                }

                            }
                            else
                            {
                                string[] row = { textBox1.Text, textBox2.Text, numericUpDown1.Value.ToString(), textBox3.Text, textBox4.Text, textBox4.Text, comboBox3.Text };

                                var txt = textBox2.Text;

                                if (!listView1.Items.ContainsKey(txt))
                                {

                                    var listViewItem = new ListViewItem(row);
                                    listViewItem.Name = textBox2.Text;
                                    listView1.Items.Add(listViewItem);
                                    button2.Enabled = true;
                                    button5.Enabled = true;
                                    // Тук ще го добави и в таблицата за доставчиците, за целта ще проверим дали не е добавен вече доставчик

                                    if ((listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text).Count() == 0))
                                    {
                                        string[] row_listview2 = { textBox2.Text, comboBox3.Text, numericUpDown1.Value.ToString(), textBox3.Text };

                                        var itme_listview2 = new ListViewItem(row_listview2);

                                        itme_listview2.Name = textBox2.Text;
                                        listView2.Items.Add(itme_listview2);
                                        calculate_kasov_bon_toatal();

                                    }
                                    else
                                    {//Ако вече ги имаме в доставчици, ъпдейт на количествата
                                        var listviewitem_product_supplier = listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text);
                                        var item500 = listviewitem_product_supplier.First();
                                        item500.SubItems[2].Text = (int.Parse(item500.SubItems[2].Text) + (int)numericUpDown1.Value).ToString();
                                    }

                                }
                                else
                                {

                                    var item3 = listView1.FindItemWithText(textBox2.Text); //Ако вече има продукт с такъв код пипаме само количествата и цените
                                    // ListViewItem item2 = listView1.Items 
                                    // .Cast<ListViewItem>()
                                    // .FirstOrDefault(x => x.Text == textBox1.Text);
                                    //listView1.Items.IndexOf(item3).ToString()

                                    item3.SubItems[2].Text = (int.Parse(item3.SubItems[2].Text) + (int)numericUpDown1.Value).ToString();//ъпдейт на количествата
                                    item3.SubItems[3].Text = textBox3.Text;//актуалната, последната цена за брой, по която ще продадем всички натрупани количества
                                    item3.SubItems[4].Text = (double.Parse(textBox3.Text) * int.Parse(item3.SubItems[2].Text)).ToString();//обща цена без отстъпка
                                    item3.SubItems[5].Text = (double.Parse(textBox3.Text) * int.Parse(item3.SubItems[2].Text)).ToString();//обща цена с отстъпка

                                    if ((listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text).Count() == 0))
                                    {
                                        string[] row_listview2 = { textBox2.Text, comboBox3.Text, numericUpDown1.Value.ToString(), textBox3.Text };

                                        var itme_listview2 = new ListViewItem(row_listview2);

                                        itme_listview2.Name = textBox2.Text;
                                        listView2.Items.Add(itme_listview2);
                                        calculate_kasov_bon_toatal();

                                    }
                                    else
                                    {//Ако вече ги имаме в доставчици, ъпдейт на количествата
                                        var listviewitem_product_supplier = listView2.Items.Cast<ListViewItem>().Where(lvi => lvi.Name == textBox2.Text && lvi.SubItems[1].Text == comboBox3.Text);
                                        var item500 = listviewitem_product_supplier.First();
                                        item500.SubItems[2].Text = (int.Parse(item500.SubItems[2].Text) + (int)numericUpDown1.Value).ToString();
                                    }

                                }

                            }





                            List<int> myList = new List<int>();
                            TextBox[] textBoxes = { textBox1, textBox2, textBox3, textBox4, textBox8 };

                            for (int i = 0; i < textBoxes.Length; i++)
                            {
                                textBoxes[i].Text = "";
                            }

                            numericUpDown1.Value = 1;
                            comboBox3.Text = "";

                        }
                        else
                        {

                            MessageBox.Show("Моля, изберете доставчик", "Доставчик", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }


                    }
                    else
                    {

                        MessageBox.Show("Моля, въведете цена", "Цена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                    MessageBox.Show("Моля, иберете или въведете артикулен номер", "Артикулен номер", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {


                MessageBox.Show("Моля, иберете или въведете продукт", "Продукт", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void calculate_kasov_bon_toatal()
        {
            double value = 0;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                value += double.Parse(listView2.Items[i].SubItems[3].Text) * int.Parse(listView2.Items[i].SubItems[2].Text);
            
            }

            textBox11.Text = value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {


                var confirmation = MessageBox.Show("Наистина ли искате да изтриете избраните редове ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {


                    // if (listView2.Items.Count > 0)
                    // {
                    //  listView2.Items.Clear();

                    //}

                    for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = listView1.SelectedItems[i];
                        listView1.Items[itm.Index].Remove();
                        //тук трябва да прихванем всички доставени стоки от другата кутия за доставчик и да ги премахмен по продуктов код




                        foreach (var item in listView2.Items
                        .Cast<ListViewItem>()
                        .Where(lvi => lvi.Name == itm.Name))
                            item.Remove();

                        calculate_kasov_bon_toatal();

                        if (listView1.Items.Count == 0)//Ако не остават редове за триене, просто деактивираме бутона  Изтрий
                        {
                            button2.Enabled = false;
                            button5.Enabled = false;
                        }

                    }
                }
            }
            else
                MessageBox.Show("Моля, изберете ред", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem.ToString() == "Юридическо лице")
            {

                northwindDataSet.EnforceConstraints = false;
                Juridical form2 = new Juridical();
                form2.LegalUpdated += new Juridical.LegalUpdaterHandler(get_from_legal);

                form2.Show();

            }
            else if (comboBox1.SelectedItem.ToString() == "Физическо лице")
            {
                Individual form3 = new Individual();
                form3.AddressUpdated += new Individual.AddressUpdaterHandler(button5_Click);
                form3.Show();

            }
        }



        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Order_Details' table. You can move, or remove it, as needed.
            this.order_DetailsTableAdapter.Fill(this.northwindDataSet.Order_Details);
            // TODO: This line of code loads data into the 'northwindDataSet.Customers' table. You can move, or remove it, as needed.
            // this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
            // TODO: This line of code loads data into the 'northwindDataSet.Products' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'northwindDataSet.Products' table. You can move, or remove it, as needed.



        }

        private void CustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
            //northwindDataSetTableAdapters.ContactsTableAdapter contactsTA= new northwindDataSetTableAdapters.ContactsTableAdapter();
            // /contactsTA.Fill(northwindDataSet.Contacts);
            //BindingSource contactsBS=new BindingSource(northwindDataSet,"Contacts");
            //customersGrid.DataSource=contactsBS;
        }

        private void customersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }


        void Autocompete()
        {
            comboBox3.Items.Add("");
            button2.Enabled = false;
            button5.Enabled = false;
            comboBox1.SelectedIndex = 0;
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            AutoCompleteStringCollection coll1 = new AutoCompleteStringCollection();
            northwindDataSetTableAdapters.ProductsTableAdapter productsTA = new northwindDataSetTableAdapters.ProductsTableAdapter();

            productsTA.Fill(northwindDataSet.Products);
            foreach (DataRow row in northwindDataSet.Products)
            {
                coll.Add(row["SKU"].ToString());
                coll1.Add(row["ProductName"].ToString());
            }
            textBox2.AutoCompleteCustomSource = coll;
            textBox1.AutoCompleteCustomSource = coll1;

            // comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            // comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // AutoCompleteStringCollection coll3 = new AutoCompleteStringCollection();
          //  northwindDataSetTableAdapters.SuppliersTableAdapter suppliersTA = new northwindDataSetTableAdapters.SuppliersTableAdapter();
          //  suppliersTA.Fill(northwindDataSet.Suppliers);
            northwindDataSet.EnforceConstraints = false;
            
            suppliersTableAdapter1.Fill(northwindDataSet.Suppliers);
            Dictionary<string, string> combo3 = new Dictionary<string, string>();
            combo3.Add("", "");

            foreach (DataRow row in northwindDataSet.Suppliers)
            {
                // comboBox2.Items.Add(row["CompanyName"].ToString());
                combo3.Add(row["CompanyName"].ToString(), row["SupplierID"].ToString());

            }

            comboBox3.DataSource = new BindingSource(combo3, null);
            comboBox3.DisplayMember = "Key";
            comboBox3.ValueMember = "Value";
        

           
            
           
            shippersTableAdapter1.Fill(northwindDataSet.Shippers);
            Dictionary<string, string> combo2 = new Dictionary<string, string>();
            combo2.Add("", "");

            foreach (DataRow row in northwindDataSet.Shippers)
            {
                // comboBox2.Items.Add(row["CompanyName"].ToString());
                combo2.Add(row["CompanyName"].ToString(), row["CompanyName"].ToString());

            }

            comboBox2.DataSource = new BindingSource(combo2, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void productsBindingSource_CurrentChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.Trim();
            productsTableAdapter1.FillBy(northwindDataSet.Products, textBox2.Text);


            if (northwindDataSet.Products.Rows.Count == 1)
            {
                textBox1.TextChanged -= textBox1_TextChanged;
                foreach (DataRow row in northwindDataSet.Products)
                {
                   
                    textBox1.Text = row["ProductName"].ToString();

                    textBox3.Text = row["UnitPrice"].ToString();
                    textBox4.Text = (decimal.Parse(textBox3.Text) * numericUpDown1.Value).ToString();
                    textBox1.TextChanged += textBox1_TextChanged;
                    comboBox3.SelectedIndex = int.Parse(row["SupplierID"].ToString());
                }

            }
            else
            {

                productsTableAdapter1.FillBy1(northwindDataSet.Products, textBox1.Text);
            if (northwindDataSet.Products.Rows.Count == 1)
            { 
                textBox1.Clear();
                comboBox3.SelectedIndex = 0;
                textBox3.Clear();
                textBox8.Clear();
                textBox4.Clear();
            }
         
            }
      
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productNameToolStripTextBox_Click(object sender, EventArgs e)
        {

        }

        private void sKUToolStripTextBox_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            productsTableAdapter1.FillBy(northwindDataSet.Products, textBox2.Text);

            if (northwindDataSet.Products.Rows.Count == 1)
            {

                foreach (DataRow row in northwindDataSet.Products)
                {
                    textBox1.TextChanged -= textBox1_TextChanged;

                    textBox1.Text = row["ProductName"].ToString();

                    textBox3.Text = row["UnitPrice"].ToString();
                    textBox4.Text = (decimal.Parse(textBox3.Text) * numericUpDown1.Value).ToString();
                    textBox1.TextChanged += textBox1_TextChanged;
                }

            }

        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {




        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
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
               || (e.KeyCode == Keys.Subtract && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.OemMinus && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.Add && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.Oemplus && ((tBox.Text.Length == 0) ||
                   tBox.Text.EndsWith("e") || tBox.Text.EndsWith("E")))
               || (e.KeyCode == Keys.D5 && Control.ModifierKeys == Keys.Shift
               || e.KeyCode == Keys.Delete
               || e.KeyCode == Keys.Back
               || e.KeyCode == Keys.Left
               || e.KeyCode == Keys.Right
               || (e.KeyCode == Keys.E) && !(tBox.Text.Contains('e')) &&
                   (tBox.Text.Contains('.') && !tBox.Text.EndsWith(".")))))
            {
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox6_Validating(sender, new CancelEventArgs());
                return;
            }
        }

        private void maskedTextBox1_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox6_KeyDown_1(object sender, KeyEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            {
                if (!(((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && !tBox.Text.Contains('%') && !(Control.ModifierKeys == Keys.Shift))
                   || (tBox.TextLength > tBox.SelectionStart && !(Control.ModifierKeys == Keys.Shift) && !(tBox.Text.Contains('.') && e.KeyCode == Keys.OemPeriod) && (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) && !(Control.ModifierKeys == Keys.Shift)
                  || (e.KeyCode == Keys.Decimal && !(tBox.Text.Contains('.'))
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


                    || (e.KeyCode == Keys.D5 && ((tBox.Text.Length != 0) && Control.ModifierKeys == Keys.Shift && (!tBox.Text.EndsWith(".")) && !(tBox.Text.Contains('%')
                       ))

                   || e.KeyCode == Keys.Delete
                   || e.KeyCode == Keys.Back
                   || e.KeyCode == Keys.Left
                   || e.KeyCode == Keys.Right
                   || (e.KeyCode == Keys.E) && !(tBox.Text.Contains('e')) &&
                       (tBox.Text.Contains('.') && !tBox.Text.EndsWith("."))))))
                {
                    e.SuppressKeyPress = true;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    textBox1_Validating(sender, new CancelEventArgs());
                    return;
                }

            }



        }

        private void textBox6_Validating_1(object sender, CancelEventArgs e)
        {



        }



        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (textBox3.Text == "")
            {
               textBox4.Clear();

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!(textBox4.Text == ""))
            {

                Discount Discount = new Discount();

                Discount._total_amount_before_discount = textBox4.Text;
                Discount._items_ordered = numericUpDown1.Value.ToString();

                Discount.Show();
            }
            else
            {

                MessageBox.Show("Моля, изберете първо продукт, преди да начислите отстъпка", "Отстъпка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public string _textBox4
        {
            get { return textBox4.Text; }
        }


        public void funData(string total_amount_discount, string price_per_item_discount)
        {

            textBox3.Text = price_per_item_discount;
            textBox8.Text = total_amount_discount;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Individual f = new Individual();
            f.AddressUpdated += new Individual.AddressUpdaterHandler(button5_Click);
            f.Show();
        }

        private void button5_Click(object sender, AddressUpdateEventArgs e)
        {


            // update the forms values from the event args

            textBox6.Text = e._Name;

            textBox7.Text = e._Name;


        }
        private void get_from_legal(object sender, LegalUpdateEventArgs e)
        {

            textBox6.Text = e._Firm;

            textBox7.Text = e._MOL;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
               ) //&& e.KeyChar != '.')    - Спирамевъвеждането на ., ако я махнем ще може да се въвежда и в началото и в края 
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (textBox5.Text.Length == 13 && int.Parse(textBox5.Text.Substring(0, 1)) == 1)
            {
                comboBox2.Text = "Еконт";

            }
            else
            {

                comboBox2.Text = "Спиди";

            }

            if (textBox5.Text == "")
            {
                comboBox2.Text = "";

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (!(textBox6.Text == "") && !(textBox7.Text == ""))
            {
                textBox6.Text = "";
                textBox7.Text = "";
                comboBox1.Text = "Свободна продажба";

            }
        }

        private void textBox6_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            // {
            // MessageBox.Show("Right-click is not allowed", "No Right-click");
            //return;
            // }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!(textBox9.Text == ""))
            {

                int orderID = 0, ProductID = 0, SupplierID = 2, ShipperID = 0;
                string ShipperCompany = "";
                productsTableAdapter1.FillBy(northwindDataSet.Products, textBox2.Text);


                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate10 = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                shippersTableAdapter1.FillBy(northwindDataSet.Shippers, comboBox2.Text);

                foreach (DataRow row in northwindDataSet.Shippers)
                {

                    ShipperID = int.Parse(row[0].ToString());
                    ShipperCompany = row[1].ToString();

                }

                customersTableAdapter1.FillBy2(northwindDataSet.Customers, textBox6.Text);

                string Bon, Bon_Suma, Bon_slovom, FU, FM, Invoice, SummerCartOrderID;
                if (!((((KeyValuePair<string, byte>)CustomerList.SelectedItem).Value) == 50))
                {
                    FU=((KeyValuePair<string, string>)comboBox4.SelectedItem).Key;
                    FM=((KeyValuePair<string, string>)comboBox4.SelectedItem).Value;
                    Bon = textBox9.Text;
                    Bon_Suma=textBox11.Text;
                    Bon_slovom=textBox12.Text;

                }
                else
                {
                    FU = null;
                    FM = null;
                    Bon = null;
                    Bon_Suma = null;
                    Bon_slovom = null;
                }
                Invoice=(textBox13.Text=="")?null:textBox13.Text;
                SummerCartOrderID=(textBox14.Text=="")?null:textBox14.Text;
                if (northwindDataSet.Customers.Rows.Count == 1)
                {
                    foreach (DataRow row in northwindDataSet.Customers)
                    {

                        if (ShipperCompany != "")
                        {


                            ordersTableAdapter1.InsertQuery6(row["CustomerID"].ToString(), 1, sqlFormattedDate10, sqlFormattedDate10, sqlFormattedDate10, ShipperID, 8.00m, ShipperCompany, row["Address"].ToString(), row["City"].ToString(), row["Region"].ToString(), row["PostalCode"].ToString(), row["Country"].ToString(), Bon, FU, FM, Bon_Suma,Bon_slovom,textBox5.Text,textBox13.Text,((KeyValuePair<string, byte>)CustomerList.SelectedItem).Key, textBox14.Text);
                        }
                        else
                        {


                            ordersTableAdapter1.InsertQuery(row["CustomerID"].ToString(), 1, sqlFormattedDate10, sqlFormattedDate10, sqlFormattedDate10, 8.00m, row["Address"].ToString(), row["City"].ToString(), row["Region"].ToString(), row["PostalCode"].ToString(), row["Country"].ToString(), Bon, FU, FM, Bon_Suma, Bon_slovom,textBox13.Text,((KeyValuePair<string, byte>)CustomerList.SelectedItem).Key, textBox14.Text);
                        }


                    }
                }
                else
                {



                    if (ShipperCompany != "")
                    {

                        ordersTableAdapter1.InsertQuery2("99999", 1, sqlFormattedDate10, sqlFormattedDate10, sqlFormattedDate10, ShipperID, 8.00m, ShipperCompany, "България", Bon, FU, FM, Bon_Suma, Bon_slovom, textBox5.Text,textBox13.Text, ((KeyValuePair<string, byte>)CustomerList.SelectedItem).Key, textBox14.Text);
                    }
                    else
                    {

                        ordersTableAdapter1.InsertQuery1("99999", 1, sqlFormattedDate10, sqlFormattedDate10, sqlFormattedDate10, 8.00m, "България", Bon, FU, FM, Bon_Suma, Bon_slovom,textBox13.Text,((KeyValuePair<string, byte>)CustomerList.SelectedItem).Key, textBox14.Text);

                    }

                }


                ordersTableAdapter1.FillBy(northwindDataSet.Orders);
                foreach (DataRow row in northwindDataSet.Orders)
                {
                    orderID = int.Parse(row[0].ToString());//хващаме номера на последно въведената поръчка

                }



                foreach (ListViewItem itemRow in listView1.Items)
                {


                    productsTableAdapter1.FillBy(northwindDataSet.Products, itemRow.SubItems[1].Text);


                    if (northwindDataSet.Products.Count == 0)
                    {

                        productsTableAdapter1.InsertQuery(itemRow.SubItems[0].Text, SupplierID, 1, "1", decimal.Parse(itemRow.SubItems[3].Text), 1, 1, 1, false, itemRow.SubItems[1].Text, sqlFormattedDate10);


                    }
                    
                    productsTableAdapter1.FillBy(northwindDataSet.Products, itemRow.SubItems[1].Text);


                    foreach (DataRow row in northwindDataSet.Products)
                    {
                        ProductID = int.Parse(row[0].ToString());

                    }




                    if (float.Parse(itemRow.SubItems[5].Text) <= float.Parse(itemRow.SubItems[4].Text))
                    {
                        order_DetailsTableAdapter.InsertQuery(orderID, ProductID, decimal.Parse(itemRow.SubItems[3].Text), short.Parse(itemRow.SubItems[2].Text), (float)Math.Round((decimal)(1 - float.Parse(itemRow.SubItems[5].Text) / float.Parse(itemRow.SubItems[4].Text)), 4), 0);
                    }
                    else
                    {
                        order_DetailsTableAdapter.InsertQuery(orderID, ProductID, decimal.Parse(itemRow.SubItems[3].Text), short.Parse(itemRow.SubItems[2].Text), 0, (float)Math.Round((decimal)(float.Parse(itemRow.SubItems[5].Text) / float.Parse(itemRow.SubItems[4].Text) - 1), 4));

                    }
                    //    for (int i = 0; i < itemRow.SubItems.Count; i++)
                    //  {
                    //MessageBox.Show(itemRow.SubItems[i].Text);
                    // Do something useful here, for example the line above.
                    // }


                }
                foreach (ListViewItem item in listView2.Items)
                {

                    DateTime myDateTime1 = DateTime.Now;
                    string sqlFormattedDate11 = myDateTime1.ToString("yyyy-MM-dd HH:mm:ss");
                    suppliersTableAdapter1.FillBy(northwindDataSet.Suppliers, item.SubItems[1].Text);
                    SupplierID = int.Parse(northwindDataSet.Suppliers.Rows[0][0].ToString());
                    productsTableAdapter1.FillBy(northwindDataSet.Products, item.SubItems[0].Text);
                    deliveryTableAdapter1.InsertQuery(item.SubItems[0].Text, int.Parse(item.SubItems[2].Text), orderID, northwindDataSet.Suppliers.Rows[0][1].ToString(), float.Parse(item.SubItems[3].Text), (float)(double.Parse(item.SubItems[3].Text) / 1.2), northwindDataSet.Products.Rows[0][1].ToString());
                    productsTableAdapter1.UpdateQuery(SupplierID,decimal.Parse(item.SubItems[3].Text),item.SubItems[0].Text);
                }

                if (!((((KeyValuePair<string, byte>)CustomerList.SelectedItem).Value) == 50))
                {
                    //тук пускаме касова блежка
                    List<byte[]> opisanie_al = new List<byte[]>();
                    Global_Variables.seq = 0x20;



                    List<Item_For_Sale> Items_For_Sale = new List<Item_For_Sale>();

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        Items_For_Sale.Add(new Item_For_Sale(int.Parse(listView1.Items[i].SubItems[2].Text), double.Parse(listView1.Items[i].SubItems[3].Text), listView1.Items[i].SubItems[0].Text, Global_Variables.seq));

                    }

                    double total = Items_For_Sale.Sum(item => item.Price); //Общата сума за цялата продажба

                    byte[] Daisy_Perfect_SKL_Status_first = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 74, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//1-ва 74 (4Ah) СТАТУС НА ФУ 1, 36, 32, 74, 5, 48, 48, 57, 51, 3
                    opisanie_al.Add(Daisy_Perfect_SKL_Status_first);

                    byte[] Daisy_Perfect_SKL_Status_second = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 74, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//2-ра 74 (4Ah) СТАТУС НА ФУ 1, 36, 33, 74, 5, 48, 48, 57, 52, 3
                    opisanie_al.Add(Daisy_Perfect_SKL_Status_second);

                    byte[] Daisy_Perfect_SKL_Status_third = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 74, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//3-та 74 (4Ah) СТАТУС НА ФУ  1, 36, 34, 74, 5, 48, 48, 57, 53, 3 
                    opisanie_al.Add(Daisy_Perfect_SKL_Status_third);

                    byte[] Posleden_nomer_na_dokument_fisrt = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 113, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//4-та, 113 (71h) ПОСЛЕДЕН НОМЕР НА ДОКУМЕНТ 1, 36, 35, 113, 5, 48, 48, 59, 61, 3
                    opisanie_al.Add(Posleden_nomer_na_dokument_fisrt);

                    byte[] ClerkNum = new byte[] { 49, 44 };//Оператор
                    byte[] Password = new byte[] { 48, 48, 48, 48, 48, 49 };//Парола на оператор – до 6 цифри
                    byte[] Others = new byte[] { 44, 48, 48, 48, 49 };//Това ще го мислиме какво е 

                    byte[] fiscalen_bon = Daisy_Perfect_SKL_Command.nachalo_na_fiscalen_bon(Global_Variables.seq++, 48, ClerkNum, Password, Others);
                    opisanie_al.Add(fiscalen_bon);

                    //5-та,	30h	48*	Начало на фискален бон
                    //Област за данни:	{ClerkNum},{Password}[,{TillNumber},{Invoice}]

                    //Отговор:		Allreceipt - Брой на всички издадени фискални и нефискални бонове от последното приключване на деня до момента (4 байта)., FiscReceipt -Брой на всички фискални бонове от последното приключване на деня до момента (4 байта).
                    //              FiscReceipt -Брой на всички фискални бонове от последното приключване на деня до момента (4 байта).


                    //for (int j=0;!(j == item2.Opisanie_block_2_print_lenght-1);j++)
                    //{
                    //Console.WriteLine(item1[j]);

                    //}

                    //double total = myList.Where(item => item.Name == "Eggs").Sum(item => item.Amount);-ако има някакво условие


                    foreach (Item_For_Sale bytter in Items_For_Sale)
                    {
                        opisanie_al.Add(bytter.opisanie_block_1_print_ready1);
                        if (bytter.opisanie_block_2_print_ready2.Length > 0)
                        {
                            opisanie_al.Add(bytter.opisanie_block_2_print_ready2);
                        }
                    }

                    // foreach(byte[]bitter in opisanie_al)
                    // {
                    // foreach (byte c in bitter)
                    // {
                    // Console.WriteLine("{0:X}", c);

                    // }
                    // }
                    //  for(int j =11;!(j==12);j++)    //!(j==opisanie_al.Count-1)
                    // {
                    // foreach (byte c in opisanie_al[j])
                    // {
                    //  Console.WriteLine("{0:X}", c);

                    // }

                    // }
                    byte[] Total_amount = Total_amount_calculate.Total_amount_cal(Global_Variables.seq++, total);//команда 49(31h) Продажба, бща сума в група Б.Б+
                    opisanie_al.Add(Total_amount);

                    byte[] Total_amount_final = Total_all.Total_amount_final_calc(Global_Variables.seq++, ((KeyValuePair<string, byte>)CustomerList.SelectedItem).Value, total);
                    opisanie_al.Add(Total_amount_final);

                    byte[] kraj_na_fiskalen_bon = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 56, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);// 1, 36, 41, 56, 5, 48, 48, 56, 58, 3
                    opisanie_al.Add(kraj_na_fiskalen_bon);

                    // byte[] posleden_nomer_na_dokument_last = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 113, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);

                    //opisanie_al.Add(posleden_nomer_na_dokument_last);

                    foreach (byte[] bitter in opisanie_al)
                    {
                        //serialPort1.Write(bitter, 0, bitter.Length);

                          myPort.DoWrite(bitter, bitter.Length); //- Тук спираме печат на боновете
                    }

                    // OK e byte[] diagnostika = Daisy_Perfect_SKL_Command.Command(seq++, 71, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);
                    // byte[] Bulstat = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 99, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//bulstat 1, 36, 36, 99, 5, 48, 48, 59, 48

                    //byte[] nomer_na_dokumet = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 113, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//new byte[] { 1, 36, 41, 113, 5, 48, 48, 60, 51 };

                    //byte[] informacia_po_bulstat = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 90, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);

                    //byte[] date_and_time = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 62, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);

                    //mySerialPort.Write(Bulstat, 0, Bulstat.Length);
                    //Thread.Sleep(500);

                    // mySerialPort.Write(nomer_na_dokumet, 0, nomer_na_dokumet.Length);
                    //Thread.Sleep(500);

                    // mySerialPort.Write(informacia_po_bulstat, 0, informacia_po_bulstat.Length);
                    //Thread.Sleep(500);

                    // byte[] Z = Daisy_Perfect_SKL_Command.Otchet_Z_s_nulirane(Global_Variables.seq++, 69, 48);//48 с нулиране, 50 без нулиране


                    //myPort.DoWrite(nomer_na_dokumet,nomer_na_dokumet.Length);

                    // myPort.DoWrite(Daisy_Perfect_SKL_Status_first, Daisy_Perfect_SKL_Status_first.Length);
                    // myPort.DoWrite(Z, Z.Length);

                    myPort.Message = string.Empty;

                    listView1.Items.Clear();
                    listView2.Items.Clear();
                    textBox11.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    posleden_nomer_na_dokument();
                }
                
                

            }
            else
            {

                MessageBox.Show("Моля, въведете номер на документ", "Документ за продажба", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            CustomerList.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textBox5.Text = "";
            comboBox1.SelectedIndex = 0;

        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {


            byte[] Z = Daisy_Perfect_SKL_Command.Otchet_Z_s_nulirane(Global_Variables.seq++, 69, 48);//48 с нулиранe


            myPort.DoWrite(Z, Z.Length);
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {

            byte[] Z = Daisy_Perfect_SKL_Command.Otchet_Z_s_nulirane(Global_Variables.seq++, 69, 50);// 50 без нулиране
            myPort.DoWrite(Z, Z.Length);
            // byte[] Daisy_Perfect_SKL_Status_first = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 74, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//1-ва 74 (4Ah) СТАТУС НА ФУ 1, 36, 32, 74, 5, 48, 48, 57, 51, 3
            //serialPort1.Write(Daisy_Perfect_SKL_Status_first, 0, Daisy_Perfect_SKL_Status_first.Length);
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            posleden_nomer_na_dokument();
        }

        private void posleden_nomer_na_dokument()
        {
            textBox9.Clear();

            byte[] nomer_na_dokumet = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 113, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//new byte[] { 1, 36, 41, 113, 5, 48, 48, 60, 51 };


            myPort.DoWrite(nomer_na_dokumet, nomer_na_dokumet.Length);
            // byte[] Daisy_Perfect_SKL_Status_first = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 74, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//1-ва 74 (4Ah) СТАТУС НА ФУ 1, 36, 32, 74, 5, 48, 48, 57, 51, 3
            //serialPort1.Write(Daisy_Perfect_SKL_Status_first, 0, Daisy_Perfect_SKL_Status_first.Length);
            textBox9.Text = myPort.Message;
            myPort.Message = string.Empty;

        }


        private void button9_Click(object sender, EventArgs e)
        {

            textBox9.Clear();
            byte[] informacia_po_bulstat = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 90, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);


            myPort.DoWrite(informacia_po_bulstat, informacia_po_bulstat.Length);
            // byte[] Daisy_Perfect_SKL_Status_first = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 74, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);//1-ва 74 (4Ah) СТАТУС НА ФУ 1, 36, 32, 74, 5, 48, 48, 57, 51, 3
            // serialPort1.Write(Daisy_Perfect_SKL_Status_first, 0, Daisy_Perfect_SKL_Status_first.Length);
            textBox9.Text = myPort.Message;
            myPort.Message = string.Empty;

        }

        private void button10_Click(object sender, EventArgs e)
        {

            textBox9.Clear();
            byte[] Bulstat = Daisy_Perfect_SKL_Command.Command(Global_Variables.seq++, 99, Global_Variables.STX, Global_Variables.ENQ, Global_Variables.ETX);


            myPort.DoWrite(Bulstat, Bulstat.Length);
            textBox9.Text = myPort.Message;
            myPort.Message = string.Empty;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // byte[] klen = Daisy_Perfect_SKL_Command.pechat_ot_klen(Global_Variables.seq++,195,"kur","kur","kur","kur");

            Klen Klen = new Klen(this.myPort);

            Klen.Show();

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            textBox5.Text = Regex.Replace(textBox5.Text, "[^0-9.]", "");
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
               && !char.IsDigit(e.KeyChar)
               )   // && e.KeyChar != '.') - ще разрешим да се въвежда ., в началото,а и навсякъде другаде
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox9_Validating(object sender, CancelEventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            int tstDbl;
            if (!int.TryParse(tBox.Text, out tstDbl))
            {
                textBox9.Clear();
                //handle bad input
                return;
            }
            else
            {

            }

            //int value OK


        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex == 0)
            {
                textBox5.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
            }
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {

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

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox10.Text = Regex.Replace(textBox10.Text, "[^0-9.]", "");
        }

        private bool ContainsNumber(string input)
        {
            return Regex.IsMatch(input, "[^0-9]");
        }

        private void textBox10_Validating(object sender, CancelEventArgs e)
        {


            textBox10.Text = Regex.Replace(textBox10.Text, "[^0-9.]", "");
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {

            
            if (double.Parse(textBox11.Text) != 0)
           {
               textBox12.Text = NumtoTextBG.Dig2Txt(textBox11.Text);
            }
            else
            {
               textBox12.Clear();
           
          }
            }
            else
            {
                textBox12.Clear();
            }
        }

        private void textBox1_Validating_1(object sender, CancelEventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
        }


    }
}

    

       

        

           
        

    

