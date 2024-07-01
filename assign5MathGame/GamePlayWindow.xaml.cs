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
using System.Windows.Shapes;

namespace assign5MathGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        /// <summary>
        /// used to hold which type of math equations will be presented.
        /// </summary>
        string sGameType;

        /// <summary>
        /// used to keep track of which turn is current, starting with 1 and ending after the 10th turn.
        /// </summary>
        int iTurnCount = 1;

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

        public GameWindow()
        {
            InitializeComponent();

        }

        public void setGameType(string gameType)
        {
            this.sGameType = gameType;

            btnGameWindowStart.Visibility = Visibility.Visible;
            lblShowMathLogic.Visibility = Visibility.Visible;
            txtbxAnswerInput.Visibility = Visibility.Visible;

        }

         void startGame()
        {
            btnGameWindowStart.IsEnabled = false;
            btnGameWindowStart.Visibility = Visibility.Collapsed;

            int iUsrAnswerInput;
            if (int.TryParse(txtbxAnswerInput.Text, out iUsrAnswerInput))
            {


            }

            if (iTurnCount < 10)
            {

                switch (sGameType)
                {
                    case "addition":
                        iMathNums = equation.add();

                        lblShowMathLogic.Content = $"{iMathNums[0]} + {iMathNums} = ";

                        break;

                    case "subtraction":


                        break;

                    case "multiplication":


                        break;

                    case "division":


                        break;
                }

                if (iUsrAnswerInput == iMathNums[2])
                {
                    iCorrectAnswers += 1;


                }

            } else
            {


            }

        }

        private void btnGameWindowStart_Click(object sender, RoutedEventArgs e)
        {
            startGame();

        }

        private void btnGameRtrnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
