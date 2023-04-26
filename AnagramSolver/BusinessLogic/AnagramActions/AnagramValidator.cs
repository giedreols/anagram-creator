
namespace BusinessLogic.AnagramActions
{
    public static class AnagramValidator
    {
        public static List<string> ValidateAmount(this List<string> list, int totalAmount)
        {
            return list.Count > totalAmount ? list.GetRange(0, totalAmount) : list;
        }
    }
}
