using CarLibrary;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Labb_12_1
{
    public class DoublyLinkedList<T> : ICloneable, IEnumerable<T> where T : ICloneable
    {
        public DoublyNode<T> head;
        public DoublyNode<T> tail;
        public int count;
        public static Random random = new Random();

        public int Count => count;

        public void Add(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);

            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }

        public void InsertAtOddPositions(T[] elements)
        {
            int currentPosition = 1;
            DoublyNode<T> current = head;

            for (int i = 0; i < elements.Length; i++)
            {
                while (current != null && currentPosition < (i * 2 + 1))
                {
                    current = current.Next;
                    currentPosition++;
                }

                if (current == null) break;

                DoublyNode<T> newNode = new DoublyNode<T>(elements[i]);
                newNode.Next = current.Next;
                newNode.Previous = current;

                if (current.Next != null)
                    current.Next.Previous = newNode;

                current.Next = newNode;

                if (newNode.Next == null)
                    tail = newNode;

                count++;
                current = newNode.Next;
                currentPosition++;
            }
        }

        public void RemoveAllFromElement(T data)
        {
            if (head == null) return;

            DoublyNode<T> current = head;
            DoublyNode<T> startNode = null;

            // 1. Находим узел для начала удаления
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    startNode = current;
                    break;
                }
                current = current.Next;
            }

            if (startNode == null) return;

            // 2. Обновляем связи в списке
            if (startNode == head)
            {
                // Если удаляем с головы - весь список очищается
                head = null;
                tail = null;
            }
            else
            {
                // Иначе связываем предыдущий узел с null
                startNode.Previous.Next = null;
                tail = startNode.Previous;
            }

            // 3. Удаляем все узлы начиная с startNode
            current = startNode;
            while (current != null)
            {
                DoublyNode<T> next = current.Next;
                // Разрываем связи узла
                current.Previous = null;
                current.Next = null;
                count--;
                current = next;
            }
        }

        public void Print()
        {
            foreach (var item in this)
            {
                if (item is Car car)
                    Console.WriteLine(car.Show());
                else
                    Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }

        public void Clear()
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                DoublyNode<T> next = current.Next;
                current.Next = null;
                current.Previous = null;
                current = next;
            }
            head = null;
            tail = null;
            count = 0;
        }

        public object Clone()
        {
            DoublyLinkedList<T> newList = new DoublyLinkedList<T>();
            foreach (var item in this)
                newList.Add((T)item.Clone());
            return newList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static Car GenerateRandomCar()
        {
            string[] brands = { "Toyota", "Honda", "Ford", "BMW", "Mercedes" };
            string[] colors = { "Red", "Blue", "Black", "White", "Silver" };

            int type = random.Next(3);
            switch (type)
            {
                case 0:
                    return new PassengerCar(
                        brands[random.Next(brands.Length)],
                        random.Next(2000, 2023),
                        colors[random.Next(colors.Length)],
                        random.Next(10000, 100000),
                        random.Next(160, 250),
                        random.Next(1000, 2000),
                        random.Next(2, 6),
                        random.Next(5, 15));
                case 1:
                    return new SUV(
                        brands[random.Next(brands.Length)],
                        random.Next(2000, 2023),
                        colors[random.Next(colors.Length)],
                        random.Next(20000, 150000),
                        random.Next(140, 220),
                        random.Next(1500, 3000),
                        random.Next(2, 6),
                        random.Next(5, 15),
                        random.Next(2) == 0 ? "Yes" : "No",
                        "Off-road");
                default:
                    return new Truck(
                        brands[random.Next(brands.Length)],
                        random.Next(2000, 2023),
                        colors[random.Next(colors.Length)],
                        random.Next(30000, 200000),
                        random.Next(120, 180),
                        random.Next(3000, 5000),
                        random.Next(1, 3),
                        random.Next(50, 150),
                        random.Next(80, 200));
            }
        }
    }
}
