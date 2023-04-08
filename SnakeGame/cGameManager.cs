using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class cGameManager
    {
        List<cVagon> WagonList { get; set; }
        public Form MainForm { get; set; }
        public int BoxSize { get; set; }
        public System.Windows.Forms.Timer GameTimer { get; set; }
        cFood Food { get; set; }
        public int Delay { get; set; }
        Label ScoreLabel { get; set; }
        public cGameManager(Form _MainForm, Label _ScoreLabel, int _BoxSize)
        {
            MainForm = _MainForm;
            BoxSize = _BoxSize;
            WagonList = new List<cVagon>();
            Delay = 300;
            InIt();
            ScoreLabel = _ScoreLabel;
        }
        public void InIt()
        {
            MainForm.Width = BoxSize * 15;
            MainForm.Height = BoxSize * 15;
            MainForm.BackColor = Color.White;

        }

        public void CreateVagon(int _X, int _Y, EDirection _Direction)
        {
            cVagon __Wagon = new cVagon(this, _X, _Y, _Direction);
            WagonList.Add(__Wagon);
        }
        public void CreateFood(int _X, int _Y)
        {
            Food = new cFood(this, _X, _Y);
        }
        public void CreateRandomFood()
        {
            Random __Random = new Random();
            int __X = __Random.Next(1, 16);
            int __Y = __Random.Next(1, 16);
            while (AnyoneIsInCoordinate(__X, __Y))
            {
                __X = __Random.Next(1, 16);
                __Y = __Random.Next(1, 16);
            }
            CreateFood(__X, __Y);
        }
        public void StartGame()
        {
            CreateVagon(5, 2, EDirection.Right);
            CreateVagon(4, 2, EDirection.Right);
            CreateVagon(3, 2, EDirection.Right);
            CreateVagon(2, 2, EDirection.Right);
            CreateRandomFood();

            GameTimer = new System.Windows.Forms.Timer();
            GameTimer.Interval = Delay;
            GameTimer.Tick += GameTimer_Tick;
            GameTimer.Start();
        }
        public void SetDelay(int _Delay)
        {
            Delay = _Delay;
            GameTimer.Stop();
            GameTimer.Interval = Delay;
            GameTimer.Start();
        }
        public cVagon GetNextVagon(cVagon _Wagon)
        {
            for (int i = 0; i < WagonList.Count; i++)
            {
                if (WagonList[i] == _Wagon)
                {
                    if (WagonList.Count > i + 1)
                    {
                        return WagonList[i + 1];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
        public void AddWagonEnd()
        {
            cVagon __Wagon = WagonList.Last();
            if (__Wagon.Direction == EDirection.Down)
            {
                CreateVagon(__Wagon.X, __Wagon.Y - 1, __Wagon.Direction);
            }
            else if (__Wagon.Direction == EDirection.Up)
            {
                CreateVagon(__Wagon.X, __Wagon.Y + 1, __Wagon.Direction);
            }
            else if (__Wagon.Direction == EDirection.Right)
            {
                CreateVagon(__Wagon.X-1, __Wagon.Y, __Wagon.Direction);
            }
            else if (__Wagon.Direction == EDirection.Left)
            {
                CreateVagon(__Wagon.X+1, __Wagon.Y , __Wagon.Direction);
            }
        }
        public void ControlFood()
        {
            if (Food != null && WagonList[0].X == Food.X && WagonList[0].Y == Food.Y)
            {
                Food.Eat();
                CreateRandomFood();
                AddWagonEnd();
            }
            ScoreLabel.Text = ((WagonList.Count - 4) * 10).ToString();

        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            DrawGrid();

            if (WagonList[0].CanMove())
            {
                WagonList[0].Move();
                ControlFood();
            }
            else
            {
                GameTimer.Stop();
                MessageBox.Show("Game Over..");
            }
        }
        public bool AnyoneIsInCoordinate(int _X, int _Y)
        {
            foreach (var __Item in WagonList)
            {
                if (__Item.X == _X && __Item.Y == _Y)
                {
                    return true;
                }
            }
            return false;
        }
        public void ChangeDirection(EDirection _Direction)
        {
            if ((_Direction == EDirection.Left || _Direction == EDirection.Right) && (WagonList[0].Direction == EDirection.Up || WagonList[0].Direction == EDirection.Down))
            {
                WagonList[0].Direction = _Direction;
            }
            if ((_Direction == EDirection.Up || _Direction == EDirection.Down) && (WagonList[0].Direction == EDirection.Right || WagonList[0].Direction == EDirection.Left))
            {
                WagonList[0].Direction = _Direction;
            }
        }
        public void DrawGrid()
        {
            Graphics __Graphics = MainForm.CreateGraphics();
            for (int X = 0; X < MainForm.Width; X += BoxSize)
            {
                __Graphics.DrawLine(Pens.Black, X, 0, X, MainForm.Width);
            }
            for (int Y = 0; Y < MainForm.Height; Y += BoxSize)
            {
                __Graphics.DrawLine(Pens.Black, 0, Y, MainForm.Height, Y);
            }
        }
    }
}
