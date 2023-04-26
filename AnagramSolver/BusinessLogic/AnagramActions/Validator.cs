
namespace BusinessLogic.AnagramActions
{
    public static class Validator
    {
        public static List<T> ValidateAmount<T>(this List<T> list, int totalAmount)
        {
            return list.Count > totalAmount ? list.GetRange(0, totalAmount) : list;
        }
    }
}
