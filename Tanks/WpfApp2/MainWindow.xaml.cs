using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        Map board;
        Player player;
        DispatcherTimer timer;
        DispatcherTimer timer2;
        Random rnd;
        SoundPlayer muzlo;
        const int LEFT = 0;
        const int UP = 1;
        const int RIGHT = 2;
        const int DOWN = 3;
        int lastShoot, lastShoot1;
        int SlowDown;
        int Count_Win = 0;
        int Count = 0;
        public MainWindow()
        {
            InitializeComponent();
            muzlo = new SoundPlayer(); ;
            muzlo.SoundLocation=@"Textures\muzlo.wav";
            muzlo.PlayLooping();
            OpenMenu();   
           
        }

        private void OpenMenu()
        {
            Rectangle r = new Rectangle();
            r.Width = 400;
            r.Height = 100;
            r.HorizontalAlignment = HorizontalAlignment.Center;
            r.VerticalAlignment = VerticalAlignment.Top;
            ImageBrush w = new ImageBrush(new BitmapImage(
                        new Uri(@"Textures\Title.png", UriKind.Relative)));
            r.Fill = w;
            grid.Children.Add(r);
            r = new Rectangle();
            r.Width = 400;
            r.Height = 100;
            r.HorizontalAlignment = HorizontalAlignment.Center;
            r.VerticalAlignment = VerticalAlignment.Center;
            w = new ImageBrush(new BitmapImage(
                        new Uri(@"Textures\Game.png", UriKind.Relative)));
            r.Fill = w;
            grid.Children.Add(r);
            this.KeyDown += new KeyEventHandler(start);

        }

        private void start(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space){
                this.KeyDown -= new KeyEventHandler(start);
                grid.Children.Clear();
                startGame();
            }
        }

        void startGame() {
            Count_Win = 0;
            lastShoot = 0;
            SlowDown = 0;

            lastShoot1 = 0;
            timer = new DispatcherTimer();
            timer2 = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer2.Tick += new EventHandler(timer_Tick2);
            timer2.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer2.Start();

            timer.Start();

            board = new Map();
            player = new Player(1, 1);
            player.life = 3;
            rnd = new Random();
            DrawField();
            DrawPlayer();
            this.KeyDown += new KeyEventHandler(KeyEvents);
            this.KeyDown += new KeyEventHandler(KeyShoot);


        }
        //ОТРИСОВКА -----------------------------------
        void DrawPlayer() {
            Rectangle r = new Rectangle();
            r.VerticalAlignment = VerticalAlignment.Top;
            r.HorizontalAlignment = HorizontalAlignment.Left;
            r.Margin = new Thickness(player.posx * board.w, player.posy * board.h, 0, 0);
            r.Width = 3*board.w;
            r.Height = 3*board.h;
            ImageBrush w = new ImageBrush(new BitmapImage(
                                 new Uri(@"Textures/Tank_Up.png", UriKind.Relative)));
            if (player.dir == LEFT)
            {
                w = new ImageBrush(new BitmapImage(
                                 new Uri(@"Textures/Tank_Left.png", UriKind.Relative)));
            }
            if (player.dir == RIGHT)
            {
                w = new ImageBrush(new BitmapImage(
                                 new Uri(@"Textures/Tank_Right.png", UriKind.Relative)));
            }
            if (player.dir ==DOWN)
            {
                w = new ImageBrush(new BitmapImage(
                                 new Uri(@"Textures/Tank_Down.png", UriKind.Relative)));
            }
            r.Fill = w;
            grid.Children.Add(r);
            
        }
        void DrawField(){
            for (int i = 0; i < board.n; i++)
            {
                for (int j = 0; j < board.n; j++)
                {
                    if (board.map[i, j] == 2){
                        ImageBrush w= new ImageBrush(new BitmapImage(
                             new Uri(@"Textures\Wall1.png", UriKind.Relative)));
                        Rectangle r = CreateRetangle(i, j,w);
                        Tuple<int, int, Rectangle> tuple = new Tuple<int, int, Rectangle>(i, j, r);
                        board.walls.Add(tuple);
                    }
                    if (board.map[i, j] == 3) {
                        ImageBrush w = new ImageBrush(new BitmapImage(
                             new Uri(@"C:\Users\rabbi\source\repos\WpfApp2\WpfApp2\Textures\Gray_Wall.png", UriKind.Relative)));

                        CreateRetangle(i, j, w);
                    }
                    if (board.map[i, j] == 4)
                    {
                        Count_Win++;
                        ImageBrush w = new ImageBrush(new BitmapImage(
                                new Uri(@"Textures\Coin.png", UriKind.Relative)));
                        Rectangle r = CreateRetangle(i, j,w);
                        Tuple<int, int, Rectangle> tuple = new Tuple<int, int, Rectangle>(i, j, r);
                        board.coins.Add(tuple);
                    }
                    if (board.map[i, j] == 6) {
                        Count_Win++;
                        AddEnemy(i, j,0,-1);

                    }

                }

            }
            int x = board.n * board.w+1;
            int y = board.n * board.w + 1;

            for (int i = 0; i < 3; i++){


            }

        }

        private void AddEnemy(int i, int j,int dir1,int pos){
            Rectangle r = new Rectangle();
            r.VerticalAlignment = VerticalAlignment.Top;
            r.HorizontalAlignment = HorizontalAlignment.Left;
            r.Margin = new Thickness(i * board.w, j * board.h, 0, 0);
            r.Width = 3 * board.w;
            r.Height = 3 * board.h;
            ImageBrush w = new ImageBrush(new BitmapImage(
                                new Uri(@"Textures\enemy_up.png", UriKind.Relative)));
            if (dir1 == DOWN){
                 w = new ImageBrush(new BitmapImage(
                               new Uri(@"Textures\enemy_down.png", UriKind.Relative)));
            }
            if (dir1 == LEFT)
            {
                w = new ImageBrush(new BitmapImage(
                              new Uri(@"Textures\enemy_left.png", UriKind.Relative)));
            }
            if (dir1 == RIGHT)
            {
                w = new ImageBrush(new BitmapImage(
                              new Uri(@"Textures\enemy_right.png", UriKind.Relative)));
            }
            r.Fill = w;
            Tuple<int, int,int, Rectangle> tuple = new Tuple<int, int,int, Rectangle>(i, j,dir1,r);
            if (pos != -1)
            {
                board.enemies.Insert(pos, tuple);
                for (int i1 = i; i1 < i + 3; i1++)
                    for (int j1 = j; j1 < j + 3; j1++)
                       if(board.map[i1, j1] != 4) board.map[i1, j1] = 6; 
            }
            else board.enemies.Add(tuple);
            grid.Children.Insert(grid.Children.Count-1, r);
            
        }

        
        Rectangle CreateRetangle(int i, int j, Brush brushes) {
            Rectangle r = new Rectangle();
            r.VerticalAlignment = VerticalAlignment.Top;
            r.HorizontalAlignment = HorizontalAlignment.Left;
            r.Margin = new Thickness(i * board.w, j * board.h, 0, 0);
            r.Width = board.w+1;
            r.Height = board.h+1;
            r.Fill = brushes;
            if (grid.Children.Count > 2)
            {
                grid.Children.Insert(grid.Children.Count - 2, r);
            }
            else grid.Children.Insert(0, r);
            return r;
        }

        //ДВИЖЕНИЕ ИГРОКА ---------------------------------
        private void KeyEvents(object sender, KeyEventArgs e)
        {
            int dir1=-1;
            if (e.Key == Key.Down) dir1 = DOWN;
            if (e.Key == Key.Up) dir1 = UP;
            if (e.Key == Key.Right) dir1 = RIGHT;
            if (e.Key == Key.Left) dir1 = LEFT;
            if (dir1 == -1) return;
            if (dir1 != player.dir) player.dir = dir1;
            else
            {
                if (SlowDown < 2) return;
                SlowDown = 0;
                if (e.Key == Key.Down && check(player.posx, player.posy + 1))
                {
                    player.posy += 1;
                }
                if (e.Key == Key.Up && check(player.posx, player.posy - 1))
                {
                    player.posy -= 1;
                }
                if (e.Key == Key.Left && check(player.posx - 1, player.posy))
                {
                    player.posx -= 1;
                }
                if (e.Key == Key.Right && check(player.posx + 1, player.posy))
                {
                    player.posx += 1;
                }
            }
            grid.Children.RemoveAt(grid.Children.Count - 1);
                DrawPlayer();
                check_coins(player.posx, player.posy);
        }

        private void check_coins(int posx, int posy) {
            for (int i = posx; i < posx + 3; i++){
                for (int j = posy; j < posy + 3; j++){
                    if (board.map[i, j] == 4) {
                        for (int i1 = 0; i1 < board.coins.Count; i1++) {
                            if (i == board.coins[i1].Item1 && j == board.coins[i1].Item2) {
                                grid.Children.Remove(board.coins[i1].Item3);
                                board.coins.RemoveAt(i1);
                                Count++;
                                if (Count == Count_Win) Win();
                            }    
                        }    
                    }
                }
            }
        }

        private void Win()
        {
            this.KeyDown -= new KeyEventHandler(KeyEvents);
            this.KeyDown -= new KeyEventHandler(KeyShoot);
            grid.Children.Clear();
            player.bullets.Clear();
            timer.Stop();
            timer2.Stop();
            board.ebull.Clear();
            Rectangle r = new Rectangle();
            r.Width = 400;
            r.Height = 100;
            r.HorizontalAlignment = HorizontalAlignment.Center;
            r.VerticalAlignment = VerticalAlignment.Center;
            ImageBrush w = new ImageBrush(new BitmapImage(
                        new Uri(@"Textures\Win.png", UriKind.Relative)));
            r.Fill = w;
            grid.Children.Add(r);
            this.KeyDown += new KeyEventHandler(toMEnu);

        }

        bool check(int posx,int posy) {
            bool flag=true;
            for (int i = posx; i < posx + 3; i++) {
                for (int j = posy; j < posy + 3; j++) {
                    if (i >= 0 && i < board.n && j >= 0 && j < board.n &&( board.map[i, j] !=2 && board.map[i,j]!=3)) ;
                    else flag = false;
                }
            }                
            return flag;
            
        }

        //СТРЕЛЬБА ----------------------------------------

        private void KeyShoot(object sender, KeyEventArgs e)
        {
            if (lastShoot < 10) return;
            if (e.Key == Key.Space) {
                player.shoot();
                lastShoot = 0;
                int px = player.bullets[player.bullets.Count - 1].Item1;
                int py = player.bullets[player.bullets.Count - 1].Item2;
                int dir = player.bullets[player.bullets.Count - 1].Item3;
                ImageBrush w = new ImageBrush(new BitmapImage(
                            new Uri(@"Textures\Bullet.png", UriKind.Relative)));
                Rectangle r = CreateRetangle(px,py,w);
                Tuple<int, int, int, Rectangle> tuple = new Tuple<int, int, int, Rectangle>(px, py, dir, r);
                player.bullets.RemoveAt(player.bullets.Count - 1);
                player.bullets.Add(tuple);
            }
        }

        private void timer_Tick(object sender, EventArgs e){
            lastShoot++;
            SlowDown++;
            ReloadBullets();
            lastShoot1++;
            ReloadEBull();

        }
        
        private void ReloadBullets()
        {
            for (int i = 0; i < player.bullets.Count; i++)
            {
                int px = player.bullets[i].Item1;
                int py = player.bullets[i].Item2;
                int dir = player.bullets[i].Item3;
                if (board.map[px, py] == 2) board.map[px, py] = 0;
                if (player.bullets[i].Item3 == LEFT) px--;
                if (player.bullets[i].Item3 == RIGHT) px++;
                if (player.bullets[i].Item3 == UP) py--;
                if (player.bullets[i].Item3 == DOWN) py++;
                grid.Children.Remove(player.bullets[i].Item4);
                player.bullets.RemoveAt(i);
                if (px >= 0 && px < board.n && py >= 0 && py < board.n && (board.map[px, py] == 0 || board.map[px, py] == 4))
                {
                    ImageBrush w = new ImageBrush(new BitmapImage(
                           new Uri(@"Textures\Bullet.png", UriKind.Relative)));
                    Rectangle r = CreateRetangle(px, py, w);
                    Tuple<int, int, int, Rectangle> tuple = new Tuple<int, int, int, Rectangle>(px, py, dir, r);
                    player.bullets.Insert(i, tuple);
                }
                else check_hit(px, py);
            }
        }

        private void check_hit(int px, int py){
            if (board.map[px, py] == 6)
            {
                for (int i1 = 0; i1 < board.enemies.Count; i1++)
                {
                    int x = board.enemies[i1].Item1;
                    int y = board.enemies[i1].Item2;
                    if (px >= x && py >= y && px < x + 3 && py < y + 3)
                    {
                        grid.Children.Remove(board.enemies[i1].Item4);
                        for (int i2 = board.enemies[i1].Item1; i2 < board.enemies[i1].Item1 + 3; i2++)
                            for (int j2 = board.enemies[i1].Item2; j2 < board.enemies[i1].Item2 + 3; j2++) board.map[i2, j2] = 0;
                        board.enemies.RemoveAt(i1);
                        board.map[x, y] = 4;

                        ImageBrush w = new ImageBrush(new BitmapImage(
                        new Uri(@"C:\Users\rabbi\source\repos\WpfApp2\WpfApp2\Textures\Coin.png", UriKind.Relative)));
                        Rectangle r = CreateRetangle(x, y, w);
                        Tuple<int, int, Rectangle> tuple = new Tuple<int, int, Rectangle>(x, y, r);
                        board.coins.Add(tuple);
                        break;
                    }

                }

            }

            if (px>=0 && py>=0 && px<board.n && py<board.n && board.map[px, py] == 2) {
                for (int i = px - 1; i <= px + 1; i++) {
                    for (int j = py - 1; j <= py + 1; j++) {
                        if (i >= 0 && i < board.n && j >= 0 && j < board.n && board.map[i, j] == 2) {
                            for (int i1 = 0; i1 < board.walls.Count; i1++) {
                                if (board.walls[i1].Item1 == i && board.walls[i1].Item2 == j) {
                                    grid.Children.Remove(board.walls[i1].Item3);
                                    board.walls.RemoveAt(i1);
                                    board.map[i, j] = 0;
                                    
                                   
                                    break;
                                    
                                }
                            }
                        
                        }
                        


                    }
                }
            }
            
        }

        //Враги-----------------------
        private void timer_Tick2(object sender, EventArgs e)
        {
            
            for (int i = 0; i < board.enemies.Count; i++)
            {
                int c = checkplayer(i);
                if (c == -1)
                {
                    ReloadEnemy(i);
                }
                else {
                    if (c == board.enemies[i].Item3) EnemiesShoot(board.enemies[i].Item1 + 1, board.enemies[i].Item2 + 1,
                                                                                   board.enemies[i].Item3);
                    else
                    {
                        int x = board.enemies[i].Item1;
                        int y = board.enemies[i].Item2;
                        Rectangle r = board.enemies[i].Item4;
                        int dir1 = c;
                        grid.Children.Remove(board.enemies[i].Item4);
                        board.enemies.RemoveAt(i);
                        AddEnemy(x, y, dir1, i);


                    }
                }
                
            }
        }

        private int checkplayer(int index){
            for (int i = 0; i < 3; i++) {
                int x = board.enemies[index].Item1+i;
                int y = board.enemies[index].Item2+i;
                for(int py = y; py > 0; py--){
                    if (board.map[x, py] == 2 || board.map[x, py] == 3) break;
                    if (x >= player.posx && py >= player.posy && x <= player.posx && py <= player.posy) return UP;
                }
                for (int py = y; py <board.n ; py++)
                {
                    if (board.map[x, py] == 2 || board.map[x, py] == 3) break;
                    if (x >= player.posx && py >= player.posy && x <= player.posx && py <= player.posy) return DOWN;
                }
                for (int px = x; px < board.n; px++)
                {
                    if (board.map[px, y] == 2 || board.map[px, y] == 3) break;
                    if (px >= player.posx && y >= player.posy && px <= player.posx && y <= player.posy) return RIGHT;
                }
                for (int px = x; px >0; px--)
                {
                    if (board.map[px, y] == 2 || board.map[px, y] == 3) break;
                    if (px >= player.posx && y >= player.posy && px <= player.posx && y <= player.posy) return LEFT;
                }

            }
            return -1;
        }

        private void ReloadEnemy(int i)
        {
               int px = board.enemies[i].Item1;
                int py = board.enemies[i].Item2;
                int dir1 = board.enemies[i].Item3;
                if (board.enemies[i].Item3 == LEFT)
                {
                    px -= 1;
                }
                if (board.enemies[i].Item3 == RIGHT)
                {
                    px += 1;
                }
                if (board.enemies[i].Item3 == UP)
                {
                    py -= 1;
                }
                if (board.enemies[i].Item3 == DOWN)
                {
                    py += 1;
                }
                if (check(px, py))
                {
                    for (int i1 = board.enemies[i].Item1; i1 < board.enemies[i].Item1 + 3; i1++)
                        for (int j1 = board.enemies[i].Item2; j1 < board.enemies[i].Item2 + 3; j1++)
                            if(board.map[i1, j1]!=4) board.map[i1, j1] = 0;
                    grid.Children.Remove(board.enemies[i].Item4);
                    board.enemies.RemoveAt(i);
                    AddEnemy(px, py, dir1,i);
                }
                else
                {
                    int x = board.enemies[i].Item1;
                    int y = board.enemies[i].Item2;
                    Rectangle r = board.enemies[i].Item4;
                    dir1 = rnd.Next(4);
                    Tuple<int, int, int, Rectangle> tuple = new Tuple<int, int, int, Rectangle>(x, y, dir1, r);
                    board.enemies.RemoveAt(i);
                    board.enemies.Insert(i, tuple);
                }
             

        }
        private void EnemiesShoot(int px,int py,int dir){
            if (lastShoot1 < 10) return;
            lastShoot1 = 0;
            ImageBrush w = new ImageBrush(new BitmapImage(
                        new Uri(@"Textures\Bullet.png", UriKind.Relative)));
            if (dir == LEFT) px-=2;
            if (dir == RIGHT) px+=2;
            if (dir == UP) py-=2;
            if (dir == DOWN) py+=2;
            Rectangle r = CreateRetangle(px, py, w);
            Tuple<int, int, int, Rectangle> tuple = new Tuple<int, int, int, Rectangle>(px, py, dir, r);
            board.ebull.Add(tuple);
            
        }

        private void ReloadEBull(){
            for (int i = 0; i < board.ebull.Count; i++)
            {
                int px = board.ebull[i].Item1;
                int py = board.ebull[i].Item2;
                int dir = board.ebull[i].Item3;
                if (board.map[px, py] == 2) board.map[px, py] = 0;
                if (dir == LEFT) px--;
                if (dir == RIGHT) px++;
                if (dir == UP) py--;
                if (dir == DOWN) py++;
                grid.Children.Remove(board.ebull[i].Item4);
                board.ebull.RemoveAt(i);
                if (px >= 0 && px < board.n && py >= 0 && py < board.n && (board.map[px, py] == 0 || board.map[px, py] == 4))
                {
                    ImageBrush w = new ImageBrush(new BitmapImage(
                           new Uri(@"Textures\Bullet.png", UriKind.Relative)));
                    Rectangle r = CreateRetangle(px, py, w);
                    Tuple<int, int, int, Rectangle> tuple = new Tuple<int, int, int, Rectangle>(px, py, dir, r);
                    board.ebull.Insert(i, tuple);
                    enemy_hit(px,py,i);
                }
                
            }
        }

        private void enemy_hit(int px,int py,int i){
            if (px >= player.posx && py >= player.posy && py < player.posy + 2 && px < player.posx + 2)
            {
                 player.life--;
                grid.Children.Remove(board.ebull[i].Item4);
                board.ebull.RemoveAt(i);
            }
            if (player.life == 0) {
                this.KeyDown -= new KeyEventHandler(KeyEvents);
                this.KeyDown -= new KeyEventHandler(KeyShoot);
                grid.Children.Clear();
                player.bullets.Clear();
                board.ebull.Clear();
            Rectangle r = new Rectangle() ;
                r.Width = 400;
                r.Height= 100;
                r.HorizontalAlignment = HorizontalAlignment.Center;
                r.VerticalAlignment = VerticalAlignment.Center;
                ImageBrush w = new ImageBrush(new BitmapImage(
                            new Uri(@"Textures\Lose.png", UriKind.Relative)));
                r.Fill = w;
                this.KeyDown += new KeyEventHandler(toMEnu);
                grid.Children.Add(r);
                timer.Stop();
                timer2.Stop();



            }
        }

        private void toMEnu(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                grid.Children.Clear();
                this.KeyDown -= new KeyEventHandler(toMEnu);
                OpenMenu();
            }
            
        }
    }
}
