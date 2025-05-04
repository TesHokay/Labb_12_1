using System;
using CarLibrary;
using Labb_12_1;

class Program
{
    static void Main()
    {
        DoublyLinkedList<Car> carList = new DoublyLinkedList<Car>();
        HashTableWithChaining<string, Car> carHashTable = new HashTableWithChaining<string, Car>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nГлавное меню:");
            Console.WriteLine("1. Работа с двунаправленным списком");
            Console.WriteLine("2. Работа с хеш-таблицей");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ListOperationsMenu(carList);
                    break;
                case "2":
                    HashTableOperationsMenu(carHashTable);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный ввод!");
                    break;
            }
        }
    }

    static void ListOperationsMenu(DoublyLinkedList<Car> list)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\nМеню работы со списком:");
            Console.WriteLine("1. Добавить случайные автомобили");
            Console.WriteLine("2. Показать список автомобилей");
            Console.WriteLine("3. Добавить элементы на нечетные позиции");
            Console.WriteLine("4. Удалить элементы начиная с заданного");
            Console.WriteLine("5. Клонировать список");
            Console.WriteLine("6. Очистить список");
            Console.WriteLine("7. Вернуться в главное меню");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddRandomCars(list);
                    break;
                case "2":
                    ShowCars(list);
                    break;
                case "3":
                    InsertAtOddPositions(list);
                    break;
                case "4":
                    RemoveFromElement(list);
                    break;
                case "5":
                    CloneList(list);
                    break;
                case "6":
                    ClearList(list);
                    break;
                case "7":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный ввод!");
                    break;
            }
        }
    }

    static void HashTableOperationsMenu(HashTableWithChaining<string, Car> hashTable)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("\nМеню работы с хеш-таблицей:");
            Console.WriteLine("1. Добавить случайные автомобили");
            Console.WriteLine("2. Показать хеш-таблицу");
            Console.WriteLine("3. Найти автомобиль по марке");
            Console.WriteLine("4. Удалить автомобиль по марке");
            Console.WriteLine("5. Показать добавление при переполнении");
            Console.WriteLine("6. Вернуться в главное меню");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddRandomCarsToHashTable(hashTable);
                    break;
                case "2":
                    ShowHashTable(hashTable);
                    break;
                case "3":
                    FindInHashTable(hashTable);
                    break;
                case "4":
                    RemoveFromHashTable(hashTable);
                    break;
                case "5":
                    DemonstrateOverflow(hashTable);
                    break;
                case "6":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный ввод!");
                    break;
            }
        }
    }

    static void AddRandomCarsToHashTable(HashTableWithChaining<string, Car> hashTable)
    {
        Console.Write("Сколько автомобилей добавить? ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                var car = DoublyLinkedList<Car>.GenerateRandomCar();
                string originalKey = car.Brand;
                string uniqueKey = originalKey;
                int attempt = 1;

                // Генерируем уникальный ключ, если такой уже существует
                while (hashTable.ContainsKey(uniqueKey))
                {
                    uniqueKey = $"{originalKey}_{attempt++}";
                }

                hashTable.Add(uniqueKey, car);
                Console.WriteLine($"Добавлен автомобиль: {uniqueKey}");
            }
        }
        else
        {
            Console.WriteLine("Некорректное количество!");
        }
    }

    static void ShowHashTable(HashTableWithChaining<string, Car> hashTable)
    {
        if (hashTable.Count == 0)
        {
            Console.WriteLine("Хеш-таблица пуста.");
        }
        else
        {
            Console.WriteLine("Содержимое хеш-таблицы:");
            hashTable.Print();
        }
    }

    static void FindInHashTable(HashTableWithChaining<string, Car> hashTable)
    {
        Console.Write("Введите марку автомобиля для поиска: ");
        string brand = Console.ReadLine();

        if (hashTable.TryGetValue(brand, out Car car))
        {
            Console.WriteLine("Найден автомобиль:");
            Console.WriteLine(car.Show());
        }
        else
        {
            Console.WriteLine($"Автомобиль с маркой {brand} не найден.");
        }
    }

    static void RemoveFromHashTable(HashTableWithChaining<string, Car> hashTable)
    {
        Console.Write("Введите марку автомобиля для удаления: ");
        string brand = Console.ReadLine();

        if (hashTable.Remove(brand))
        {
            Console.WriteLine($"Автомобиль {brand} удален из хеш-таблицы.");
        }
        else
        {
            Console.WriteLine($"Автомобиль с маркой {brand} не найден.");
        }
    }

    static void DemonstrateOverflow(HashTableWithChaining<string, Car> hashTable)
    {
        // Создаем маленькую таблицу для демонстрации
        var smallTable = new HashTableWithChaining<string, Car>(3);

        var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
        var car2 = new Car("Honda", 2019, "Blue", 22000, 170, 1400);
        var car3 = new Car("Ford", 2021, "Black", 27000, 190, 1600);
        var car4 = new Car("BMW", 2018, "White", 35000, 210, 1700);

        Console.WriteLine("\nДемонстрация работы метода цепочек:");

        Console.WriteLine("\nДобавляем Toyota:");
        smallTable.Add(car1.Brand, car1);
        smallTable.Print();

        Console.WriteLine("\nДобавляем Honda:");
        smallTable.Add(car2.Brand, car2);
        smallTable.Print();

        Console.WriteLine("\nДобавляем Ford:");
        smallTable.Add(car3.Brand, car3);
        smallTable.Print();

        Console.WriteLine("\nДобавляем BMW:");
        smallTable.Add(car4.Brand, car4);
        smallTable.Print();

        Console.WriteLine("\nВсе элементы успешно добавлены, несмотря на маленький размер таблицы.");
    }

    static void AddRandomCars(DoublyLinkedList<Car> list)
    {
        Console.Write("Сколько автомобилей добавить? ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            for (int i = 0; i < count; i++)
                list.Add(DoublyLinkedList<Car>.GenerateRandomCar());
            Console.WriteLine($"Добавлено {count} автомобилей.");
        }
        else
        {
            Console.WriteLine("Некорректное количество!");
        }
    }

    static void ShowCars(DoublyLinkedList<Car> list)
    {
        if (list.Count == 0)
            Console.WriteLine("Список пуст.");
        else
            list.Print();
    }

    static void InsertAtOddPositions(DoublyLinkedList<Car> list)
    {
        Console.Write("Сколько автомобилей добавить? ");
        if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
        {
            Car[] newCars = new Car[count];
            for (int i = 0; i < count; i++)
                newCars[i] = DoublyLinkedList<Car>.GenerateRandomCar();

            list.InsertAtOddPositions(newCars);
            Console.WriteLine($"Добавлено {count} автомобилей на нечетные позиции.");
        }
        else
        {
            Console.WriteLine("Некорректное количество!");
        }
    }

    static void RemoveFromElement(DoublyLinkedList<Car> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список пуст!");
            return;
        }

        Console.WriteLine("Текущий список:");
        list.Print();

        Console.Write("Введите марку автомобиля для удаления: ");
        string brand = Console.ReadLine();

        Car carToFind = new Car(brand, 0, "", 0, 0, 0);

        int beforeCount = list.Count;
        list.RemoveAllFromElement(carToFind);

        if (list.Count < beforeCount)
        {
            Console.WriteLine($"Удалены все автомобили начиная с {brand}.");
            Console.WriteLine("Обновленный список:");
            list.Print();
        }
        else
        {
            Console.WriteLine($"Автомобиль с маркой {brand} не найден.");
        }
    }

    static void CloneList(DoublyLinkedList<Car> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список пуст!");
            return;
        }

        var clonedList = (DoublyLinkedList<Car>)list.Clone();
        Console.WriteLine("Клонированный список:");
        clonedList.Print();
        clonedList.Clear();
    }

    static void ClearList(DoublyLinkedList<Car> list)
    {
        list.Clear();
        Console.WriteLine("Список очищен.");
    }
}