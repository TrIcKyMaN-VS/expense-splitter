public class Expense
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int PaidBy { get; set; }
    public decimal Amount { get; set; }
    public List<Split> Splits { get; set; } = new List<Split>();
}