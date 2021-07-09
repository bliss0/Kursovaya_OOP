using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;



namespace KR
{



    abstract public class NumericSeries
    {
        protected double startValue;//начальное значение ряда
        protected double[] series;//сам ряд 
     
        public NumericSeries()//конструктор для ввода начального значения
        {
            Console.WriteLine("Введите начальное значение");
            while (!double.TryParse(Console.ReadLine(), out startValue))
                Console.WriteLine("Некорректность ввода");

        }
        abstract public double SumOfConvergence();//сумма сходящегося ряда
        abstract public double[] HelpNumber(int k);//подсчет элемента числового ряда(для каждого свое)

        public void Output(double[] mas)//вывод числового ряда
        {
            Console.WriteLine("Полученный числовой ряд");
            for (int i = 0; i < mas.Length; i++)
                Console.Write(mas[i] + "\t");
            Console.WriteLine("\n\n");
        }
        
        public double[] MemberOfSeries()//числовой ряд из к элементов
        {
            int k;
            Console.WriteLine("Введите количество элементов ряда");
            while (!int.TryParse(Console.ReadLine(), out k) || k < 1)
                Console.WriteLine("Пользователь ввел символ или неудовлетворяющее кол-во (кол-во < 1)");

            series = HelpNumber(k);
            return series;
        }
        public double[] MemberOfSeriesFrom()//числовой ряд с индекстами от к до k1 
        {
            int k1 = 0;
            int k = 0;

            Console.WriteLine("Введите индексы начала и конца искомого ряда поочереди (индексы считаются с 0)");
            Console.WriteLine("1 индекс меньше 2 индекса и  индекс от 0 ");
            while (!int.TryParse(Console.ReadLine(), out k) || k < 0)
                Console.WriteLine("Пользователь ввел символ или неудовлетворяющий индекс ");

            while (!int.TryParse(Console.ReadLine(), out k1) || k >= k1)
                Console.WriteLine("Пользователь ввел символ или неудовлетворяющий индекс ");

            double[] exrs = new double[k1 - k + 1];//вспомогательный массив 
            int helpc = 0;
            double[] helpmas = HelpNumber(k1 + 1);

            for (int i = k; i < helpmas.Length; i++)
            { exrs[helpc++] = helpmas[i]; }

            return exrs;
        }
        public double NumberWithNumber()//элемент числового ряда с индексом
        {

            int k;
            Console.WriteLine("Введите индекс элемента");
            Console.WriteLine("Индексы от 0 ");

            while (!int.TryParse(Console.ReadLine(), out k) || k < 0)
                Console.WriteLine("Пользователь ввел символ или неудовлетворяющий индекс ");

            double[] helpmas = HelpNumber(k + 5);
            return helpmas[k];

        }


        public double SumWithNumbers()//сумма двух элементов числового ряда
        {
            Console.WriteLine("Введите поочередно индексы элементов, сумму которых хотите получить");
            double sum = NumberWithNumber() + NumberWithNumber();
            return sum;
        }
        

        public void Restart()
        {
            Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }

    }
    public class Fibonache : NumericSeries
    {

        public override double[] HelpNumber(int k)
        {
            double[] help = new double[k];


            help[0] = help[1] = startValue;

            for (int i = 2; i < k; i++)
                help[i] = help[i - 1] + help[i - 2];
            return help;
        }

        public override double SumOfConvergence()
        {
            return 0;
        }

    }
    public class Pow2PrevNum : NumericSeries
    {

        public override double[] HelpNumber(int k)
        {
            double[] help = new double[k];


            help[0] = startValue;

            for (int i = 1; i < k; i++)
                help[i] = Math.Pow(help[i - 1], 2);
            return help;
        }
        public override double SumOfConvergence()
        {
            return 0;
        }

    }

    public class Exponent : NumericSeries
    {


        public int Factorial(int numb)
        {
            int res = 1;
            for (int i = numb; i > 1; i--)
                res *= i;
            return res;
        }

        public override double[] HelpNumber(int k)
        {
            double[] help = new double[k];


            help[1] = startValue;
            help[0] = 1;
            for (int i = 2; i < k; i++)
                help[i] = Math.Pow(help[i - 1], i) / Factorial(i);
            return help;
        }
        public override double SumOfConvergence()
        {
            return 0;
        }

    }

    public class GarmnPow2 : NumericSeries
    {
        public override double[] HelpNumber(int k)
        {
            double[] help = new double[k];


            help[0] = startValue;
            for (int i = 1; i < k; i++)
                help[i] = 1 / Math.Pow(i, 2);
            return help;
        }
        public override double SumOfConvergence()//сумма сходящегося ряда
        {
            Console.WriteLine("Введите кол-во элементов ряда, сумму которого хотите посчитать");
            int k;
            while (!int.TryParse(Console.ReadLine(), out k) || k < 0)
                Console.WriteLine("Пользователь ввел символ или неудовлетворяющий индекс ");
            double sum = 0;
            double[] helpmas = HelpNumber(k);
            for (int i = 0; i < helpmas.Length; i++)
                sum += helpmas[i];
            return sum;
        }



    }
    static class Program
    {

        public static void Swich(NumericSeries ob1)
        {
            int c = -1;
            while (c != 0)
            {
                Console.WriteLine("Выберите действия над рядом\n" +
                "1.Посчтитать первые к членов ряда\n" +
                                 "2.Посчитать члены ряда начиная с какого-то и заканчивая каким-то\n" +
                                 "3.Посчитать элемент с индексом\n" +
                                 "4.Посчитать сумму 2 элементов ряда\n" +
                                 "5.Сумма сходящегося ряда из k членов\n" +
                                 "6.Рестарт программы\n" +
                "0.Закрыть программу");



                while (!int.TryParse(Console.ReadLine(), out c) || c < 0 || c > 6)
                    Console.WriteLine("Пользователь ввел символ или неудовлетворяющий номер ");


                switch (c)
                {

                    case 1:
                        ob1.Output(ob1.MemberOfSeries());
                        break;
                    case 2:
                        ob1.Output(ob1.MemberOfSeriesFrom());
                        break;
                    case 3:
                        Console.WriteLine("Элемент под индексом = " + ob1.NumberWithNumber() + "\n");
                        break;
                    case 4:
                        Console.WriteLine("Сумма двух чисел последовательности = " + ob1.SumWithNumbers() + "\n");
                        break;
                    case 5:
                        if (ob1 is GarmnPow2)
                            Console.WriteLine("Сумма сходящегося ряда = " + ob1.SumOfConvergence() + "\n");
                        else Console.WriteLine("Ряд не является сходящимся \n");
                        break;
                    case 6:
                        ob1.Restart();
                        break;
                }
            }



        }
        static void Main()
        {
            Console.WriteLine("Выберите предложенный ряд");
            Console.WriteLine("1.Ряд Фибоначче\n" +
            "2.Ряд квадратов\n" +
                            "3.Экспонненциальный ряд\n" +
            "4.Гармонический ряд\n");


            int a;
            while (!int.TryParse(Console.ReadLine(), out a) || a < 0 || a > 4)
                Console.WriteLine("Пользователь ввел символ или неудовлетворяющий номер ");


            NumericSeries ob1;


            switch (a)
            {
                case 1:
                    Console.WriteLine("Вы выбрали ряд Фибоначчи");
                    ob1 = new Fibonache();
                    Swich(ob1);
                    break;
                case 2:
                    Console.WriteLine("Вы выбрали ряд Квадратов предыдущих чисел");
                    ob1 = new Pow2PrevNum();
                    Swich(ob1);
                    break;
                case 3:
                    Console.WriteLine("Вы выбрали Экспоненциальный ряд");
                    ob1 = new Exponent();
                    Swich(ob1);
                    break;
                case 4:
                    Console.WriteLine("Вы выбрали Гармонический ряд");
                    ob1 = new GarmnPow2();
                    Swich(ob1);
                    break;



            }



        }

    }
}
