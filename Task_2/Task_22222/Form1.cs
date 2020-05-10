using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_22222
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        Clock min1, sec1, hour1;
        Clock min2, sec2, hour2;
        int time1 = 0,time2=0;
        Lever lev1,lev2;


        public Form1()
        {
            InitializeComponent();
            hour1 = new Clock(this, 100, 150, 200, 3600,0.5);
            min1 = new Clock(this, 100, 250, 200, 60,0.5);
            sec1 = new Clock(this, 100, 350, 200, 1,0.5);
            lev1 = new Lever(this,460,200,0.68);

            sec2 = new Clock(this, 100, 290, 75, 1, 0.35);
            min2 = new Clock(this, 100, 220, 75, 60, 0.35);
            hour2 = new Clock(this, 100, 150, 75, 3600, 0.35);
            lev2 = new Lever(this, 370, 75, 0.5);

            this.MouseWheel += new MouseEventHandler(this_MouseWheel);
        }

        void this_MouseWheel(object sender, MouseEventArgs e)
        {
            int mx = Cursor.Position.X-this.Left;
            int my = Cursor.Position.Y-this.Top-30;
            Graphics g = this.CreateGraphics();
            if (sec1.Check(mx, my)){
                sec1.ChangeTime(time1,e.Delta,g);
                time1 += (e.Delta / 120);
            }
            if (min1.Check(mx, my)){
                min1.ChangeTime(time1, e.Delta,g);
                time1 += 60*(e.Delta / 120);
            }
            if (hour1.Check(mx, my)){
                hour1.ChangeTime(time1, e.Delta,g);
                time1 += 3600*(e.Delta/120);
            }

            if (sec2.Check(mx, my))
            {
                sec2.ChangeTime(time2, e.Delta, g);
                time1 += (e.Delta / 120);
            }
            if (min2.Check(mx, my))
            {
                min2.ChangeTime(time2, e.Delta, g);
                time1 += 60 * (e.Delta / 120);
            }
            if (hour2.Check(mx, my))
            {
                hour2.ChangeTime(time2, e.Delta, g);
                time2 += 3600 * (e.Delta / 120);
            }
            int f = e.Delta;

        }
        
        private void Form1_Paint(object sender, PaintEventArgs e){
            Graphics g = this.CreateGraphics();
            min1.Draw(g,0);
            sec1.Draw(g,1);
            hour1.Draw(g,2);
            lev1.Draw(g);
            min2.Draw(g, 0);
            sec2.Draw(g, 1);
            hour2.Draw(g, 2);
            lev2.Draw(g);

            //g.DrawString("Hours", new Font("Arial", 14), Brushes.Black, 50, 70);
            //g.DrawString("Minutes", new Font("Arial", 14), Brushes.Black, 205, 70);
            //g.DrawString("Seconds", new Font("Arial", 14), Brushes.Black, 365, 70);

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int mx = Cursor.Position.X - this.Left;
            int my = Cursor.Position.Y - this.Top - 30;
            if (lev1.Check(mx, my)) {
                Graphics g = this.CreateGraphics();
                if (lev1.on) lev1.TurnOff(g);
                else lev1.TurnOn(g);
            }
            if (lev2.Check(mx, my))
            {
                Graphics g = this.CreateGraphics();
                if (lev2.on) lev2.TurnOff(g);
                else lev2.TurnOn(g);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;    
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (lev1.on)
            {
                time1++;
                Graphics g = this.CreateGraphics();
                sec1.ReDraw(g, time1, 1);
                min1.ReDraw(g, time1, 60);
                hour1.ReDraw(g, time1, 3600);

            }
            if (lev2.on)
            {
                time2++;
                Graphics g = this.CreateGraphics();
                sec2.ReDraw(g, time2, 1);
                min2.ReDraw(g, time2, 60);
                hour2.ReDraw(g, time2, 3600);

            }
        }

    }
}
