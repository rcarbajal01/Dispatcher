using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPP.DPPData
{
    public class Transaction
    {
        public int idTransaction { get; set; }

        public int idCustomer { get; set; }

        public string decision { get; set; }

        public decimal amount { get; set; }

        private DateTime _consultDate = Convert.ToDateTime("1900/01/01");
        public DateTime consultDate 
        {
            get { return _consultDate; }
            set { _consultDate = value; } 
        }

        private DateTime _processDate = Convert.ToDateTime("1900/01/01");
        public DateTime processDate
        {
            get { return _processDate; }
            set { _processDate = value; }
        }

        public string sold { get; set; }

        public string declineReason { get; set; }

        public string rejectReason { get; set; }

        public decimal totalAmount { get; set; }

        public decimal elegAmount { get; set; }

        public decimal balance { get; set; }

        public decimal totalDue { get; set; }

        public decimal monthlyAmount { get; set; }

        public decimal interestMonthlyAmount { get; set; }

        public decimal totalInterestAmount { get; set; }

        public decimal ivaMonthlyAmount { get; set; }

        public decimal newBalance { get; set; }

        public decimal payments { get; set; }

        public decimal balanceGS { get; set; }

        private DateTime _limitPaymentDate = Convert.ToDateTime("1900/01/01");
        public DateTime limitPaymentDate
        {
            get { return _limitPaymentDate; }
            set { _limitPaymentDate = value; }
        }

        private DateTime _dueDate = Convert.ToDateTime("1900/01/01");
        public DateTime dueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        private DateTime _cutDate = Convert.ToDateTime("1900/01/01");
        public DateTime cutDate
        {
            get { return _cutDate; }
            set { _cutDate = value; }
        }

        public string status { get; set; }

        public string user { get; set; }

    }
}
