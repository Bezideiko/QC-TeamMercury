using System;
using System.Linq;

namespace BattleFieldNamespace
{
    class BattleFieldGame
    {
        /// <summary>
        /// The battlefield of the game
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
        /// Reference to the BattleField, for testing purposes
        /// </summary>
        public BattleField BattleField
        {
            get
            {
                return this.battleField;
            }
        }

        /// <summary>
        /// Factory Design Pattern used to separate game creation from gameplay
        /// Gets FieldSize from user's input
        /// </summary>
        /// <returns>New game created with the user's input data</returns>
        public static BattleFieldGame CreateBattleFieldGameFactory()
        {
            //read user's input for the game field size
            int gameFieldSize = BattleFieldGame.InputFieldSize();

            return new BattleFieldGame(gameFieldSize);
        }

        private const int MaxFieldSize = 10;

        /// <summary>
        /// Game Welcome Screen
        /// User is asked to enter a number for BattleField Size
        /// Checkings if the input is correct
        /// </summary>
        /// <returns>Number for creating BattleField</returns>
        private static int InputFieldSize()
        {
            int readNumber;

            //Display welcome message
            Console.WriteLine("Welcome to \"Battle Field\" game!");
            Console.Write("Enter battle field size (no more than 10): n = ");

            while (!int.TryParse(Console.ReadLine(), out readNumber) || readNumber <= 0 || readNumber > MaxFieldSize)
            {
                Console.WriteLine("The input is wrong. Please enter an integer - field size (0 < N <= 10): ");
            }

            return readNumber;
        }

        /// <summary>
        /// Checks whether the end game condition is fulfilled. 
        /// </summary>
        /// <returns>True if all the bombs in the battlefield have exploded.</returns>
        public bool IsGameOver()
        {
            return this.battleField.RemovedBombsCount == this.battleField.InitialBombsCount;
        }

        /// <summary>
        /// Checks if input data coordinates for row and column are in the desired range
        /// </summary>
        /// <param name="row">Gets a row coordinate</param>
        /// <param name="column">Gets a column coordinate</param>
        /// <returns>True (in the range) or false (out of range coordinates)</returns>
        public bool IsInputCoordinatesInRange(int row, int column)
        {
            bool isRowInCorrectRange = (row >= 0) && (row <= this.battleField.GameFieldSize - 1);
            bool isColumnInCorrectRange = (column >= 0) && (column <= this.battleField.GameFieldSize - 1);

            bool isCorrectPositions = isRowInCorrectRange && isColumnInCorrectRange;
            return isCorrectPositions;
        }

        /// <summary>
        /// Initializing Battle Field
        /// Playing game until method IsOver() is true
        /// End game with message to user.
        /// </summary>
        public void GameSession()
        {
            //Initialize BattleField
            this.battleField.InitilizeBattleField();

            //Initial display of the battleField
            Console.WriteLine(this.battleField.ToString());

            //Main Game Cycle
            while (!(IsGameOver()))
            {
                PlayBattleFieldTurn();
            }

            //Display End of Game message
            Console.WriteLine("Game Over. Detonated Mines: {0}", this.battleField.DetonatedBombs);
        }

        /// <summary>
        /// Playing game
        /// When correct input data is set, bomb explosion is performed
        /// If explosion is successful, the battlefield is reprinted
        /// </summary>
        private void PlayBattleFieldTurn()
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

        /// <summary>
        /// Reading and storing user's input data
        /// </summary>
        /// <param name="intputRow">Coordinate for row</param>
        /// <param name="inputColumn">Coordinate for column</param>
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
