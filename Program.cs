using System;
using System.Threading;

namespace LifeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            setup();
        }

        static void setup()
        {
            LifeGame lifeGame = new LifeGame();
            lifeGame.Run();
        }
    }

    class LifeGame
    {
        string[,] Cells = new string[16, 16];

        const int x = 16;
        const int y = 16;

        const string livingCell = "●";
        const string dyingCell = "○";

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
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Cells[i, j] = dyingCell;
                }
            }
        }

        int ReturnAskResult()
        {
            bool result = false;
            int i = 0;
            while(result == false)
            {
                string text = Ask();
                result = int.TryParse(text, out i);
            }
            return i;
        }

        string Ask()
        {
            Console.WriteLine("Type Number you want to generate");
            string typeText = Console.ReadLine();
            return typeText;
        }

        void SetLivingCellsWithRandomNum(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Random r1 = new Random();
                int num1 = r1.Next(0, 16);
                int num2 = r1.Next(0, 16);

                SetLivingCells(num1, num2);
            }
        }

        void CheckAllCell()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
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

        int CheckNearLivingCells(int x,int y)
        {
            int trueCount = 0;
            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    if (GetCellCondition(i, j) ==livingCell)
                    {
                        trueCount++;
                    }
                }
            }

            return trueCount;
        } 

        bool SurvivalCheck(int x,int y)
        {
            if(GetCellCondition(x,y) == livingCell)
            {
                int livingCellsCount = CheckNearLivingCells(x, y);
                if (livingCellsCount == 2 || livingCellsCount == 3)
                {
                    return true;
                }
            }
            if(GetCellCondition(x, y) == dyingCell)
            {
                int livingCellsCount = CheckNearLivingCells(x, y);
                if (livingCellsCount == 3)
                {
                    return true;
                }
            }
            return false;
        }

        string GetCellCondition(int x,int y)
        {
            if(x >= 16 || y >= 16){ return dyingCell; }
            if(x < 0 || y < 0)
            {
                return dyingCell;
            }
            return Cells[x, y];
        } 


        void SetLivingCells(int x,int y)
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
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < y; j++)
                {
                    Console.Write(Cells[i, j]);
                }
            }
        }
    }
}
