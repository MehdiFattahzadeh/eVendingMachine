using System;
using System.Collections.Generic;
using System.Linq;

namespace eVendingMachine.Domain.Service
{
    public class CashChanger : ICashChanger
    {
        public List<Cash> CalculateOutCashes(List<Cash> cashes, decimal TotalOutCash)
        {
            var result = new List<Cash>();
            var orderedCashes = cashes.OrderByDescending(c => c.Price).ToList();
            var target = TotalOutCash;
            Cash currentCash = null;
            while (target > 0)
            {
                for (int i = 0; i < orderedCashes.Count; i++)
                {
                    if (target >= orderedCashes[i].Price)
                    {
                        currentCash = orderedCashes[i];
                        target -= currentCash.Price;
                        break;
                    }
                }
                result.Add(currentCash);
            }
            return result;
        }
    }
}
