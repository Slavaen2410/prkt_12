using System;

// Делегат для события изменения свойства
public delegate void PropertyeventHandler(object sender, PropertyEventArgs e);

// Класс аргументов для события изменения свойства
public class PropertyEventArgs : EventArgs
{
    public string PropertyName { get; }

    public PropertyEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
}

// Интерфейс события изменения свойства
public interface iPropertyChanged
{
    event PropertyeventHandler PropertyChanged;
}

// Класс, реализующий интерфейс и содержащий свойства
public class MyClass : iPropertyChanged
{
    private string _myProperty;

    public string MyProperty
    {
        get { return _myProperty; }
        set
        {
            if (_myProperty != value)
            {
                _myProperty = value;
                OnPropertychanged(nameof(MyProperty));
            }
        }
    }

    // Событие изменения свойства
    public event PropertyeventHandler PropertyChanged;

    // Метод для вызова события изменения свойства
    protected virtual void OnPropertychanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
    }
}

class Program
{
    static void Main()
    {
        // Создаем объект класса
        MyClass myObject = new MyClass();

        // Подписываемся на событие изменения свойства
        myObject.PropertyChanged += MyHandler;

        // Изменяем свойство (это вызовет событие)
        myObject.MyProperty = "Новое значение";
    }

    static void MyHandler(object sender, PropertyEventArgs e)
    {
        Console.WriteLine($"Свойство {e.PropertyName} было изменено на {((MyClass)sender).MyProperty}.");
    }
}
