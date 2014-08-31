using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp3
{
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
             base.OnNavigatedTo(e);

            string msg = "";
            int pl;
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {

                pl = Convert.ToInt32(msg);
                win.Text = "PLAYER " + pl ;

            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)    
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml" , UriKind.Relative));
            

        }
          




    }
}