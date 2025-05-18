using System;
using System.Collections.Generic;
using CarLibrary;

namespace Labb_12_1
{
    public class HashTableWithChaining<TKey, TValue> where TValue : ICloneable
    {
        private const int DefaultCapacity = 10;
        private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;

        public int Count { get; private set; }

        public HashTableWithChaining(int capacity = DefaultCapacity)
        {
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
        }

        private int GetBucketIndex(TKey key)
        {
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode % buckets.Length);
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            var bucket = buckets[bucketIndex];

            if (bucket != null)
            {
                foreach (var pair in bucket)
                {
                    if (pair.Key.Equals(key))
                        return true;
                }
            }
            return false;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);

            if (buckets[bucketIndex] == null)
                buckets[bucketIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();

            var bucket = buckets[bucketIndex];
            foreach (var pair in bucket)
            {
                if (pair.Key.Equals(key))
                    throw new ArgumentException("Элемент с таким ключом уже существует");
            }

            bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
            Count++;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            var bucket = buckets[bucketIndex];

            if (bucket != null)
            {
                foreach (var pair in bucket)
                {
                    if (pair.Key.Equals(key))
                    {
                        value = pair.Value;
                        return true;
                    }   
                }
            }

            value = default;
            return false;
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int bucketIndex = GetBucketIndex(key);
            var bucket = buckets[bucketIndex];

            if (bucket != null)
            {
                var node = bucket.First;
                while (node != null)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        bucket.Remove(node);
                        Count--;
                        return true;
                    }
                    node = node.Next;
                }
            }

            return false;
        }

        public void Print()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null && buckets[i].Count > 0)
                {
                    foreach (var pair in buckets[i])
                    {
                        if (pair.Value is Car car)
                            Console.WriteLine($"[{pair.Key}]: {car.Show()}");
                        else
                            Console.WriteLine($"[{pair.Key}]: {pair.Value}");
                    }
                }
            }
        }
    }
}