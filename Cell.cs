using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLifeUI
{
    public class Cell : Button
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public bool IsAlive { get; set; }
        public bool IsAliveNextRound { get; set; }

        /* Constructor */
        public Cell(bool currentState, int row, int col)
        {
            IsAlive = currentState;
            Row = row;
            Col = col;
            this.Background = Brushes.Black;
        }

        /* OnClick Override, what happens when the cell is clicked*/
        protected override void OnClick()
        {
            base.OnClick();
            if (IsAlive)
            {
                IsAlive = false;
                this.Opacity = 0;
            }
            else
            {
                IsAlive = true;
                this.Opacity = 1;
            }

            if (IsAliveNextRound)
            {
                IsAliveNextRound = false;
                this.Opacity = 0;
            }
            else
            {
                IsAliveNextRound = true;
                this.Opacity = 1;
            }
        }

        public void NextRound(int neighboursCount, int minNeighbour, int maxNeighbour, int reqNeighbour)
        {
            if(IsAlive && (neighboursCount < minNeighbour || neighboursCount > maxNeighbour))
            {
                IsAliveNextRound = false;
                this.Background = Brushes.Red;
            }else if(!IsAlive && neighboursCount == reqNeighbour)
            {
                IsAliveNextRound = true;
                this.Background = Brushes.Green;
                this.Opacity = 1;
            } 
        }
    }
}
