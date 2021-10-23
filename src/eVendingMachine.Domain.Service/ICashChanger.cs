using System.Collections.Generic;

namespace eVendingMachine.Domain.Service
{
    public interface ICashChanger
    {
        List<Cash> CalculateOutCashes(List<Cash> cashes, decimal TotalOutCash);
    }
}