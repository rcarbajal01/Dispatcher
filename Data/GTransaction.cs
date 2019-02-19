using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPP.DPPData
{
    public class GTransaction
    {        

        public decimal amount { get; set; }

        private DateTime _date = Convert.ToDateTime("1900/01/01");
        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }        

        public string merchant { get; set; }

        public string type { get; set; }

        public string creditPlan { get; set; }        

    }
}
