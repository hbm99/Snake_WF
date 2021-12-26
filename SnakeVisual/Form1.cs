using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeApp;
using System.IO;

namespace SnakeVisual
{
    public partial class Form1 : Form
    {
        SpaceCreator spaceCreator;
        GameCenter gameCenter;
        Size cellSize;
        bool gamePlaying;
        SetObject setObject;
        int gameScore;
        int interval;
        public Form1()
        {
            InitializeComponent();
            spaceCreator = new SpaceCreator();
            gameCenter = new GameCenter(new Map(15, 25), 5);
            cellSize = new Size(30, 30);
            gamePlaying = false;
            setObject = new SetObject();
            gameScore = 0;
            interval = 1;
            timer1.Interval = 150;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            int dim = 30;
            pictureBox1.Height = gameCenter.World.RowsCount * dim + 1;
            pictureBox1.Width = gameCenter.World.ColumnsCount * dim + 1;


            for (int i = 0; i < gameCenter.World.RowsCount; i++)
                for (int j = 0; j < gameCenter.World.ColumnsCount; j++)
                {
                    
                    if (gameCenter.World[i, j].Object == null)
                        graphics.DrawImage(Image.FromFile("resources\\0.jpg"), j * dim, i * dim, dim, dim);
                    else if (gameCenter.World[i, j].Object.Shape == 2)
                    {
                        string dir = "resources\\" + gameCenter.World[i, j].Object.ToString() + ".jpg";
                        graphics.DrawImage(Image.FromFile(dir), j * dim, i * dim, dim, dim);
                        Egg temp = gameCenter.World[i, j].Object as Egg;
                        SolidBrush new_brush = new SolidBrush(Color.Black);
                        graphics.DrawString(temp.Number.ToString(), Font, new_brush, j * dim + 10, i * dim + 10);
                    }
                    else if (gameCenter.GameOver && gameCenter.World[i, j].Object.Shape == 3)
                    {
                        graphics.DrawImage(Image.FromFile("resources\\muerto.jpg"), j * dim, i * dim, dim, dim);
                    }
                    else
                    {
                        string dir = "resources\\" + gameCenter.World[i, j].Object.ToString() + ".jpg";
                        graphics.DrawImage(Image.FromFile(dir), j * dim, i * dim, dim, dim);
                    }
                }
        }
        private void createWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spaceCreator.ShowDialog();

            int rows = spaceCreator.Rows;
            int columns = spaceCreator.Columns;
            int eggs = spaceCreator.Eggs;
            gameCenter = new GameCenter(new Map(rows, columns), eggs);

            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval > 30 && gameScore >= 10 * interval) 
            {
                timer1.Interval -= 10;
                interval++;
            }

            if (gamePlaying)
            {
                gameCenter.Play();
                gameScore = gameCenter.snake.gameScore;
                if (gameCenter.GameOver)
                {
                    pictureBox1.Refresh();
                    gamePlaying = false;
                    MessageBox.Show("     GAME OVER!!!     :-(");
                    labelPoints.Text = gameScore.ToString();
                }
                labelPoints.Text = gameScore.ToString();
                pictureBox1.Refresh();
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamePlaying = true;
            pictureBox1.Enabled = false;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamePlaying = false;
            labelPoints.Text = gameScore.ToString();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamePlaying = false;
            spaceCreator = new SpaceCreator();
            gameCenter = new GameCenter(new Map(15, 25), 5);
            cellSize = new Size(30, 30);
            setObject = new SetObject();
            gameScore = 0;
            labelPoints.Text = gameScore.ToString();
            interval = 1;
            timer1.Interval = 150;
            pictureBox1.Enabled = true;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int row = e.Y / cellSize.Height;
            int column = e.X / cellSize.Width;

            if (gameCenter.World[row, column].Object == null)
            {
                setObject.ShowDialog();

                gameCenter.Mediator.CreateObject(row, column, setObject.Shape);
            }
            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 37:
                    if (!(gameCenter.snake.Direction == 1)) 
                        gameCenter.snake.Direction = 3;
                    break;
                case 38:
                    if (!(gameCenter.snake.Direction == 2)) 
                        gameCenter.snake.Direction = 0;
                    break;
                case 39:
                    if (!(gameCenter.snake.Direction == 3))
                        gameCenter.snake.Direction = 1;
                    break;
                case 40:
                    if (!(gameCenter.snake.Direction == 0))
                        gameCenter.snake.Direction = 2;
                    break;
                default:
                    break;
            }
        }

        private void loadWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdLoad = new OpenFileDialog();
            ofdLoad.FileName = "Select a text file";
            ofdLoad.Filter = "No format text | *.txt";
            ofdLoad.Title = "Open text file";
            int aux = 0;
            int eggs = 10;

            if (ofdLoad.ShowDialog() == DialogResult.OK)
            {
                StreamReader read = File.OpenText(ofdLoad.FileName);
                string[] temp;
                string s;
                int rowMax = int.MinValue;
                int colMax = int.MinValue;

                do
                {
                    if (aux == 0)
                    {
                        read.ReadLine();
                        aux++;
                    }
                    s = read.ReadLine();
                    if (s != null)
                    {
                        temp = s.Split();

                        if (int.Parse(temp[0]) > rowMax)
                            rowMax = int.Parse(temp[0]);

                        if (int.Parse(temp[1]) > colMax)
                            colMax = int.Parse(temp[1]);
                    }
                }
                while (s != null);

                gameCenter = new GameCenter(new Map(15, 25), eggs);

                read = File.OpenText(ofdLoad.FileName);
                aux = 0;
                temp = null;
                s = "";
                do
                {
                    if (aux == 0)
                    {
                        read.ReadLine();
                        aux++;
                    }
                    s = read.ReadLine();
                    if (s != null)
                    {
                        temp = s.Split();
                        Parser.Parse(gameCenter, int.Parse(temp[0]), int.Parse(temp[1]));
                    }
                }
                while (s != null);

                pictureBox1.Refresh();

            }
        }

        private void saveWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int map = random.Next(int.MinValue, int.MaxValue);
            StreamWriter written = File.CreateText(map + ".txt");

            int count = 0;
            for (int i = 0; i < gameCenter.World.RowsCount; i++)
                for (int j = 0; j < gameCenter.World.ColumnsCount; j++)
                    if (gameCenter.World[i, j].Object != null && gameCenter.World[i, j].Object.Shape == 1) 
                        count++;

            String content = count.ToString();
            written.WriteLine(content);
            written.Flush();

            for (int i = 1; i < gameCenter.World.RowsCount; i++)
                for (int j = 0; j < gameCenter.World.ColumnsCount; j++) 
                {
                    if (gameCenter.World[i, j].Object != null)  
                    {
                        content = i + " " + j;
                        written.WriteLine(content);
                        written.Flush();
                    }
                }
        }
    }
}
