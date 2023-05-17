﻿namespace AnagramSolver.BusinessLogic.AnagramActions
{
	public static class AnagramValidator
	{
		public static List<T> TrimIfTooManyItems<T>(this List<T> list, int totalAmount)
		{
			return list.Count > totalAmount ? list.GetRange(0, totalAmount) : list;
		}
	}
}
