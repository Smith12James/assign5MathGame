using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assign5MathGame
{
    /// <summary>
    /// class is used to read csv and write to the csv.
    /// this csv should only have 10 entries, so as someone is able to score higher, or if the score is the same and finish faster
    /// they will take that respective spot, and move all below down one ranking. All logic pertaining to reading, checking ranking
    /// and writing to csv will be handled by this file.
    /// </summary>
    internal class CSVReadWrite
    {
        /// <summary>
        /// store file path, using current directory
        /// </summary>
        string sFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "highScores.csv");

        /// <summary>
        /// used to hold current csv rankings, and is updated with new info if a ranking update is needed
        /// </summary>
        string[,] sCsvData;

        /// <summary>
        /// used to check if user has scored high enough to get a place in the game
        /// </summary>
        bool bUsrPlaced;

        /// <summary>
        /// stringbuilder is used to ensure that formatting of the csv has the correct commas and line breaks when writing to the csv
        /// </summary>
        StringBuilder sbCsvFormat = new StringBuilder();

        /// <summary>
        /// public method used in finalScore to get highscore 
        /// </summary>
        /// <returns>
        /// string array with all top 10 player scores.
        /// order is "Name, Age, Score, Time"
        /// </returns>
        public string[,] getHighScore(string sUsrName, int iUsrAge, int iUsrScore, int iElapsedTime)
        {
            readCSV();

            checkRanking(sUsrName, iUsrAge, iUsrScore, iElapsedTime);

            return sCsvData;
        }

        /// <summary>
        /// read from csv and write to sCsvArr
        /// </summary>
        private async void readCSV()
        {
            try
            {
                string[] lines = File.ReadAllLines(sFilePath);

                int iRows = lines.Length;
                int iCols = 4;

                sCsvData = new string[iRows, iCols];
                await Task.Run(() =>
                {
                    for (int i = 0; i < iRows; i++)
                    {
                        string[] sData = lines[i].Split(',');

                        for (int x = 0; x < iCols; x++)
                        {
                            sCsvData[i, x] = sData[x];

                        }

                    }

                });
                

            }
            catch (FileNotFoundException e)
            {
                throw new Exception("File not found: " + e.Message);
            
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            
            }
            

        }

        /// <summary>
        /// used to check if current user score can place in the highscore list.
        /// If user can place, they will take their ranking, and move all others down the list. One players score is likely to be removed
        /// </summary>
        private async void checkRanking(string sName, int iAge, int iScore, int iTime)
        {
            try
            {
                int iRows = sCsvData.GetLength(0);
                int iCols = sCsvData.GetLength(1);

                // temporary array to store current data without overwriting previous array. Once complete this will overwrite constructor array
                string[,] sTempArr = new string[iRows, iCols];

                await Task.Run(() =>
                {
                    for (int i = 0; i < iRows; i++)
                    {
                        // this iTempScore is the current score from the csv file, to compare with the current user score.
                        int iTempScore = int.Parse(sCsvData[i, 2]);

                        if (iScore > iTempScore) // if current players score is greater than current csv score, then use current array place
                        {
                            sTempArr[i, 0] = sName;
                            sTempArr[i, 1] = iAge.ToString();
                            sTempArr[i, 2] = iScore.ToString();
                            sTempArr[i, 3] = iTime.ToString();

                            sbCsvFormat.Append($"{sTempArr[i, 0]},{sTempArr[i, 1]},{sTempArr[i, 2]},{sTempArr[i, 3]}\n");

                            sName = sCsvData[i, 0];
                            iAge = int.Parse(sCsvData[i, 1]);
                            iScore = int.Parse(sCsvData[i, 2]);
                            iTime = int.Parse(sCsvData[i, 3]);

                        }
                        else if (iScore == iTempScore) // if score is equal, compare times
                        {
                            // this iTempTime is the current time from the csv file, to compare with the current users time
                            int iTempTime = int.Parse(sCsvData[i, 3]);

                            if (iTime < iTempTime) // if current users time is less than current csv time, write this player first
                            {
                                sTempArr[i, 0] = sName;
                                sTempArr[i, 1] = iAge.ToString();
                                sTempArr[i, 2] = iScore.ToString();
                                sTempArr[i, 3] = iTime.ToString();

                                sbCsvFormat.Append($"{sTempArr[i, 0]},{sTempArr[i, 1]},{sTempArr[i, 2]},{sTempArr[i, 3]}\n");

                                // change the current comparison to the place that is being taken over by the current user.
                                // the goal is the update the current users place and push all others down one place, and hopefully remove the last name
                                sName = sCsvData[i, 0];
                                iAge = int.Parse(sCsvData[i, 1]);
                                iScore = int.Parse(sCsvData[i, 2]);
                                iTime = int.Parse(sCsvData[i, 3]);

                            }

                        }
                        else
                        {
                            sTempArr[i, 0] = sCsvData[i, 0];
                            sTempArr[i, 1] = sCsvData[i, 1];
                            sTempArr[i, 2] = sCsvData[i, 2];
                            sTempArr[i, 3] = sCsvData[i, 3];

                            sbCsvFormat.Append($"{sTempArr[i, 0]},{sTempArr[i, 1]},{sTempArr[i, 2]},{sTempArr[i, 3]}\n");

                        }

                    }

                    sCsvData = sTempArr;

                    writeCSV();

                });                

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}\nSource:{ex.Source}\n{ex.StackTrace}");

            }

        }

        /// <summary>
        /// used to take current score and overwrite current csv.
        /// this will take place after checkRankings is called, and only when an update is made.
        /// </summary>
        private async void writeCSV()
        {
            try
            {
                await Task.Run(() =>
                {
                    StreamWriter swWriter = new StreamWriter(sFilePath, false);
                    swWriter.Write(sbCsvFormat.ToString());
                    swWriter.Close();

                });
                

            }
            catch (InsufficientMemoryException ex)
            {
                throw new InsufficientMemoryException(ex.Message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

    }
}