using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ServerStart();
        }

        static void ServerStart()
        {
            // Создаем сокет Tcp/Ip
            Socket socket = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1213);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                socket.Bind(ip);
                socket.Listen(10);

                Console.WriteLine("Ожидаем соединение через порт {0}", ip);

                // Начинаем слушать соединения
                while (true)
                {
                    Socket handler = socket.Accept();

                    int groupOfCommand = DataReceiveSend.RNumber(handler);
                    int typeOfCommand = DataReceiveSend.RNumber(handler);
                    Proccesing.ServerAction(handler, groupOfCommand, typeOfCommand);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

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
            for (int i = 0; i < size * 4; i+= 4)
            {
                arr[i/4] = BitConverter.ToInt32(new byte[] {data[i], data[i+1], data[i+2], data[i+3] }, 0);
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

    public class Proccesing
    {
        public static void ServerAction(Socket socket, int groupOfCommand, int typeOfCommand)
        {
            switch (groupOfCommand)
            {
                case 1:
                    switch (typeOfCommand)
                    {
                        case 1:
                            Console.WriteLine("Начинаю выполнять Training_1_Server (Разминка 1)");
                            Training_1_Server(socket);
                            break;
                        case 2:
                            Console.WriteLine("Начинаю выполнять Training_2_Server (Разминка 2)");
                            Training_2_Server(socket);
                            break;
                        case 3:
                            Console.WriteLine("Начинаю выполнять Training_3_Server (Разминка 3)");
                            Training_3_Server(socket);
                            break;
                    }
                    break;
                case 2:
                    switch (typeOfCommand)
                    {
                        case 1:
                            Console.WriteLine("Начинаю выполнять First_1_2_4_5_7_Server (Задание 1.1)");
                            First_1_2_4_5_7_Server(socket, typeOfCommand);
                            break;
                        case 2:
                            Console.WriteLine("Начинаю выполнять First_1_2_4_5_7_Server (Задание 1.2)");
                            First_1_2_4_5_7_Server(socket, typeOfCommand);
                            break;
                        case 3:
                            Console.WriteLine("Начинаю выполнять Training_1_Server (Задание 1.3)");
                            Training_1_Server(socket);
                            break;
                        case 4:
                            Console.WriteLine("Начинаю выполнять First_1_2_4_5_7_Server (Задание 1.4)");
                            First_1_2_4_5_7_Server(socket, typeOfCommand);
                            break;
                        case 5:
                            Console.WriteLine("Начинаю выполнять First_1_2_4_5_7_Server (Задание 1.5)");
                            First_1_2_4_5_7_Server(socket, typeOfCommand);
                            break;
                        case 6:
                            Console.WriteLine("Начинаю выполнять First_6_Server (Задание 1.6)");
                            First_6_Server(socket);
                            break;
                        case 7:
                            Console.WriteLine("Начинаю выполнять First_1_2_4_5_7_Server (Задание 1.7)");
                            First_1_2_4_5_7_Server(socket, typeOfCommand);
                            break;
                        case 8:
                            Console.WriteLine("Начинаю выполнять First_8_Server (Задание 1.8)");
                            First_8_Server(socket);
                            break;
                        case 9:
                            Console.WriteLine("Начинаю выполнять First_9_Server (Задание 1.9)");
                            First_9_Server(socket);
                            break;
                        case 10:
                            Console.WriteLine("Начинаю выполнять First_10_Server (Задание 1.10)");
                            First_10_Server(socket);
                            break;
                        case 11:
                            Console.WriteLine("Начинаю выполнять First_11_Server (Задание 1.11)");
                            First_11_Server(socket);
                            break;
                        case 12:
                            Console.WriteLine("Начинаю выполнять First_12_Server (Задание 1.12)");
                            First_12_Server(socket);
                            break;
                        case 13:
                            Console.WriteLine("Начинаю выполнять First_13_Server (Задание 1.13)");
                            First_13_Server(socket);
                            break;
                        case 14:
                            Console.WriteLine("Начинаю выполнять First_14_Server (Задание 1.14)");
                            First_14_Server(socket);
                            break;
                    }
                    break;
                case 3:
                    switch (typeOfCommand)
                    {
                        case 1:
                            Console.WriteLine("Начинаю выполнять Second_1_Server (Задание 2.1)");
                            Second_1_Server(socket);
                            break;
                        case 2:
                            Console.WriteLine("Начинаю выполнять Second_2_Server (Задание 2.2)");
                            Second_2_Server(socket);
                            break;
                        case 3:
                            Console.WriteLine("Начинаю выполнять Second_3_Server (Задание 2.3)");
                            Second_3_Server(socket);
                            break;
                        case 4:
                            Console.WriteLine("Начинаю выполнять Second_4_Server (Задание 2.4)");
                            Second_4_Server(socket);
                            break;
                        case 5:
                            Console.WriteLine("Начинаю выполнять Second_5_Server (Задание 2.5)");
                            Second_5_Server(socket);
                            break;
                        case 6:
                            Console.WriteLine("Начинаю выполнять Second_6_Server (Задание 2.6)");
                            Second_6_Server(socket);
                            break;
                        case 7:
                            Console.WriteLine("Начинаю выполнять Second_7_Server (Задание 2.7)");
                            Second_7_Server(socket);
                            break;
                        case 8:
                            Console.WriteLine("Начинаю выполнять Second_8_Server (Задание 2.8)");
                            Second_8_Server(socket);
                            break;
                        case 9:
                            Console.WriteLine("Начинаю выполнять Second_9_Server (Задание 2.9)");
                            Second_9_Server(socket);
                            break;
                        case 10:
                            Console.WriteLine("Начинаю выполнять Second_10_Server (Задание 2.10)");
                            Second_10_Server(socket);
                            break;
                        case 11:
                            Console.WriteLine("Начинаю выполнять Second_11_Server (Задание 2.11)");
                            Second_11_Server(socket);
                            break;
                        case 12:
                            Console.WriteLine("Начинаю выполнять Second_12_Server (Задание 2.12)");
                            Second_12_Server(socket);
                            break;
                        case 13:
                            Console.WriteLine("Начинаю выполнять Second_13_Server (Задание 2.13)");
                            Second_13_Server(socket);
                            break;
                    }
                    break;
            }
        }
        static void Training_1_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            input[0] *= input[0];
            input[1] *= input[1];
            input[2] *= input[2];

            DataReceiveSend.SData(socket, input);
        }
        static void Training_2_Server(Socket socket)
        {
            string inp1 = DataReceiveSend.RString(socket);
            string inp2 = DataReceiveSend.RString(socket);
            DataReceiveSend.SString(socket, inp1 + inp2);
        }
        static void Training_3_Server(Socket socket)
        {
            int num = 1;
            while(num == 1)
            {
                num = DataReceiveSend.RNumber(socket);
                if (num == 0)
                    continue;
                if (num != 1)
                    DataReceiveSend.SNumber(socket, -1);
                else
                    DataReceiveSend.SNumber(socket, 0);
            }
            DataReceiveSend.SNumber(socket, 0);
            num = DataReceiveSend.RNumber(socket);
            while (num == 0)
            {
                DataReceiveSend.SNumber(socket, num);
                num = DataReceiveSend.RNumber(socket);
            }
            DataReceiveSend.SNumber(socket, num * num);
        }
        static void First_1_2_4_5_7_Server(Socket socket, int action)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            switch (action)
            {
                case 1:
                    input[0] = input.Max();
                    break;
                case 2:
                    input[0] = input[0] * input[1] * input[2];
                    break;
                case 4:
                    input[0] = input.Sum();
                    break;
                case 5:
                    input[0] = (int)Math.Round(input.Average(), 0);
                    break;
                case 7:
                    input[0] = input[0] % 3 == 0? 1 : 0;
                    input[0] += input[1] % 3 == 0 ? 1 : 0;
                    input[0] += input[2] % 3 == 0 ? 1 : 0;
                    break;
            }
            DataReceiveSend.SNumber(socket, input[0]);
        }
        static void First_6_Server(Socket socket)
        {
            int counter = 0;
            int num = DataReceiveSend.RNumber(socket);
            while (num != 0)
            {
                ++counter;
                num = DataReceiveSend.RNumber(socket);
            }

            DataReceiveSend.SNumber(socket, counter);
        }
        static void First_8_Server(Socket socket)
        {
            int num = DataReceiveSend.RNumber(socket);

            string numStr = "" + num;
            if (numStr.Contains("-"))
                numStr = numStr.Substring(1);

            List<int> res = new List<int>();
            foreach (char c in numStr)
                res.Add(Int32.Parse("" + c));

            DataReceiveSend.SData(socket, res.ToArray());
        }
        static void First_9_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            int tmp = input[0];
            input[0] *= input[1];
            input[1] += tmp;

            DataReceiveSend.SData(socket, input);
        }
        static void First_10_Server(Socket socket)
        {
            int num = DataReceiveSend.RNumber(socket);

            for (int i = 0; i < num; ++i)
                DataReceiveSend.SNumber(socket, num);

            DataReceiveSend.SNumber(socket, 0);
        }
        static void First_11_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            DataReceiveSend.SBool(socket, input[1] % input[0] == 0);
        }
        static void First_12_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            input[0] = -input[0] + input[1];
            input[1] = input[2] + input[3];

            DataReceiveSend.SData(socket, input);
        }
        static void First_13_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            if(input[0] > input[1])
                DataReceiveSend.SData(socket, new int[] { input[0] + input[1] });
            else
                DataReceiveSend.SData(socket, new int[] { input[0] * input[0], input[1] * input[1] });
        }
        static void First_14_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            int res = input[0] - input[1];

            DataReceiveSend.SNumber(socket, res);

            if (res < 0)
                DataReceiveSend.SNumber(socket, input[0] + input[1]);
        }
        static void Second_1_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            int[] res1 = input.Select(it => it * it).ToArray();
            int[] res2 = input.Select(it => it * it * it).ToArray();

            DataReceiveSend.SData(socket, res1);
            DataReceiveSend.SData(socket, res2);
        }
        static void Second_2_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            int[] res1 = input.Where(it => input.ToList().IndexOf(it) % 2 == 1).ToArray();
            int[] res2 = input.Where(it => input.ToList().IndexOf(it) % 2 == 0).ToArray();

            DataReceiveSend.SData(socket, res1);
            DataReceiveSend.SData(socket, res2);
        }
        static void Second_3_Server(Socket socket)
        {
            string[] input;
            DataReceiveSend.RData(socket, out input, '$');
            string ch = DataReceiveSend.RString(socket);

            string[] res = input.Where(it => !it.Contains(ch[0])).ToArray();

            DataReceiveSend.SData(socket, res, '$');
        }
        static void Second_4_Server(Socket socket)
        {
            double[] input;
            DataReceiveSend.RData(socket, out input);

            double x1, x2;
            //дискриминант
            var discriminant = Math.Pow(input[1], 2) - 4 * input[0] * input[2];
            if (discriminant < 0)
            {
                DataReceiveSend.SData(socket, new double[] { });
                DataReceiveSend.SString(socket, "Корней нет");
            }
            else
            {
                if (discriminant == 0) //квадратное уравнение имеет два одинаковых корня
                {
                    x1 = -input[1] / (2 * input[0]);
                    x2 = x1;
                    DataReceiveSend.SData(socket, new double[] { x1 });
                }
                else //уравнение имеет два разных корня
                {
                    x1 = (-input[1] + Math.Sqrt(discriminant)) / (2 * input[0]);
                    x2 = (-input[1] - Math.Sqrt(discriminant)) / (2 * input[0]);
                    DataReceiveSend.SData(socket, new double[] { x1, x2 });
                }
            }
        }
        static void Second_5_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            int[] res;
            if (input.Sum() > input.Length)
                res = input.Distinct().OrderByDescending(it => it).ToArray().AsSpan(0, 3).ToArray();
            else
                res = input.Select(it => it * it).ToArray();

            DataReceiveSend.SData(socket, res);
        }
        static void Second_6_Server(Socket socket)
        {
            int[] input1;
            DataReceiveSend.RData(socket, out input1);

            string[] input2;
            DataReceiveSend.RData(socket, out input2, '$');

            string[] res = input1.SelectMany(it => input2.Select(str => $"{it} {str}")).ToArray();

            DataReceiveSend.SData(socket, res, '$');
        }
        static void Second_7_Server(Socket socket)
        {
            int[][] input;
            DataReceiveSend.RData(socket, out input);

            DataReceiveSend.SData(socket, input);
        }
        static void Second_8_Server(Socket socket)
        {
            DataReceiveSend.SNumber(socket, new Random().Next(2, 10));

            string[] input;
            DataReceiveSend.RData(socket, out input, '$');

            DataReceiveSend.SString(socket, string.Join("", input));
        }
        static void Second_9_Server(Socket socket)
        {
            int num = DataReceiveSend.RNumber(socket);

            DataReceiveSend.SBool(socket, num == 0);
            if (num == 0)
            {
                string[] input;
                DataReceiveSend.RData(socket, out input, '$');

                DataReceiveSend.SString(socket, string.Join("", input));
            } else
            {
                int[] input;
                DataReceiveSend.RData(socket, out input);

                DataReceiveSend.SDouble(socket, input.Average());
            }
        }
        static void Second_10_Server(Socket socket)
        {
            string input = DataReceiveSend.RString(socket);

            DataReceiveSend.SData(socket, input.Replace(",", "").Split(' ').ToHashSet().ToArray(), '$');
        }
        static void Second_11_Server(Socket socket)
        {
            int[] input;
            DataReceiveSend.RData(socket, out input);

            if(input.Sum() % 2 == 0)
            {
                DataReceiveSend.SBool(socket, true);
                DataReceiveSend.SNumber(socket, DataReceiveSend.RString(socket).Length);
            } else
            {
                DataReceiveSend.SBool(socket, false);
                DataReceiveSend.RData(socket, out input);
                DataReceiveSend.SNumber(socket, input.Sum());
            }
        }
        static void Second_12_Server(Socket socket)
        {
            string input = DataReceiveSend.RString(socket);

            string[] words = input.Replace(",", "").Split(' ').ToArray();
            int chnum = string.Join("", words).Length;

            DataReceiveSend.SNumber(socket, chnum);
            DataReceiveSend.SData(socket, words, '$');

            words = words.Where(it => words.ToList().IndexOf(it) % 2 == 1).ToArray();
            DataReceiveSend.SData(socket, words, '$');
        }
        static void Second_13_Server(Socket socket)
        {
            int N = new Random().Next(1, 20);
            DataReceiveSend.SNumber(socket, N);

            int[] arr = Enumerable.Range(1, N).Select(i => new Random().Next(-10, 10)).ToArray();
            DataReceiveSend.SData(socket, arr);

            DataReceiveSend.SNumber(socket, N - arr.Where(it => it == 0).Count());
        }
    }
}
