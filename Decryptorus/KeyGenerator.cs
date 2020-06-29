using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Decryptorus
{
    public class KeyGenerator
    {
        private int[] key = null;
        public int[] Key { get => key; set => key = value; }

        public int begin = 65;
        public int limit = 97;

        public int[] exclude = { 91, 92, 93, 94, 95, 96 };

        public string Initialize(int size = 4)
        {
            int[] newKey = new int[size];
            for (int i = 0; i <= newKey.Length - 1; i++)
            {
                newKey[i] = begin;
            }

            Key = newKey;

            return getKeyText(Key);
        }

        public string getKeyText(int[] keyToText)
        {
            var result = new StringBuilder();
            for (int i = 0; i <= keyToText.Length - 1; i++)
            {
                result.Append(Convert.ToChar(keyToText[i]));
            }

            return result.ToString();
        }

        public string IncrementKey()
        {
            if (Key == null)
            {
                return Initialize();
            }
            Key = IncrementChars(Key);
            return getKeyText(key);
        }

        private int[] IncrementChars(int[] key, int depth = 1)
        {
            int index = key.Length - depth;

            if (key[index] + 1 <= limit)
            {
                key[index]++;
                while (Array.IndexOf(exclude, key[index]) > -1)
                {
                    key[index]++;
                }
            }
            else if (index != 0)
            {
                key[index] = begin;
                key = IncrementChars(key, depth + 1);
            }

            return key;
        }
    }
}