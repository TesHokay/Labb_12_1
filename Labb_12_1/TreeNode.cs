using System;
using System.Collections;
using System.Collections.Generic;
using CarLibrary;

namespace Labb_12_1
{
    //Класс узла бинарного дерева
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public TreeNode(T data)
        {
            Data = data;
        }
    }

    //Класс бинарного дерева
    public class BinaryTree<T> where T : IComparable<T>
    {
        public TreeNode<T> Root { get; private set; }

        //Создание идеально сбалансированного дерева
        public void CreateBalancedTree(T[] elements)
        {
            Root = CreateBalancedTree(elements, 0, elements.Length - 1);
        }

        private TreeNode<T> CreateBalancedTree(T[] elements, int start, int end)
        {
            if (start > end) return null;

            int mid = (start + end) / 2;
            TreeNode<T> node = new TreeNode<T>(elements[mid])
            {
                Left = CreateBalancedTree(elements, start, mid - 1),
                Right = CreateBalancedTree(elements, mid + 1, end)
            };
            return node;
        }

        //Печать дерева по уровням
        // Печать дерева по уровням с полной информацией об объектах
        public void PrintByLevels()
        {
            if (Root == null)
            {
                Console.WriteLine("Дерево пусто");
                return;
            }

            Queue<(TreeNode<T>, int)> queue = new Queue<(TreeNode<T>, int)>();
            queue.Enqueue((Root, 0));

            while (queue.Count > 0)
            {
                var (current, level) = queue.Dequeue();

                // Выводим уровень и информацию об объекте
                Console.WriteLine($"Уровень {level}:");
                if (current.Data is Car car)
                {
                    Console.WriteLine(car.Show());
                }
                else
                {
                    Console.WriteLine(current.Data.ToString());
                }
                Console.WriteLine(); // Добавляем пустую строку для разделения

                if (current.Left != null)
                    queue.Enqueue((current.Left, level + 1));
                if (current.Right != null)
                    queue.Enqueue((current.Right, level + 1));
            }
        }

        //Поиск элемента с максимальной стоимостью (для Car)
        public T FindMaxValue()
        {
            if (Root == null) return default;

            T max = Root.Data;
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                TreeNode<T> current = stack.Pop();

                if (current.Data is Car currentCar && max is Car maxCar)
                {
                    if (currentCar.Price > maxCar.Price)
                        max = current.Data;
                }

                if (current.Right != null) stack.Push(current.Right);
                if (current.Left != null) stack.Push(current.Left);
            }

            return max;
        }

        //Преобразование в дерево поиска
        public BinaryTree<T> ToBinarySearchTree()
        {
            List<T> elements = new List<T>();
            InOrderTraversal(Root, elements);
            elements.Sort();

            BinaryTree<T> bst = new BinaryTree<T>();
            bst.CreateBalancedTree(elements.ToArray());
            return bst;
        }

        private void InOrderTraversal(TreeNode<T> node, List<T> elements)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.Right, elements);
        }

        private TreeNode<T> Remove(TreeNode<T> node, T key)
        {
            if (node == null) return null;

            int compareResult = key.CompareTo(node.Data);
            if (compareResult < 0)
                node.Left = Remove(node.Left, key);
            else if (compareResult > 0)
                node.Right = Remove(node.Right, key);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                node.Data = MinValue(node.Right);
                node.Right = Remove(node.Right, node.Data);
            }
            return node;
        }

        private T MinValue(TreeNode<T> node)
        {
            T min = node.Data;
            while (node.Left != null)
            {
                min = node.Left.Data;
                node = node.Left;
            }
            return min;
        }

        //Удаление дерева из памяти
        public void Clear()
        {
            Root = null;
        }
    }
}
