using System;
using Microsoft.Win32;

Add1 Add1 = new Add1();
Add1.RegistryAdder1("123", "MyValue_2");


public class Add1
{
    public void RegistryAdder1(string inputString, string valueName = "MyValue")
    {
        // Путь к ключу реестра
        string keyPath = @"SOFTWARE\MyApp";

        // keyPath - путь к ключу
        // valueName - имя ключа
            // Если не передано в функцию - то по умолчанию "MyValue"

        // Создание/открытие ключа
        RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath);

        // Запись значения
        key.SetValue(valueName, inputString);
        Console.WriteLine($"Значение '{valueName}' записано в реестр.");

        // Чтение значения
        object value = key.GetValue(valueName);
        Console.WriteLine($"Значение '{valueName}' из реестра: {value}");

        // Закрытие ключа
        key.Close();
    }
}