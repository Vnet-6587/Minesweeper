using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SAPER
{
    public partial class Form1 : Form
    {
        public int par = 0;
        public int count = 0;
        public int[,] board = null;
        public bool loss = false;
        public bool victory = false;
        public bool start = false;
        public bool flag = false;
        public int winGoal = 0;
        public Form1()
        {
            InitializeComponent();
            button4.Enabled = false;
        }
        public void clear()
        {
            this.loss = false;
            this.victory = false;
            this.winGoal = 0;
            for (int k = 0; k < 10; k++)
            {
                foreach (Label l in this.Controls.OfType<Label>())
                {
                    Controls.Remove(l);
                }
                foreach (Button btn in this.Controls.OfType<Button>().Where<Button>(btn => btn.Name != "button4" && btn.Name != "button2" && btn.Name != "button1" && btn.Name != "button3" && btn.Name != "button5"))
                {
                    Controls.Remove(btn);
                }
                for (int i = 0; i <= count + 1; i++)
                {
                    for (int j = 0; j <= count + 1; j++)
                    {
                        this.board[i, j] = 0;
                    }
                }
            }
        }
        public void create()
        {
            int name = 0;
            this.board = new int[count + 4, count + 4];
            Random r = new Random();
            for (int i = 0; i < count * (count / 4); i++)
            {
                int x = r.Next(2, count + 1);
                int y = r.Next(2, count + 1);
                this.board[x, y] = 1;
            }
            for (int i = 2; i < count + 2; i++)
            {
                for (int j = 2; j < count + 2; j++)
                {
                    Button b = new Button();
                    b.Location = new Point(j * par, i * par);
                    b.Size = new Size(par, par);
                    b.Name = i.ToString() + "," + j.ToString();
                    b.Click += new EventHandler(button_Click);
                    b.BackColor = Color.LightGray;
                    Controls.Add(b);
                    name++;
                }
            }
            for (int i = 2; i < count + 2; i++)
            {
                for (int j = 2; j < count + 2; j++)
                {
                    if (this.board[i, j] != 1)
                    {
                        this.winGoal++;
                    }
                }
            }
            Console.WriteLine(this.winGoal);
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (this.start == false)
            {
                count = 10;
                par = 50;
                button4.Enabled = true;
            }
            else if (flag == false)
            {
                button1.Text = "Flag";
                flag = true;
            }
            else
            {
                button1.Text = "Shovel";
                flag = false;
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.start == false)
            {
                count = 15;
                par = 35;
                button4.Enabled = true;
            }
            else
            {
                clear();
                button1.Text = "Easy";
                button2.Text = "Medium";
                button3.Visible = true;
                button4.Text = "Start";
                button5.Visible = true;
                button4.Enabled = false;
                start = false;
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            count = 20;
            par = 30;
            button4.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.start == true)
            {
                clear();
            }
            button3.Visible = false;
            button5.Visible = false;
            button1.Text = "Shovel";
            button2.Text = "Back";
            button4.Text = "Restart?";
            this.start = true;
            create();
        }
        public int bombCount(int x, int y)
        {
            int bombCount = 0;
            if (this.board[x, y] == 1)
            {
                MessageBox.Show("BOMBA");
                this.loss = true;
                button4.Text = "Restart :|";
                return 10;
            }
            if (this.board[x, y - 1] == 1)
            {
                bombCount++;
            }
            if (this.board[x + 1, y - 1] == 1)
            {
                bombCount++;
            }
            if (this.board[x + 1, y] == 1)
            {
                bombCount++;
            }
            if (this.board[x + 1, y + 1] == 1)
            {
                bombCount++;
            }
            if (this.board[x, y + 1] == 1)
            {
                bombCount++;
            }
            if (this.board[x - 1, y + 1] == 1)
            {
                bombCount++;
            }
            if (this.board[x - 1, y] == 1)
            {
                bombCount++;
            }
            if (this.board[x - 1, y - 1] == 1)
            {
                bombCount++;
            }
            return bombCount;
        }
        public void revealing(int x, int y)
        {
            if (bombCount(x, y - 1) != 0)
            {
                
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + (y - 1).ToString()))
                {
                    b.Visible = false;
                    labeling(x, y - 1, b);
                }
            }
            if (bombCount(x + 1, y - 1) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == (x + 1).ToString() + "," + (y - 1).ToString()))
                {
                    b.Visible = false;
                    labeling(x + 1, y - 1, b);
                }
            }
            if (bombCount(x + 1, y) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == (x + 1).ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                    labeling(x + 1, y, b);
                }
            }
            if (bombCount(x + 1, y + 1) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == (x + 1).ToString() + "," + (y + 1).ToString()))
                {
                    b.Visible = false;
                    labeling(x + 1, y + 1, b);
                }
            }
            if (bombCount(x, y + 1) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + (y + 1).ToString()))
                {
                    b.Visible = false;
                    labeling(x, y + 1, b);
                }
            }
            if (bombCount(x - 1, y + 1) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == (x - 1).ToString() + "," + (y + 1).ToString()))
                {
                    b.Visible = false;
                    labeling(x - 1, y + 1, b);
                }
            }
            if (bombCount(x - 1, y) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == (x - 1).ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                    labeling(x - 1, y, b);
                }
            }
            if (bombCount(x - 1, y - 1) != 0)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == (x - 1).ToString() + "," + (y - 1).ToString()))
                {
                    b.Visible = false;
                    labeling(x - 1, y - 1, b);
                }
            }
        }
        public void curious(int x, int y)
        {
            int tempx = x;
            int tempy = y;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                    revealing(x, y);
                }
                x++;
            }
            x = tempx;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                    revealing(x, y);
                }
                x--;
            }
            x = tempx;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                    revealing(x, y);
                }
                y++;
            }
            y = tempy;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                    revealing(x, y);
                }
                y--;
            }
        }
        public void sequence(int x, int y)
        {
            int tempx = x;
            int tempy = y;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                }
                x++;
                curious(x, y);
            }
            x = tempx;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                }
                x--;
                curious(x, y);
            }
            x = tempx;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                }
                y++;
                curious(x, y);
            }
            y = tempy;
            while (bombCount(x, y) == 0 && x > 1 && y > 1 && x < count + 2 && y < count + 2)
            {
                foreach (Button b in Controls.OfType<Button>().Where<Button>(b => b.Name == x.ToString() + "," + y.ToString()))
                {
                    b.Visible = false;
                }
                y--;
                curious(x, y);
            }
        }
        public void labeling(int x, int y, Button b)
        {
            Label l = new Label();
            l.Text = bombCount(x, y).ToString();
            l.Location = b.Location;
            l.Size = new Size(this.par, this.par);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Font = new Font("Unispace", 15);
            switch (bombCount(x, y))
            {
                case 1:
                    l.ForeColor = Color.LightBlue;
                    break;
                case 2:
                    l.ForeColor = Color.LightGreen;
                    break;
                case 3:
                    l.ForeColor = Color.Red;
                    break;
                case 4:
                    l.ForeColor = Color.Purple;
                    break;
            }
            Controls.Add(l);
        }
        public int[] getName(Button b)
        {
            int x; int y;
            String X = null; String Y = null;
            bool xyswicth = false;
            for (int i = 0; i < b.Name.Length; i++)
            {
                if (xyswicth == false)
                {
                    X += b.Name[i];
                }
                else
                {
                    Y += b.Name[i];
                }
                if (xyswicth == false && b.Name[i + 1] == ',')
                {
                    xyswicth = true;
                    i++;
                }
            }
            x = int.Parse(X);
            y = int.Parse(Y);
            int[] result = { x, y };
            return result;
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int winCon = 0;
            int x = getName(b)[0];
            int y = getName(b)[1];
            if (flag == false)
            {
                int bombCount;
                bombCount = this.bombCount(x, y);
                if (loss == false && victory == false)
                {
                    b.Visible = false;
                    if (bombCount > 0)
                    {
                        labeling(x, y, b);
                    }
                    else
                    {
                        sequence(x, y);
                    }
                }
                for (int i = 2; i < count + 2; i++)
                {
                    for (int j = 2; j < count + 2; j++)
                    {
                        foreach (Button n in Controls.OfType<Button>().Where<Button>(n => n.Name == i.ToString() + "," + j.ToString() && n.Visible == false))
                        {
                            winCon++;
                        }
                    }
                }
                Console.WriteLine(winCon);
                if (winCon == this.winGoal)
                {
                    this.victory = true;
                    MessageBox.Show("You did it!");
                }
            }
            else
            {
                if (b.BackColor == Color.LightGreen)
                {
                    b.BackColor = Color.LightGray;
                }
                else
                {
                    b.BackColor = Color.LightGreen;
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
