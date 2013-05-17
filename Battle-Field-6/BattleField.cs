namespace BattleFieldNamespace
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class representing the Game field, storing bombs and actions performed on the battleField
    /// </summary>
    internal class BattleField
    {
        //size for the BattleField
        private readonly int gameFieldSize;

        private readonly Random randomGenerator;

        //array of explosion patterns methods
        private readonly ExplosionPatternsDelegate[] explosionPatterns = new ExplosionPatternsDelegate[5];

        //Restrictions on Bomb Ratio
        private const double MaxBombRation = 0.3;
        private const double MinBombRatio = 0.15;

        //BattleField
        private string[,] gameField;

        //removed bombs so far
        private int removedBombsCount = 0;

        //user detonated bombs
        private int detonatedBombs = 0;

        //readonly since it is never changed
        private int initialBombsCount;

        /// <summary>
        /// Initialize a new instance of Battle Field
        /// </summary>
        /// <param name="battleFieldSize">gets size of the Battle Field</param>
        public BattleField(int battleFieldSize)
        {
            this.gameFieldSize = battleFieldSize;
            this.randomGenerator = new Random();

            this.explosionPatterns[0] = this.ExplosionPatternOne;
            this.explosionPatterns[1] = this.ExplosionPatternTwo;
            this.explosionPatterns[2] = this.ExplosionPatternThree;
            this.explosionPatterns[3] = this.ExplosionPatternFour;
            this.explosionPatterns[4] = this.ExplosionPatternFive;
        }

        //delegate for explosion patterns
        public delegate void ExplosionPatternsDelegate(int row, int column);

        /// <summary>
        /// Gets Initial Bombs located on the BattleField
        /// </summary>
        public int InitialBombsCount
        {
            get
            {
                return this.initialBombsCount;
            }
        }

        /// <summary>
        /// Gets CurrentDetonatedBombs by the user
        /// </summary>
        public int DetonatedBombs
        {
            get
            {
                return this.detonatedBombs;
            }
        }

        /// <summary>
        /// Gets Current total bombs detonated, from the user and from secondary explosions
        /// </summary>
        public int RemovedBombsCount
        {
            get
            {
                return this.removedBombsCount;
            }
        }

        /// <summary>
        /// Gets Size of the Battle Field
        /// </summary>
        public int GameFieldSize
        {
            get
            {
                return this.gameFieldSize;
            }
        }

        ///<summary>
        ///Property for accessing Game Field elements
        ///</summary>
        public string this[int indexRow, int indexCol]
        {
            get
            {
                return this.gameField[indexRow, indexCol];
            }
        }

        /// <summary>
        /// Initializying Battle Field with randomly placed bombs with ration 15%-30%
        /// </summary>
        public void InitilizeBattleField()
        {
            this.InitializeEmptyBattleField();

            this.GenerateRandomBattleField();
        }

        /// <summary>
        /// Performing user initiated bomb explosion
        /// </summary>
        /// <returns>Return true if the explosion was successfull, false if no bomb is hit.</returns>
        public bool MineCell(int row, int column)
        {
            //Check whether a bomb is hit
            if (gameField[row, column] == "X" || gameField[row, column] == "-")
            {
                //If no bomb is hit return false
                return false;
            }

            int explosionPattern = Convert.ToInt32(gameField[row, column]);

            explosionPatterns[explosionPattern - 1](row, column);

            this.detonatedBombs++;

            return true;
        }

        /// <summary>
        /// Creates a string with the whole battleField with the requested format
        /// </summary>
        /// <returns>Result in String format</returns>
        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();

            resultString.Append(Environment.NewLine);
            resultString.Append("   ");

            for (int topIndex = 0; topIndex <= gameFieldSize - 1; topIndex++)
            {
                resultString.Append(topIndex).Append(" ");
            }

            resultString.Append(Environment.NewLine);
            resultString.Append("   ");

            for (int index = 0; index <= gameFieldSize - 1; index++)
            {
                resultString.Append("--");

            }

            resultString.Append(Environment.NewLine);

            for (int rowIndex = 0; rowIndex <= gameFieldSize - 1; rowIndex++)
            {
                resultString.Append(rowIndex).Append("| ");
                for (int columnIndex = 0; columnIndex <= gameFieldSize - 1; columnIndex++)
                {
                    resultString.Append(gameField[rowIndex, columnIndex]).Append(" ");
                }

                resultString.Append(Environment.NewLine);
            }

            return resultString.ToString();
        }

        /// <summary>
        /// Initializying empty Battle Field
        /// </summary>
        private void InitializeEmptyBattleField()
        {
            this.gameField = new string[gameFieldSize, gameFieldSize];
            for (int row = 0; row <= gameFieldSize - 1; row++)
            {
                for (int column = 0; column <= this.gameFieldSize - 1; column++)
                {
                    this.gameField[row, column] = "-";
                }
            }
        }

        /// <summary>
        /// Get random integer in inclusive interval
        /// </summary>
        /// <param name="min">minimum result integer number</param>
        /// <param name="max">maximum result integer number </param>
        private int GetRandomNumberInRange(int min, int max)
        {
            return this.randomGenerator.Next(min, max + 1);
        }

        /// <summary>
        /// Fill Battle Field with Bombs (with ration 15%-30%) on random positions
        /// </summary>
        private void GenerateRandomBattleField()
        {
            int row;
            int column;
            int bombsAdded = 0;
            int stopGeneration = 0;

            while (bombsAdded + 1 <= MaxBombRation * this.gameFieldSize * this.gameFieldSize)
            {
                row = GetRandomNumberInRange(0, this.gameFieldSize - 1);
                column = GetRandomNumberInRange(0, this.gameFieldSize - 1);

                if (this.gameField[row, column] == "-")
                {
                    this.gameField[row, column] = Convert.ToString(GetRandomNumberInRange(1, 5));
                    bombsAdded++;
                    if (bombsAdded >= MinBombRatio * gameFieldSize * gameFieldSize)
                    {
                        stopGeneration = GetRandomNumberInRange(0, 1);
                        if (stopGeneration == 1)
                        {
                            break;
                        }
                    }
                }
            }

            this.initialBombsCount = bombsAdded;
        }

        //Explosion patterns
        private void ExplosionPatternOne(int row, int column)
        {
            this.RemoveBombIfPossible(row, column);
            this.RemoveBombIfPossible(row - 1, column - 1);
            this.RemoveBombIfPossible(row + 1, column - 1);
            this.RemoveBombIfPossible(row - 1, column + 1);
            this.RemoveBombIfPossible(row + 1, column + 1);
        }

        private void ExplosionPatternTwo(int row, int column)
        {
            for (int rowIndex = row - 1; rowIndex <= row + 1; rowIndex++)
            {
                for (int columnIndex = column - 1; columnIndex <= column + 1; columnIndex++)
                {
                    this.RemoveBombIfPossible(rowIndex, columnIndex);
                }
            }
        }

        private void ExplosionPatternThree(int row, int column)
        {
            for (int rowIndex = row - 1; rowIndex <= row + 1; rowIndex++)
            {
                for (int columnIndex = column - 1; columnIndex <= column + 1; columnIndex++)
                {
                    this.RemoveBombIfPossible(rowIndex, columnIndex);
                }
            }

            this.RemoveBombIfPossible(row - 2, column);
            this.RemoveBombIfPossible(row, column - 2);
            this.RemoveBombIfPossible(row, column + 2);
            this.RemoveBombIfPossible(row + 2, column);
        }

        private void ExplosionPatternFour(int row, int column)
        {
            for (int rowIndex = row - 1; rowIndex <= row + 1; rowIndex++)
            {
                for (int columnIndex = column - 2; columnIndex <= column + 2; columnIndex++)
                {
                    this.RemoveBombIfPossible(rowIndex, columnIndex);
                }
            }

            this.RemoveBombIfPossible(row - 2, column - 1);
            this.RemoveBombIfPossible(row - 2, column);
            this.RemoveBombIfPossible(row - 2, column + 1);
            this.RemoveBombIfPossible(row + 2, column - 1);
            this.RemoveBombIfPossible(row + 2, column);
            this.RemoveBombIfPossible(row + 2, column + 1);
        }

        private void ExplosionPatternFive(int row, int column)
        {
            for (int rowIndex = row - 2; rowIndex <= row + 2; rowIndex++)
            {
                for (int columnIndex = column - 2; columnIndex <= column + 2; columnIndex++)
                {
                    this.RemoveBombIfPossible(rowIndex, columnIndex);
                }
            }
        }

        /// <summary>
        /// Removing a bomb from position on the battleField
        /// </summary>
        private void RemoveBombIfPossible(int row, int column)
        {
            //Check for correct position
            if (row < 0 || row > this.gameFieldSize - 1)
            {
                return;
            }

            if (column < 0 || column > this.gameFieldSize - 1)
            {
                return;
            }

            //Check for a Bomb
            if (this.gameField[row, column] != "X" && this.gameField[row, column] != "-")
            {
                //Remove a Bomb
                this.removedBombsCount++;
                this.gameField[row, column] = "X";
            }
        }
    }
}
