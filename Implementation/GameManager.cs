using FindTheBullGame_RefactoringProject.Implementation.BullGame;
using FindTheBullGame_RefactoringProject.Inteface;
using FindTheBullGame_RefactoringProject.Inteface.BullGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBullGame_RefactoringProject.Implementation
{
    public class GameManager: IGameManager
    {
        ILogicService logicService;
        IUserInterface userInputService;

        public GameManager(IUserInterface _userInputService)
        {
            this.userInputService = _userInputService;
        }

        public bool StartGame(string userName, int gameSelection)
        {
            //_ = gameSelection switch
            //{
            //    1 => StartBullGame(userName),
            //    2 => StartSudokuGame(userName),
            //    _ => throw new ArgumentOutOfRangeException()
            //};

            //return true;

            _ = gameSelection switch
            {
                1 => new LogicService(userName, userInputService),
                2 => StartSudokuGame(userName),
                _ => throw new ArgumentOutOfRangeException()
            };

            return true;
        }

        private bool StartSudokuGame(string userName)
        {
            // Show new game start notification message
            userInputService.ShowNewGameStartNotification(userName, "Sudoku");

            // Show new game start notification message
            userInputService.ShowSudokuNotAvailableMessage(userName, "Sudoku");

            return true;
        }

        private bool StartBullGame(string userName)
        {
            logicService = new LogicService();

            int difficultyLevel = 4;

            // flag to check if user wants to exit
            var quitPlay = false;

            while (!quitPlay)
            {
                // Show new game start notification message
                userInputService.ShowNewGameStartNotification(userName, "Find the bull");

                // Set a goal value as per difficulty level (Difficuly level - No of characters in the goal string. Set to 4)
                var goal = logicService.MakeGoal(difficultyLevel);

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
                logicService.SaveGuessResult(userName, guessCount);

                // Show successful message to user with guess count
                userInputService.ShowSuccessfulGuessMessage(guessCount);

                // Get all players result sorted by average from text file
                var results = logicService.GetPlayersData();

                // print sorted results
                userInputService.PrintSummary(results);

                // Get player confirmation on starting new game or exit the game.
                if (!userInputService.AskIfUserWantsToContinueMessage())
                {
                    quitPlay = true;
                }
            }

            return quitPlay;
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
                var guessResult = logicService.GetGuessResult(guess, goal, difficultyLevel);

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

    }
}
