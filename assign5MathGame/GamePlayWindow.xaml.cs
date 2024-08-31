
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;

namespace assign5MathGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        /// <summary>
        /// string used to store user name, and is set with the setName() method
        /// </summary>
        string sUsrName;

        /// <summary>
        /// int used to store user's age, and is set with the setAge() method
        /// </summary>
        int iUsrAge;

        /// <summary>
        /// used to hold which type of math equations will be presented.
        /// </summary>
        string sGameType;

        /// <summary>
        /// used to keep track of which turn is current, starting with 1 and ending after the 10th turn.
        /// </summary>
        int iTurnCount = 0;

        /// <summary>
        /// hold all numbers for the math logic, should be in order from initial number, second number, and answer number.
        /// Example ([array] --> gui presentation): ([1,2,3] --> 1+2=3) or ([2,5,10] --> 2*5=10)
        /// </summary>
        int[] iMathNums;

        /// <summary>
        /// stores how many answers user got correct
        /// </summary>
        int iCorrectAnswers;

        /// <summary>
        /// initialize mathLogic class to populate numbers and answers for the GUI
        /// </summary>
        mathLogic equation = new mathLogic();

        /// <summary>
        /// initialize playSounds, which is used to play a sound depending on what the user interaction is
        /// </summary>
        playSounds sounds = new playSounds();

        /// <summary>
        /// initialize dispatcher timer. 
        /// I believe this will use a separate thread that will also allow the UI to update with the timer.
        /// </summary>
        private DispatcherTimer dtTimer;

        /// <summary>
        /// initialize timer to be used in the setTimer() method
        /// </summary>
        //System.Timers.Timer tTimer;

        /// <summary>
        /// used to track time elapsed. This number is used as a value for the UI and 
        /// </summary>
        int iTimeKeeper = 0;

        public GameWindow()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                MessageBoxButton msgbxBtns = MessageBoxButton.OK;

                MessageBox.Show("An error occured: " +  e.Message, "Error Occured", msgbxBtns);

                this.Close();
            
            }
            
        }

        /// <summary>
        /// used to pass the game type from main menu to the game play window.
        /// The game play type is later used to 
        /// </summary>
        /// <param name="gameType"></param>
        public void setGameType(string gameType)
        {
            try
            {
                btnGameWindowStart.IsEnabled = true;
                btnGameWindowStart.Visibility = Visibility.Visible;
                txtblckIntroMsg.Visibility = Visibility.Visible;

                lblShowMathLogic.Visibility = Visibility.Hidden;
                txtbxAnswerInput.Visibility = Visibility.Hidden;
                btnCheckAnswer.Visibility = Visibility.Hidden;

                this.sGameType = gameType;

            }
            catch (Exception ex)
            {
                throw new Exception("Issue establishing Game Type: " + ex.Message);

            }

        }

        

        private void btnGameWindowStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sounds.helloThere();

                btnGameWindowStart.IsEnabled = false;
                btnGameWindowStart.Visibility = Visibility.Hidden;
                txtblckIntroMsg.Visibility = Visibility.Hidden;

                lblShowMathLogic.Visibility = Visibility.Visible;
                txtbxAnswerInput.Visibility = Visibility.Visible;
                btnCheckAnswer.Visibility = Visibility.Visible;

                setTimer();

                generateEquation();

            }
            catch (Exception ex)
            {
                throw new Exception("Issue starting game: " + ex.Message);

            }

        }

        /// <summary>
        /// will close current game window and return to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGameRtrnMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtTimer.Stop();

                this.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Issue closing game with 'Exit' Button: " + ex.Message);

            }

        }

        /// <summary>
        /// used to generate the next question based on user selection from the main menu.
        /// first step is to reset the text box to white, and opacity of 50 and then call external method from math logic class.
        /// </summary>
        private void generateEquation()
        {
            try
            {
                txtbxAnswerInput.Clear();

                switch (sGameType)
                {
                    case "addition":
                        iMathNums = equation.add();

                        lblShowMathLogic.Content = $"{iMathNums[0]} + {iMathNums[1]} = ";

                        break;

                    case "subtraction":
                        iMathNums = equation.sub();

                        lblShowMathLogic.Content = $"{iMathNums[0]} - {iMathNums[1]} = ";

                        break;

                    case "multiplication":
                        iMathNums = equation.multi();

                        lblShowMathLogic.Content = $"{iMathNums[0]} × {iMathNums[1]} = ";

                        break;

                    case "division":
                        iMathNums = equation.divis();

                        lblShowMathLogic.Content = $"{iMathNums[0]} ÷ {iMathNums[1]} = ";

                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Issue generating equation: " + ex.Message);

            }

        }


        private async void showInputError()
        {
            try
            {
                await this.Dispatcher.BeginInvoke(() =>
                {
                    lblInputError.Visibility = Visibility.Visible;

                });
                await Task.Delay(1000);

                txtbxAnswerInput.Clear();
                txtbxAnswerInput.Focus();
                lblInputError.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                throw new Exception("Error with hiding input" + ex.Message);

            }

        }

        /// <summary>
        /// used to show light saber based on wether answer is correct or incorrect
        /// if correct show green, else show red
        /// </summary>
        private async void showSaberImg(bool bCorrect)
        {
            try
            {
                if (bCorrect)
                {

                    await this.Dispatcher.BeginInvoke(() =>
                    {
                        Debug.WriteLine("Green");

                        imgSaberGreen.Visibility = Visibility.Visible;
                        imgSaberGreen.UpdateLayout();

                    });


                }
                else
                {

                    await this.Dispatcher.BeginInvoke(() =>
                    {
                        Debug.WriteLine("Red");

                        imgSaberRed.Visibility = Visibility.Visible;
                        imgSaberRed.UpdateLayout();

                    });


                }

            }
            catch (Exception ex)
            {
                throw new Exception("Issue showing lightsaber image: " + ex.Message);

            }

        }

        /// <summary>
        /// used to hide the image of the lightsaber, in an async process
        /// </summary>
        private async void hideSaberImg()
        {
            try
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    imgSaberGreen.Visibility = Visibility.Hidden;
                    imgSaberGreen.UpdateLayout();

                    imgSaberRed.Visibility = Visibility.Hidden;
                    imgSaberRed.UpdateLayout();

                });

            }
            catch (Exception ex)
            {
                throw new Exception("Issue hiding saber Image: " + ex.Message);

            }

        }

        /// <summary>
        /// user clicks button to check answer, checks array generated from mathlogic class and compares user input to the 
        /// answer. If correct, a green lightsaber is shown, if incorrect a red lightsaber is shown. Equation is also hidden and 
        /// then shown to the user again after half a second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCheckAnswer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iUsrAnswerInput;

                if (int.TryParse(txtbxAnswerInput.Text, out iUsrAnswerInput) && iUsrAnswerInput >= 0)
                {
                    iTurnCount++;

                    Debug.WriteLine("Turn Count: " + iTurnCount);

                    Debug.WriteLine("User Answer: " + iUsrAnswerInput.ToString() + "\nCorrect Answer: " + iMathNums[2].ToString());

                    if (iUsrAnswerInput == (iMathNums[2]))
                    {
                        iCorrectAnswers += 1;

                        await this.Dispatcher.BeginInvoke(() =>
                        {
                            lblShowMathLogic.Visibility = Visibility.Hidden;
                            showSaberImg(true);

                        });

                        sounds.correctSound();

                    }
                    else
                    {

                        await this.Dispatcher.BeginInvoke(() =>
                        {
                            lblShowMathLogic.Visibility = Visibility.Hidden;
                            showSaberImg(false);

                        });

                        sounds.incorrectSound();

                    }

                    if (iTurnCount >= 10)
                    {
                        dtTimer.Stop();

                        finalScore finalScore = new finalScore();

                        finalScore.setName(sUsrName);
                        finalScore.setAge(iUsrAge);
                        finalScore.setScore(iCorrectAnswers);
                        finalScore.setTime(iTimeKeeper);
                        finalScore.setScoreLabel();

                        finalScore.ShowDialog();

                        this.Close();

                    }
                    
                }
                else
                {
                    showInputError();

                    throw new Exception("Incorrect value entered. Numbers only 0 or greater");

                }

                await Task.Delay(700);

                generateEquation();

                hideSaberImg();
                lblShowMathLogic.Visibility = Visibility.Visible;
                txtbxAnswerInput.Focus();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Source: {ex.Source}\nError: {ex.Message}");
                lblInputError.Visibility = Visibility.Hidden;

            }
            

        }

        /// <summary>
        /// method is used to set players name, passed from the main window
        /// </summary>
        /// <param name="sName"></param>
        public void setName(string sName)
        {
            try
            {
                sUsrName = sName;

            }
            catch (Exception ex)
            {
                throw new Exception("Issue setting users name: " + ex.Message);

            }

        }

        /// <summary>
        /// used to set players age, passed from the main window
        /// </summary>
        /// <param name="iAge"></param>
        public void setAge(int iAge)
        {
            try
            {
                iUsrAge = iAge;

            }
            catch (Exception ex)
            {
                throw new Exception("Issue setting users age: " + ex.Message);

            }
            
        }

        /// <summary>
        /// used to set the timer, had some help from chatGPT to figure best timer to update UI
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void setTimer()
        {
            try
            {
                dtTimer = new DispatcherTimer();

                dtTimer.Interval = TimeSpan.FromSeconds(1);
                dtTimer.Tick += showTimer;

                dtTimer.Start();

            }
            catch (Exception ex)
            {
                throw new Exception("Issue with creating timer: " + ex.Message);

            }
            

        }

        /// <summary>
        /// used to show timer in the UI, timer is located top right.
        /// Method is called every second. had some help from chatGPT to figure best timer to update UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void showTimer(Object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("Timer: " + e.ToString());

                iTimeKeeper++;
                lblShowTimer.Content = iTimeKeeper.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("Issue with showing timer: " + ex.Message);

            }
            

        }

    }
}
