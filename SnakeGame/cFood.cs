using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class cFood
    {
        public cGameManager GameManager { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public EDirection Direction { get; set; }
        Label FoodGraphic { get; set; }
        public cFood(cGameManager _GameManager, int _X, int _Y)
        {
            GameManager = _GameManager;
            X = _X;
            Y = _Y;
            InIt();
        }
        public void InIt()
        {
            FoodGraphic = new Label();
            FoodGraphic.Size = new Size(GameManager.BoxSize - 30, GameManager.BoxSize - 30);
            int __X = ((X - 1) * GameManager.BoxSize) + 16;
            int __Y = ((Y - 1) * GameManager.BoxSize) + 16;
            FoodGraphic.Location = new Point(__X, __Y);
            FoodGraphic.BackColor = Color.Yellow;
            GameManager.MainForm.Controls.Add(FoodGraphic);
        }
        public void Eat()
        {
            GameManager.MainForm.Controls.Remove(FoodGraphic);
            if (GameManager.Delay > 10)
            {
                GameManager.SetDelay(GameManager.Delay - 10);

            }
        }
    }
}
