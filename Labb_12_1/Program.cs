using System;
using CarLibrary;
using Labb_12_1;

class Program
{
    static void Main()
    {
        DoublyLinkedList<Car> carList = new DoublyLinkedList<Car>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить случайные автомобили");
            Console.WriteLine("2. Показать список автомобилей");
            Console.WriteLine("3. Добавить элементы на нечетные позиции");
            Console.WriteLine("4. Удалить элементы начиная с заданного");
            Console.WriteLine("5. Клонировать список");
            Console.WriteLine("6. Очистить список");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddRandomCars(carList);
                    break;
                case "2":
                    ShowCars(carList);
                    break;
                case "3":
                    InsertAtOddPositions(carList);
                    break;
                case "4":
                    RemoveFromElement(carList);
                    break;
                case "5":
                    CloneList(carList);
                    break;
                case "6":
                    ClearList(carList);
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный ввод!");
                    break;
            }
        }
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

        // Создаем временный объект только с маркой для сравнения
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