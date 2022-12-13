using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    public class Frontend
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<ConsoleColor> Colors { get; private set; }

        public void Start()
        {
            Colors = new();
            SizeSelection();
            ColorSelection();
        }


        /// <summary>
        /// Сообщение об ошибке программы
        /// </summary>
        /// <param name="ex"></param>
        private void WriteExMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Выбор размера поля для игры
        /// </summary>
        private void SizeSelection()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите число (ширину окна) от 2 до 100: ");
                    Width = int.Parse(Console.ReadLine());

                    Console.Write("Введите число (высоту окна) от 2 до 50: ");
                    Height = int.Parse(Console.ReadLine());
                    if (Width >= 2 && Height >= 2 && Width <= 100 && Height <= 50)
                        break;
                    WriteExMsg("Введите значения в нужном диапазоне!");
                }
                catch (Exception ex)
                {
                    WriteExMsg(ex.Message);
                }
            }
        }

        /// <summary>
        /// Выбор цвета для игры
        /// </summary>
        private void ColorSelection()
        {
            while (true)
            {
                try
                {
                    List<ConsoleColor> colorList = new();
                    int i = 0;
                    foreach (var item in Enum.GetValues<ConsoleColor>())
                    {
                        if (item == ConsoleColor.Black)
                            continue;
                        Console.WriteLine($"{++i}) {item}");
                        colorList.Add(item);
                    }
                    Console.Write("Выберите цвета которые будут участвовать в игре, напишите цифры через пробел (минимум 2 цвета): ");
                    string[] numbers = Console.ReadLine().Split(' ');

                    
                            

                    if (numbers.Length >= 2)
                    {
                        foreach (var item in numbers)
                        {
                            Colors.Add(colorList[int.Parse(item) - 1]);
                        }
                        break;
                    }
                    WriteExMsg("Введите более одного цвета и номера цветов должны существоть!");
                }
                catch (Exception ex)
                {
                    WriteExMsg(ex.Message);
                }
            }

        }

    }
}
