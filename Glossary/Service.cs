using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary
{
    public class Service
    {
        private Frontend _front;
        private Backend _backend;

        public void StartGame()
        {
            string answer;
            while (true)
            {
                _front = new();
                _front.Start();
                _backend = new(_front.Width,_front.Height, _front.Colors);
                _backend.Start();
                
                if (PlayAgain())
                    break;
            }

        }

        private bool PlayAgain()
        {
            string answer;
            while (true)
            {
                Console.WriteLine("Хотите начать заново? Напишите 'y' - да, 'n' - нет");
                answer = Console.ReadLine();

                switch (answer)
                {
                    case "n":
                        return true;
                    case "y":
                        return false;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Введите одну из указанных букв!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }
    }
}
