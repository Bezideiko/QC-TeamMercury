namespace BattleFieldNamespace
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// console-based implementation of the game "Battle Field" in which the player tries to clean 
    /// a matrix of numbers and empty cells by series of explosions which detonate areas of different sizes. 
    /// </summary>
    public class BattleFieldGame
    {
        /// <summary>
        /// The maximum size of a Battle Field
        /// </summary>
        public const int MaxFieldSize = 10;

        /// <summary>
        /// The battlefield of the game
        /// </summary>
        private readonly BattleField battleField;

        /// <summary>
        /// Private constructor forcing the use of CreateBattleFieldGameFactory() method
        /// </summary>
        public BattleFieldGame(int battleFieldSize)
        {
            this.battleField = new BattleField(battleFieldSize);
        }

        /// <summary>
        /// Creating new game, depending on user's input
        /// Start playing created game
        /// </summary>
        public static void Main(string[] args)
        {
            BattleFieldGame newBattleFieldGame = BattleFieldGame.CreateBattleFieldGameFactory();
            newBattleFieldGame.GameSession();
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

        /// <summary>
        /// Game Welcome Screen
        /// User is asked to enter a number for BattleField Size
        /// Checkings if the input is correct
        /// </summary>
        /// <returns>Number for creating BattleField</returns>
        public static int InputFieldSize()
        {
            int readNumber;

            //Display welcome message
            Console.WriteLine("Welcome to \"Battle Field\" game!");
            Console.Write("Enter battle field size (no more than 10): n = ");

            bool isCorrectlyFormattedInput = false;

            isCorrectlyFormattedInput = int.TryParse(Console.ReadLine(), out readNumber);

            while (!isCorrectlyFormattedInput || readNumber <= 0 || readNumber > MaxFieldSize)
            {
                Console.WriteLine("The input is wrong. Please enter an integer for field size (0 < N <= 10): ");

                isCorrectlyFormattedInput = int.TryParse(Console.ReadLine(), out readNumber);
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

            Debug.Assert(this.battleField.GameFieldSize > 0 && this.battleField.GameFieldSize <= MaxFieldSize,
            "Asserting the the battleField is initialized and in the correct size.");

            //Initial display of the battleField
            Console.WriteLine(this.battleField.ToString());

            //Main Game Cycle
            while (!this.IsGameOver())
            {
                this.PlayBattleFieldGameTurn();
            }

            //Display End of Game message
            Console.WriteLine("Game Over. Detonated Mines: {0}", this.battleField.DetonatedBombs);
        }

        /// <summary>
        /// Playing game
        /// When correct input data is set, bomb explosion is performed
        /// If explosion is successful, the battlefield is reprinted
        /// </summary>
        public void PlayBattleFieldGameTurn()
        {
            int inputRow = -1;
            int inputColumn = -1;

            //Read User's input
            try
            {
                this.ReadUserInput(ref inputRow, ref inputColumn);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
                return;
            }

            ////Validate User's input
            if (!this.IsInputCoordinatesInRange(inputRow, inputColumn))
            {
                Console.WriteLine("Invalid coordinates entered. Please enter valid coordinates.");
                return;
            }

            ////Perform explosion
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

        /// <summary>
        /// Reading and storing user's input data
        /// </summary>
        /// <param name="intputRow">Coordinate for row</param>
        /// <param name="inputColumn">Coordinate for column</param>
        public void ReadUserInput(ref int intputRow, ref int inputColumn)
        {
            Console.Write("Please Enter Coordinates: ");

            string inputRowAndColumn = Console.ReadLine();
            string[] rowAndColumnSplit = inputRowAndColumn.Split(' ');

            if (rowAndColumnSplit.Length != 2)
            {
                throw new FormatException("Input in incorrect format. Please use the followin format: [number number]");
            }

            bool isInputRowInCorrectFormat = int.TryParse(rowAndColumnSplit[0], out intputRow);
            if (!isInputRowInCorrectFormat)
            {
                throw new FormatException("Input in incorrect format. Please use the followin format: [number number]");
            }

            bool isInputColumnInCorrectFormat = int.TryParse(rowAndColumnSplit[1], out inputColumn);
            if (!isInputColumnInCorrectFormat)
            {
                throw new FormatException("Input in incorrect format. Please use the followin format: [number number]");
            }
        }
    }
}
