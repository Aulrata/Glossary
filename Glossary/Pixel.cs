using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    public class Pixel
    {
        public Pixel(int positionX, int positionY, ConsoleColor color)
        {
            PositionX = positionX;
            PositionY = positionY;
            Color = color;
        }

        public int PositionY { get; set; }

        public int PositionX { get; set; }

        public ConsoleColor Color { get; set; }

        public bool IsAdded { get; set; } = false;
    }
}
