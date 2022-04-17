namespace SharpChain.Model
{
    public class Transaction
    {
        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }

        public Transaction(string from, string to, double amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }

        public override string ToString()
        {
            return string.Format("From:{0}, To:{1}, Amount:{2}", From, To, Amount);
        }
    }
}
