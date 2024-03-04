using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_task_13
{
    public partial class FoxesAndHens : Form
    {
        Button[,] GlobalButtonsField;
        GameButton[,] gameField;
        int PlayerTurn, lastX, lastY;
        Random FoxRandom;
        public FoxesAndHens()
        {
            InitializeComponent();
            FoxRandom = new Random();
            gameField = new GameButton[7,7];
            GlobalButtonsField = new Button[,]{
                { button1, button2, button3, button4, button5, button6, button7 },
                { button8, button9, button10, button11, button12, button13, button14 },
                { button15, button16, button17, button18, button19, button20, button21 },
                { button22, button23, button24, button25, button26, button27, button28 },
                { button29, button30, button31, button32, button33, button34, button35 },
                { button36, button37, button38, button39, button40, button41, button42 },
                { button43, button44, button45, button46, button47, button48, button49 }
            };
            lastX = lastY = 0;
            PlayerTurn = 1;

            // Создание массива кнопок игры.
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameField[i,j] = new GameButton(i, j, GlobalButtonsField[i,j]);
                }
            }

            // Установка границ полей.
            gameField[0, 0].State = 0;
            gameField[0, 1].State = 0;
            gameField[1, 0].State = 0;
            gameField[1, 1].State = 0;
            gameField[5, 5].State = 0;
            gameField[5, 6].State = 0;
            gameField[6, 5].State = 0;
            gameField[6, 6].State = 0;
            gameField[0, 5].State = 0;
            gameField[0, 6].State = 0;
            gameField[1, 5].State = 0;
            gameField[1, 6].State = 0;
            gameField[5, 0].State = 0;
            gameField[5, 1].State = 0;
            gameField[6, 0].State = 0;
            gameField[6, 1].State = 0;

            // Установка лис.
            gameField[2,2].State = 2;
            gameField[2,4].State = 4;

            // Установка куриц
            for (int i = 0; i < 7; i++)
            {
                gameField[3, i].State = 3;
                gameField[4, i].State = 3;
            }

            gameField[5, 2].State = 3;
            gameField[5, 3].State = 3;
            gameField[5, 4].State = 3;
            gameField[6, 2].State = 3;
            gameField[6, 3].State = 3;
            gameField[6, 4].State = 3;

            // Установка начального состояния поля
            UpdateField();
        }

        private void UpdateField()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameField[i, j].SetButton();
                }
            }
        }

        private void FoxTurn()
        {
            double fox = Math.Round(FoxRandom.NextDouble(), 1);
            bool foxTurn = true;
            for (int i = 0;i < 7; i++)
            {
                for (int j = 0;j < 7; j++)
                {
                    // ход оной лисы
                    if (gameField[i, j].State == 2 && fox >= 0.5)
                    {
                        gameField[i, j].Foxing(gameField, FoxRandom);
                        UpdateField();
                        foxTurn = false;
                        PlayerTurn = 1;
                    }
                    // ход другой лисы
                    else if(gameField[i, j].State == 4 && fox < 0.5)
                    {
                        gameField[i, j].Foxing(gameField, FoxRandom);
                        UpdateField();
                        foxTurn = false;
                        PlayerTurn = 1;
                    }
                    // чтобы ходила одна лиса за раз
                    if (!foxTurn && PlayerTurn == 1)
                    {
                        return;
                    }
                }
            }
        }

        private void ButtonToutch(int x, int y)
        {
            if (x == lastX && y == lastY)
            {
                // Есди нажата таже кнопка
                UpdateField();
                lastX = 0; lastY = 0;
                return;
            }
            if (gameField[x, y].State == 1)
            {
                // Если нажата кнопка, позволяющая сделать ход
                gameField[x, y].State = 3;
                gameField[lastX, lastY].State = 1;
                PlayerTurn = 0;
                UpdateField();
                FoxTurn(); // ход лисы
            }
            else 
            {
                // проверка возможных ходов у нажатой кнопки
                gameField[x, y].CheckTurn(gameField);
            }

            lastX = x; lastY = y;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ButtonToutch(0, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ButtonToutch(0, 3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ButtonToutch(0, 4);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ButtonToutch(1, 2);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ButtonToutch(1, 3);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ButtonToutch(1, 4);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 0);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 1);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 2);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 3);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 4);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 5);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ButtonToutch(2, 6);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 0);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 1);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 2);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 3);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 4);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 5);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            ButtonToutch(3, 6);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 0);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 1);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 2);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 3);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 4);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 5);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            ButtonToutch(4, 6);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            ButtonToutch(5, 2);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            ButtonToutch(5, 3);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            ButtonToutch(5, 4);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            ButtonToutch(6, 2);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            ButtonToutch(6, 3);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            ButtonToutch(6, 4);
        }
    }
}
