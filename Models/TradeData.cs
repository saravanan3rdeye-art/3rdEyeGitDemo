namespace _3rdEyeGitDemo.Models
{
    public class TradeData
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
        public int Volume { get; set; }
        public DateTime Timestamp { get; set; }
        public string PriceDirection { get; set; } = "unchanged";
    }
}