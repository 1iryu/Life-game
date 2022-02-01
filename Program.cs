using System;
using System.Threading;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            LifeGame lifeGame = new LifeGame();
            lifeGame.Run();
        }
    }

    class LifeGame
    {

        const int mapSizeX = 30;
        const int mapSizeY = 30;

        const string livingCell = "■";
        const string dyingCell = "□";

        string[,] Cells = new string[mapSizeX, mapSizeY];

        public void Run()
        {
            InitCells();
            SetLivingCellsWithRandomNum(ReturnAskResult());
            KeepUpdate();
        }

        void KeepUpdate()
        {
            while (true)
            {
                UpdateCell();
            }
        }

        void InitCells()
        {
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    Cells[i, j] = dyingCell;
                }
            }
        }

        int ReturnAskResult()
        {
            bool result = false;
            int i = 0;
            while (result == false)
            {
                Console.WriteLine("Type Count you want to random generate(if count is small,nothing happend)");
                string typeText = Console.ReadLine();
                result = int.TryParse(typeText, out i);
            }
            return i;
        }

        void SetLivingCellsWithRandomNum(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Random r1 = new Random();
                int num1 = r1.Next(0, mapSizeX);
                int num2 = r1.Next(0, mapSizeY);

                SetLivingCells(num1, num2);
            }
        }

        void CheckAllCell()
        {
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    if (SurvivalCheck(i, j))
                    {
                        Cells[i, j] = livingCell;
                    }
                    else
                    {
                        Cells[i, j] = dyingCell;
                    }
                }
            }
        }

        int CheckNearLivingCells(int x, int y)
        {
            int trueCount = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (GetCellCondition(i, j) == livingCell)
                    {
                        if (i == x && j == y)
                        {
                            trueCount += 0;
                        }
                        else
                        {
                            trueCount++;
                        }
                    }
                }
            }

            return trueCount;
        }

        bool SurvivalCheck(int x, int y)
        {
            if (GetCellCondition(x, y) == livingCell)
            {
                int livingCellsCount = CheckNearLivingCells(x, y);
                if (livingCellsCount == 2 || livingCellsCount == 3)
                {
                    return true;
                }
            }
            if (GetCellCondition(x, y) == dyingCell)
            {
                int livingCellsCount = CheckNearLivingCells(x, y);
                if (livingCellsCount == 3)
                {
                    return true;
                }
            }
            return false;
        }

        string GetCellCondition(int xPos, int yPos)
        {
            if (xPos >= mapSizeX || yPos >= mapSizeY) { return dyingCell; }
            if (xPos < 0 || yPos < 0) { return dyingCell; }
            return Cells[xPos, yPos];
        }


        void SetLivingCells(int x, int y)
        {
            Cells[x, y] = livingCell;
        }

        void SetDyingCells(int x, int y)
        {
            Cells[x, y] = dyingCell;
        }

        void UpdateCell()
        {
            CheckAllCell();
            Thread.Sleep(500);
            Console.Clear();
            for (int i = 0; i < mapSizeX; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < mapSizeY; j++)
                {
                    Console.Write(Cells[i, j]);
                }
            }
        }
    }
}
