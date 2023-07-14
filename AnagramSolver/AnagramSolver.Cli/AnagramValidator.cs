namespace AnagramSolver.Cli
{
    [Obsolete("new implementation uses WebApp")]
    public static class AnagramValidator
    {
        public static IList<T> TrimIfTooManyItems<T>(this IList<T> list, int totalAmount)
        {
            List<T> tempList = list.ToList();
            return tempList.Count > totalAmount ? tempList.GetRange(0, totalAmount) : tempList;
        }
    }
}
