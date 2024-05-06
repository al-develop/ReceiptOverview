using System;

namespace ReceiptOverview.Models
{
    public class Position
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}