
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

        public GameWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// used to pass the game type from main menu to the game play window.
        /// The game play type is later used to 
        /// </summary>
        /// <param name="gameType"></param>
        public void setGameType(string gameType)
        {
            btnGameWindowStart.IsEnabled = true;
            btnGameWindowStart.Visibility = Visibility.Visible;

            lblShowMathLogic.Visibility = Visibility.Hidden;
            txtbxAnswerInput.Visibility = Visibility.Hidden;
            btnCheckAnswer.Visibility = Visibility.Hidden;

            this.sGameType = gameType;

        }

        

        private void btnGameWindowStart_Click(object sender, RoutedEventArgs e)
        {
            sounds.helloThere();

            btnGameWindowStart.IsEnabled = false;
            btnGameWindowStart.Visibility = Visibility.Hidden;

            lblShowMathLogic.Visibility = Visibility.Visible;
            txtbxAnswerInput.Visibility = Visibility.Visible;
            btnCheckAnswer.Visibility = Visibility.Visible;

            generateEquation();

        }

        /// <summary>
        /// will close current game window and return to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGameRtrnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// used to generate the next question based on user selection from the main menu.
        /// first step is to reset the text box to white, and opacity of 50 and then call external method from math logic class.
        /// </summary>
        private void generateEquation()
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

                    lblShowMathLogic.Content = $"{iMathNums[0]} * {iMathNums[1]} = ";

                    break;

                case "division":
                    iMathNums = equation.divis();

                    lblShowMathLogic.Content = $"{iMathNums[0]} / {iMathNums[1]} = ";

                    break;
            }

        }

        /// <summary>
        /// used to show light saber based on wether answer is correct or incorrect
        /// if correct show green, else show red
        /// </summary>
        private async void showSaberImg(bool bCorrect)
        {
            if (bCorrect)
            {

                await this.Dispatcher.BeginInvoke(() =>
                {
                    Debug.WriteLine("Green");

                    imgSaberGreen.Visibility = Visibility.Visible;
                    imgSaberGreen.UpdateLayout();

                });
                

            } else
            {

                await this.Dispatcher.BeginInvoke(() =>
                {
                    Debug.WriteLine("Red");

                    imgSaberRed.Visibility = Visibility.Visible;
                    imgSaberRed.UpdateLayout();

                });
                

            }

        }

        /// <summary>
        /// used to hide the image of the lightsaber, in an async process
        /// </summary>
        private async void hideSaberImg()
        {
            await this.Dispatcher.InvokeAsync(() =>
            {
                imgSaberGreen.Visibility = Visibility.Hidden;
                imgSaberGreen.UpdateLayout();

                imgSaberRed.Visibility = Visibility.Hidden;
                imgSaberRed.UpdateLayout();

            });

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
            int iUsrAnswerInput;
            iTurnCount++;

            Debug.WriteLine("Turn Count: " + iTurnCount);

            if (int.TryParse(txtbxAnswerInput.Text, out iUsrAnswerInput))
            {

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


                    this.Close();

                }
                else
                {

                    

                }
            } else
            {


            }

            await Task.Delay(700);

            generateEquation();

            hideSaberImg();
            lblShowMathLogic.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// method is used to set players name
        /// </summary>
        /// <param name="sName"></param>
        public void setName(string sName)
        {
            sUsrName = sName;

        }


        public void setAge(int iAge)
        {
            iUsrAge = iAge;

        }

    }
}
