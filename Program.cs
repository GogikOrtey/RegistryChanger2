using System;
using Microsoft.Win32;

Add1 Add1 = new Add1();

Add1.RegistryAdder_readKeyValue("MyValue_2");
Add1.RegistryAdder_addOrRecreateNewValue("MyValue_3", "Новое значение 1");
Add1.RegistryAdder_readKeyValue("MyValue_3");
Add1.RegistryAdder_addOrRecreateNewValue("MyValue_3", "Новое значение 2");
Add1.RegistryAdder_readKeyValue("MyValue_3");

public class Add1
{
    // Этот клас реализует 2 процедуры:
    //
    // 1. Создание нового ключа в реестре
    // RegistryAdder_addOrRecreateNewValue(имя ключа, значение ключа, путь к ключу)
    //
    // 2. Чтение значения ключа по его имени
    // RegistryAdder_readKeyValue(имя ключа, путь к ключу)
    // Эта процедура возвращает значение, а также выводит его в консоль


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
}
