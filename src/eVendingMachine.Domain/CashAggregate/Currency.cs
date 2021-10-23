namespace eVendingMachine.Domain
{
    public class Currency:Entity,IAggregateRoot
    {
        private Currency()
        {
            // For EF
        }
        public Currency(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public string Name { get; private set; }
        public string Symbol { get; private set; }
    }

  
}
