using System;
using System.Collections;
using System.Collections.Generic;
using CarLibrary;

namespace Labb_12_1
{
    // Обобщенная коллекция Stack на базе двунаправленного списка
    public class MyCollection<T> : IEnumerable<T>, ICollection<T> where T : ICloneable
    {
        private DoublyLinkedList<T> _list = new DoublyLinkedList<T>();

        // 1. Конструкторы
        public MyCollection() { }

        public MyCollection(int length, Func<T> generator)
        {
            for (int i = 0; i < length; i++)
            {
                Push(generator());
            }
        }

        public MyCollection(MyCollection<T> other)
        {
            foreach (var item in other)
            {
                Push((T)item.Clone());
            }
        }

        // Основные операции стека
        public void Push(T item)
        {
            _list.Add(item);
        }

        public T Pop()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Стек пуст");

            T item = _list.tail.Data;
            _list.RemoveAllFromElement(item);
            return item;
        }

        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Стек пуст");

            return _list.tail.Data;
        }

        // Реализация IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator(_list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class StackEnumerator : IEnumerator<T>
        {
            private DoublyNode<T> _current;
            private DoublyLinkedList<T> _list;

            public StackEnumerator(DoublyLinkedList<T> list)
            {
                _list = list;
                _current = null;
            }

            public T Current => _current.Data;

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = _list.head;
                }
                else
                {
                    _current = _current.Next;
                }

                return _current != null;
            }

            public void Reset()
            {
                _current = null;
            }
        }

        // Реализация ICollection<T>
        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            Push(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            foreach (var element in _list)
            {
                if (element.Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Недостаточно места в целевом массиве");

            int i = arrayIndex;
            foreach (var item in _list)
            {
                array[i++] = item;
            }
        }

        public bool Remove(T item)
        {
            int oldCount = _list.Count;
            _list.RemoveAllFromElement(item);
            return _list.Count < oldCount;
        }
    }
}