using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Entities
{
    public enum LoanState { PENDING, CURRENT, OVERDUE, COMPLETE }

    public static class LoanConstants
    {
        public const int LOAN_PERIOD = 14;
    }

}
