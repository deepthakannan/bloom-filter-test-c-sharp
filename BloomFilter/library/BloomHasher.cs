using System;
using System.Security.Cryptography;
using System.Text;

namespace BloomFilter
{
    public interface IBloomHasher
    {
        long Hash(object data, long maxHash);
    }

    public class ObjectHasher : IBloomHasher
    {
        public long Hash(object data, long maxHash)
        {
            int origHash = data.GetHashCode();
            return Math.Abs(origHash)%maxHash;
        }
    }

    public class SHA1Hasher : IBloomHasher
    {
        private HashAlgorithm hasher;

        public SHA1Hasher()
        {
            hasher = new SHA1CryptoServiceProvider();
        }

        public long Hash(object data, long maxHash)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(PadInputIfNecessary(data.ToString()));
            var origHash = BitConverter.ToInt32(bytes, 0);
            return Math.Abs(origHash)%maxHash;
        }

        string PadInputIfNecessary(string input) {
            if (input == null) {
                return GetPaddding(0);
            }

            if (input.Length < 4) {
                return input + GetPaddding(input.Length);
            }

            return input;
        }

        string GetPaddding(int inputLength) {
            switch(inputLength) {
                case 0:
                    return "XXXX";
                case 1:
                    return "XXX";
                case 2:
                    return "XX";
                case 3:
                    return "X";
                default:
                    return "";
            }
        }
    }

}