using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

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

        public static string[] search(string wordToSearch,string[] dirs)
        {
            List<string> listItems= new List<string>();
            Regex match = new Regex(@wordToSearch);
        
            int count = 0;
            foreach( string dir in dirs)
            {
                
                string[] sortedfilesArray= Directory.GetFiles(dir, "*Sorted*");
                listItems=sortedfilesArray.ToList();
                foreach (string presentFile in sortedfilesArray)
                {
                    string fileContent = File.ReadAllText(presentFile);string[] fileContentArray;
                    using(StreamReader sr= new StreamReader(presentFile))
                    {
                        fileContentArray= sr.ReadToEnd().Split(',');
                    }
                
                    if (match.IsMatch(fileContent))
                    {
                        
                        
                        count = Count(wordToSearch, fileContentArray);
                        listItems.Add($"{wordToSearch}  found in {Path.GetFileName(presentFile)} {count}times");
                        //if (count > 1)
                        //{

                        //    listItems.Add(count.ToString());
                        //    if (listItems.ToString().Contains(Path.GetFileName(presentFile)))
                        //    {
                        //        continue;


                        //    }
                        //    else {
                        //        listItems.Add($"{wordToSearch}  found in {Path.GetFileName(presentFile)} {count}times");

                        //    }

                        //}

                        //else if (count == 1)
                        //{

                        //    listItems.Add($"{wordToSearch}  found in {Path.GetFileName(presentFile)}");

                        //    // listItems.Add("testing if this line is working");
                        //}

                    }
                   

                    else
                    {
                        listItems.Add(wordToSearch + " not found");
                        continue;
                    }

                }
            }



            return listItems.ToArray();

        }
       
    }
   
}