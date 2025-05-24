using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.IO;




using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;


namespace Storage_Solution
{
    public class PortReader
    {
        private SerialPort _port;
        private string _name = string.Empty;
        private string _error = string.Empty;
        private string _msg = string.Empty;
      
        public string Name
        {
            get { return _name; }
            set { _port.PortName = value; }
        }

        public string Message
        {
            get { return _msg; }
            set { _msg = value; }
        }

        public string Error
        {
            get { return _error; }
        }

        

        public PortReader()
        {

            try
            {
                _port = new SerialPort();

                _port.BaudRate = 9500;
                _port.Parity = Parity.None;
                _port.StopBits = StopBits.One;
                _port.DataBits = 8;
                _port.Handshake = Handshake.None;
                _port.ReadTimeout = 500;
                _port.WriteTimeout = 500;
                _port.DataReceived += new SerialDataReceivedEventHandler(PortReader_SerialDataReceived);
                _port.ErrorReceived += new SerialErrorReceivedEventHandler(PortReader_SerialErrorReceived);
            }
            catch (IOException ex) { MessageBox.Show(ex.Message); }


        }


        private void PortReader_SerialErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("Error recieved: " + e.EventType);
        }

        private void PortReader_SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            // Form1 frm = Application.OpenForms["Form1"] as Form1;
            if (!_port.IsOpen)
            {
                _error = "Can't read. Port is closed.";
                return;
            }

            int bytes = _port.BytesToRead;
            byte[] respBuffer = new byte[bytes];
            _port.Read(respBuffer, 0, bytes);


            int[] tempAry = respBuffer.Select(n => Convert.ToInt32(n)).ToArray();


            if (Array.IndexOf(tempAry, 113) > -1)
            {

                var newArray = tempAry.Skip(Array.IndexOf(tempAry, 113)).Take(7).ToArray();

                for (int j = 1; j <= newArray.Length - 1; j++)
                {
                    _msg += (Convert.ToChar(newArray[j]));
                }
                int num2;
                if (int.TryParse(_msg, out num2))
                {

                    _msg = (++num2).ToString();// Увеличаваме текущия номер с единица
                    // It was assigned.
                   
                }
               

               // frm.textBox9.Text = _msg;
               // _msg = "";
               
            }
            else
            {

                if (Array.IndexOf(tempAry, 90) > -1)
                {

                    var newArray = tempAry.Skip(Array.IndexOf(tempAry, 90)).Take(60).ToArray();

                    for (int j = 1; j <= newArray.Length - 1; j++)
                    {
                        _msg += (Convert.ToChar(newArray[j]));
                        

                    }
                  //  frm.textBox9.Text = _msg;
                   // _msg = "";
                }
                else
                {

                    if (Array.IndexOf(tempAry, 99) > -1)
                    {

                        var newArray = tempAry.Skip(Array.IndexOf(tempAry, 99)).Take(10).ToArray();

                        for (int j = 1; j <= newArray.Length - 1; j++)
                        {
                            _msg += (Convert.ToChar(newArray[j]));
                            
                           

                        }

                      ///  frm.textBox9.Text = _msg;

                       // _msg = "";
                        //RxString = RxString = string.Join(" ", respBuffer.Select(b => b.ToString()));


                    }
                    else
                    {

                        //RxString = string.Join(" ", respBuffer.Select(b => b.ToString()));
                    }

                }
                
            }

        }

        public bool DoOpen()
        {
            if (_port.PortName == "")
            {
                _error = "Please supply port name!";
                return false;
            }

            if (_port.IsOpen)
            {
                _port.Close();
            }

            _port.Open();
            return true;
        }

        public bool DoClose()
        {
            if (!_port.IsOpen)
            {
                _error = "Can't close port that is not in open state.";
                return false;
            }

            _port.Close();
            return true;
        }
        public void DoWrite(byte[] c, int len)
        {
            _port.Write(c, 0, len);
            Thread.Sleep(500);
        }
    }
}


