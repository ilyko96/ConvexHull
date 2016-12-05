using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создать объект класса, инициирующего событие
            Deystvie md = new Deystvie();
            //Подписаться  на событие
            md.myEvent += MySum.Sum;
            md.myEvent += MyPrz.Prz;
            md.myEvent += MyDel.Del;

            // Вызвать метод, который приведет к запуску вашего события 
            //  на основе вашего же делегата
            md.Zapusk(4, 5);

            Console.ReadKey();
        }

    }

    public delegate int DeystvieDelegate(int x, int y);
    public class Deystvie
    {
        public event DeystvieDelegate myEvent;
        // Объявляем Событие типа делегата: 
        //public event <НазваниеДелегата> <НазваниеСобытия>; 

        public int Zapusk(int x, int y)
        {
            // запускаем событие 
            return myEvent(x, y);
        }
    }
    public class MySum
    {
        public static int Sum(int x, int y)
        {
            Console.WriteLine(x + y);
            return x + y;
        }
    }

    public class MyPrz
    {
        public static int Prz(int x, int y)
        {
            Console.WriteLine(x * y);
            return x * y;
        }
    }

    public class MyDel
    {
        public static int Del(int x, int y)
        {
            Console.WriteLine(x / y);
            return x / y;
        }
    }

}
