using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BloomFilter
{
    public class Program
    {
        static void Main(string[] args)
        {
            var words = File.ReadLines("./wordlist.txt").ToList();
            TestBloomFilter(words, new BloomFilter(words.Count), words, "Insert All words and Check all words");
            TestBloomFilter(words.Take(words.Count / 2).ToList(), new BloomFilter(words.Count), words.Skip(words.Count / 2).ToList(), "Insert first half of all words and Check with remaining half of all words. BitArraySize = 10 times n");
            TestBloomFilter(words.Take(words.Count / 2).ToList(), new BloomFilter(words.Count / 10), words.Skip(words.Count / 2).ToList(), "Insert first half of all words and Check with remaining half of all words. BitArraySize = 1 times n");
            TestBloomFilter(words.Take(words.Count / 2).ToList(), new BloomFilter(words.Count / 20), words.Skip(words.Count / 2).ToList(), "Insert first half of all words and Check with remaining half of all words. BitArraySize = n / 2");
        }

        static void TestBloomFilter(IList<string> wordsToInsert, IBloomFilter filter, IList<string> wordsTocheck, string test) { 
            HashSet<string> wordsAddedToFilter = new HashSet<string>();
            foreach(var word in wordsToInsert)
            {
                filter.Insert(word);
                wordsAddedToFilter.Add(word);
            }

            int falsePositives = 0;
            int totalChecked = 0;
            int falseNegatives = 0;
            foreach(var word in wordsTocheck)
            {
                totalChecked++;
                
                if(filter.Contains(word))
                {
                    if(!wordsAddedToFilter.Contains(word))
                    {
                        // Console.WriteLine($"{word} is false positive");
                        falsePositives++;
                    }
                }
                else if(wordsAddedToFilter.Contains(word))
                {
                    falseNegatives++;
                }
            }
            Console.WriteLine($"{falsePositives} falsePositives and {falseNegatives} falseNegatives found in {totalChecked} entries. Test: [{test}]");
        }

        
    }
}
