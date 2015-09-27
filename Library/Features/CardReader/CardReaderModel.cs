using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Library.Features.CardReader
{
    public class CardReaderModel : ICardReaderModel
    {
        [Required(ErrorMessage = @"Borrower ID cannot be empty or blank.")]
        [Integer(ErrorMessage = @"Borrower ID must be a positive integer.")]
        [Max(int.MaxValue - 1, ErrorMessage = @"Borrower ID cannot be so big.")]
        public string BorrowerId { get; set; }
    }
}
