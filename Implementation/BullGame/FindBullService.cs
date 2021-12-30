using FindTheBullGame_RefactoringProject.Inteface;
using FindTheBullGame_RefactoringProject.Inteface.BullGame;
using FindTheBullGame_RefactoringProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindTheBullGame_RefactoringProject.Implementation.BullGame
{
    public class FindBullService : IFindBullService, IDisposable
    {
        private IUserInterface userInputService;
        public FindBullService(IUserInterface _userInputService)
        {
            userInputService = _userInputService;
        }

        // For testing
        public FindBullService() { }

        /// <summary>
        /// Start bull game
        /// </summary>
        /// <param name="userName"></param>
        public void StartGame(string userName)
        {
            int difficultyLevel = 4;

            // flag to check if user wants to exit
            var quitPlay = false;

            while (!quitPlay)
            {
                // Show new game start notification message
                userInputService.ShowNewGameStartNotification(userName, "Find the bull");

                // Set a goal value as per difficulty level (Difficuly level - No of characters in the goal string. Set to 4)
                var goal = MakeGoal(difficultyLevel);

                // Ask if user wants to start practice match else start new compititive match
                var practiceMatch = userInputService.AskConfirmationAboutPracticeMatch();

                if (practiceMatch)
                {
                    // Start practice match and show Goal value to user
                    userInputService.ShowPracticeMatchMessage(goal);
                }

                // Start game and get guess count after successful finish
                var guessCount = StartGuessGame(goal, difficultyLevel);

                // Save guess results for the user in text file
                SaveGuessResult(userName, guessCount);

                // Show successful message to user with guess count
                userInputService.ShowSuccessfulGuessMessage(guessCount);

                // Get all players result sorted by average from text file
                var results = GetPlayersData();

                // print sorted results
                userInputService.PrintSummary(results);

                // Get player confirmation on starting new game or exit the game.
                if (!userInputService.AskIfUserWantsToContinueMessage())
                {
                    quitPlay = true;
                }
            }
        }
        
        /// <summary>
        /// Create goal value for the game as per diffculty level
        /// </summary>
        /// <param name="difficultyLevel">Number of characters to set difficult level</param>
        /// <returns>string - Goal value set by system</returns>
        public string MakeGoal(int difficultyLevel = 4)
        {
            try
            {
                var randomGenerator = new Random();
                var goal = string.Empty;
                for (int i = 0; i < difficultyLevel; i++)
                {
                    var random = randomGenerator.Next(10);
                    while (goal.Contains(random.ToString()))
                    {
                        random = randomGenerator.Next(10);
                    }
                    goal += random.ToString();
                }
                return goal;
            }
            catch (Exception ex)
            {
                // Log error
                throw;
            }
        }

        /// <summary>
        /// Get guess value - Check if guess is matching with the goal
        /// </summary>
        /// <param name="userGuess">guess value entered by user</param>
        /// <param name="goal">goal value set by system</param>
        /// <param name="difficultyLevel">level of difficulty</param>
        /// <returns>string - Guess result</returns>
        public string GetGuessResult(string userGuess, string goal, int difficultyLevel)
        {
            try
            {
                if (string.IsNullOrEmpty(userGuess))
                    throw new ArgumentNullException("Guess input is not provided");

                if (string.IsNullOrEmpty(goal))
                    throw new ArgumentNullException("Goal input is not provided");


                int bullCount = 0, cowCount = 0;

                if (userGuess.Length < difficultyLevel)
                    userGuess += Enumerable.Repeat(string.Empty, difficultyLevel - userGuess.Length);

                for (var index = 0; index < difficultyLevel; index++)
                {
                    var letter = userGuess[index];
                    if (goal.IndexOf(letter) == index)
                    {
                        bullCount++;
                    }
                    else if (goal.Any(x => x == letter))
                    {
                        cowCount++;
                    }
                }
                return $"{String.Concat(Enumerable.Repeat("B", bullCount))},{String.Concat(Enumerable.Repeat("C", cowCount))}";
            }
            catch (Exception ex)
            {
                // Log error
                throw;
            }
        }

        /// <summary>
        /// Save guess result for the user in text file
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="guessCount">Attempts made before successful guess.</param>
        public void SaveGuessResult(string userName, int guessCount, string fileName = "result.txt")
        {
            using (var output = new StreamWriter(fileName, append: true))
            {
                output.WriteLine($"{userName}#&#{guessCount}");
                output.Close();
            }
        }

        /// <summary>
        /// Get all players data saved in text file
        /// </summary>
        /// <returns>List of PlayerData - Unique username and sorted on average number of guesses.</returns>
        public List<PlayerData> GetPlayersData(string fileName = "result.txt")
        {
            var playersData = new List<PlayerData>();
            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0)
                {
                    var nameAndScore = sr.ReadLine().Split(new string[] { "#&#" }, StringSplitOptions.None);

                    var playerRecord = playersData.Find(x => x.Name.Equals(nameAndScore[0]));

                    if (playerRecord == null)
                    {
                        playersData.Add(new PlayerData(nameAndScore[0], Convert.ToInt32(nameAndScore[1])));
                    }
                    else
                    {
                        playerRecord.Update(Convert.ToInt32(nameAndScore[1]));
                    }
                }
            }

            playersData.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));

            return playersData;
        }

        /// <summary>
        /// Start guess game. Exit when goal value matched with user guess.
        /// </summary>
        /// <param name="goal">Goal value to match</param>
        /// <returns>int - Guess counts to match correct goal / bull value</returns>
        private int StartGuessGame(string goal, int difficultyLevel)
        {
            var isCorrectGuess = false;
            var guessCount = 1;

            while (!isCorrectGuess)
            {
                // Read user input
                var guess = userInputService.ReadUserGuess();

                // check guess status
                var guessResult = GetGuessResult(guess, goal, difficultyLevel);

                // If guess does not match goal
                if (!string.Equals(guessResult, String.Concat(Enumerable.Repeat("B", difficultyLevel)) + ",", StringComparison.OrdinalIgnoreCase))
                {
                    guessCount++;
                    userInputService.ShowUserGuessResult(guessResult, false);
                }
                else // Guess matches the goal
                {
                    userInputService.ShowUserGuessResult(guessResult, true);
                    isCorrectGuess = true;
                }
            }
            return guessCount;
        }

        public void Dispose()
        {
            
        }

    }
}

