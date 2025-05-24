using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.Net;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = "";
            
            string urlAddress = textBox1.Text;
      //  //http://tashev-galving.com/promo-pack/328/HITACHI+%D0%97%D0%B8%D0%BC%D0%B0+2015%D0%B3.

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

           
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Workbook newWorkbook = excelApp.Workbooks.Add();
            Sheets excelSheets = newWorkbook.Worksheets;
            string currentSheet = "Sheet1";
            Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
            
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(data);

         HtmlNodeCollection htmlnodes = doc.DocumentNode
         .SelectNodes("//div[contains(@class,'tg-promo-footer-info tg-promo-footer-info-promo-pack')]");
         
           // HtmlNodeCollection htmlnodes2 = doc.DocumentNode
          // .SelectNodes("//h4");

         HtmlNodeCollection htmlnodes3 = doc.DocumentNode
     .SelectNodes("//div[contains(@class,'tg-promo-footer-box-promo-title tg-promo-footer-box-promo-title-promo-pack')]/h4");

         HtmlNodeCollection htmlnodes4 = doc.DocumentNode
 .SelectNodes("//div[contains(@class,'tg-promo-footer-image tg-promo-footer-image-promo-pack')]");
         
         // var divs = doc.DocumentNode
        // .SelectNodes("//a[@href]");

            for (int i = 0; i <= htmlnodes.Count - 1; i++)
            {

                excelWorksheet.Cells[i + 1, 2] = ExtractDecimalFromString(htmlnodes[i].InnerText);
                excelWorksheet.Cells[i + 1, 1] = htmlnodes3[i].InnerText;
                excelWorksheet.Cells[i + 1, 3] = htmlnodes4[i].ChildNodes[1].Attributes[2];
 
            }
 
        }

        private dynamic ExtractDecimalFromString(string str)
        {
            Regex digits = new Regex(@"^\D*?((-?(\d+(\.\d+)?))|(-?\.\d+)).*");
            Match mx = digits.Match(str);
            //Console.WriteLine("Input {0} - Digits {1} {2}", str, mx.Success, mx.Groups);

            return mx.Success ? Convert.ToDecimal(mx.Groups[1].Value) : 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data = "";

            //string urlAddress = textBox1.Text;
            string urlAddress = "http://tashev-galving.com/000000-999-5/0//filters?filter_2=Bosch";
            //  //http://tashev-galving.com/promo-pack/328/HITACHI+%D0%97%D0%B8%D0%BC%D0%B0+2015%D0%B3.

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }


            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Workbook newWorkbook = excelApp.Workbooks.Add();
            Sheets excelSheets = newWorkbook.Worksheets;
            string currentSheet = "Sheet1";
            Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(data);

            HtmlNodeCollection htmlnodes4 = doc.DocumentNode
.SelectNodes("//div[contains(@class,'tg-product tg-productb')]");

            HtmlNodeCollection htmlnodes5 = doc.DocumentNode
.SelectNodes("//div[contains(@class,'tg-productimg tg-productimgb')]");

           
           // HtmlNodeCollection htmlnodes4 = doc.DocumentNode
//.SelectNodes("//div[contains(@class,'tg-product-contents')]");

            // var divs = doc.DocumentNode
            // .SelectNodes("//a[@href]");

            for (int i = 0; i <= htmlnodes4.Count - 1; i++)
            {
               // for (int j = 0; j <= 4; j++)
              //  {
                    // excelWorksheet.Cells[i + 1, 3+j] = htmlnodes4[i].ChildNodes[j].InnerText;
               
               // }

                for (int j = 1; j <= 10; j++)
                      {
                    excelWorksheet.Cells[i + 1, j] = htmlnodes4[i].Attributes[j];

                    }

                excelWorksheet.Cells[i + 1, 11] = htmlnodes5[i].InnerHtml;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            // Workbook newWorkbook = excelApp.Workbooks.Add();
            Workbook newWorkbook = excelApp.Workbooks.Open(@"C:\Users\Svetlio\Documents\Promotions_Apryl.xlsx");
            Sheets excelSheets = newWorkbook.Worksheets;
            
            
            for (int z = 1; z <= 217; z++)
            {

            string data = "";

            string currentSheet = "Sheet2";
            Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);

            string urlAddress = (string)(excelSheets.get_Item("Sheet1").cells[z, 9]as Range).Value;
           

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

        
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(data);
            HtmlNodeCollection htmlnodes4 = doc.DocumentNode
.SelectNodes("//*[contains(@class,'clickforproduct')]");
            for (int i = 0; i <= htmlnodes4.Count - 1; i++)
            {
              
                for (int j = 1; j <= 1; j++)
                {

                    excelWorksheet.Cells[z, 1] = (string)(excelSheets.get_Item("Sheet1").cells[z, 8] as Range).Value;
                    excelWorksheet.Cells[z, j+i+1] = htmlnodes4[i].Attributes[j];

                }

                //excelWorksheet.Cells[i + 1, 11] = htmlnodes5[i].InnerHtml;
            }

        }

    }
}
}