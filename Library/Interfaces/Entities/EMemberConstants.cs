using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Entities
{
    public enum MemberState { BORROWING_ALLOWED, BORROWING_DISALLOWED }

    public static class MemberConstants
    {
        public const int LOAN_LIMIT = 5;

        public const float FINE_LIMIT = 10.0f;
    }

}
