namespace SnakeGame
{
    public partial class Form1 : Form
    {
        cGameManager GameManager { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        /* 50 px
         * Width : 750
         * Height : 750
         */
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameManager = new cGameManager(this,labelScore,50);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
            GameManager.StartGame();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    GameManager.ChangeDirection(EDirection.Down);
                    break;
                case Keys.Up:
                    GameManager.ChangeDirection(EDirection.Up);
                    break;
                case Keys.Right:
                    GameManager.ChangeDirection(EDirection.Right);
                    break;
                case Keys.Left:
                    GameManager.ChangeDirection(EDirection.Left);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}