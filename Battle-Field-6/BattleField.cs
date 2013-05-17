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

        public int InitialBombsCount
        {
            get
            {
                return this.initialBombsCount;
            }
        }

        public int DetonatedBombs
        {
            get
            {
                return this.detonatedBombs;
            }
        }

        public int RemovedBombsCount
        {
            get
            {
                return this.removedBombsCount;
            }
        }

        public int GameFieldSize
        {
            get
            {
                return this.gameFieldSize;
            }
        }

        ///<summary>
        ///Property for accessing Game Field elements
        ///needed for testing purposes
        ///</summary>
        public string this[int indexRow, int indexCol]
        {
            get
            {
                return this.gameField[indexRow, indexCol];
            }
        }

        /// <summary>
        /// Initializying Battle Field
        /// by initializing firstly empty battle field
        /// and after generating random battle field
        /// </summary>
        public void InitilizeBattleField()
        {
            this.InitializeEmptyBattleField();

            this.GenerateRandomBattleField();
        }

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
        /// Generating random Battle Field
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

        public void BombOne(int row, int column)
        {
            gameField[row, column] = "X";
            removedBombsCount++;

            RemoveBombIfPossible(row - 1, column - 1);
            RemoveBombIfPossible(row + 1, column - 1);
            RemoveBombIfPossible(row - 1, column + 1);
            RemoveBombIfPossible(row + 1, column + 1);
        }

        public void BombTwo(int row, int column)
        {
            BombOne(row, column);

            RemoveBombIfPossible(row - 1, column);
            RemoveBombIfPossible(row, column - 1);
            RemoveBombIfPossible(row, column + 1);
            RemoveBombIfPossible(row + 1, column);
        }

        public void BombThree(int row, int column)
        {
            BombTwo(row, column);

            RemoveBombIfPossible(row - 2, column);
            RemoveBombIfPossible(row, column - 2);
            RemoveBombIfPossible(row, column + 2);
            RemoveBombIfPossible(row + 2, column);
        }

        public void BombFour(int row, int column)
        {
            BombThree(row, column);

            RemoveBombIfPossible(row - 1, column - 2);
            RemoveBombIfPossible(row + 1, column - 2);
            RemoveBombIfPossible(row - 2, column - 1);
            RemoveBombIfPossible(row + 2, column - 1);
            RemoveBombIfPossible(row - 2, column + 1);
            RemoveBombIfPossible(row + 2, column + 1);
            RemoveBombIfPossible(row - 1, column + 2);
            RemoveBombIfPossible(row + 1, column + 2);

        }

        public void BombFive(int row, int column)
        {
            BombFour(row, column);

            RemoveBombIfPossible(row - 2, column - 2);
            RemoveBombIfPossible(row + 2, column - 2);
            RemoveBombIfPossible(row - 2, column + 2);
            RemoveBombIfPossible(row + 2, column + 2);
        }

        /// <summary>
        /// Removing Bomb from position on the battleField
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
        /// Performing bomb explosion
        /// Initially we admit the explosion is successfull
        /// </summary>
        /// <returns>Is the explosion successfull or not</returns>
        public bool MineCell(int row, int column)
        {
            bool isExplosionSuccessfull = true;
            int cellNumber;

            if ((gameField[row, column] == "X") || ((gameField[row, column]) == "-"))
            {
                cellNumber = 0;
            }
            else
            {
                cellNumber = Convert.ToInt32(gameField[row, column]);
            }

            switch (cellNumber)
            {
                case 1:
                    {
                        BombOne(row, column);
                        detonatedBombs++;
                        break;
                    }
                case 2:
                    {
                        BombTwo(row, column);
                        detonatedBombs++;
                        break;
                    }
                case 3:
                    {
                        BombThree(row, column);
                        detonatedBombs++;
                        break;
                    }
                case 4:
                    {
                        BombFour(row, column);
                        detonatedBombs++;
                        break;
                    }
                case 5:
                    {
                        BombFive(row, column);
                        detonatedBombs++;
                        break;
                    }
                default:
                    {
                        isExplosionSuccessfull = false;
                        break;
                    }
            }

            return isExplosionSuccessfull;
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
