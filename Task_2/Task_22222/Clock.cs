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
    class Clock{
        double angle;
        Form1 master;
        int mod;
        int r;
        int x, y; //location
        int cx, cy;
        //construction
        public Clock(Form1 form, int r1, int x1, int y1,int mod1) {
            x = x1;
            y = y1;
            r = r1;
            cx = x+r;
            mod = mod1;
            cy = y;
            master = form;
           
            angle = 0;
        }

        internal void Draw(Graphics g) {
            g.DrawRectangle(Pens.Red, x, y,2*r+1,2*r+1);
            g.FillRectangle(Brushes.LightGray, x, y, 2 * r + 1, 2 * r + 1);
            g.DrawEllipse(Pens.Black, x, y, 2*r, 2*r);
            g.DrawString("12", new Font("Arial", 10),Brushes.Red,x+r-5,y);
            g.DrawString("9", new Font("Arial", 10), Brushes.Red, x+5, y+r-5);
            g.DrawString("6", new Font("Arial", 10), Brushes.Red, x + r-3, y+2*r-15);
            g.DrawString("3", new Font("Arial", 10), Brushes.Red, x + 2*r-15, y + r-5);
            g.DrawLine(Pens.Black, x + r, y + r, cx, cy);
           
        }
        internal void ReDraw(Graphics g,int time,int mode){
            g.DrawString(time.ToString(), new Font("Arial", 10), Brushes.Black, 50, 50);
            if (mode == 60 && time > 55) {
                int i = 0;
            }
            if (time / mode >(time-1)/mode){
                angle += (float)(-12* Math.PI / 360.0);
                cx = -(int)((double)r * Math.Sin(angle))+x+r;
                cy = -(int)((double)r * Math.Cos(angle))+y+r;
                master.Invalidate();
            }
            g.DrawLine(Pens.Black, x + r, y + r, cx, cy);
            //g.DrawString(time.ToString(), new Font("Arial", 10), Brushes.Black, 50, 50);

        }

        public bool Check(int mx, int my) {
            return mx>=x && mx<=x+2*r && my>=y && my<=2*r+y;
        }

        public void ChangeTime(int time,int delta,Graphics g) {
            angle += (float)(-12*(delta/120) *Math.PI / 360.0);
            cx = -(int)((double)r * Math.Sin(angle)) + x + r;
            cy = -(int)((double)r * Math.Cos(angle)) + y + r;
            master.Invalidate();
            g.DrawLine(Pens.Black, x + r, y + r, cx, cy);
        }
    }
}
