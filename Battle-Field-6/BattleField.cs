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

        //readonly since it is never changed
        private int initialBombsCount;

        public BattleField(int battleFieldSize)
        {
            this.gameFieldSize = battleFieldSize;
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

        //Property for accessing the elements of the Game fiel
        //needed for testing purposes
        public string this[int indexRow, int indexCol]
        {
            get
            {
                return this.gameField[indexRow, indexCol];
            }
        }

        internal void CreateBattleTable()
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

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return Convert.ToInt32(random.Next(min, max));
        }


        public void FillInTheFields()
        {
            int row;
            int column;

            int bombsAdded = 0;

            while (bombsAdded + 1 <= 0.3 * gameFieldSize * gameFieldSize)
            {
                row = RandomNumber(0, gameFieldSize - 1);
                column = RandomNumber(0, gameFieldSize - 1);

                if (gameField[row, column] == "-")
                {
                    gameField[row, column] = Convert.ToString(RandomNumber(1, 6));
                    bombsAdded++;

                    if (bombsAdded >= 0.15 * gameFieldSize * gameFieldSize)
                    {
                        int stopFilling = RandomNumber(0, 1);
                        if (stopFilling == 1)
                        {
                            break;
                        }
                    }
                }
            }

            //Setting the readolny variable its final value
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

        //Perform an explosion
        public bool MineCell(int row, int column)
        {
            //By  default the explosion is successfull
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

        //Creating a string with the whole battleField with the requested format
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
