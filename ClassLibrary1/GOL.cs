using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GOL
    {
        Random rnd = new Random();
        st[,] grid = new st[40, 60];
        int gennum = 0;

        //Basic Console Setup (Color...Settings)
        public void ConsoleSetup()
        {
            Console.Title = "Travis Caro : Conway's Game Of Life!";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorVisible = false;
            Console.WindowWidth = 100;
            Console.WindowHeight = 50;
            Console.WriteLine("\n Generation: #" + gennum.ToString("000"));
        }

        //Gathers and parses user input
        static int GatherInput(int low = 0, int high = 5, string prompt = "Select Option ==> ")
        {
            //Input collection variable and user prompt
            int input;
            Console.Write(prompt);

            //This bulletproofs the input so that it loops until the input is a number within range
            while (!int.TryParse(Console.ReadLine(), out input) || input < low || input > high)
                Console.Write(" -- Invalid Input! --   " + prompt);

            return input;
        }

        //Blueprint for variables in struct
        public struct st
        {
            public int cur;
            public int fut;
            public int age;
        }

        //For every cell in array, sets all values to 0
        public void ArrayClear()
        {
            for (int i = 1; i < grid.GetLength(0) - 1; i++)
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    grid[i, j].cur = 0;
                    grid[i, j].fut = 0;
                    grid[i, j].age = 0;
                }
        }

        //Displays command with a prompt and calls check selection method
        public void StartSelection()
        {
            Console.SetCursorPosition(0, 45);
            int selection = GatherInput(0, 5, " 0 - Quit   1 - Random   2 - Glider   3 - R-Pentomino   4 - DieHard :  ");
            CheckSelection(selection);
        }

        //After the simulation has been ran number of generations desired, prompts user to continue or quit the simulation
        public void ContinueSelection(ref bool cont, ref bool restart)
        {
            Console.SetCursorPosition(0,50);

            int selection = GatherInput(0, 3, " Select option => 0 - Quit   1 - Continue   2 - Restart  3 - Get Statistics :  ");

            if (selection == 0)
                Quit();
            else if (selection == 1)
                cont = true;
            else if (selection == 2)
            {
                restart = true;
                gennum = 0;
                ArrayClear();
            }
            else if (selection == 3)
            {
                GetStats();
                ContinueSelection(ref cont, ref restart);
            }
        }

        //Calls appropriate method based upon user input
        private void CheckSelection(int selection)
        {
            if(selection == 0)
                Quit();
            if(selection == 1)
                Random();
            if(selection == 2)
                Glider();
            if(selection == 3)
                RPentomino();
            if (selection == 4)
                DieHard();
        }

        //Closes the Console
        private void Quit()
        {
            Console.Write("\n\n THANKS FOR PLAYING!  \n\n Press any key to exit...");
            Console.ReadKey();            
            Environment.Exit(0);
        }

        //Fills the array randomly switching the cells to either 1 (alive) or 0 (dead)
        private void Random()
        {
            
            for (int i = 1; i < grid.GetLength(0)-1; i++)
                for (int j = 1; j < grid.GetLength(1)-1; j++)
                {
                    grid[i, j].cur = rnd.Next(0, 2);
                    grid[i, j].fut = 0;
                    grid[i, j].age = 0;
                }
        }

        //Creates gliders on the grid
        private void Glider()
        {
            int i = 5, 
                j = 5;

            MakeGliderR(i, j);

            i = 10;
            j = 10;

            MakeGliderR(i, j);

            i = 25;
            j = 15;

            MakeGliderR(i, j);

            i = 5;
            j = 25;

            MakeGliderL(i, j);

            i = 10;
            j = 20;

            MakeGliderL(i, j);

            i = 25;
            j = 45;

            MakeGliderL(i, j);
        }
               
        //Makes a glider that moves down to the right
        private void MakeGliderR(int i, int j)
        {
            grid[i, j].cur = 1;
            grid[i + 1, j + 1].cur = 1;
            grid[i + 2, j + 1].cur = 1;
            grid[i + 2, j].cur = 1;
            grid[i + 2, j - 1].cur = 1;
        }

        //Makes a glider that moves down to the left
        private void MakeGliderL(int i, int j)
        {
            grid[i, j].cur = 1;
            grid[i + 1, j - 1].cur = 1;
            grid[i + 2, j - 1].cur = 1;
            grid[i + 2, j].cur = 1;
            grid[i + 2, j + 1].cur = 1;
        }

        //Creates the R-Pentomino layout
        private void RPentomino()
        {
            grid[20, 30].cur = 1;
            grid[20, 29].cur = 1;
            grid[19, 30].cur = 1;
            grid[21, 30].cur = 1;
            grid[19, 31].cur = 1;
        }
        
        //DieHard pattern
        private void DieHard()
        {
            grid[20, 30].cur = 1;
            grid[20, 31].cur = 1;
            grid[21, 31].cur = 1;
            grid[21, 35].cur = 1;
            grid[21, 36].cur = 1;
            grid[21, 37].cur = 1;
            grid[19, 36].cur = 1;
        }

        //Gathers the desired number of generations the user would like the simulation to run
        public int EnterGenerations()
        {
            Console.SetCursorPosition(0, 47);
               
            int numgens = GatherInput(1, 500, " Enter Number of Generations (1-500): ");
            return numgens;
        }

        //Goes across the grid changing the future values for each cell based on neighboring cells
        public void UpdateGrid()
        {                   
            int neighborsum;

            for(int i = 1; i < grid.GetLength(0) - 1; i++)
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    neighborsum = (grid[i - 1, j - 1].cur +
                                   grid[i - 1, j].cur +
                                   grid[i - 1, j + 1].cur +
                                   grid[i,j-1].cur +
                                   grid[i,j+1].cur +
                                   grid[i+1,j-1].cur +
                                   grid[i+1,j].cur +
                                   grid[i+1,j+1].cur
                                   );


                    FutureUpdate(neighborsum, i, j);
                    AgeUpdate(i, j);
                                            
                }
            CurrentUpdate();
            
        }

        //Compares number of neighbors to determine what the future value should be and sets it
        private void FutureUpdate(int neighborsum, int i, int j)
        {
            //Loneliness
            if (neighborsum < 2)
                grid[i, j].fut = 0;

                //Overcrowding
            else if (neighborsum >= 4)
                grid[i, j].fut = 0;

                //Survive
            else if (grid[i, j].cur == 1 && neighborsum >= 2 && neighborsum <= 3)
                grid[i, j].fut = 1;

                //Birth
            else if (grid[i, j].cur == 0 && neighborsum == 3)
                grid[i, j].fut = 1;
        }              

        //Officially changes the current values to future values to advance a generation
        private void CurrentUpdate()
        {
            for (int i = 1; i < grid.GetLength(0) - 1; i++)
                for (int j = 1; j < grid.GetLength(1) - 1; j++)
                {
                    if (grid[i, j].cur != grid[i, j].fut)
                    {
                        grid[i, j].cur = grid[i, j].fut;
                    }

                }
            gennum++;
        }

        //Increments the age of cells that have survived from one generation to the next
        private void AgeUpdate(int i, int j)
        {
            if (grid[i, j].cur == 1 && grid[i, j].fut == 1)
                grid[i, j].age++;
            else
                grid[i, j].age = 0;
        }

        //Sets the color to output based on the age of the cell
        private void SetColorByAge(int i, int j)
        {

            if (grid[i, j].age == 0)
                Console.ForegroundColor = ConsoleColor.Yellow;

            else if (grid[i, j].age == 1)
                Console.ForegroundColor = ConsoleColor.Green;

            else if (grid[i, j].age == 2)
                Console.ForegroundColor = ConsoleColor.Blue;

            else if (grid[i, j].age == 3)
                Console.ForegroundColor = ConsoleColor.Magenta;

            else if (grid[i, j].age >= 4)
                Console.ForegroundColor = ConsoleColor.Red;

        }

        private void GetStats()
        {
            int age0 = 0,
                age1 = 0,
                age2 = 0,
                age3 = 0,
                age4 = 0,
                numalive = 0,
                numdead = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j].cur == 1)
                        numalive++;
                    else
                        numdead++;

                    if (grid[i, j].age == 0 && grid[i,j].cur == 1)
                        age0++;

                    else if (grid[i, j].age == 1)
                        age1++;

                    else if (grid[i, j].age == 2)
                        age2++;

                    else if (grid[i, j].age == 3)
                        age3++;

                    else if (grid[i, j].age >= 4)
                        age4++;
                }

            Console.WriteLine("\n Statitistics Breakdown");
            Console.WriteLine(" Alive: " + numalive);
            Console.WriteLine(" Dead: " + numdead);
            Console.WriteLine(" Age 0: " + age0);
            Console.WriteLine(" Age 1: " + age1);
            Console.WriteLine(" Age 2: " + age2);
            Console.WriteLine(" Age 3: " + age3);
            Console.WriteLine(" Age 4 or older: " + age4);
                                                          
        }

        //Prints out the grid
        public void PrintGrid()
        {            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\n Generation: #" + gennum.ToString("000"));

            for (int i = 0; i < grid.GetLength(0); i++)
            {            
                Console.WriteLine();
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    SetColorByAge(i,j);
                    
                    if (grid[i,j].cur == 1)
                        Console.Write((char)30);
                    else
                        Console.Write((char)32);

                }
            }
            
        }


    }
}
