using System;

namespace eVendingMachine.Dto
{
    public class CashDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
