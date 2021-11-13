using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static bool flag = true;
        static int menuNum = 0;
        static void Main(string[] args)
        {
            try
            {
                SendMessageFromSocket();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
        static void SendMessageFromSocket()
        {
            while (flag)
            {
                Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1213);

                socket.Connect(ip);

                switch (menuNum)
                {
                    case 0:
                        MenuTextMain();
                        MainMenuAction(socket);
                        break;
                    case 1:
                        MenuText0();
                        MenuAction(socket);
                        break;
                    case 2:
                        MenuText1();
                        MenuAction(socket);
                        break;
                    case 3:
                        MenuText2();
                        MenuAction(socket);
                        break;

                }

                // Выводим текст меню
                // Действия с меню
                Console.WriteLine();
                Console.Clear();

                // Освобождаем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket.Dispose();
            }
            Console.WriteLine("Программа завершена. Нажмите что-нибудь!");
            Console.ReadKey();

        }
        static void MenuTextMain()
        {
            Console.WriteLine("МЕНЮ");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("1) Разминка");
            Console.WriteLine("2) Первые задачи");
            Console.WriteLine("3) Вторые задачи");
            Console.WriteLine("0) Завершить работу");
            Console.WriteLine("-------------------------------------------------------------");
        }
        static void MenuText0()
        {
            Console.WriteLine("РАЗМИНКА");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("1) Ввести 3 числа, получить их квадраты");
            Console.WriteLine("2) Ввести 2 строки, получить их конкатенацию");
            Console.WriteLine("3) Команды серверу");
            Console.WriteLine("0) Назад");
            Console.WriteLine("-------------------------------------------------------------");
        }
        static void MenuText1()
        {
            Console.WriteLine("ПЕРВЫЕ ЗАДАЧИ");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("1) Задание 1. Ввести три числа, вывести наибольшее.");
            Console.WriteLine("2) Задание 2. Ввести три числа, вывести их произведение.");
            Console.WriteLine("3) Задание 3. Ввести три числа, вывести их квадраты.");
            Console.WriteLine("4) Задание 4. Ввести три числа, вывести их сумму.");
            Console.WriteLine("5) Задание 5. Ввести три числа, вывести их среднее, арифметическое.");
            Console.WriteLine("6) Задание 6. Вводить числа, пока не будет введен 0. Вернуть количество ненулевых чисел.");
            Console.WriteLine("7) Задание 7. Ввести три числа, вывести количество кратных трем");
            Console.WriteLine("8) Задание 8. Ввести число, вывести составляющие его цифры.");
            Console.WriteLine("9) Задание 9. Ввести 2 числа, вывести их произведение и сумму.");
            Console.WriteLine("10) Задание 10. Ввести одно число, вывести его столько же раз.");
            Console.WriteLine("11) Задание 11. Ввести два числа, вывеси делится ли второе на первое без остатка.");
            Console.WriteLine("12) Задание 12. Ввести 4 числа, вывести две суммы –первое + второе и третье + четвертое.");
            Console.WriteLine("13) Задание 13. Ввести 2 числа, если первое больше второго вернуть их сумму, если нет то их квадраты.");
            Console.WriteLine("14) Задание 14. Ввести 2 числа, вывести их разность, и если она меньше нуля, вернуть их сумму.");
            Console.WriteLine("0) Назад");
            Console.WriteLine("-------------------------------------------------------------");
        }
        static void MenuText2()
        {
            Console.WriteLine("ВТОРЫЕ ЗАДАЧИ");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("1) Задание 1. Ввести массив числе из Nэлементов, вывести массив их квадратов, и массив их кубов");
            Console.WriteLine("2) Задание 2. Ввести массив из N элементов, вывести массив четных (по номеру) элементов, вывести массив нечетных (по номеру)");
            Console.WriteLine("3) Задание 3. Ввести массив строк из Nстрок, ввести букву. Вернуть массив строк, в которых отсутствует введённая буква.");
            Console.WriteLine("4) Задание 4. Ввести квадратное уравнение (три числа), решить его на сервере и вернуть два корня, или один корень или «корней нет». На клиенте вычислений не проводить!");
            Console.WriteLine("5) Задание 5. Ввести массив из Nчисел, если их сумма больше их количества вернуть три максимальных, если нет вернуть квадраты элементов массива.");
            Console.WriteLine("6) Задание 6. Ввести массив из Nчисел и Mстрок. Вернуть массив строк, как их декартово произведение.");
            Console.WriteLine("7) Задание 7. Ввести массив N*M, передать его на сервер и вернуть обратно.");
            Console.WriteLine("8) Задание 8. Cгенерировать на сервере случайное число, передать на клиент. Ввести массив строк в размере этого числа, передать массив на сервер и вернуть конкатенацию этих строк.");
            Console.WriteLine("9) Задание 9. Ввести число, если оно равно 0, отправить на сервер массив из N строк и вернуть их конкатенацию, если не 0, отправить на сервер массив чисел и вернуть их среднее.");
            Console.WriteLine("10) Задание 10. Ввести строку. Отправить ее на сервер и получить массив из слов этой строки");
            Console.WriteLine("11) Задание 11. Ввести три числа, отправить на сервер. Если их сумма четная, клиент должен отправить строку и получить количество символов. Если нет, клиент отправляет массив из N символов и получает их сумму.");
            Console.WriteLine("12) Задание 12. Ввести строку, вывести количество букв и слов в ней и четные(по номеру) слова");
            Console.WriteLine("13) Задание 13. Сгенерировать случайное число R от 1 до 20, создать массив случайных чисел от -10 до 10 и отправить его на сервер, вывести количество ненулевых элементов");
            Console.WriteLine("0) Назад");
            Console.WriteLine("-------------------------------------------------------------");
        }
        static void MainMenuAction(Socket socket)
        {
            Console.Write("Введите номер пункта меню:  ");
            int typeOfCommand = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            if (typeOfCommand == 0)
                flag = false;
            else if (typeOfCommand >= 1 && typeOfCommand <= 3)
                menuNum = typeOfCommand;
        }
        static void MenuAction(Socket socket)
        {
            Console.Write("Введите номер пункта меню:  ");
            int typeOfCommand = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            switch (menuNum)
            {
                case 1:
                    switch (typeOfCommand)
                    {
                        case 1:
                            Training_1_Client(socket);
                            break;
                        case 2:
                            Training_2_Client(socket);
                            break;
                        case 3:
                            Training_3_Client(socket);
                            break;
                        case 0:
                            menuNum = 0;
                            break;
                    }
                    break;

                case 2:
                    switch (typeOfCommand)
                    {
                        case 1:
                            First_1_2_4_5_7_Client(socket, typeOfCommand);
                            break;
                        case 2:
                            First_1_2_4_5_7_Client(socket, typeOfCommand);
                            break;
                        case 3:
                            Training_1_Client(socket);
                            break;
                        case 4:
                            First_1_2_4_5_7_Client(socket, typeOfCommand);
                            break;
                        case 5:
                            First_1_2_4_5_7_Client(socket, typeOfCommand);
                            break;
                        case 6:
                            First_6_Client(socket);
                            break;
                        case 7:
                            First_1_2_4_5_7_Client(socket, typeOfCommand);
                            break;
                        case 8:
                            First_8_Client(socket);
                            break;
                        case 9:
                            First_9_Client(socket);
                            break;
                        case 10:
                            First_10_Client(socket);
                            break;
                        case 11:
                            First_11_Client(socket);
                            break;
                        case 12:
                            First_12_Client(socket);
                            break;
                        case 13:
                            First_13_Client(socket);
                            break;
                        case 14:
                            First_14_Client(socket);
                            break;
                        case 0:
                            menuNum = 0;
                            break;
                    }
                    break;

                case 3:
                    switch (typeOfCommand)
                    {
                        case 1:
                            Second_1_Client(socket);
                            break;
                        case 2:
                            Second_2_Client(socket);
                            break;
                        case 3:
                            Second_3_Client(socket);
                            break;
                        case 4:
                            Second_4_Client(socket);
                            break;
                        case 5:
                            Second_5_Client(socket);
                            break;
                        case 6:
                            Second_6_Client(socket);
                            break;
                        case 7:
                            Second_7_Client(socket);
                            break;
                        case 8:
                            Second_8_Client(socket);
                            break;
                        case 9:
                            Second_9_Client(socket);
                            break;
                        case 10:
                            Second_10_Client(socket);
                            break;
                        case 11:
                            Second_11_Client(socket);
                            break;
                        case 12:
                            Second_12_Client(socket);
                            break;
                        case 13:
                            Second_13_Client(socket);
                            break;
                        case 0:
                            menuNum = 0;
                            break;
                    }
                    break;
            }
        }
        static void Training_1_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 1);
            DataReceiveSend.SNumber(socket, 1);

            Console.Write("Введите 3 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[3] { Int32.Parse(input[0]), Int32.Parse(input[1]), Int32.Parse(input[2]) };

            DataReceiveSend.SData(socket, arr);

            DataReceiveSend.RData(socket, out arr);

            Console.Write("Квадраты этих чисел: " + arr[0] + ' ' + arr[1] + ' ' + arr[2]);
            Console.ReadLine();
        }
        static void Training_2_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 1);
            DataReceiveSend.SNumber(socket, 2);

            Console.Write("Введите первую строку:  ");
            string input1 = Console.ReadLine();
            Console.Write("Введите вторую строку:  ");
            string input2 = Console.ReadLine();

            DataReceiveSend.SString(socket, input1);
            DataReceiveSend.SString(socket, input2);

            string result = DataReceiveSend.RString(socket);

            Console.Write("Конкатенация данных строк: " + result);
            Console.ReadLine();
        }
        static void Training_3_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 1);
            DataReceiveSend.SNumber(socket, 3);

            int num = 0;
            while (num == 0)
            {
                Console.Write("Введите номер команды:  ");
                DataReceiveSend.SNumber(socket, Int32.Parse(Console.ReadLine()));
                num = DataReceiveSend.RNumber(socket);
            }

            if (num == -1)
                Console.Write("До первого нуля можно вводить только 1.");
            else
                Console.Write("Квадрат числа: " + num);
            Console.ReadLine();
        }
        static void First_1_2_4_5_7_Client(Socket socket, int action)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, action);

            Console.Write("Введите 3 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[3] { Int32.Parse(input[0]), Int32.Parse(input[1]), Int32.Parse(input[2]) };

            DataReceiveSend.SData(socket, arr);

            arr[0] = DataReceiveSend.RNumber(socket);

            switch (action)
            {
                case 1:
                    Console.Write("Наибольшее из них: " + arr[0]);
                    break;
                case 2:
                    Console.Write("Их произведение: " + arr[0]);
                    break;
                case 4:
                    Console.Write("Их сумма: " + arr[0]);
                    break;
                case 5:
                    Console.Write("Их среднее арифметическое: " + arr[0]);
                    break;
                case 7:
                    Console.Write("Кратных трем: " + arr[0]);
                    break;
            }
            Console.ReadLine();
        }
        static void First_6_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 6);

            int num = 1;
            while (num != 0)
            {
                Console.Write("Введите число:  ");
                num = Int32.Parse(Console.ReadLine());
                DataReceiveSend.SNumber(socket, num);
            }

            Console.Write("Количество ненулевых чисел: " + DataReceiveSend.RNumber(socket));
            Console.ReadLine();
        }
        static void First_8_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 8);

            Console.Write("Введите число:  ");
            int num = Int32.Parse(Console.ReadLine());

            DataReceiveSend.SNumber(socket, num);

            int[] res;
            DataReceiveSend.RData(socket, out res);

            Console.Write("Составляющи его цифры: " + res[0]);
            for (int i = 1; i < res.Length; ++i)
                Console.Write(", " + res[i]);
            Console.ReadLine();
        }
        static void First_9_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 9);

            Console.Write("Введите 2 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[2] { Int32.Parse(input[0]), Int32.Parse(input[1]) };

            DataReceiveSend.SData(socket, arr);

            DataReceiveSend.RData(socket, out arr);

            Console.WriteLine("Их произведение: " + arr[0]);
            Console.WriteLine("Их сумма: " + arr[1]);
            Console.ReadLine();
        }
        static void First_10_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 10);

            Console.Write("Введите число:  ");
            DataReceiveSend.SNumber(socket, Int32.Parse(Console.ReadLine()));

            Console.Write("Вывод:  ");
            int num = DataReceiveSend.RNumber(socket);
            while (num != 0)
            {
                Console.Write(num + " ");
                num = DataReceiveSend.RNumber(socket);
            }

            Console.WriteLine();
            Console.WriteLine("-----------Вывод закончен-----------");
            Console.ReadLine();
        }
        static void First_11_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 11);

            Console.Write("Введите 2 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[2] { Int32.Parse(input[0]), Int32.Parse(input[1]) };

            DataReceiveSend.SData(socket, arr);

            Console.WriteLine("Второе делится на первое без остатка: " + DataReceiveSend.RBool(socket));
            Console.ReadLine();
        }
        static void First_12_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 12);

            Console.Write("Введите 4 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[4] { Int32.Parse(input[0]), Int32.Parse(input[1]), Int32.Parse(input[2]), Int32.Parse(input[3]) };

            DataReceiveSend.SData(socket, arr);

            DataReceiveSend.RData(socket, out arr);

            Console.WriteLine("–первое + второе: " + arr[0]);
            Console.WriteLine("третье + четвертое: " + arr[1]);
            Console.ReadLine();
        }
        static void First_13_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 13);

            Console.Write("Введите 2 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[2] { Int32.Parse(input[0]), Int32.Parse(input[1]) };

            DataReceiveSend.SData(socket, arr);

            int[] res;
            DataReceiveSend.RData(socket, out res);

            if (res.Length == 1)
                Console.WriteLine("Получена их сумма: " + res[0]);
            else
                Console.WriteLine("Получены их квадраты: " + res[0] + " " + res[1]);

            Console.ReadLine();
        }
        static void First_14_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 2);
            DataReceiveSend.SNumber(socket, 14);

            Console.Write("Введите 2 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[2] { Int32.Parse(input[0]), Int32.Parse(input[1]) };

            DataReceiveSend.SData(socket, arr);

            int res = DataReceiveSend.RNumber(socket);

            Console.WriteLine("Получена их разница: " + res);
            if (res < 0)
                Console.WriteLine("Получена их сумма: " + DataReceiveSend.RNumber(socket));

            Console.ReadLine();
        }
        static void Second_1_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 1);

            Console.Write("Введите массив чисел через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');

            int[] arr = input.Select(str => Int32.Parse(str)).ToArray();

            DataReceiveSend.SData(socket, arr);

            int[] res1, res2;
            DataReceiveSend.RData(socket, out res1);
            DataReceiveSend.RData(socket, out res2);

            Console.WriteLine("Массив их квадратов: " + $"[{string.Join(", ", res1)}]");
            Console.WriteLine("Массив их кубов: " + $"[{string.Join(", ", res2)}]");

            Console.ReadLine();
        }
        static void Second_2_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 2);

            Console.Write("Введите массив чисел через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');

            int[] arr = input.Select(str => Int32.Parse(str)).ToArray();

            DataReceiveSend.SData(socket, arr);

            int[] res1, res2;
            DataReceiveSend.RData(socket, out res1);
            DataReceiveSend.RData(socket, out res2);

            Console.WriteLine("Четные по номеру: " + $"[{string.Join(", ", res1)}]");
            Console.WriteLine("Нечетные по номеру: " + $"[{string.Join(", ", res2)}]");

            Console.ReadLine();
        }
        static void Second_3_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 3);

            Console.Write("Введите массив строк через $:  ");
            string[] input = Console.ReadLine().Split('$');

            DataReceiveSend.SData(socket, input, '$');

            Console.Write("Введите букву:  ");
            string ch = Console.ReadLine();
            DataReceiveSend.SString(socket, ch);

            DataReceiveSend.RData(socket, out input, '$');

            Console.WriteLine("Строки, в которых данной буквы нет: " + $"[[{string.Join("], [", input)}]]");

            Console.ReadLine();
        }
        static void Second_4_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 4);

            Console.Write("Введите 3 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            double[] arr = new double[3] { Double.Parse(input[0]), Double.Parse(input[1]), Double.Parse(input[2]) };

            Console.WriteLine($"Уравнение: {arr[0]}*x^2{(arr[1] < 0 ? " - ":" + ") + Math.Abs(arr[1])}*x{(arr[2] < 0 ? " - " : " + ") + Math.Abs(arr[2])} = 0");

            DataReceiveSend.SData(socket, arr);

            DataReceiveSend.RData(socket, out arr);

            if(arr.Length > 0)
                if(arr.Length == 1)
                    Console.WriteLine($"Найденный корень: x = {arr[0]}");
                else
                    Console.WriteLine($"Найденные корни: x1 = {arr[0]}; x2 = {arr[1]}");
            else
                Console.WriteLine(DataReceiveSend.RString(socket));

            Console.ReadLine();
        }
        static void Second_5_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 5);

            Console.Write("Введите массив чисел через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');

            int[] arr = input.Select(str => Int32.Parse(str)).ToArray();

            DataReceiveSend.SData(socket, arr);

            int[] res;
            DataReceiveSend.RData(socket, out res);

            Console.WriteLine("Получен массив: " + $"[{string.Join(", ", res)}]");

            Console.ReadLine();
        }
        static void Second_6_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 6);

            Console.Write("Введите массив чисел через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = input.Select(str => Int32.Parse(str)).ToArray();

            DataReceiveSend.SData(socket, arr);

            Console.Write("Введите массив строк через $:  ");
            input = Console.ReadLine().Split('$');

            DataReceiveSend.SData(socket, input, '$');

            DataReceiveSend.RData(socket, out input, '$');

            Console.WriteLine("Их декартово произведение: " + $"[[{string.Join("], [", input)}]]");
            Console.ReadLine();
        }
        static void Second_7_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 7);

            Console.Write("Введите количество строк:  ");
            int N = Int32.Parse(Console.ReadLine());

            int[][] arr = new int[N][];

            for (int i = 0; i < N; ++i)
            {
                Console.Write($"Введите {i+1} строкy через пробел:  "); 
                string[] input = Console.ReadLine().Split(' ');
                arr[i] = input.Select(str => Int32.Parse(str)).ToArray();
            }

            DataReceiveSend.SData(socket, arr);

            DataReceiveSend.RData(socket, out arr);

            Console.Write("Полученная матрица: \n[\n" + $"[[{string.Join("], [", arr[0])}]]");
            for (int i = 1; i < N; ++i)
            {
                Console.Write($",\n[[{string.Join("], [",  arr[i])}]]");
            }
            Console.WriteLine("\n]");

            Console.ReadLine();
        }
        static void Second_8_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 8);

            int num = DataReceiveSend.RNumber(socket);
            string[] input = new string[num];

            Console.WriteLine($"Сервер запрашивает {num} строк.");
            for (int i = 0; i < num; ++i)
            {
                Console.Write($"Введите {i+1} строкy:  ");
                input[i] = Console.ReadLine();
            }

            DataReceiveSend.SData(socket, input, '$');

            Console.WriteLine("Их конкатенация: " + DataReceiveSend.RString(socket));

            Console.ReadLine();
        }
        static void Second_9_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 9);

            Console.Write($"Введите число:  ");
            int num = Int32.Parse(Console.ReadLine());

            DataReceiveSend.SNumber(socket, num);
            if(DataReceiveSend.RBool(socket))
            {
                Console.Write($"Введите количество строк:  ");
                int N = Int32.Parse(Console.ReadLine());

                string[] input = new string[N];
                for (int i = 0; i < N; ++i)
                {
                    Console.Write($"Введите {i + 1} строкy:  ");
                    input[i] = Console.ReadLine();
                }

                DataReceiveSend.SData(socket, input, '$');
                Console.WriteLine("Их конкатенация: " + DataReceiveSend.RString(socket));
                Console.ReadLine();
            }
            else
            {
                Console.Write("Введите массив чисел через пробел:  ");
                string[] input = Console.ReadLine().Split(' ');

                int[] arr = input.Select(str => Int32.Parse(str)).ToArray();

                DataReceiveSend.SData(socket, arr);

                Console.WriteLine("Их среднее арифметическое: " + DataReceiveSend.RDouble(socket));
                Console.ReadLine();
            }
        }
        static void Second_10_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 10);

            Console.Write($"Введите строку:  ");
            string input = Console.ReadLine();

            DataReceiveSend.SString(socket, input);

            string[] output;
            DataReceiveSend.RData(socket, out output, '$');

            Console.WriteLine("Слова данной строки: " + $"[[{string.Join("], [", output)}]]");
            Console.ReadLine();
        }
        static void Second_11_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 11);

            Console.Write("Введите 3 числа через пробел:  ");
            string[] input = Console.ReadLine().Split(' ');
            int[] arr = new int[3] { Int32.Parse(input[0]), Int32.Parse(input[1]), Int32.Parse(input[2]) };

            DataReceiveSend.SData(socket, arr);

            if(DataReceiveSend.RBool(socket))
            {
                Console.Write($"Введите строкy :  "); 
                string tinput = Console.ReadLine();
                DataReceiveSend.SString(socket, tinput);
                Console.WriteLine("Символов в ней: " + DataReceiveSend.RNumber(socket));
            }
            else
            {
                Console.Write("Введите массив чисел через пробел:  ");
                string[] tinput = Console.ReadLine().Split(' ');
                int[] tarr = tinput.Select(str => Int32.Parse(str)).ToArray();

                DataReceiveSend.SData(socket, tarr);
                Console.WriteLine("Их Сумма: " + DataReceiveSend.RNumber(socket));
            }

            Console.ReadLine();
        }
        static void Second_12_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 12);

            Console.Write($"Введите строку:  ");
            string input = Console.ReadLine();

            DataReceiveSend.SString(socket, input);

            Console.WriteLine("Букв: " + DataReceiveSend.RNumber(socket));
            string[] output;
            DataReceiveSend.RData(socket, out output, '$');

            Console.WriteLine("Слова данной строки: " + $"[[{string.Join("], [", output)}]]");

            DataReceiveSend.RData(socket, out output, '$');
            Console.WriteLine("Четный по номеру слова данной строки: " + $"[[{string.Join("], [", output)}]]");

            Console.ReadLine();
        }
        static void Second_13_Client(Socket socket)
        {
            DataReceiveSend.SNumber(socket, 3);
            DataReceiveSend.SNumber(socket, 13);
            
            Console.WriteLine($"Сервером сгенерирована рамерность массива случайных чисел: {DataReceiveSend.RNumber(socket)}");

            int[] res;
            DataReceiveSend.RData(socket, out res);

            Console.WriteLine("Полученный массив: " + $"[{string.Join(", ", res)}]");

            Console.WriteLine($"Из них ненулевых чисел: {DataReceiveSend.RNumber(socket)}");

            Console.ReadLine();
        }
        public class DataReceiveSend
        {
            public static int RNumber(Socket socket)
            {
                int number;
                //Выделяем размер под INT
                byte[] data = new byte[4];
                //Получаем данные
                socket.Receive(data);
                //Конвертируем в INT
                number = BitConverter.ToInt32(data, 0);
                return number;
            }
            public static void SNumber(Socket socket, int val)
            {
                byte[] data;
                //Представляем число в виде байтов
                data = BitConverter.GetBytes(val);
                //Отправляем число
                socket.Send(data);
            }
            public static double RDouble(Socket socket)
            {
                double number;
                //Выделяем размер под INT
                byte[] data = new byte[sizeof(double)];
                //Получаем данные
                socket.Receive(data);
                //Конвертируем в INT
                number = BitConverter.ToDouble(data, 0);
                return number;
            }
            public static void SDouble(Socket socket, double val)
            {
                byte[] data;
                //Представляем число в виде байтов
                data = BitConverter.GetBytes(val);
                //Отправляем число
                socket.Send(data);
            }
            public static string RString(Socket socket)
            {
                int size = RNumber(socket);
                //получаем сами данные
                byte[] data = new byte[size];
                socket.Receive(data);
                //переводим в строку
                string str = Encoding.Unicode.GetString(data);
                return str;
            }
            public static void SString(Socket socket, string input)
            {
                //Перевод в byte
                byte[] data = Encoding.Unicode.GetBytes(input);
                //Представление размера в виде byte
                byte[] dataSize_byte = BitConverter.GetBytes(data.Length);
                socket.Send(dataSize_byte);
                socket.Send(data);
            }
            public static bool RBool(Socket socket)
            {
                bool f;
                //Выделяем размер под INT
                byte[] data = new byte[sizeof(bool)];
                //Получаем данные
                socket.Receive(data);
                //Конвертируем в INT
                f = BitConverter.ToBoolean(data, 0);
                return f;
            }
            public static void SBool(Socket socket, bool val)
            {
                byte[] data;
                //Представляем число в виде байтов
                data = BitConverter.GetBytes(val);
                //Отправляем число
                socket.Send(data);
            }
            public static void RData(Socket socket, out string str)
            {
                int size = RNumber(socket);
                //получаем сами данные
                byte[] data = new byte[size];
                socket.Receive(data);
                //переводим в строку
                str = Encoding.Unicode.GetString(data);
            }
            public static void RData(Socket socket, out int num)
            {
                //Выделяем размер под INT
                byte[] data = new byte[4];
                //Получаем данные
                socket.Receive(data);
                //Конвертируем в INT
                num = BitConverter.ToInt32(data, 0);
            }
            public static void RData(Socket socket, out int[] arr)
            {
                int size = RNumber(socket);
                //получаем сами данные
                byte[] data = new byte[size * 4];
                socket.Receive(data);
                arr = new int[size];
                for (int i = 0; i < size * 4; i += 4)
                {
                    arr[i / 4] = BitConverter.ToInt32(new byte[] { data[i], data[i + 1], data[i + 2], data[i + 3] }, 0);
                }
            }
            public static void SData(Socket socket, in int[] arr)
            {
                SNumber(socket, arr.Length);
                byte[] data = new byte[arr.Length * sizeof(int)];
                Buffer.BlockCopy(arr, 0, data, 0, data.Length);
                socket.Send(data);
            }
            public static void RData(Socket socket, out int[,] arr)
            {
                int N = RNumber(socket);
                int M = RNumber(socket);
                //получаем сами данные
                byte[] data = new byte[M * N * sizeof(int)];
                socket.Receive(data);
                arr = new int[M, N];

                for (int i = 0; i < N; ++i)
                    for (int j = 0; j < M; ++j)
                    {
                        arr[i, j] = BitConverter.ToInt32(data.AsSpan(i * M * 4 + j * 4, 4).ToArray(), 0);
                    }
            }
            public static void SData(Socket socket, in int[,] arr)
            {
                int N = arr.GetLength(0);
                int M = arr.GetLength(1);

                int counter = 0;
                int[] sendingArray = new int[arr.Length];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        sendingArray[counter] = arr[i, j];
                        counter++;
                    }
                }
                byte[] data = new byte[sendingArray.Length * sizeof(int)];
                Buffer.BlockCopy(sendingArray, 0, data, 0, data.Length);

                SNumber(socket, N);
                SNumber(socket, M);
                socket.Send(data);
            }
            public static void RData(Socket socket, out int[][] arr)
            {
                int[] Ms;
                RData(socket, out Ms);
                int N = Ms.Length;
                //получаем сами данные
                byte[] data = new byte[Ms.Sum() * sizeof(int)];
                socket.Receive(data);
                arr = new int[N][];

                for (int i = 0; i < N; ++i)
                {
                    arr[i] = new int[Ms[i]];
                    int l = i > 0 ? Ms.AsSpan(0, i).ToArray().Sum() : 0;
                    for (int j = 0; j < Ms[i]; ++j)
                    {
                        arr[i][j] = BitConverter.ToInt32(data.AsSpan(l * 4 + j * 4, 4).ToArray(), 0);
                    }
                }
            }
            public static void SData(Socket socket, in int[][] arr)
            {
                int[] Ms = arr.Select(it => it.Length).ToArray();
                int N = Ms.Length;

                int counter = 0;
                int[] sendingArray = new int[Ms.Sum()];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < Ms[i]; j++)
                    {
                        sendingArray[counter] = arr[i][j];
                        counter++;
                    }
                }
                byte[] data = new byte[sendingArray.Length * sizeof(int)];
                Buffer.BlockCopy(sendingArray, 0, data, 0, data.Length);

                SData(socket, Ms);
                socket.Send(data);
            }
            public static void RData(Socket socket, out double[] arr)
            {
                int size = RNumber(socket);
                //получаем сами данные
                byte[] data = new byte[size * sizeof(double)];
                socket.Receive(data);
                arr = new double[size];
                for (int i = 0; i < size * sizeof(double); i += sizeof(double))
                {
                    arr[i / sizeof(double)] = BitConverter.ToDouble(data.AsSpan(i, sizeof(double)).ToArray(), 0);
                }
            }
            public static void SData(Socket socket, in double[] arr)
            {
                SNumber(socket, arr.Length);
                byte[] data = new byte[arr.Length * sizeof(double)];
                Buffer.BlockCopy(arr, 0, data, 0, data.Length);
                socket.Send(data);
            }
            public static void RData(Socket socket, out string[] arr, char separator)
            {
                arr = RString(socket).Split(separator);
            }
            public static void SData(Socket socket, in string[] arr, char separator)
            {
                SString(socket, String.Join(separator, arr));
            }
        }
    }
}
