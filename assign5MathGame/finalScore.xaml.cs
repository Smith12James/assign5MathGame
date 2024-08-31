using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace assign5MathGame
{
    /// <summary>
    /// Interaction logic for finalScore.xaml
    /// </summary>
    public partial class finalScore : Window
    {
        /// <summary>
        /// sets users name
        /// </summary>
        string sUsrName;

        /// <summary>
        /// sets users age
        /// </summary>
        int iUsrAge;

        /// <summary>
        /// sets users score
        /// </summary>w
        int iUsrScore;

        /// <summary>
        /// sets users time elapsed
        /// </summary>
        int iUsrElapsedTime;

        /// <summary>
        /// initialize sounds, to play sounds based on whether player won/lost
        /// </summary>
        playSounds sounds = new playSounds();

        /// <summary>
        /// initialize class CSVReadWrite
        /// </summary>
        CSVReadWrite csvReadWrite = new CSVReadWrite();

        public finalScore()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());

            }
            

        }

        /// <summary>
        /// This method will grab all info sent from gameplay and set it in txtblckScoreScreen.
        /// This is set to public, as Main window initializes this screen, and after all info is passed from gameplay window,
        /// label is set and then showDialog() is called
        /// </summary>
        public async void setScoreLabel()
        {
            StringBuilder sb = new StringBuilder(); // stringBuilder for current player score

            sb.Append($"FINAL SCORE\n\nName:\n{sUsrName}\n\nAge:\n{iUsrAge}\n\nScore:\n{iUsrScore}/10\n\nTime Elapsed:\n{iUsrElapsedTime} seconds");

            txtblckScoreScreen.Text = sb.ToString();

            StringBuilder sbHighScore = new StringBuilder("HIGH SCORES\n\nName\tAge\tScore\tTime\n");

            await this.Dispatcher.BeginInvoke(new Action(() =>
            {
                string[,] sHighScoreList = csvReadWrite.getHighScore(sUsrName, iUsrAge, iUsrScore, iUsrElapsedTime);

                for (int i = 0; i < sHighScoreList.GetLength(0); i++)
                {
                    sbHighScore.Append($"{sHighScoreList[i, 0]}\t{sHighScoreList[i, 1]}\t{sHighScoreList[i, 2]}\t{sHighScoreList[i, 3]}\n");

                }

            }));
            
            txtblckHighScore.Text = sbHighScore.ToString();

        }

        /// <summary>
        /// public method is called from Gameplaywindow, and sets the users name
        /// </summary>
        public void setName(string sName)
        {
            sUsrName = sName;

        }

        /// <summary>
        /// public method is called from Gameplaywindow, and sets the users age
        /// </summary>
        public void setAge(int iAge)
        {
            iUsrAge = iAge;

        }

        /// <summary>
        /// public method is called from Gameplaywindow, and sets the users score
        /// </summary>
        public void setScore(int iScore)
        {
            iUsrScore = iScore;

            if (iScore > 6)
            {
                imgDarthVader.Visibility = Visibility.Hidden;
                sounds.wonGame();
            }
            else
            {
                imgDarthVader.Visibility = Visibility.Visible;
                sounds.lostGame();

            }

        }

        /// <summary>
        /// public method is called from Gameplaywindow, and sets the users time elapsed
        /// </summary>
        public void setTime(int iTime)
        {
            iUsrElapsedTime = iTime;

        }

        /// <summary>
        /// when clicking Exit button, this window will close, and gameplay window will also close based on order of code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
