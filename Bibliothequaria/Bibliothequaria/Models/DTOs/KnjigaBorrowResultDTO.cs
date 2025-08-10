namespace Bibliothequaria.Models.DTOs
{
    public class KnjigaBorrowResultDTO
    {
        public int IdTransakcije { get; set; }
        public DateOnly RokVracanja { get; set; }  // borrow date + 2 months
    }
}
