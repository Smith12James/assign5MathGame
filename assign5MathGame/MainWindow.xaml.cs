using System.Configuration;
using System.Diagnostics;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace assign5MathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Inisiantiate GameWindow as gameWindow
        /// </summary>
        GameWindow gameWindow;

        /// <summary>
        /// Stores users age for later use
        /// </summary>
        int iUsrAge;

        /// <summary>
        /// Stores users name for later use
        /// </summary>
        string sUsrName;

        /// <summary>
        /// if age is set correctly, this is set to true, and used in final check to open game window
        /// </summary>
        bool bUsrAge;

        /// <summary>
        /// if name is set correctly, this is set to true, and used in final check to open game window
        /// </summary>
        bool bUsrName;

        /// <summary>
        /// remains null until start game button is clicked, at which point the game is set based on which radio button
        /// is selected.
        /// </summary>
        string gameType;

        /// <summary>
        /// initialize playsounds so that theme plays on startup and stops when window closes or when begin game is clicked.
        /// </summary>
        playSounds playSounds = new playSounds();

        /// <summary>
        /// Initialize current window and other windows needed later: GameWindow, 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            
            playSounds.imperialMarch(true);

        }

        /// <summary>
        /// start game function that will check if name is entered and greater than one character, check if age is
        /// between 3 and 10, and if one radio button is selected will minimize this window and open the Game Play Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            playSounds.imperialMarch(false);

            if (txtbxUsrName.Text == "")
            {
                lblMainNameWarning.Content = "Please enter your name";

                bUsrName = false;

            } 
            
            else if (txtbxUsrName.Text.Length > 1)
            {
                lblMainNameWarning.Content = "";
                bUsrName = true;

                sUsrName = txtbxUsrName.Text;

            }

            if (!(Int32.TryParse(txtbxUsrAge.Text, out iUsrAge)) || iUsrAge < 3 || iUsrAge > 10)
            {
                lblMainAgeWarning.Content = "Please enter a number between 3 and 10";

                bUsrAge = false;

            } 
            
            else if (Int32.TryParse(txtbxUsrAge.Text, out iUsrAge) && iUsrAge > 2 && iUsrAge < 11)
            {
                lblMainAgeWarning.Content = "";
                bUsrAge = true;

            }

            if ((rdoAdd.IsChecked == true || rdoSub.IsChecked == true || rdoMultip.IsChecked == true || rdoDivis.IsChecked == true) && bUsrAge && bUsrName)
            {
                this.Hide();

                if (rdoAdd.IsChecked == true)
                {
                    gameType = "addition";

                } else if (rdoSub.IsChecked == true)
                {
                    gameType = "subtraction";

                } else if (rdoMultip.IsChecked == true)
                {
                    gameType = "multiplication";

                } else if (rdoDivis.IsChecked == true)
                {
                    gameType = "division";

                }

                gameWindow = new GameWindow();

                gameWindow.setGameType(gameType);
                gameWindow.setName(sUsrName);
                gameWindow.setAge(iUsrAge);

                gameWindow.ShowDialog();

                this.Show();
                
            } 

            else if (rdoAdd.IsChecked == false && rdoSub.IsChecked == false && rdoMultip.IsChecked == false && rdoDivis.IsChecked == false)
            {
                lblWarnRdoWarning.Content = "Please select a game option";

            }

        }
    }
}