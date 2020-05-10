using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WpfApp2
{
    class Map{

        public byte[,] map;
        public int n, m;
        public int w, h;
       
        public List<Tuple<int, int,Rectangle>> walls;
        public List<Tuple<int, int,int, Rectangle>> enemies;
        public List<Tuple<int, int, int, Rectangle>> ebull;

        public List<Tuple<int, int, Rectangle>> coins;

        public Map() {
            n = 60;
            m = 60;
            w = 12;
            h = 12;
            walls = new List<Tuple<int, int,Rectangle>>();
            coins = new List<Tuple<int, int, Rectangle>>();
            enemies=new List<Tuple<int, int,int, Rectangle>>();
            ebull=  new List<Tuple<int, int, int, Rectangle>>();

            map = new byte[n, m];
            StreamReader sr = new StreamReader("Map.txt");
            string line;
            int i = 0;
            while (i<60 &&(line = sr.ReadLine()) != null){
                Console.WriteLine(line);
                for (int j = 0; j < 59; j++) {
                    map[j, i] = (byte)(line[j] - '0');
                }
                i++;
            }
        }
      

    }
}
