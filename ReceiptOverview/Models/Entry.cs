namespace ReceiptOverview.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}