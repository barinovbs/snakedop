namespace Snake
{
    public partial class Form1 : Form
    {
        private GameController _controller;
        public Form1()
        {
            InitializeComponent();
            _controller = new GameController(panel1.Size);
            _controller.EatFood += ControllerOnEatFood;
            _controller.Grow += ControllerOnGrow;
            
        }

        private void ControllerOnGrow(int length)
        {
            label2.Text = (length-3).ToString();
            //Form2 form2 = new Form2(length - 3);
            //form2.Show(); // ��� form2.ShowDialog(),


        }

        private void ControllerOnEatFood()
        {
            snakeTimer.Interval -= 15;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            _controller.PaintScene(panel1.CreateGraphics());
        }
        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (_controller != null)
            {
                _controller.ControllerSize = panel1.Size;
                panel1.Invalidate();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (snakeTimer.Enabled)
                snakeTimer.Stop();
            else snakeTimer.Start();
        }

        private void snakeTimer_Tick(object sender, EventArgs e)
        {
            if (!_controller.NextStep())
            {
                snakeTimer.Stop();
            }
            _controller.PaintScene(panel1.CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    {
                        _controller.TurnSnake(Direction.Left);
                        break;
                    }
                case Keys.Right:
                    {
                        _controller.TurnSnake(Direction.Right);
                        break;
                    }
                case Keys.Up:
                    {
                        _controller.TurnSnake(Direction.Top);
                        break;
                    }
                case Keys.Down:
                    {
                        _controller.TurnSnake(Direction.Bottom);
                        break;
                    }
            }
        }



    }
}