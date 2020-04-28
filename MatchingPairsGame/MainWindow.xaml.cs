using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global 
 
        Random randGen = new Random();
        Uri solidBackground = new Uri(System.Environment.CurrentDirectory + "\\backgroundImage\\Solid.jpg");
        Uri[] imageChosen = new Uri[36];
        private System.Windows.Threading.DispatcherTimer myTimer;
        Image[] imageArray;
        Border[] borderArray;
       
        int countTime = 3;
        int levelNum = 0;
        int numLives = 0;
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(solidBackground);
            this.Background = myBrush;
            myTimer = new System.Windows.Threading.DispatcherTimer();
            myTimer.Interval = TimeSpan.FromMilliseconds(1000);
            myTimer.Tick += myTimerTicked;
            imageArray = new Image[36];
            borderArray = new Border[36];
            addingBorders();
            UriImages();
           

        }
        public void myTimerTicked(Object sender, EventArgs e)
        {

            countTime--;
            if (countTime == 0)
            {
                myTimer.Stop();
                countTime = UserTimeSet(countTime);
                for (int j = 0; j < 6; j++)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        imageArray[i + 6 * j].Visibility = Visibility.Hidden;
                    }
                }
            }

        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            btnStartGame.IsEnabled = false;
             ShufflingImages(ref imageChosen);
            for (int i = 0; i < imageArray.Length; i++)
            {
                imageArray[i].Source = new BitmapImage(imageChosen[i]);
            }
            myTimer.Start();

        }
        private void addingBorders()
        {
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 6; i++)
                {
                    borderArray[i + 6 * j] = new Border();
                    borderArray[i + 6 * j].BorderBrush = new SolidColorBrush(Colors.Black);
                    borderArray[i + 6 * j].BorderThickness = new Thickness(2);
                    borderArray[i + 6 * j].Background = new SolidColorBrush(Colors.OrangeRed);
                    BaseGrid.Children.Add(borderArray[i + 6 * j]);
                    imageArray[i + 6 * j] = new Image();
                    imageArray[i + 6 * j].Stretch = Stretch.UniformToFill;
                    borderArray[i + 6 * j].Child = imageArray[i + 6 * j];
                    borderArray[i + 6 * j].Height = 75;
                    borderArray[i + 6 * j].Width = 95;
                    borderArray[i + 6 * j].Margin = new Thickness(-800 + 195 * i, -325 + 155 * j, 0, 0);
                    borderArray[i + 6 * j].MouseDown += BorderClicked;
                    imageArray[i + 6 * j].MouseDown += ImageClicked;
                }
            }
        }
        private void BorderClicked(Object sender, EventArgs e)
        {
            Border selectedBorder = (Border)sender;

            for (int i = 0; i < imageArray.Length; i++)
            { 
                if (borderArray[i] == selectedBorder)
                {
                    imageArray[i].Visibility = Visibility.Visible;
                    CheckIfWon(ref imageArray[i], ref imageArray[i]);
                }

            }
                    

        }
        private void ImageClicked(Object sender, EventArgs e)
        {
         Image selectedImage = (Image)sender;
          selectedImage.Visibility = Visibility.Visible;

        }
        private void UriImages()
        {

            Uri randImages;
            for (int i = 0; i < 36; i++)
            {
                if (i >= 18)
                {
                    randImages = new Uri(System.Environment.CurrentDirectory + "\\PicturesforGame\\Movie" + (i - 18) + ".jpg");
                    imageChosen[i] = new Uri(randImages.ToString());
                }
                else
                {
                    randImages = new Uri(System.Environment.CurrentDirectory + "\\PicturesforGame\\Movie" + i + ".jpg");
                    imageChosen[i] = new Uri(randImages.ToString());
                }
            }
        }
        private void ShufflingImages(ref System.Uri[] images)
        {
            if (levelNum == 0)
            {
                for (int i = 0; i < images.Length - 1; i++)
                {
                    int pickingImages = randGen.Next(i, images.Length);
                    Uri temp = images[pickingImages];
                    images[pickingImages] = images[i];
                    images[i] = temp;

                }
            }
        }


        private void btnHowToPlay_Click(object sender, RoutedEventArgs e)
        {
            InstructionsWindow instructionWindow = new InstructionsWindow();
            instructionWindow.ShowDialog();
        }

        private void btnSetTime_Click(object sender, RoutedEventArgs e)
        {
            btnSetTime.IsEnabled = false;
            countTime = UserTimeSet(countTime);
        }
        private int UserTimeSet(int time)
        {
            if (rdBtnThreeSec.IsChecked == true)
            {
                return 3;
            }
            else if (rdBtnFourSec.IsChecked == true)
            {
                return  4;
            }
            else
            {
                return  5;
            }
        }
        private void CheckIfWon(ref Image firstPair, ref Image secondPair)
        {
            for (int i = 0; i < imageArray.Length; i++)
            {
                if (firstPair.Source == secondPair.Source)
                {
                    firstPair.Visibility = Visibility.Visible;
                    secondPair.Visibility = Visibility.Visible;
                }
                else
                {
                    firstPair.Visibility = Visibility.Hidden;
                    secondPair.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
