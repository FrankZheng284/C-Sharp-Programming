using System;
using System.Reflection;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // 获取Person类的Type对象
        Type personType = typeof(Person);

        // 获取Person类的构造方法
        ConstructorInfo constructor = personType.GetConstructor(new Type[] { typeof(string), typeof(int) });

        if (constructor != null)
        {
            // 使用构造方法创建Person类的实例
            object personInstance = constructor.Invoke(new object[] { "John Doe", 30 });

            // 显示Person实例的信息
            MethodInfo displayInfoMethod = personType.GetMethod("DisplayInfo");
            if (displayInfoMethod != null)
            {
                displayInfoMethod.Invoke(personInstance, null);
            }
        }
    }
}
  