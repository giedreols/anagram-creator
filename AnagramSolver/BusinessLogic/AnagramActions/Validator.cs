using Cli;

namespace BusinessLogic.AnagramActions
{
    public static class Validator
    {
        public static List<string> ValidateAmount(this List<string> list)
        {
            int totalAmount = new Configuration().TotalAmount; // Configuration should be created during startup of application, and passed via parameter 

            if (list.Count > totalAmount)
            {
                return list.GetRange(0, totalAmount);
            }
            return list;
        }
    }
}
