using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    internal class Space
    {
        private readonly int _limitX;
        private readonly int _limitY;
        private readonly List<Pixel> _pixelOfThisSpace;
        private readonly List<Pixel> _adjacentPixels;   // Смежные пиксели пространства
        private readonly Pixel[,] _allPixels;

        public Space(Pixel[,] pixels, int limitX, int limitY)
        {
            _pixelOfThisSpace = new List<Pixel>();
            _adjacentPixels = new List<Pixel>();
            _allPixels = pixels;

            _limitX = limitX;
            _limitY = limitY;
        }

        /// <summary>
        /// Цвет простарнства
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Создание пространтсва (Установка цвета и добавление пикселей в пространство)
        /// </summary>
        /// <param name="x">Coord X</param>
        /// <param name="y">Coord Y</param>
        public void CreateSpace(int x, int y)
        {
            SetColor(x, y);
            AlgorithmDFS(x, y);

        }

        /// <summary>
        /// Выполнение алгоритма по поиску в глубину пикселей для пространства 
        /// </summary>
        /// <param name="x">Coord X</param>
        /// <param name="y">Coord Y</param>
        private void AlgorithmDFS(int x, int y)
        {
            if (_allPixels[x, y].Color == Color)
            {
                if (_allPixels[x, y].IsAdded == true)
                    return;
                _pixelOfThisSpace.Add(_allPixels[x, y]);
                _allPixels[x, y].IsAdded = true;

                if (TryNext(x + 1, y))
                    AlgorithmDFS(x + 1, y);     // Right
                if (TryNext(x, y + 1))
                    AlgorithmDFS(x, y + 1);     // Down
                if (TryNext(x - 1, y))
                    AlgorithmDFS(x - 1, y);     // Left
                if (TryNext(x, y - 1))
                    AlgorithmDFS(x, y - 1);     // Up
            }
            else
            {
                if (_adjacentPixels.Contains(_allPixels[x, y]))
                    return;

                _adjacentPixels.Add(_allPixels[x, y]);
            }
        }

        /// <summary>
        /// Проверка чтобы мы не вышли за границы поля
        /// </summary>
        /// <param name="x">Coord X</param>
        /// <param name="y">Coord Y</param>
        /// <returns></returns>
        private bool TryNext(int x, int y)
        {
            if (x < 0 || y < 0 || x > _limitX - 1 || y > _limitY - 1)
                return false;
            return true;
        }

        /// <summary>
        /// Устанавливает цвет простарнства по первому пикселю который в него входит
        /// </summary>
        /// <param name="x">Coord X</param>
        /// <param name="y">Coord Y</param>
        private void SetColor(int x, int y)
        {
            if (Color == default)
                Color = _allPixels[x, y].Color;
        }

        public bool CheckCountOfPixelInThisSpace()
        {
            if (_pixelOfThisSpace.Count == 0)
                return true;
            return false;
        }


        /// <summary>
        /// Закрашивает случайный смежный пиксель
        /// </summary>
        public Pixel[,] ColorARandomAdjacentPixel(Pixel[,] pixels)
        {
            // Этап 4
            //float random2 = (float)_pixelOfThisSpace.Count / (float)pixels.Length;
            //float random3 = Random.Shared.NextSingle();
            //if (random2 < random3)
            //    return pixels;
            for (int i = 0; i < _pixelOfThisSpace.Count; i++)
            {
                if (_pixelOfThisSpace[i].Color != Color)
                    _pixelOfThisSpace.RemoveAt(i);
            }

            if (_pixelOfThisSpace.Count == 0)
                return pixels;

            if (_adjacentPixels.Count == 0)
                return pixels;

            int random = Random.Shared.Next(_adjacentPixels.Count);
            pixels[_adjacentPixels[random].PositionX, _adjacentPixels[random].PositionY].Color = Color;

            Console.BackgroundColor = Color;
            Console.SetCursorPosition(_adjacentPixels[random].PositionX, _adjacentPixels[random].PositionY);
            Console.Write(' ');
            Console.BackgroundColor = default;

            return pixels;
        }
    }
}
