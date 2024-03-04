using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace practice_task_13
{
    internal class GameButton
    {
        private int x, y;
        private int state;
        Button button;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int State { get { return state; } set { state = value; } }

        public GameButton(int x, int y, Button button)
        {
            this.x = x;
            this.y = y;
            this.button = button;
            this.state = 1;
        }

        public void SetButton()
        {
            if (state == 2 || state == 4) 
            {
                button.Enabled = false;
                button.BackColor = Color.Coral;
                button.Text = "Л";
            }
            else if (state == 3)
            {
                button.Enabled = true;
                button.BackColor = Color.Cornsilk;
                button.Text = "К";
            }
            else if (state == 0) 
            {
                button.Enabled = false;
                button.BackColor = Color.Black;
                button.Text = string.Empty;
            }
            else
            {
                button.Enabled = false;
                button.BackColor = Color.Gray;
                button.Text = string.Empty;
            }
        }

        public void CheckTurn(GameButton[,] Field)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (this.x == i && this.y == j) { continue; }
                    Field[i, j].button.Enabled = false;
                }
            }

            try
            {
                if (Field[x - 1, y].State == 1)
                {
                    Field[x - 1, y].button.Enabled = true;
                    Field[x - 1, y].button.BackColor = Color.Green;
                }
            }
            catch { }

            try
            {
                if (Field[x, y - 1].State == 1)
                {
                    Field[x, y - 1].button.Enabled = true;
                    Field[x, y - 1].button.BackColor = Color.Green;
                }
            }
            catch { }

            try
            {
                if (Field[x, y + 1].State == 1)
                {
                    Field[x, y + 1].button.Enabled = true;
                    Field[x, y + 1].button.BackColor = Color.Green;
                }
            }
            catch { }
        }

        // Ход лисы
        public void Foxing(GameButton[,] Field, Random random)
        {
            // хранение координат на случай возможности съесть несколько куриц за раз
            int tmpX = x, tmpY = y;
            // определение направления возможности съесть
            int directionOfEAT = CanEat(tmpX, tmpY, Field);

            if (directionOfEAT != 0)
            {
                // поедание куриц, пока есть возможность
                do
                {
                    Eat(tmpX, tmpY, directionOfEAT, Field);
                    tmpX = x; tmpY = y;
                    directionOfEAT = CanEat(tmpX, tmpY, Field);
                } while (directionOfEAT != 0);
            }
            else
            {
                // если нет возможности съесть, то просто ходим
                Move(Field, random);   
            }
        }

        private int CanEat(int x, int y, GameButton[,] Field)
        {
            // проверка возможности съесть
            try
            {
                if (Field[x - 2, y].State == 1 && Field[x - 1, y].State == 3) return 1;
            }
            catch { }
            try
            {
                if (Field[x + 2, y].State == 1 && Field[x + 1, y].State == 3) return 2;
            }
            catch { }
            try
            {
                if (Field[x, y - 2].State == 1 && Field[x, y - 1].State == 3) return 3;
            }
            catch { }
            try
            {
                if (Field[x, y + 2].State == 1 && Field[x, y + 1].State == 3) return 4;
            }
            catch { }
            return 0;
        }

        private void Move(GameButton[,] Field, Random random)
        {
            bool flag = true;
            int direction = random.Next(0, 3);
            while (flag)
            {
                switch (direction)
                {
                    case 0:
                        {
                            try
                            {
                                if (Field[x - 1, y].State == 1)
                                {
                                    Field[x - 1, y].State = state;
                                    Field[x, y].State = 1;
                                    flag = false;
                                }
                            }
                            catch { }
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                if (Field[x + 1, y].State == 1)
                                {
                                    Field[x + 1, y].State = state;
                                    Field[x, y].State = 1;
                                    flag = false;
                                }
                            }
                            catch { }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                if (Field[x, y - 1].State == 1)
                                {
                                    Field[x, y - 1].State = state;
                                    Field[x, y - 1].State = 1;
                                    flag = false;
                                }
                            }
                            catch { }
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                if (Field[x, y + 1].State == 1)
                                {
                                    Field[x, y + 1].State = state;
                                    Field[x, y + 1].State = 1;
                                    flag = false;
                                }
                            }
                            catch { }
                            break;
                        }
                }
                direction = random.Next(0, 3);
            }
        }

        private void Eat(int x, int y, int direction, GameButton[,] Field) 
        {
            switch (direction)
            {
                case 1:
                    {
                        Field[x, y].State = 1;
                        Field[x - 2, y].State = 2;
                        Field[x - 1, y].State = 1;
                        break;
                    }
                case 2:
                    {
                        Field[x, y].State = 1;
                        Field[x + 2, y].State = 2;
                        Field[x + 1, y].State = 1;
                        break;
                    }
                case 3:
                    {
                        Field[x, y].State = 1;
                        Field[x, y - 2].State = 2;
                        Field[x, y - 1].State = 1;
                        break;
                    }
                case 4:
                    {
                        Field[x, y].State = 1;
                        Field[x, y + 2].State = 2;
                        Field[x, y + 1].State = 1;
                        break;
                    }
            }
        }
    }
}
