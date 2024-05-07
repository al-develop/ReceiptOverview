using System;
using System.Collections.Generic;

namespace ReceiptOverview.Models
{
    public class Position
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<Entry> Entries { get; set; }
    }
}