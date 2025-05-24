using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage_Solution
{
   public  class AddressUpdateEventArgs
    {
        // add local member variables to hold text

        private string mName;

       
        // class constructor

        public AddressUpdateEventArgs(string sName )
        {

            this.mName = sName;

        }

        public string _Name
        {

            get
            {

                return mName;

            }

        }


        }
    
    }


