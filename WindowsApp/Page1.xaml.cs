using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace PhoneApp3
{
    public partial class Page1 : PhoneApplicationPage
    {

        struct node
        {
            public int x;
            public int y;

        };

        int rows, columns, players;
        int x, y;
        int[] flag = new int[15];
        int[,] a;
        int[,] b;
        int turn = 0;
        int w = 1;

        bool gameEnd = false;

        public Page1()
        {
            InitializeComponent();
     //       enter.Text = "Player 1 chance";

        }


        void printMatrix(int [,]a,int [,]b,int r,int c)
{
   foreach(Button bt in buttonCanvas.Children)
   {
       int i,j;
        i = Convert.ToInt32(bt.Name.Split(',')[1]);
        j = Convert.ToInt32(bt.Name.Split(',')[2]);
        bt.Content = "" + a[i, j];//+"," + b[i, j];
    
      if(b[i,j]==0)
        bt.Background = new SolidColorBrush(Colors.Transparent);

      if(b[i,j]==1)
        bt.Background= new SolidColorBrush(Colors.Blue);

     if (b[i, j] == 2)
         bt.Background = new SolidColorBrush(Colors.Red);

     if (b[i, j] == 3)
         bt.Background = new SolidColorBrush(Colors.Green);

     if (b[i, j] == 4)
         bt.Background = new SolidColorBrush(Colors.Yellow);

     if (b[i, j] == 5)
         bt.Background = new SolidColorBrush(Colors.Orange);

     if (b[i, j] == 6)
         bt.Background = new SolidColorBrush(Colors.LightGray);

     if (b[i, j] == 7)
         bt.Background = new SolidColorBrush(Colors.Magenta);

     if (b[i, j] == 8)
         bt.Background = new SolidColorBrush(Colors.Cyan);

   }
           
    
}



        bool checkIsValid(int x, int y, int p)
        {
            if (b[x, y] == p || b[x, y] == 0)
                return true;
            else
                return false;
        }



        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("msg", out msg))

                players = Convert.ToInt32(msg.Split(',')[0]);
            int k = Convert.ToInt32(msg.Split(',')[1]);
            if (k == 1)
            {
                rows = 5;
                columns = 5;
            }
            else if (k == 2)
            {
                rows = 8;
                columns = 6;
            }
            else
            {
                rows = 13;
                columns = 10;
            }



            if (k == 3)
            {

                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                       Button btn = new Button();
                        btn.Name = "btn," + j+","+ i;
                        btn.Height = 80;
                        btn.Width = 70;
                        btn.Margin = new Thickness(i * 45 - 50, j * 55 - 50, -20, -10);
                        buttonCanvas.Children.Add(btn);
                        btn.Tap += buttonTapHandler;
                    }
                }
            }
            else if (k == 2)
            {

                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        Button btn = new Button();
                        btn.Name = "btn," + j+"," + i;
                        btn.Height = 120;
                        btn.Width = 100;
                        btn.Margin = new Thickness(i * 78 - 50, j * 95 - 50, -20, -10);
                        buttonCanvas.Children.Add(btn);
                        btn.Tap += buttonTapHandler;
                    }
                }


            }
            else
            {

                for (int i = 0; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        Button btn = new Button();
                        btn.Name = "btn," + j +","+ i;
                        btn.Height = 120;
                        btn.Width = 120;

                        //btn.Background = SystemColors.ControlDarkDarkColor;
                        btn.Margin = new Thickness(i * 95 - 50, j * 95 - 50, -20, -10);
                        buttonCanvas.Children.Add(btn);
                        btn.Tap += buttonTapHandler;
                    }
                }



            }


            a = new int[20, 20];
            b = new int[20, 20];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    a[i, j] = 0;
                    b[i, j] = 0;
                }



            for (int i = 1; i <= players; i++)
                flag[i] = 1;

        

        }


        public bool checkifplayerloses(int p, int r, int c)
        {
            bool flag = true;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                    if (b[i, j] == p)
                    {
                        flag = false;
                        break;
                    }
                if (flag == false)
                    break;
            }

            return flag;

        }



        int capacity(int x, int y, int r, int c)
        {
            if ((x == 0 && y == 0) || (x == 0 && y == c - 1) || (x == r - 1 && y == 0) || (x == r - 1 && y == c - 1))
                return 2;
            else if (x >= 1 && x < r - 1 && y >= 1 && y < c - 1)
                return 4;
            else if ((x == 0 || x == r - 1) && (y != 0 && y != c - 1))
                return 3;
            else
                return 3;

        }


        void playthechance(int x, int y, int r, int c, int p)
        {
            Queue<node> q = new Queue<node>();
            node ne;
            ne.x = x;
            ne.y = y;
            q.Enqueue(ne);
            a[x, y]++;
            if (a[x, y] == 1)
                b[x, y] = p;
                // printMatrix(a, b, r, c);



            while (q.Count != 0)
            {

                bool breakflag = false;

                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                        if (b[i, j] != p && b[i, j] != 0)
                        {
                            breakflag = true;
                            break;
                        }
                    if (breakflag)
                        break;
                }

                if (!breakflag)
                    break;




                node curr;
                curr = q.Peek();
                q.Dequeue();
                if (a[curr.x, curr.y] >= capacity(curr.x, curr.y, r, c))
                {
                    a[curr.x, curr.y] = 0;
                    b[curr.x, curr.y] = 0;
                    if (curr.x >= 1)
                    {
                        node nu;
                        nu.x = curr.x - 1;
                        nu.y = curr.y;
                        q.Enqueue(nu);
                        a[nu.x, nu.y]++;
                        b[nu.x, nu.y] = p;
                    }
                    if (curr.x < r - 1)
                    {
                        node nu;
                        nu.x = curr.x + 1;
                        nu.y = curr.y;
                        q.Enqueue(nu);
                        a[nu.x, nu.y]++;
                        b[nu.x, nu.y] = p;


                    }
                    if (curr.y >= 1)
                    {
                        node nu;
                        nu.y = curr.y - 1;
                        nu.x = curr.x;
                        q.Enqueue(nu);
                        a[nu.x, nu.y]++;
                        b[nu.x, nu.y] = p;
                    }
                    if (curr.y < c - 1)
                    {
                        node nu;
                        nu.y = curr.y + 1;
                        nu.x = curr.x;
                        q.Enqueue(nu);
                        a[nu.x, nu.y]++;
                        b[nu.x, nu.y] = p;

                    }



                }
            }

            printMatrix(a, b, r, c);



        }




        private void buttonTapHandler(object sender, System.Windows.Input.GestureEventArgs e)
        {
            turn++;
            Button source = (Button)sender;

            x = Convert.ToInt32(source.Name.Split(',')[1]);
            y = Convert.ToInt32(source.Name.Split(',')[2]);

         //   enter.Text = "Player  " + w + "  Chance  ";
            if (checkIsValid(x, y, w))
            {
                playthechance(x, y, rows, columns, w);


                for (int i = 1; i <= players; i++)
                {
                    if (turn > players && checkifplayerloses(i, rows, columns))
                    {
                        flag[i] = 0;
                    }
                }


                int count = 0;
                for (int j = 1; j <= players; j++)
                    if (flag[j] == 1)
                        count++;

                if (count == 1)
                {
                    gameEnd = true;
                    NavigationService.Navigate(new Uri("/Page2.xaml?msg=" + w, UriKind.Relative));

                    // go to new xaml page
                }

                int chance = w;
                for (int i = w + 1; i <= players; i++)
                {
                    if (flag[i] == 1)
                    {
                        chance = i;
                        break;
                    }

                }

                if (chance == w)
                {

                    for (int i = 1; i < w; i++)
                    {
                        if (flag[i] == 1)
                        {
                            chance = i;
                            break;
                        }
                    }
                }

                  
                w = chance;

                if (w == 1)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush = new SolidColorBrush(Colors.Blue);
                    }
                    
                if (w == 2)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush = new SolidColorBrush(Colors.Red);
                    }

                if (w== 3)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush = new SolidColorBrush(Colors.Green);
                    }
                if (w == 4)
                   foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush = new SolidColorBrush(Colors.Yellow);
                   }
                if (w == 5)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush =new SolidColorBrush(Colors.Orange);
                    }
                if (w == 6)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush =new SolidColorBrush(Colors.LightGray);
                    }
                if (w == 7)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush =new SolidColorBrush(Colors.Magenta);}


                if (w == 8)
                    foreach (Button bt in buttonCanvas.Children)
                    {
                        int i, j;
                        i = Convert.ToInt32(bt.Name.Split(',')[1]);
                        j = Convert.ToInt32(bt.Name.Split(',')[2]);
                        bt.BorderBrush = new SolidColorBrush(Colors.Cyan);
                    }


            }
            else
            {

            }
        }
        


    }   
}



