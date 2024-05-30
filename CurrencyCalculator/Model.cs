namespace CurrencyCalculator;

public class Model
{
    public record TableRate
    {
        public string table { get; set; }
        public string no { get; set; }
        public DateTime tradingDate { get; set; }
        public DateTime effectiveDate { get; set; }
        public List<Rate> rates { get; set; }
    }
    public record Rate
    {
        public string  currency { get; set; }
        public string code { get; set; }
        public decimal mid { get; set; }
    }
}