using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApp2
{
    
    class Player
    {
        const int LEFT = 0;
        const int UP = 1;
        const int RIGHT = 2;
        const int DOWN = 3;
        public int posx,posy,dir;
        public int life;
        public List<Tuple<int, int,int,Rectangle>> bullets;
        public Player(int x, int y) {
            posx = x;
            posy = y;
            dir = 0;
            bullets = new List<Tuple<int, int, int,Rectangle>>();

        }
        public void shoot() {
            life = 3;
            int px = posx+1;
            int py = posy+1;
            if (dir == LEFT) px--;
            if (dir == RIGHT) px++;
            if (dir == UP) py--;
            if (dir == DOWN) py++;
            Rectangle r = new Rectangle();
            Tuple<int, int, int,Rectangle> tuple = new Tuple<int, int, int,Rectangle>(px, py, dir,r);
            bullets.Add(tuple);
        }
    }
}
