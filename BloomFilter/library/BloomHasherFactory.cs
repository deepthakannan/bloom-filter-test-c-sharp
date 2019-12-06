using System;
using System.Collections.Generic;

namespace BloomFilter
{
    public class BloomFilterHashFactory {
        public IEnumerable<IBloomHasher> GetBloomHashers() {
            yield return new ObjectHasher();
            yield return new SHA1Hasher();
        }
    }
}