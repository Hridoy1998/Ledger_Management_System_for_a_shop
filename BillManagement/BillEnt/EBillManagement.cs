using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillManagement.BillEnt
{
    public class EBillManagement
    {
        public int Id
        {
            set;
            get;
        }

        public string Name
        {
            set;
            get;
        }

        public string Date
        {
            set;
            get;
        }

        public int Total
        {
            set;
            get;
        }


        public int Due
        {
            set;
            get;
        }
    }
}
