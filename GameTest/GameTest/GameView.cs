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
    public partial class GameView : Form
    {
        public GameView(int[,] map, LabirentAlgorithm labirentAlgorithm, Point squareSize)
        {
            InitializeComponent();
            curPointOfPlayer = labirentAlgorithm.startPoint;
            this.squareSize = squareSize;
            this.labirentAlgorithm = labirentAlgorithm;
            this.map = map;
            CreatePictureBoxs();
            List<Point> points = GetPointsThenMap();
            PaintToPictureBox(points);
        }
        private Point curPointOfPlayer;
        private Point squareSize;
        private List<PictureBox> pictureBoxes;
        LabirentAlgorithm labirentAlgorithm;
        int[,] map;
        int size = 100;
        private void CreatePictureBoxs()
        {
            Size flpSize = flowLayoutPanel1.Size;
            int width = squareSize.X * size + (size / 2);
            flowLayoutPanel1.Size = new Size(width, flpSize.Height);
            pictureBoxes = new List<PictureBox>();
            for (int i = 0; i < squareSize.X * squareSize.Y; i++)
            {
                pictureBoxes.Add(CreatePictureBox());
            }
        }
        private PictureBox CreatePictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            var margin = pictureBox.Margin;
            margin.All = 0;
            pictureBox.Margin = margin;
            pictureBox.Size = new Size(size, size);
            flowLayoutPanel1.Controls.Add(pictureBox);
            return pictureBox;
        }
        private void PaintToPictureBox(List<Point> pointList)
        {
            int counter = 0;
            foreach (var item in pointList)
            {
                if (CheckPointInLabirentMap(item))
                {
                    if (map[item.X, item.Y] == 1)
                    {
                        pictureBoxes[counter].BackColor = Color.White;
                    }
                    else
                        pictureBoxes[counter].BackColor = Color.Black;
                }
                else
                    pictureBoxes[counter].BackColor = Color.Aqua;

                counter++;
            }
            int myPosition = GetCurrentPointInViewPoints(pointList);
            pictureBoxes[myPosition].BackColor = Color.Orange;
        }
        private int GetCurrentPointInViewPoints(List<Point> points)
        {
            int counter = 0;
            foreach (var item in points)
            {
                if (curPointOfPlayer.X == item.X && curPointOfPlayer.Y == item.Y)
                {
                    return counter;
                }
                counter++;
            }
            return counter;
        }
        private bool CheckPointInLabirentMap(Point point)
        {
            if (point.X >= 0 && point.X < labirentAlgorithm.mapLenght.X &&
                point.Y >= 0 && point.Y < labirentAlgorithm.mapLenght.Y
                )
            {
                return true;
            }
            return false;
        }
        private List<Point> GetPointsThenMap()
        {
            List<Point> points = new List<Point>(squareSize.X * squareSize.Y);
            int yFront = squareSize.Y - (squareSize.Y / 2);
            int x = (squareSize.X - 1) / 2;
            for (int i = 0; i < squareSize.Y; i++)
            {
                int xTemp = -x;
                for (int j = 0; j < squareSize.X; j++)
                {
                    points.Add(new Point(curPointOfPlayer.X + yFront, curPointOfPlayer.Y + xTemp));
                    xTemp++;
                }
                yFront--;
            }
            return points;
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void CheckMovable(Point point)
        {
            if (point.X == labirentAlgorithm.finishPoint.X && point.Y == labirentAlgorithm.finishPoint.Y)
            {
                MessageBox.Show("You won !");
                this.Hide();
            }
            if (CheckPointInLabirentMap(point) && map[point.X, point.Y] == 1)
            {
                curPointOfPlayer = point;
                List<Point> points = GetPointsThenMap();
                PaintToPictureBox(points);
            }
        }
        private void GameView_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Point point = new Point(curPointOfPlayer.X + 1, curPointOfPlayer.Y);
            CheckMovable(point);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Point point = new Point(curPointOfPlayer.X, curPointOfPlayer.Y - 1);
            CheckMovable(point);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Point point = new Point(curPointOfPlayer.X, curPointOfPlayer.Y + 1);
            CheckMovable(point);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Point point = new Point(curPointOfPlayer.X - 1, curPointOfPlayer.Y);
            CheckMovable(point);
        }
    }
}
