using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.InputWordActions
{
    public class Permutator
    {
        private HashSet<string> allPermutations;


        public Permutator()
        {
            allPermutations = new HashSet<string>();
        }


        public new HashSet<string> GetPermutations(string word)
        {
            GeneratePermutations(word.ToCharArray(), word.Count());
            allPermutations.Remove(word);
            return allPermutations;
        }

        private void GeneratePermutations(char[] arr, int size)
        {
            if (size == 1)
            {
                allPermutations.Add(new string(arr));
                return;
            }

            for (int i = 0; i < size; i++)
            {
                GeneratePermutations(arr, size - 1);

                if (size % 2 == 1)
                {
                    char temp = arr[0];
                    arr[0] = arr[size - 1];
                    arr[size - 1] = temp;
                }
                else
                {
                    char temp = arr[i];
                    arr[i] = arr[size - 1];
                    arr[size - 1] = temp;
                }
            }
        }
    }
}
