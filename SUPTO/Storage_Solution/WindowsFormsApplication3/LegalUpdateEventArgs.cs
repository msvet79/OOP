using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage_Solution
{
   public class LegalUpdateEventArgs
    {
       // add local member variables to hold text

        private string mFirm;
        private string mMOL;

       
        // class constructor

        public LegalUpdateEventArgs(string sFirm, string sMOL)
        {

          this.mFirm = sFirm;

          this.mMOL = sMOL;


        }

        public string _Firm
        {

            get
            {

                return mFirm;

            }
        }
        public string _MOL
        {

            get
            {

                return mMOL;

            }
        }
   }
}
