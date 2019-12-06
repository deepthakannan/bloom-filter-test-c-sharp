using System;
using System.Collections.Generic;

namespace BloomFilter
{
    public interface IBloomFilter
    {
        void Insert(string data);
        bool Contains(string data);
    }

    public class BloomFilter : IBloomFilter
    {
        private BloomFilterHashFactory factory = new BloomFilterHashFactory();
        IEnumerable<IBloomHasher> hashers = null;
        public BloomFilter(int maxItems = 1000)
        {
            hashers = factory.GetBloomHashers();
            bloomStore = new bool[maxItems * 10];
        }
        
        private bool[] bloomStore = null;
        public void Insert(string data)
        {
            foreach (var hasher in hashers)
            {
                var storeIndex = hasher.Hash(data, bloomStore.LongLength-1);
                bloomStore[storeIndex] = true;
            }
        }

        public bool Contains(string data)
        {
            foreach (var hasher in hashers)
            {
                var storeIndex = hasher.Hash(data, bloomStore.LongLength-1);
                var exists = bloomStore[storeIndex];
                if (!exists)
                {
                    return false;
                }
            }
            return true;
        }
    }
}