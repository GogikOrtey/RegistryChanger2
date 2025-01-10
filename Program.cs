using System;
using Microsoft.Win32;

Add1 Add1 = new Add1();
//Add1.RegistryAdder2("123", "MyValue_2");

Add1.RegistryAdder_readKeyValue("MyValue_2");
Add1.RegistryAdder_addOrRecreateNewValue("MyValue_3", "Новое значение 1");
Add1.RegistryAdder_readKeyValue("MyValue_3");
Add1.RegistryAdder_addOrRecreateNewValue("MyValue_3", "Новое значение 2");
Add1.RegistryAdder_readKeyValue("MyValue_3");

public class Add1
{
    // Вторая, основная версия метода
    public void RegistryAdder2(string inputString, string valueName = "MyValue")
    {

    }

    // Метод, который создаёт новый, или перезаписывает существующий ключ
    // Метод принимает: Имя ключа, Значение ключа и Путь к ключу (есть определение по умолчанию)
    public void RegistryAdder_addOrRecreateNewValue(string keyName, string keyValue, string keyPath = @"SOFTWARE\MyApp")
    {
        try
        {
            // Создание/открытие ключа
            RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath);

            // Запись значения
            key.SetValue(keyName, keyValue);
            //Console.WriteLine($"\nЗначение '{keyName}' записано в реестр.");
            Console.WriteLine("\nЗначение ключа записано в реестр:");
            Console.WriteLine(keyName + " = " + keyValue);

            // Закрытие ключа
            key.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"При создании или перезаписи нового ключа по пути {keyPath}, с именем {keyName}, произошла ошибка: {ex.Message}");
        }
    }

    // Метод, который ищет ключ по пути и его имени, и возвращает значение ключа, а также печатает его в консоли
    // Обработка ошибок производится через Console.WriteLine
    // Метод принимает: Имя ключа и Путь к ключу (есть определение по умолчанию)
    public string RegistryAdder_readKeyValue(string keyName, string keyPath = @"SOFTWARE\MyApp")
    {
        string keyValue = "";

        try
        {
            // Открытие ключа реестра
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    // Чтение значения
                    object value = key.GetValue(keyName);
                    if (value != null)
                    {
                        keyValue = value.ToString();
                    }
                    else
                    {
                        Console.WriteLine($"Значение '{keyName}' не найдено.");
                    }
                }
                else
                {
                    Console.WriteLine($"Ключ '{keyPath}' не найден.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"При попытке чтения ключа {keyPath}, с именем {keyName}, произошла необработанная ошибка: {ex.Message}");
        }

        Console.WriteLine("\nЗначение ключа успешно получено:");
        Console.WriteLine(keyName + " = " + keyValue);

        return keyValue;
    }



    //// Первая версия метода
    //public void RegistryAdder1(string keyValue, string keyName = "MyValue")
    //{
    //    // keyPath - путь к ключу 
    //    // keyName - имя ключа 
    //    // Если не передано в функцию - то по умолчанию "MyValue"

    //    // Путь к ключу реестра
    //    string keyPath = @"SOFTWARE\MyApp";

    //    try
    //    {
    //        // Создание/открытие ключа
    //        RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath);

    //        // Запись значения
    //        key.SetValue(keyName, keyValue);
    //        Console.WriteLine($"Значение '{keyName}' записано в реестр.");

    //        // Чтение значения
    //        object value = key.GetValue(keyName);
    //        Console.WriteLine($"Значение '{keyName}' из реестра: {value}");

    //        // Закрытие ключа
    //        key.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Произошла ошибка: {ex.Message}");
    //    }
    //}
}
