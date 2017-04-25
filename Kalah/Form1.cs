using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalah
{
    public partial class Form1 : Form
    {
        int flag_on_step = 1;
        const int AI_CON = 0;
        AI AI_INTEL = new AI();
        // 0 - bot player
        // 1 - top player

        Bitmap snow = new Bitmap("snow.gif");
        Bitmap forest = new Bitmap("kalah_new.bmp"); //"forest.jpg"
        Bitmap kalah_pic = new Bitmap("kalah_new.bmp");
        Board brd = new Board(6);
        
        PictureBox[,] Cells = new PictureBox[2, 6];
        Label[,] Labels = new Label[2, 7];



        const int TOP_LANE = 277;
        const int BOT_LANE = 367;
        const int MIDDLE_LANE = 345;

        public int[,] borders = {
            { 1, 285 , 345 },
            { 2, 355 , 415 },
            { 3, 435 , 490 },
            { 4, 510 , 570 },
            { 5, 585 , 640 },
            { 6, 660 , 720 }
            //{idx, left_Board_pix, right_Board_pix}
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1000, 690);
            this.BackgroundImage = forest;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            INIT(brd.board);
            //brd.make_hod(1, 1);
            //update(brd.board);

            //List<Board> tmp = brd.getPossibleBoards(brd, 1);
            //update(tmp[0].board);
            //int j = 0;
        }


        public void INIT(int[,] s)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Label buf_l = new Label();
                    buf_l.BackColor = Color.Red;
                    
                    buf_l.Top = i == 0 ? TOP_LANE -45 : BOT_LANE - 45;
                    buf_l.Left = borders[j, 1]-9;
                    buf_l.Height = 13;
                    buf_l.Width = borders[j, 2] - borders[j, 1];
                    buf_l.TextAlign = ContentAlignment.MiddleCenter;
                    
                    buf_l.Click += new System.EventHandler(this.kalah_Click);


                    PictureBox buf = new PictureBox();

                    buf.Left = borders[j, 1]-9;
                    buf.Top = i == 0 ? TOP_LANE-30 : BOT_LANE-30;

                    buf.Width = borders[j, 2] - borders[j, 1];
                    buf.Height = 60;
                    buf.BackColor = Color.Transparent;
                    buf.Image = null;

                    buf.Click += new System.EventHandler(this.kalah_Click);
                    

                    Labels[i, j] = buf_l;
                    Cells[i, j] = buf;

                    Controls.Add(buf);
                    Controls.Add(buf_l);
                }
            }
            Label first_kalah = new Label();
            first_kalah.Top = 220;
            first_kalah.Left = 200;
            first_kalah.Width = 60;
            first_kalah.Height = 13;
            first_kalah.Text = "0";
            first_kalah.BackColor = Color.Red;
            first_kalah.TextAlign = ContentAlignment.MiddleCenter;
            Labels[0, 6] = first_kalah;
            Controls.Add(first_kalah);

            Label second_kalah = new Label();
            second_kalah.Top = 400;
            second_kalah.Left = 720;
            second_kalah.Width = 60;
            second_kalah.Height = 13;
            second_kalah.Text = "0";
            second_kalah.BackColor = Color.Red;
            second_kalah.TextAlign = ContentAlignment.MiddleCenter;
            Labels[1, 6] = second_kalah;
            Controls.Add(second_kalah);



            update(s);
        }

        public void update(int[,] s)
        {
            Labels[0, 6].Text = s[0, 0].ToString();
            Labels[1, 6].Text = s[1, 7].ToString();
            for (int i =0; i< 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Labels[i, j].Text = s[i, j+1].ToString();

                    if (s[i, j + 1] == 0)
                    {
                        Cells[i,j].Image = null;
                    }
                    else if (s[i, j + 1] == 1)
                    {
                        Cells[i,j].Image = null;
                    }
                    else if (s[i, j + 1] == 2)
                    {
                        Cells[i, j].Image = null;
                    }
                    else
                    {
                        Cells[i, j].Image = null;
                    }
                }
            }
        }

        public void FormChanged(object o, EventArgs e)
        {
            this.Size = new Size(1000, 690);
        }

        private void kalah_Click(object sender, EventArgs e)
        {
            int player;
            int x = Cursor.Position.X - this.DesktopLocation.X;
            int y = Cursor.Position.Y - this.DesktopLocation.Y;
            this.Text = x.ToString() + " " + y.ToString();

            player = y < MIDDLE_LANE ? 0:1;

            int idx = -1;
            int idy = player;

            for (int i =0; i < 6;i++) {
                if(borders[i,1]<x && borders[i, 2] > x) {
                    idx = borders[i, 0];
                    break;
                }
            }

            if (idx == -1)
                return;

            if (player != flag_on_step)
                return;

            if( brd.make_hod(idx, idy) != 1)
                flag_on_step = (flag_on_step + 1) % 2;
            update(brd.board);
            
            
            //this.Text = idx.ToString() + "  " + idy.ToString();
            
        }
        

        #region <trash>

        //animation = new PictureBox();
        //animation.BackgroundImage = forest;
        //    animation.Image = snow;
        //    animation.BackgroundImageLayout = ImageLayout.Stretch;
        //    animation.SizeMode = PictureBoxSizeMode.StretchImage;



        //    this.BackColor = Color.Red;
        //    //backgroundImage.BackColor = Color.Black;

        //    Rectangle screenSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

        //    this.Left = 0;
        //    animation.Left = 0;

        //    this.Top = 0;
        //    animation.Top = 0;

        //    //this.Height = screenSize.Height;
        //    animation.Height = this.Height;


        //    //this.Width = screenSize.Width;
        //    animation.Width = this.Width;
        //    forest.SetResolution(this.Height, this.Width);

        //    this.BackgroundImage = forest;
        //    this.BackgroundImageLayout = ImageLayout.Stretch;

        //    kalah = new PictureBox();
        //kalah.Parent = animation;
        //    kalah.BackColor = Color.Transparent;

        //    kalah.Width = (this.Width / 3) * 2;
        //    kalah.Height = (this.Height / 3) * 2;

        //    kalah.Left = (this.Width - kalah.Width) / 2;
        //    kalah.Top = (this.Height - kalah.Height) / 2;

        //    kalah.Image = kalah_pic;
        //    kalah.Click += new EventHandler(this.kalah_Click);

        //kalah.SizeMode = PictureBoxSizeMode.StretchImage;

        //    //this.Controls.Add(kalah);
        //    //this.Controls.Add(animation);
        #endregion

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(1000, 690);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(flag_on_step == AI_CON)
            {
                AI_INTEL.brd = brd;
                brd = AI_INTEL.make_hod(AI_CON, 6);
                update(brd.board);
                flag_on_step = (flag_on_step + 1) % 2;
            }

        }
    }
}
