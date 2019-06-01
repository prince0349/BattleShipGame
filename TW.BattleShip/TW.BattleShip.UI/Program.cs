using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.BattleShip.BusinessServices;
using TW.BattleShip.Constants;

namespace TW.BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ThoughtWorks Battleship game!");
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine();
            Console.WriteLine("Please chhose your option:");
            Console.WriteLine("1. Read Inputs from File.");
            Console.WriteLine("2. Enter Inputs Mannually.");
            Console.WriteLine("3. Exit the game.");
            Console.WriteLine();

            //We can Implement dependency injection and object life management here
            var battleshipBoardService = new BattleshipBoardService();
            var gamePlayBoardService = new GamePlayBoardService();
            var interpreter = new Interpreter(battleshipBoardService, gamePlayBoardService);

            Console.WriteLine("Please enter your option");
            int option = 0;
            bool isValidOption = false;
            while (isValidOption == false)
            {
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine(ApplicationConstants.InvalidOptionErrorMessage);                    
                }
                isValidOption = option == 1 || option == 2 || option ==3;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("File");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Please enter input manually");
                    Console.WriteLine("Type 'Exit' to exit the game.");
                    Console.Write("Enter width and height of battle area (for example: 5 E): ");
                    var exit = false;
                    while (exit == false)
                    {
                        var isContinue = interpreter.Parse(battleshipBoardService.GetCurrentInput() + Console.ReadLine());
                        exit = isContinue;
                    }
                    break;
                default:
                    Console.WriteLine("Thank you for playing, ThoughtWorks Battleship game!\n Press any key to Exit:");
                    Console.ReadKey();
                    break;
            }

        }
    }
}
