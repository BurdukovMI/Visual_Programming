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
        Clock min, sec, hour;
        int time = 0;
        Lever lev;


        public Form1()
        {
            InitializeComponent();
            sec = new Clock(this, 75, 365, 100, 1);
            min = new Clock(this, 75, 205, 100, 60);
            hour = new Clock(this, 75, 50, 100, 3600);
            lev = new Lever(this);

            this.MouseWheel += new MouseEventHandler(this_MouseWheel);
        }

        void this_MouseWheel(object sender, MouseEventArgs e)
        {
            int mx = Cursor.Position.X-this.Left;
            int my = Cursor.Position.Y-this.Top-30;
            Graphics g = this.CreateGraphics();
            if (sec.Check(mx, my)){
                sec.ChangeTime(time,e.Delta,g);
                time += (e.Delta / 120);
            }
            if (min.Check(mx, my)){
                min.ChangeTime(time, e.Delta,g);
                time += 60*(e.Delta / 120);
            }
            if (hour.Check(mx, my)){
                hour.ChangeTime(time, e.Delta,g);
                time += 3600*(e.Delta/120);
            }
            int f = e.Delta;

        }
        
        private void Form1_Paint(object sender, PaintEventArgs e){
            Graphics g = this.CreateGraphics();
            min.Draw(g);
            sec.Draw(g);
            hour.Draw(g);
            lev.Draw(g);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int mx = Cursor.Position.X - this.Left;
            int my = Cursor.Position.Y - this.Top - 30;
            if (lev.Check(mx, my)) {
                Graphics g = this.CreateGraphics();
                if (lev.on) {
                    lev.TurnOff(g);
                }
                else {

                    lev.TurnOn(g);
                }
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
            if (lev.on)
            {
                time++;
                Graphics g = this.CreateGraphics();
                sec.ReDraw(g, time, 1);
                min.ReDraw(g, time, 60);
                hour.ReDraw(g, time, 3600);

            }
        }

    }
}
