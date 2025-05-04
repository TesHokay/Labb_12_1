using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CarLibrary;
using Labb_12_1;

namespace HashTableTests
{
    [TestClass]
    public class HashTableTest
    {
        [TestMethod]
        public void Constructor_InitializesEmptyTable()
        {
            // Arrange & Act
            var table = new HashTableWithChaining<string, Car>();

            // Assert
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void Add_NewItem_IncreasesCount()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();
            var car = new Car("Toyota", 2020, "Red", 25000, 180, 1500);

            // Act
            table.Add("car1", car);

            // Assert
            Assert.AreEqual(1, table.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_DuplicateKey_ThrowsException()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("Honda", 2019, "Blue", 22000, 170, 1400);

            // Act
            table.Add("car1", car1);
            table.Add("car1", car2);
        }

        [TestMethod]
        public void TryGetValue_ExistingKey_ReturnsTrueAndValue()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();
            var expectedCar = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            table.Add("car1", expectedCar);

            // Act
            bool result = table.TryGetValue("car1", out Car actualCar);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedCar.Brand, actualCar.Brand);
            Assert.AreEqual(expectedCar.Year, actualCar.Year);
        }

        [TestMethod]
        public void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();

            // Act
            bool result = table.TryGetValue("nonexistent", out Car actualCar);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(actualCar);
        }

        [TestMethod]
        public void Remove_ExistingItem_ReturnsTrueAndDecreasesCount()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();
            var car = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            table.Add("car1", car);

            // Act
            bool result = table.Remove("car1");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void Remove_NonExistingItem_ReturnsFalse()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();

            // Act
            bool result = table.Remove("nonexistent");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();
            var car = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            table.Add("car1", car);

            // Act
            bool result = table.ContainsKey("car1");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();

            // Act
            bool result = table.ContainsKey("nonexistent");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Count_ReflectsActualNumberOfItems()
        {
            // Arrange
            var table = new HashTableWithChaining<string, Car>();
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("Honda", 2019, "Blue", 22000, 170, 1400);

            // Act
            table.Add("car1", car1);
            table.Add("car2", car2);
            table.Remove("car1");

            // Assert
            Assert.AreEqual(1, table.Count);
        }

        [TestMethod]
        public void CollisionHandling_WorksCorrectly()
        {
            // Arrange - создаем маленькую таблицу для гарантии коллизий
            var smallTable = new HashTableWithChaining<string, Car>(2);
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("Honda", 2019, "Blue", 22000, 170, 1400);
            var car3 = new Car("Ford", 2021, "Black", 27000, 190, 1600);

            // Act
            smallTable.Add("key1", car1);
            smallTable.Add("key2", car2);
            smallTable.Add("key3", car3);

            // Assert
            Assert.AreEqual(3, smallTable.Count);
            Assert.IsTrue(smallTable.ContainsKey("key1"));
            Assert.IsTrue(smallTable.ContainsKey("key2"));
            Assert.IsTrue(smallTable.ContainsKey("key3"));
        }
    }
}