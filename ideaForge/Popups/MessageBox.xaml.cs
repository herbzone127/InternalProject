﻿using IdeaForge.Core.Utilities;
using MahApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for CMessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }
        static MessageBox cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public CMessageTitle messageTitle { get; set; }
        static Window LoginPage { get; set; }
        public enum CMessageButton
        {
            Ok,
            Yes,
            No,
            Cancel,
            Confirm,
            Successful
        }
        public string GetTitle(CMessageTitle value)
        {
            return Enum.GetName(typeof(CMessageTitle), value);
        }
        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }

        public enum CMessageTitle
        {
            Error,
            Info,
            Warning,
            Confirm,
            Successful
        }
        public static DialogResult Show(string message, CMessageTitle title, CMessageButton okButton,string coloredMessage)
        {
            cMessageBox = new MessageBox();
            Global.isStoped = true;
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(okButton);
            //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(title);
            cMessageBox.lblColorMessage.Text = coloredMessage;
            BackgroundBlur();

       
                //if(Application.Current.MainWindow is Login)
                //{
                //    //LoginPage = new Window();
                //    LoginPage = Application.Current.MainWindow;
                //    //LoginPage.Background = ConvertColor("#000000");
                //    //LoginPage.Opacity = 0.1;
                //    //LoginPage.AllowsTransparency = true;
                //    LoginPage.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
                //    LoginPage.IsEnabled = false;
                //}
                //if(Application.Current.MainWindow is Dashboard)
                //{
                //    //LoginPage = new Window();
                //    LoginPage = Application.Current.MainWindow;
                //    //LoginPage.Background = ConvertColor("#000000");
                //    //LoginPage.Opacity = 0.1;
                //    //LoginPage.AllowsTransparency = true;
                //    LoginPage.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
                //    LoginPage.IsEnabled = false;
                //}

            //}
          
            //cMessageBox.Effect =new DropShadowEffect() { Color = Colors.Black, Opacity=100, ShadowDepth=0,BlurRadius=100,  };
            //icon
            switch (title)
            {
                case CMessageTitle.Error:
                    //cMessageBox.msgLogo.Kind = PackIconKind.Error;
                    //cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/ErrorBack.png", UriKind.Relative));
                    cMessageBox.btnOk.Background = ConvertColor("#91C84F");
                 
                    break;
                case CMessageTitle.Info:
                    //cMessageBox.msgLogo.Kind = PackIconKind.InfoCircle;
                    //cMessageBox.msgLogo.Foreground = Brushes.Blue;
                    //cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    //cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Warning:
                    //cMessageBox.msgLogo.Kind = PackIconKind.Warning;
                    //cMessageBox.msgLogo.Foreground = Brushes.Yellow;
                    //cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    //cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Confirm:
                    cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/SuccessBack.png",UriKind.Relative));
                    cMessageBox.btnOk.Background= ConvertColor("#91C84F");
            
                    //cMessageBox.msgLogo.Kind = PackIconKind.QuestionMark;
                    //cMessageBox.msgLogo.Foreground = Brushes.Gray;

                    break;
            }
            if (Global.isPopupShown==0)
            {
                Global.isPopupShown = 1;
                cMessageBox.ShowDialog();
            }
          
            return result;
        }
        public static DialogResult ShowError(string message)
        {
            cMessageBox = new MessageBox();
            Global.isStoped = true;
            LoginPage = new Window();
            BackgroundBlur();
            //if (LoginPage != null)
            //{
            //    LoginPage = Application.Current.MainWindow;
            //    LoginPage.IsEnabled = false;
            //    LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //   if(cMessageBox.Parent is Login)
            //    {
            //        LoginPage = cMessageBox.Parent as Login;
            //        LoginPage.IsEnabled = false;
            //        LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    }
            //   if(cMessageBox.Parent is Dashboard)
            //    {
            //        LoginPage = cMessageBox.Parent as Dashboard;
            //        LoginPage.IsEnabled = false;
            //        LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    }

            //}
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(CMessageButton.Ok);
            //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtMessage.HorizontalAlignment=System.Windows.HorizontalAlignment.Center;
            cMessageBox.lblColorMessage.Visibility = Visibility.Hidden;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(CMessageTitle.Error);
            cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/ErrorBack.png", UriKind.Relative));
            cMessageBox.btnOk.Background = ConvertColor("#91C84F");
            if (Global.isPopupShown == 0)
            {
                Global.isPopupShown = 1;
                cMessageBox.ShowDialog();
            }

            return result;
        }
        public static DialogResult ShowSuccess(string message, string coloredMessage)
        {
            cMessageBox = new MessageBox();
            Global.isStoped = true;
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(CMessageButton.Ok);
            BackgroundBlur();
            //LoginPage = new Window();
            //if (LoginPage != null)
            //{
            //    LoginPage = Application.Current.MainWindow;
            //    LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            //    if(cMessageBox.Parent is Login)
            //    {
            //        LoginPage = cMessageBox.Parent as Login;
            //        LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    }
            //    if (cMessageBox.Parent is Dashboard)
            //    {
            //        LoginPage = cMessageBox.Parent as Dashboard;
            //        LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    }
            //}
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(CMessageTitle.Confirm);
            cMessageBox.lblColorMessage.Text = coloredMessage;
            cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/SuccessBack.png", UriKind.Relative));
            cMessageBox.btnOk.Background = ConvertColor("#91C84F");
            if (Global.isPopupShown == 0)
            {
                Global.isPopupShown = 1;
                cMessageBox.ShowDialog();
            }
            return result;
        }

        public static DialogResult ShowSuccessful(string message, string coloredMessage)
        {
            cMessageBox = new MessageBox();
            Global.isStoped = true;
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(CMessageButton.Ok);
            BackgroundBlur();
            //LoginPage = new Window();
            //if (LoginPage != null)
            //{
            //    LoginPage = Application.Current.MainWindow;
            //    LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            //    if(cMessageBox.Parent is Login)
            //    {
            //        LoginPage = cMessageBox.Parent as Login;
            //        LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    }
            //    if (cMessageBox.Parent is Dashboard)
            //    {
            //        LoginPage = cMessageBox.Parent as Dashboard;
            //        LoginPage.Effect =new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            //    }
            //}
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(CMessageTitle.Successful);
            cMessageBox.lblColorMessage.Text = coloredMessage;
            cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/SuccessfulAdminIcon.png", UriKind.Relative));
            cMessageBox.btnOk.Background = ConvertColor("#91C84F");
            if (Global.isPopupShown == 0)
            {
                Global.isPopupShown = 1;
                cMessageBox.ShowDialog();
            }
            return result;
        }
        private void BntOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            Border border = new Border();
            BackgroundClear();
            cMessageBox.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            BackgroundClear();
            cMessageBox.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 0;
            growAnimation.To = 1;
            storyboard.Children.Add(growAnimation);
           
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);

            storyboard.Begin();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BackgroundClear();
            Global.isPopupShown = 0;
            Global.isStoped = false;
            //LoginPage.AllowsTransparency = false;
            Closing -= Window_Closing;
            e.Cancel = true;
            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 1;
            growAnimation.To = 0;
            storyboard.Children.Add(growAnimation);
      
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);
            growAnimation.Completed += GrowAnimation_Completed;
       
            storyboard.Begin();
        }

        private void GrowAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
            Global.isPopupShown = 0;
        }
        private static Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }
        public static void BackgroundBlur()
        {
            var loginWindow = App.Current.Windows.OfType<Login>().FirstOrDefault();
            if (loginWindow != null)
            {

                loginWindow.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            }
            var dashboardWindow = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            if (dashboardWindow != null)
            {
                dashboardWindow.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            }
        }
        public void BackgroundClear()
        {
            var loginWindow = App.Current.Windows.OfType<Login>().FirstOrDefault();
            if (loginWindow != null)
            {
                if (loginWindow.Effect != null)
                {
                    loginWindow.Effect = null;
                }

            }
            var dashboardWindow = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            if (dashboardWindow != null)
            {
                if (dashboardWindow.Effect != null)
                {
                    dashboardWindow.Effect = null;
                }

            }
        }
        private void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            Global.isPopupShown = 0;
            this.Close();
        }
    }
}