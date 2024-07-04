using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace assign5MathGame
{
    /// <summary>
    /// this class will play sounds from the folders, and will be used to hold all logic for playing the sounds.
    /// This will help keep code clean, and reduce lines/arrays of file names.
    /// </summary>
    class playSounds
    {

        /// <summary>
        /// plays the darth vader theme "Imperial March"
        /// </summary>
        public void imperialMarch(bool bPlaySound)
        {
            string soundFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "darth-vader-theme-song-101soundboards.wav");

            try
            {
                SoundPlayer spImperialMarch = new SoundPlayer(soundFilePath);

                if (bPlaySound) { spImperialMarch.Play(); }
                else { spImperialMarch.Stop(); }

            }
            catch (Exception e)
            {
                throw new ArgumentException(("issue with sound " + soundFilePath), e);

            }

        }

        /// <summary>
        /// plays one of a handful of sounds when the user gets a question correct
        /// </summary>
        public void correctSound()
        {
            string sFileName;

            Random rnd = new Random();

            int i = rnd.Next(1, 4);

            switch(i)
            {
                case 1:
                    sFileName = "acknowledged-2-101soundboards.wav";

                    break;

                case 2:
                    sFileName = "good-job-101soundboards.wav";

                    break;

                default:
                    sFileName = "you-must-be-very-proud-101soundboards.wav";

                    break;

            }

            string soundFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName);

            try
            {
                SoundPlayer spCorrect = new SoundPlayer(soundFilePath);
                spCorrect.Play();

            }
            catch (Exception e) 
            {
                throw new ArgumentException(("issue with sound " + soundFilePath), e);

            }

        }

        /// <summary>
        /// plays one of a handful of sounds when the user gets a question incorrect
        /// </summary>
        public void incorrectSound()
        {
            string sFileName;

            Random rnd = new Random();

            int i = rnd.Next(1, 4);

            switch (i)
            {
                case 1:
                    sFileName = "surrender-jedi-101soundboards.wav";

                    break;

                case 2:
                    sFileName = "problem-101soundboards.wav";

                    break;

                default:
                    sFileName = "i-have-a-bad-feeling-about-this-101soundboards.wav";

                    break;

            }

            string soundFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName);

            try
            {
                SoundPlayer spIncorrect = new SoundPlayer(soundFilePath);
                spIncorrect.Play();

            }
            catch (Exception e)
            {
                throw new ArgumentException(("issue with sound " + soundFilePath), e);

            }

        }

        /// <summary>
        /// begins when player starts the game.
        /// Sound is Obi Wan saying "Hello There"
        /// </summary>
        public void helloThere()
        {
            string soundFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hello-there-obi.wav");

            try
            {
                SoundPlayer spObiWanHello = new SoundPlayer(soundFilePath);
                spObiWanHello.Play();

            }
            catch (Exception e)
            {
                throw new ArgumentException(("issue with sound " + soundFilePath), e);

            }

        }

        /// <summary>
        /// plays from a handful of sounds when the user gets less than 7/10 answers correct
        /// </summary>
        public void lostGame()
        {
            string sFileName;

            Random rnd = new Random();

            int i = rnd.Next(1, 3);

            switch (i)
            {
                case 1:
                    sFileName = "traitor-101soundboards.wav";

                    break;

                default:
                    sFileName = "breathing-101soundboards.wav";

                    break;

            }

            string soundFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName);

            try
            {
                SoundPlayer spLost = new SoundPlayer(soundFilePath);
                spLost.Play();

            }
            catch(Exception e)
            {
                throw new ArgumentException(("issue with sound " + soundFilePath), e);

            }
            

        }

        /// <summary>
        /// plays from a handful of sounds when a user gets 7/10 or more answers correct
        /// </summary>
        public void wonGame()
        {
            string sFileName;

            Random rnd = new Random();

            int i = rnd.Next(1, 3);

            switch (i)
            {
                case 1:
                    sFileName = "the-negotiations-were-101soundboards.wav";

                    break;

                default:
                    sFileName = "you-were-right-about-one-thing-master-101soundboards.wav";

                    break;

            }

            string soundFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName);

            try
            {
                SoundPlayer spWon = new SoundPlayer(soundFilePath);
                spWon.Play();

            }
            catch (Exception e)
            {
                throw new ArgumentException(("issue with sound " + soundFilePath), e);

            }

        }

    }

}
