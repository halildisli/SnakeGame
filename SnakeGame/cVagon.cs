using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class cVagon
    {
        public cGameManager GameManager { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public EDirection Direction { get; set; }
        Label WagonGraphic { get; set; }
        public cVagon(cGameManager _GameManager, int _X, int _Y, EDirection _Direction)
        {
            GameManager = _GameManager;
            X = _X;
            Y = _Y;
            Direction = _Direction;
            InIt();
        }
        public void InIt()
        {
            WagonGraphic = new Label();
            WagonGraphic.Size = new Size(GameManager.BoxSize - 1, GameManager.BoxSize - 1);
            int __X = ((X - 1) * GameManager.BoxSize) + 1;
            int __Y = ((Y - 1) * GameManager.BoxSize) + 1;
            WagonGraphic.Location = new Point(__X, __Y);
            WagonGraphic.BackColor = Color.Green;
            GameManager.MainForm.Controls.Add(WagonGraphic);
        }
        public bool CanMove()
        {
            int __CurrentX = X;
            int __CurrentY = Y;
            if (Direction == EDirection.Left)
            {
                __CurrentX--;
            }
            if (Direction == EDirection.Right)
            {
                __CurrentX++;
            }
            if (Direction == EDirection.Up)
            {
                __CurrentY--;
            }
            if (Direction == EDirection.Down)
            {
                __CurrentY++;
            }
            if (GameManager.AnyoneIsInCoordinate(__CurrentX,__CurrentY))
            {
                return false;
            }
            int __X = ((__CurrentX - 1) * GameManager.BoxSize) + 1;
            int __Y = ((__CurrentY - 1) * GameManager.BoxSize) + 1;
            if (__CurrentX < 1 || __CurrentY < 1 || __CurrentX > 15 || __CurrentY > 15)
            {
                return false;
            }
            return true;
        }
        public void Move()
        {
            if (Direction == EDirection.Left)
            {
                X--;

            }
            if (Direction == EDirection.Right)
            {
                X++;
            }
            if (Direction == EDirection.Up)
            {
                Y--;
            }
            if (Direction == EDirection.Down)
            {
                Y++;
            }

            int __X = ((X - 1) * GameManager.BoxSize) + 1;
            int __Y = ((Y - 1) * GameManager.BoxSize) + 1;
            WagonGraphic.Location = new Point(__X, __Y);
            cVagon __Wagon=GameManager.GetNextVagon(this);
            if (__Wagon!=null)
            {
                __Wagon.Move();
                __Wagon.Direction = Direction;
            }
        }
    }
}
