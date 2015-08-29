using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL

            /**************************
             * 
             *          Travis Caro 
             *          CIS 244
             *          Conway's Game of Life
             * 
             * 
             *          ALGORITHM
             *         -------------
             *      Create 2D Array filled with structs
             *      Fill Struct variables accordingly
             *      Give User preset options or random to show grid
             *      Print the initial grid, begin simulation
             *      
             *      For Simulation-->
             *      
             *      Go across grid, summing the current states of neighbors. (if statement [i-1,j-1] + [i-1,j] + ...)
             *      Depending on what the status of neighbors (sum), change future value to alive/dead (1,0)
             *      Once finished checking all cells, update the grid ( current = future --> age++)
             *      Reprint the grid with newly update cells and generation, begin rechecking!
             * 
             ***************************/


{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate GOL Object
            Library.GOL GOL = new Library.GOL();
            
            int numgens;
            bool cont, restart;
            

            do
            {
                restart = false;
                          
                GOL.ConsoleSetup();
                GOL.StartSelection();
                GOL.PrintGrid();

                do
                {
                    cont = false;
                    numgens = GOL.EnterGenerations();

                    while (numgens > 0)
                    {
                        GOL.UpdateGrid();
                        GOL.PrintGrid();
                        System.Threading.Thread.Sleep(20);
                        numgens--;
                    } 


                    GOL.ContinueSelection(ref cont, ref restart);

                    
                }while (cont == true) ;

                Console.Clear();
            } while (restart == true) ;


        }
    }
}
