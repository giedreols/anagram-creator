﻿using Cli;

namespace BusinessLogic.AnagramActions
{
    public static class Validator
    {
        public static List<string> ValidateAmount(this List<string> list)
        {
            int totalAmount = new Configuration().TotalAmount;

            if (list.Count > totalAmount)
            {
                return list.GetRange(0, totalAmount);
            }
            return list;
        }
    }
}