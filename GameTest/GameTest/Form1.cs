using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTest
{
    public partial class Form1 : Form
    {
        private int[,] map;
        private Rectangle rectangle;
        private int padding = 40;
        private Point mapSize;
        public Form1()
        {
            InitializeComponent();
            mapSize = new Point(100, 40);
            createGame();
            setRectangle();
        }
        private void setRectangle()
        {
            int width = (this.Size.Width - padding) / (mapSize.X);
            int height = (this.Size.Height - padding) / (mapSize.Y);
            if (width < height)
            {
                height = width;
            }
            else
                width = height;
            rectangle = new Rectangle(new Point(0, 0), new Size(width, height));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Point position = new Point(padding / 2, padding / 2);
            int counter = 0;
            SolidBrush whitePen = new SolidBrush(Color.White);
            SolidBrush blackPen = new SolidBrush(Color.Black);
            foreach (var item in map)
            {
                rectangle.Location = position;
                Graphics graphics = e.Graphics;
                if (item == 1)
                    graphics.FillRectangle(whitePen, rectangle);

                else
                    graphics.FillRectangle(blackPen, rectangle);
                if ((counter + 1) % mapSize.Y == 0)
                {
                    position = new Point(position.X + rectangle.Width, padding / 2);
                }
                else
                {
                    position = new Point(position.X, position.Y + (rectangle.Height));
                }
                counter++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createGame();
        }
        private void createGame()
        {
            LabirentAlgorithm labirentAlgorithm = new LabirentAlgorithm(LabirentAlgorithm.GameLevel.easy, mapSize, 5, 7, 5,7);
            map = labirentAlgorithm.GetRandomMap();
            this.Invalidate();
            GameView gameView = new GameView(map, labirentAlgorithm, new Point(5,6));
            gameView.Show();
        }
    }
}
