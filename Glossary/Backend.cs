using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    public class Backend
    {
        private List<ConsoleColor> _colors = new();
        readonly List<Space> _spaces = new();
        private Pixel[,]? _pixels;
        private int _width;
        private int _height;

        public Backend(int width, int height,List<ConsoleColor> colors)
        {
            _width = width;
            _height = height;
            _colors = colors;
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Fill();
            while (true)
            {
                SpaceCreator();
                OneStep();
                CheckingTheSpaceForEmptiness();
                //Thread.Sleep(1);
                //Console.ReadKey();
               
                if (_spaces.Count == 1)
                    break;
                else RestartField();
            }
            Thread.Sleep(1000);
            Console.Clear();

        }


        /// <summary>
        /// Находит все пространства
        /// </summary>
        private void SpaceCreator()
        {
            int countOfSpaces = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
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

        /// <summary>
        /// Поверяет пространства на пустоту, если там нет пикселя, то пространство удаляется 
        /// </summary>
        private void CheckingTheSpaceForEmptiness()
        {
            
            for (int i = 0; i < _spaces.Count; i++)
            {
                if (_spaces[i].CheckCountOfPixelInThisSpace())
                    _spaces.RemoveAt(i);
            }
        }

        /// <summary>
        /// Заполнет поле случайными цветами
        /// </summary>
        private void Fill()
        {
            Console.Clear();
            _pixels = new Pixel[_width, _height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
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
    }
}
