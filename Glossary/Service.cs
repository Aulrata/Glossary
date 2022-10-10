using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    internal class Service
    {
        readonly List<ConsoleColor> _colors = new();
        readonly List<Space> _spaces = new();
        private Pixel[,]? _pixels;
        private int _width;
        private int _height;

        /// <summary>
        /// Выбор размера поля для игры
        /// </summary>
        private void SizeSelection()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите размер окна через пробел (Ширина, высота): ");
                    string[] sizes = Console.ReadLine().Split(' ');
                    _width = int.Parse(sizes[0]);
                    _height = int.Parse(sizes[1]);
                    //Console.SetWindowSize(_width+10, _height+10);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Выбор цвета для игры
        /// </summary>
        private void ColorSelection()
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
            Console.Write("Выберите цвета которые будут участвовать в игре, напишите цифры через пробел: ");
            string[] numbers = Console.ReadLine().Split(' ');

            foreach (var item in numbers)
            {
                _colors.Add(colorList[int.Parse(item) - 1]);
            }

        }

        /// <summary>
        /// Заполнет поле случайными цветами
        /// </summary>
        private void Fill()
        {
            Console.Clear();
            _pixels = new Pixel[_width, _height];
            for (int y = 0; y < _width; y++)
            {
                for (int x = 0; x < _height; x++)
                {
                    ConsoleColor color = _colors[Random.Shared.Next(_colors.Count)];      //Выбор рандомного цвета
                    Console.BackgroundColor = color;
                    Console.Write(' ');
                    _pixels[x, y] = new Pixel(x, y, color);     // Добавление данного пикселя в список
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// Находит все пространства
        /// </summary>
        private void SpaceCreator()
        {
            int countOfSpaces = 0;
            for (int y = 0; y < _width; y++)
            {
                for (int x = 0; x < _height; x++)
                {
                    if (_pixels[x, y].IsAdded)
                        continue;

                    _spaces.Add(new Space(_pixels, _width, _height));
                    _spaces[countOfSpaces].CreateSpace(x, y);
                    countOfSpaces++;

                }
            }

        }

        private void OneStep()
        {
            foreach (var item in _spaces)
            {

                _pixels = item.ColorARandomAdjacentPixel(_pixels);

            }
        }

        private void RestartField()
        {
            foreach (Pixel? item in _pixels)
            {
                item.IsAdded = false;
            }
            _spaces.Clear();
        }

        private void CheckingTheSpaceForEmptiness()
        {
            for (int i = 0; i < _spaces.Count; i++)
            {
                if (_spaces[i].CheckCountOfPixelInThisSpace())
                    _spaces.RemoveAt(i);
            }
        }

        public void StartGame()
        {
            SizeSelection();
            ColorSelection();
            Fill();
            Console.CursorVisible = false;
            while (true)
            {
                SpaceCreator();
                OneStep();
                CheckingTheSpaceForEmptiness();
                Thread.Sleep(400);
                //Console.ReadKey();
                RestartField();
                if (_spaces.Count == 1)
                    break;
            }

        }
    }
}
