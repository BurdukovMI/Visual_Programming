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
        double size;
        Form1 master;
        public bool on;
        public Lever(Form1 form,int x1,int y1,double size1) {
            x = x1;
            y = y1;
            size = size1;
            w = (int)(50*size1);
            h = (int)(150 * size1);
            on = false;
            cx = x1;
            cy = y1;
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
            cx= x;
            cy = y;
            g.FillRectangle(Brushes.Gray, cx, cy, w, w);

        }
        public void TurnOn(Graphics g)
        {
            master.Invalidate();
            on = true;
            cx = x;
            cy = y+h-w;
            g.FillRectangle(Brushes.Gray, cx, cy, w, w);

        }
    }
}
