using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectriesViewer
{
    public class Sort
    {
        public static int Count(string word, string[] words)
        {
            // finding the index of one of the items in the array
            int itemIndex = Array.BinarySearch(words, word);

            // iterate backwards until we find the first match
            if (itemIndex > 0)
                while (itemIndex > 0 && words[itemIndex] == word)
                    itemIndex--;

            // now itemIndex is one item before the start of the words
            int count = 0;
            while (itemIndex < words.Length && itemIndex >= 0)
            {
                if (words[itemIndex] == word)
                    count++;

                itemIndex++;

                if (itemIndex < words.Length)
                    if (words[itemIndex] != word)
                        break;
            }

            return count;
        }

    }
}