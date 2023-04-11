using Contracts.Models;

namespace BusinessLogic.InputWordActions
{
    public class Permutator
    {
        private HashSet<string> allPermutations;

        public Permutator()
        {
            allPermutations = new HashSet<string>();
        }

        public InputWord GeneratePermutations(InputWord word)
        {
            Permute(word.MainForm.ToCharArray(), word.MainForm.Length);
            allPermutations.Remove(word.MainForm);
            word.Permutations = allPermutations;
            return word;
        }

        private void Permute(char[] arr, int size)
        {
            if (size == 1)
            {
                allPermutations.Add(new string(arr));
                return;
            }

            for (int i = 0; i < size; i++)
            {
                Permute(arr, size - 1);

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

