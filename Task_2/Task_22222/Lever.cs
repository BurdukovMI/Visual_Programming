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
    class Lever
    {
        int x, y, w, h;
        int cx, cy;
        Form1 master;
        public bool on;
        public Lever(Form1 form) {
            x = 530;
            y = 100;
            w = 50;
            h = 150;
            on = false;
            cx = 530;
            cy = 100;
            master = form;
        }
        public void Draw(Graphics g) {
            
            g.FillRectangle(Brushes.LightGray, x, y, w, h);
            g.FillRectangle(Brushes.Gray, cx, cy, w, w);

        }
        
        public bool Check(int mx,int my) {
            return mx >= cx && mx <= cx + w && my >= cy && my <= cy + w;
        }

        public void TurnOff(Graphics g) {
            master.Invalidate();
            on = false;
            cx= cx = 530;
            cy = 100;
            g.FillRectangle(Brushes.Gray, cx, cy, w, w);

        }
        public void TurnOn(Graphics g)
        {
            master.Invalidate();
            on = true;
            cx =530;
            cy = 200;
            g.FillRectangle(Brushes.Gray, cx, cy, w, w);

        }
    }
}
