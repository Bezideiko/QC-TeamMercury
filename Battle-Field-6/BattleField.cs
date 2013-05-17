using System;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
{
    internal class BattleField
    {
        //Restrictions on Bomb Ratio
        private const double MaxBombRation = 0.3;
        private const double MinBombRatio = 0.15;

        private string[,] gameField;

        private readonly int gameFieldSize;

        private int removedBombsCount = 0;

        private int detonatedBombs = 0;

        private readonly Random randomGenerator;

        //readonly since it is never changed
        private int initialBombsCount;

        /// <summary>
        /// Constructor for new Battle Field
        /// </summary>
        /// <param name="battleFieldSize">gets size of the Battle Field</param>
        public BattleField(int battleFieldSize)
        {
            this.gameFieldSize = battleFieldSize;
            this.randomGenerator = new Random();
        }

        /// <summary>
        /// Initial Bombs located on the BattleField
        /// </summary>
        public int InitialBombsCount
        {
            get
            {
                return this.initialBombsCount;
            }
        }

        /// <summary>
        /// CurrentDetonatedBombs by the user
        /// </summary>
        public int DetonatedBombs
        {
            get
            {
                return this.detonatedBombs;
            }
        }

        /// <summary>
        /// Current total bombs detonated, from the user and from secondary explosions
        /// </summary>
        public int RemovedBombsCount
        {
            get
            {
                return this.removedBombsCount;
            }
        }

        /// <summary>
        /// Size of the Battle Field
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
        /// Initializying empty Battle Field
        /// </summary>
        private void InitializeEmptyBattleField()
        {
            gameField = new string[gameFieldSize, gameFieldSize];
            for (int row = 0; row <= gameFieldSize - 1; row++)
            {
                for (int column = 0; column <= gameFieldSize - 1; column++)
                {
                    gameField[row, column] = "-";
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

            while (bombsAdded + 1 <= MaxBombRation * gameFieldSize * gameFieldSize)
            {
                row = GetRandomNumberInRange(0, gameFieldSize - 1);
                column = GetRandomNumberInRange(0, gameFieldSize - 1);

                if (gameField[row, column] == "-")
                {
                    gameField[row, column] = Convert.ToString(GetRandomNumberInRange(1, 5));
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
        public void ExplosionPatternOne(int row, int column)
        {
            gameField[row, column] = "X";
            removedBombsCount++;

            RemoveBombIfPossible(row - 1, column - 1);
            RemoveBombIfPossible(row + 1, column - 1);
            RemoveBombIfPossible(row - 1, column + 1);
            RemoveBombIfPossible(row + 1, column + 1);
        }

        public void ExplosionPatternTwo(int row, int column)
        {
            ExplosionPatternOne(row, column);

            RemoveBombIfPossible(row - 1, column);
            RemoveBombIfPossible(row, column - 1);
            RemoveBombIfPossible(row, column + 1);
            RemoveBombIfPossible(row + 1, column);
        }

        public void ExplosionPatternThree(int row, int column)
        {
            ExplosionPatternTwo(row, column);

            RemoveBombIfPossible(row - 2, column);
            RemoveBombIfPossible(row, column - 2);
            RemoveBombIfPossible(row, column + 2);
            RemoveBombIfPossible(row + 2, column);
        }

        public void ExplosionPatternFour(int row, int column)
        {
            ExplosionPatternThree(row, column);

            RemoveBombIfPossible(row - 1, column - 2);
            RemoveBombIfPossible(row + 1, column - 2);
            RemoveBombIfPossible(row - 2, column - 1);
            RemoveBombIfPossible(row + 2, column - 1);
            RemoveBombIfPossible(row - 2, column + 1);
            RemoveBombIfPossible(row + 2, column + 1);
            RemoveBombIfPossible(row - 1, column + 2);
            RemoveBombIfPossible(row + 1, column + 2);

        }

        public void ExplosionPatternFive(int row, int column)
        {
            ExplosionPatternFour(row, column);

            RemoveBombIfPossible(row - 2, column - 2);
            RemoveBombIfPossible(row + 2, column - 2);
            RemoveBombIfPossible(row - 2, column + 2);
            RemoveBombIfPossible(row + 2, column + 2);
        }
        //End of Explosion Patterns

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
            if (gameField[row, column] != "X" && gameField[row, column] != "-")
            {
                //Remove a Bomb
                this.removedBombsCount++;
                gameField[row, column] = "X";
            }
        }

        /// <summary>
        /// Performing user initiated bomb explosion
        /// </summary>
        /// <returns>Return true if the explosion was successfull, false if no bomb is hit.</returns>
        public bool MineCell(int row, int column)
        {
            //Check whether a bomb is hit
            if ((gameField[row, column] == "X") || ((gameField[row, column]) == "-"))
            {
                //If no bomb is hit return false
                return false;
            }

            int explosionPattern = Convert.ToInt32(gameField[row, column]);;

            switch (explosionPattern)
            {
                case 1:
                    {
                        ExplosionPatternOne(row, column);
                        break;
                    }
                case 2:
                    {
                        ExplosionPatternTwo(row, column);
                        break;
                    }
                case 3:
                    {
                        ExplosionPatternThree(row, column);
                        break;
                    }
                case 4:
                    {
                        ExplosionPatternFour(row, column);
                        break;
                    }
                case 5:
                    {
                        ExplosionPatternFive(row, column);
                        break;
                    }
            }

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
    }
}
