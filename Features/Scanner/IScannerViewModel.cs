using System;
using Library.Features.CardReader;
using Prism.Commands;

namespace Library.Features.Scanner
{
    public interface IScannerViewModel
    {
        string BarCode { get; set; }
        bool Enabled { get; set; }
    }
}