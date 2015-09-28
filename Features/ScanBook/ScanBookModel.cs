using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Interfaces.Entities;

namespace Library.Features.ScanBook
{
    public class ScanBookModel
    {
        public int Barcode { get; set; }
        public IMember Borrower { get; set; }
    }
}
