using System;
using System.Linq;

namespace BattleFieldNamespace
{
    class BattleFieldGame
    {
        /// <summary>
        /// Reference to the BattleField.cs
        /// </summary>
        private readonly BattleField battleField;

        /// <summary>
        /// Private constructor forcing the use of CreateBattleFieldGameFactory() method
        /// </summary>
        private BattleFieldGame(int battleFieldSize)
        {
            this.battleField = new BattleField(battleFieldSize);
        }

        /// <summary>
        /// Factory Design Pattern used to separate game creation from gameplay
        /// Gets FieldSize from user's input
        /// </summary>
        /// <returns>New game created with the user's input data</returns>
        public static BattleFieldGame CreateBattleFieldGameFactory()
        {
            int gameFieldSize = BattleFieldGame.InputFieldSize();

            return new BattleFieldGame(gameFieldSize);
        }

        /// <summary>
        /// Game Welcome Screen
        /// User is asked to enter a number for BattleField Size
        /// Checkings if the input is correct
        /// </summary>
        /// <returns>Number for creating BattleField</returns>
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
        public bool IsInputCoordinatesInRange(int row, int column)
        {
            if ((row >= 0) && (row <= this.battleField.GameFieldSize - 1) && (column >= 0) && (column <= this.battleField.GameFieldSize - 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GameSession()
        {
            //Initialize Battle Field
            this.battleField.InitilizeBattleField();

            Console.WriteLine(this.battleField.ToString());

            while (!(IsOver()))
            {
                PlayBattleField();
            }

            Console.WriteLine("Game Over. Detonated Mines: {0}", this.battleField.DetonatedBombs);
        }

        /// <summary>
        /// When correct input data is set, bomb explosion is performed
        /// If explosion is successful, the battlefield is printed
        /// </summary>
        private void PlayBattleField()
        {
            int inputRow = -1;
            int inputColumn = -1;

            bool isCorrectUserInput = this.ReadUserInput(ref inputRow, ref inputColumn);

            if (isCorrectUserInput)
            {
                bool isExplosionSuccessfull = this.battleField.MineCell(inputRow, inputColumn);

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

        private bool ReadUserInput(ref int intputRow, ref int inputColumn)
        {
            Console.Write("Please Enter Coordinates: ");

            string inputRowAndColumn = Console.ReadLine();
            string[] rowAndColumnSplit = inputRowAndColumn.Split(' ');

            if ((rowAndColumnSplit.Length) <= 1)
            {
                intputRow = -1;
                inputColumn = -1;
            }
            else
            {
                if (!(int.TryParse(rowAndColumnSplit[0], out intputRow)))
                {
                    intputRow = -1;
                }

                if (!(int.TryParse(rowAndColumnSplit[1], out inputColumn)))
                {
                    inputColumn = -1;
                }
            }

            if (IsInputCoordinatesInRange(intputRow, inputColumn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creating new game, depending on user's input
        /// Start playing created game
        /// </summary>
        static void Main(string[] args)
        {
            BattleFieldGame newBattleFieldGame = BattleFieldGame.CreateBattleFieldGameFactory();
            newBattleFieldGame.GameSession();
        }
    }
}
