using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using CarLibrary;
using System.Collections.Generic;

namespace Labb_12_1.Tests
{
    [TestClass]
    public class MyCollectionTests
    {
        [TestMethod]
        public void Constructor_Default_ShouldCreateEmptyCollection()
        {
            // Arrange & Act
            var collection = new MyCollection<Car>();

            // Assert
            Assert.AreEqual(0, collection.Count);
            Assert.IsFalse(collection.Any());
        }

        [TestMethod]
        public void Constructor_WithLengthAndGenerator_ShouldCreateCollectionWithSpecifiedLength()
        {
            // Arrange
            int expectedCount = 5;
            var random = new Random();

            // Act
            var collection = new MyCollection<Car>(expectedCount, () =>
                new Car($"Brand{random.Next()}", 2000 + random.Next(23), "Color", 10000 + random.Next(90000), 150 + random.Next(50), 1000 + random.Next(9000)));

            // Assert
            Assert.AreEqual(expectedCount, collection.Count);
        }

        [TestMethod]
        public void Push_ShouldAddItemToCollection()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new Car("Test", 2023, "Red", 30000, 180, 2000);

            // Act
            collection.Push(car);

            // Assert
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(car, collection.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyCollection_ShouldThrowException()
        {
            // Arrange
            var emptyCollection = new MyCollection<Car>();

            // Act
            emptyCollection.Pop();
        }

        [TestMethod]
        public void Pop_ShouldRemoveAndReturnLastAddedItem()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new Car("Car1", 2020, "Blue", 25000, 170, 1500);
            var car2 = new Car("Car2", 2021, "Red", 30000, 180, 1600);
            collection.Push(car1);
            collection.Push(car2);

            // Act
            var poppedItem = collection.Pop();

            // Assert
            Assert.AreEqual(car2, poppedItem);
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(car1, collection.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_EmptyCollection_ShouldThrowException()
        {
            // Arrange
            var emptyCollection = new MyCollection<Car>();

            // Act
            emptyCollection.Peek();
        }

        [TestMethod]
        public void Peek_ShouldReturnLastAddedItemWithoutRemoving()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new Car("Test", 2023, "Red", 30000, 180, 2000);
            collection.Push(car);

            // Act
            var peekedItem = collection.Peek();

            // Assert
            Assert.AreEqual(car, peekedItem);
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void Contains_ExistingItem_ShouldReturnTrue()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new Car("Test", 2023, "Red", 30000, 180, 2000);
            collection.Push(car);

            // Act & Assert
            Assert.IsTrue(collection.Contains(car));
        }

        [TestMethod]
        public void Contains_NonExistingItem_ShouldReturnFalse()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new Car("Test", 2023, "Red", 30000, 180, 2000);

            // Act & Assert
            Assert.IsFalse(collection.Contains(car));
        }

        [TestMethod]
        public void Remove_ExistingItem_ShouldReturnTrueAndRemoveItem()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new Car("Car1", 2020, "Blue", 25000, 170, 1500);
            var car2 = new Car("Car2", 2021, "Red", 30000, 180, 1600);
            collection.Push(car1);
            collection.Push(car2);

            // Act
            bool result = collection.Remove(car2);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, collection.Count);
            Assert.IsFalse(collection.Contains(car2));
        }

        [TestMethod]
        public void Remove_NonExistingItem_ShouldReturnFalse()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new Car("Car1", 2020, "Blue", 25000, 170, 1500);
            var car2 = new Car("Car2", 2021, "Red", 30000, 180, 1600);
            collection.Push(car1);

            // Act
            bool result = collection.Remove(car2);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void Clear_ShouldEmptyTheCollection()
        {
            // Arrange
            var collection = new MyCollection<Car>(3, () => DoublyLinkedList<Car>.GenerateRandomCar());

            // Act
            collection.Clear();

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void CopyTo_ShouldCopyElementsToArray()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new Car("Car1", 2020, "Blue", 25000, 170, 1500);
            var car2 = new Car("Car2", 2021, "Red", 30000, 180, 1600);
            collection.Push(car1);
            collection.Push(car2);
            var array = new Car[3];

            // Act
            collection.CopyTo(array, 1);

            // Assert
            Assert.IsNull(array[0]);
            Assert.AreEqual(car1, array[1]);
            Assert.AreEqual(car2, array[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ShouldThrowException()
        {
            // Arrange
            var collection = new MyCollection<Car>();

            // Act
            collection.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_NegativeIndex_ShouldThrowException()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var array = new Car[1];

            // Act
            collection.CopyTo(array, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_ArrayTooSmall_ShouldThrowException()
        {
            // Arrange
            var collection = new MyCollection<Car>(2, () => DoublyLinkedList<Car>.GenerateRandomCar());
            var array = new Car[1];

            // Act
            collection.CopyTo(array, 0);
        }

        [TestMethod]
        public void GetEnumerator_ShouldEnumerateAllItems()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new Car("Car1", 2020, "Blue", 25000, 170, 1500);
            var car2 = new Car("Car2", 2021, "Red", 30000, 180, 1600);
            collection.Push(car1);
            collection.Push(car2);
            var enumeratedItems = new List<Car>();

            // Act
            foreach (var car in collection)
            {
                enumeratedItems.Add(car);
            }

            // Assert
            Assert.AreEqual(2, enumeratedItems.Count);
            Assert.AreEqual(car1, enumeratedItems[0]);
            Assert.AreEqual(car2, enumeratedItems[1]);
        }
    }
}
