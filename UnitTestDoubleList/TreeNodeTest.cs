using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using CarLibrary;

namespace Labb_12_1.Tests
{
    [TestClass]
    public class TreeNodeTest
    {
        [TestMethod]
        public void CreateBalancedTree_WithEmptyArray_ShouldCreateEmptyTree()
        {
            // Arrange
            var tree = new BinaryTree<Car>();
            Car[] emptyArray = Array.Empty<Car>();

            // Act
            tree.CreateBalancedTree(emptyArray);

            // Assert
            Assert.IsNull(tree.Root);
        }

        [TestMethod]
        public void CreateBalancedTree_WithSingleElement_ShouldCreateTreeWithRootOnly()
        {
            // Arrange
            var tree = new BinaryTree<Car>();
            var car = new Car("Toyota", 2020, "Red", 25000, 180, 1500);

            // Act
            tree.CreateBalancedTree(new[] { car });

            // Assert
            Assert.IsNotNull(tree.Root);
            Assert.IsNull(tree.Root.Left);
            Assert.IsNull(tree.Root.Right);
            Assert.AreEqual(car, tree.Root.Data);
        }

        [TestMethod]
        public void CreateBalancedTree_WithMultipleElements_ShouldCreateBalancedTree()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            int[] elements = { 1, 2, 3, 4, 5, 6, 7 };

            // Act
            tree.CreateBalancedTree(elements);

            // Assert
            Assert.AreEqual(4, tree.Root.Data);
            Assert.AreEqual(2, tree.Root.Left.Data);
            Assert.AreEqual(6, tree.Root.Right.Data);
        }

        [TestMethod]
        public void FindMaxValue_WithCarObjects_ShouldReturnCarWithMaxPrice()
        {
            // Arrange
            var tree = new BinaryTree<Car>();
            var cars = new[]
            {
                new Car("Toyota", 2020, "Red", 25000, 180, 1500),
                new Car("Honda", 2019, "Blue", 22000, 170, 1400),
                new Car("BMW", 2021, "Black", 35000, 190, 1600)
            };
            tree.CreateBalancedTree(cars);

            // Act
            var maxCar = tree.FindMaxValue();

            // Assert
            Assert.AreEqual("BMW", maxCar.Brand);
            Assert.AreEqual(35000, maxCar.Price);
        }

        [TestMethod]
        public void FindMaxValue_WithEmptyTree_ShouldReturnDefault()
        {
            // Arrange
            var tree = new BinaryTree<Car>();

            // Act
            var result = tree.FindMaxValue();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ToBinarySearchTree_ShouldReturnSortedTree()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            int[] elements = { 5, 3, 7, 2, 4, 6, 8 };
            tree.CreateBalancedTree(elements);

            // Act
            var bst = tree.ToBinarySearchTree();

            // Assert
            Assert.IsTrue(IsBinarySearchTree(bst.Root, int.MinValue, int.MaxValue));
        }

        [TestMethod]
        public void Clear_ShouldEmptyTheTree()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            int[] elements = { 1, 2, 3 };
            tree.CreateBalancedTree(elements);

            // Act
            tree.Clear();

            // Assert
            Assert.IsNull(tree.Root);
        }

        [TestMethod]
        public void PrintByLevels_ShouldNotThrowWithEmptyTree()
        {
            // Arrange
            var tree = new BinaryTree<Car>();

            // Act & Assert (should not throw)
            tree.PrintByLevels();
        }

        // Helper methods for tree validation
        private bool IsBinarySearchTree(TreeNode<int> node, int min, int max)
        {
            if (node == null) return true;

            if (node.Data.CompareTo(min) < 0 || node.Data.CompareTo(max) > 0)
                return false;

            return IsBinarySearchTree(node.Left, min, node.Data - 1) &&
                   IsBinarySearchTree(node.Right, node.Data + 1, max);
        }

        private bool Contains(TreeNode<int> node, int value)
        {
            if (node == null) return false;
            if (node.Data == value) return true;
            return Contains(node.Left, value) || Contains(node.Right, value);
        }
    }
}