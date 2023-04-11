using Cli;
using Contracts.Data;
using System.Transactions;

namespace BusinessLogic.AnagramActions
{
    public static class Validator
    {
        public static List<string> ValidateAmount(this List<string> list)
        {
            var totalAmount = new Configuration().TotalAmount;
            return list.GetRange(0, totalAmount);
        }
    }
}
