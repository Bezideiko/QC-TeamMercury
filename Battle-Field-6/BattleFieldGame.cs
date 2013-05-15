using System;
using System.Linq;

namespace BattleFieldNamespace
{
    class BattleFieldGame
    {
        //The game holds a reference to the BattleField
        private readonly BattleField battleField;

        //Constructor is private, forcing the use of Factory method
        private BattleFieldGame(int battleFieldSize)
        {
            this.battleField = new BattleField(battleFieldSize);
        }

        //Factory Design pattern
        //used to separate the creation of teh game from the Gameplay
        public static BattleFieldGame CreateBattleFieldGameFactory()
        {
            //Get user input for the creation of the game
            int gameFieldSize = BattleFieldGame.InputFieldSize();

            //Create a Game
            return new BattleFieldGame(gameFieldSize);
        }

        private static int InputFieldSize()
        {
            int readNumber;
            Console.WriteLine("Welcome to \"Battle Field\" game!");
            Console.Write("Enter battle field size (no more than 10): n = ");
            
            while (!int.TryParse(Console.ReadLine(), out readNumber) || readNumber <= 0 || readNumber > 10)
            {
                Console.WriteLine("The input is wrong. Please enter an integer - field size (0 < N <= 10): ");
            }

            return readNumber;
        }

        //Renamed from Over()
        public bool IsOver()
        {
            return this.battleField.RemovedBombsCount == this.battleField.InitialBombsCount;
        }

        //Renamed from OutOfRangeCoordinates
        public bool IsOutOfRangeCoordinates(int row, int column)
        {
            if ((row >= 0) && (row <= this.battleField.GameFieldSize - 1) && (column >= 0) && (column <= this.battleField.GameFieldSize - 1))
            {
                return false;
            }

            return true;
        }

        public void GameSession()
        {
            this.battleField.InitilizeBattleField();

            Console.WriteLine(this.battleField.ToString());

            while (!(IsOver()))
            {
                PlayBattleField();
            }

            Console.WriteLine("Game Over. Detonated Mines: {0}", this.battleField.DetonatedBombs);
        }
  
        private void PlayBattleField()
        {
            bool isCorrectUserInput = this.ReadUserInput();

            int row;
            int column;

            Console.Write("Please Enter Coordinates: ");

            string inputRowAndColumn = Console.ReadLine();
            string[] rowAndColumnSplit = inputRowAndColumn.Split(' ');


            if ((rowAndColumnSplit.Length) <= 1)
            {
                row = -1; 
                column = -1;
            }
            else
            {
                if (!(int.TryParse(rowAndColumnSplit[0], out row)))
                {
                    row = -1;
                }

                if (!(int.TryParse(rowAndColumnSplit[1], out column)))
                {
                    column = -1;
                }
            }

            if ((IsOutOfRangeCoordinates(row, column)))
            {
                Console.WriteLine("This Move Is Out Of Area.");
            }
            else
            {
                bool isExplosionSuccessfull = this.battleField.MineCell(row, column);

                if (isExplosionSuccessfull)
                {
                    Console.WriteLine(this.battleField.ToString());
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
            }
        }

        private bool ReadUserInput()
        {
            return true;
        }

        static void Main(string[] args)
        {
            //Create a game, depending on user's input
            BattleFieldGame newBattleFieldGame = BattleFieldGame.CreateBattleFieldGameFactory();
            
            //Play the created game
            newBattleFieldGame.GameSession();
        }
    }
}
