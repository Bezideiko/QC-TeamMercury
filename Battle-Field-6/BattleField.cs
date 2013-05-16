using System;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
{
    internal class BattleField
    {
        private string[,] gameField;

        private readonly int gameFieldSize;

        private int removedBombsCount = 0;

        private int detonatedBombs = 0;

        private readonly Random randomGenerator;

        //readonly since it is never changed
        private int initialBombsCount;

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

        public void InitilizeBattleField()
        {
            this.InitializeEmptyBattleField();
            this.GenerateRandomBattleField();
        }

        private  void InitializeEmptyBattleField()
        {
            gameField = new string[gameFieldSize, gameFieldSize];
            for (int i = 0; i <= gameFieldSize - 1; i++)
            {
                for (int j = 0; j <= gameFieldSize - 1; j++)
                {
                    gameField[i, j] = "-";
                }
            }
        }

        private int GetRandomNumberInRange(int min, int max)
        {
            return this.randomGenerator.Next(min, max + 1);
        }

        private void GenerateRandomBattleField()
        {
            int row;
            int column;
            int bombsAdded = 0;

            while (bombsAdded + 1 <= 0.3 * gameFieldSize * gameFieldSize)
            {
                row = GetRandomNumberInRange(0, gameFieldSize - 1);
                column = GetRandomNumberInRange(0, gameFieldSize - 1);

                if (gameField[row, column] == "-")
                {
                    gameField[row, column] = Convert.ToString(GetRandomNumberInRange(1, 5));
                    bombsAdded++;

                    if (bombsAdded >= 0.15 * gameFieldSize * gameFieldSize)
                    {
                        int stopFilling = GetRandomNumberInRange(0, 1);
                        if (stopFilling == 1)
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
            if ((row - 1 >= 0) && (column - 1 >= 0))
            {
                if (gameField[row - 1, column - 1] != "X" && gameField[row - 1, column - 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 1, column - 1] = "X";
            }

            if ((row + 1 <= gameFieldSize - 1) && (column - 1 >= 0))
            {
                if (gameField[row + 1, column - 1] != "X" && gameField[row + 1, column - 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 1, column - 1] = "X";
            }

            if ((row - 1 >= 0) && (column + 1 <= gameFieldSize - 1))
            {
                if (gameField[row - 1, column + 1] != "X" && gameField[row - 1, column + 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 1, column + 1] = "X";
            }

            if ((row + 1 <= gameFieldSize - 1) && (column + 1 <= gameFieldSize - 1))
            {
                if (gameField[row + 1, column + 1] != "X" && gameField[row + 1, column + 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 1, column + 1] = "X";
            }
        }

        public void BombTwo(int row, int column)
        {
            BombOne(row, column);

            if (row - 1 >= 0)
            {
                if (gameField[row - 1, column] != "X" && gameField[row - 1, column] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 1, column] = "X";
            }

            if (column - 1 >= 0)
            {
                if (gameField[row, column - 1] != "X" && gameField[row, column - 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row, column - 1] = "X";
            }

            if (column + 1 <= gameFieldSize - 1)
            {
                if (gameField[row, column + 1] != "X" && gameField[row, column + 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row, column + 1] = "X";
            }

            if (row + 1 <= gameFieldSize - 1)
            {
                if (gameField[row + 1, column] != "X" && gameField[row + 1, column] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 1, column] = "X";
            }
        }

        public void BombThree(int row, int column)
        {
            BombTwo(row, column);

            if (row - 2 >= 0)
            {
                if (gameField[row - 2, column] != "X" && gameField[row - 2, column] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 2, column] = "X";
            }

            if (column - 2 >= 0)
            {
                if (gameField[row, column - 2] != "X" && gameField[row, column - 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row, column - 2] = "X";
            }

            if (column + 2 <= gameFieldSize - 1)
            {
                if (gameField[row, column + 2] != "X" && gameField[row, column + 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row, column + 2] = "X";
            }

            if (row + 2 <= gameFieldSize - 1)
            {
                if (gameField[row + 2, column] != "X" && gameField[row + 2, column] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 2, column] = "X";
            }
        }

        public void BombFour(int row, int column)
        {
            BombThree(row, column);

            if ((row - 1 >= 0) && (column - 2 >= 0))
            {
                if (gameField[row - 1, column - 2] != "X" && gameField[row - 1, column - 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 1, column - 2] = "X";
            }

            if ((row + 1 <= gameFieldSize - 1) && (column - 2 >= 0))
            {
                if (gameField[row + 1, column - 2] != "X" && gameField[row + 1, column - 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 1, column - 2] = "X";
            }

            if ((row - 2 >= 0) && (column - 1 >= 0))
            {
                if (gameField[row - 2, column - 1] != "X" && gameField[row - 2, column - 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 2, column - 1] = "X";
            }

            if ((row + 2 <= gameFieldSize - 1) && (column - 1 >= 0))
            {
                if (gameField[row + 2, column - 1] != "X" && gameField[row + 2, column - 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 2, column - 1] = "X";
            }

            if ((row - 1 >= 0) && (column + 2 <= gameFieldSize - 1))
            {
                if (gameField[row - 1, column + 2] != "X" && gameField[row - 1, column + 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 1, column + 2] = "X";
            }

            if ((row + 1 <= gameFieldSize - 1) && (column + 2 <= gameFieldSize - 1))
            {
                if (gameField[row + 1, column + 2] != "X" && gameField[row + 1, column + 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 1, column + 2] = "X";
            }

            if ((row - 2 >= 0) && (column + 1 <= gameFieldSize - 1))
            {
                if (gameField[row - 2, column + 1] != "X" && gameField[row - 2, column + 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 2, column + 1] = "X";
            }

            if ((row + 2 <= gameFieldSize - 1) && (column + 1 <= gameFieldSize - 1))
            {
                if (gameField[row + 2, column + 1] != "X" && gameField[row + 2, column + 1] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 2, column + 1] = "X";
            }
        }

        public void BombFive(int row, int column)
        {
            BombFour(row, column);

            if ((row - 2 >= 0) && (column - 2 >= 0))
            {
                if (gameField[row - 2, column - 2] != "X" && gameField[row - 2, column - 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 2, column - 2] = "X";
            }

            if ((row + 2 <= gameFieldSize - 1) && (column - 2 >= 0))
            {
                if (gameField[row + 2, column - 2] != "X" && gameField[row + 2, column - 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 2, column - 2] = "X";
            }

            if ((row - 2 >= 0) && (column + 2 <= gameFieldSize - 1))
            {
                if (gameField[row - 2, column + 2] != "X" && gameField[row - 2, column + 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row - 2, column + 2] = "X";
            }

            if ((row + 2 <= gameFieldSize - 1) && (column + 2 <= gameFieldSize - 1))
            {
                if (gameField[row + 2, column + 2] != "X" && gameField[row + 2, column + 2] != "-")
                {
                    removedBombsCount++;
                }
                gameField[row + 2, column + 2] = "X";
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
        /// <returns>Result intring format</returns>
        public override string ToString()
        {
            StringBuilder resultString = new StringBuilder();

            resultString.Append(Environment.NewLine);
            resultString.Append("   ");

            for (int k = 0; k <= gameFieldSize - 1; k++)
            {
                resultString.Append(k).Append(" ");
            }
            resultString.Append(Environment.NewLine);
            resultString.Append("   ");

            for (int k = 0; k <= gameFieldSize - 1; k++)
            {
                resultString.Append("--");
            }
            resultString.Append(Environment.NewLine);

            for (int i = 0; i <= gameFieldSize - 1; i++)
            {
                resultString.Append(i).Append("| ");
                for (int j = 0; j <= gameFieldSize - 1; j++)
                {
                    resultString.Append(gameField[i, j]).Append(" ");
                }

                resultString.Append(Environment.NewLine);
            }

            return resultString.ToString();
        }
    }
}
