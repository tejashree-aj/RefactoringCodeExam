using FindTheBullGame_RefactoringProject.Inteface;
using FindTheBullGame_RefactoringProject.Models;
using System;
using System.Collections.Generic;

namespace FindTheBullGame_RefactoringProject.Implementation
{
    public class UserInterface : IUserInterface
    {
        /// <summary>
        /// Ask user to enter username and validate the username. If not validated then ask to reenter.
        /// </summary>
        /// <returns>string - Username</returns>
        public string ShowMessageToEnterUsername()
        {
            Console.WriteLine("Enter your user name:");
            var name = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please enter valid user name:");
                name = Console.ReadLine().Trim();
            }
            Console.WriteLine();
            return name;
        }

        /// <summary>
        /// Select the game from selection
        /// </summary>
        /// <returns></returns>
        public int GetUserGameSelection()
        {
            Console.WriteLine();
            Console.WriteLine("Please select game you want to play - ");
            Console.WriteLine("1. Find The Bull");
            Console.WriteLine("2. Sudoku");
            Console.WriteLine("0. Exit");
            var selection = int.TryParse(Console.ReadLine(), out int result);
            while (!selection || result <= 0 || result > 2)
            {
                if (result == 0)
                    return result;

                Console.WriteLine("Please select game option from given list:");
                selection = int.TryParse(Console.ReadLine(), out result);
            }
            Console.WriteLine();
            return result;
        }

        /// <summary>
        /// Show welcome message with user name
        /// </summary>
        /// <param name="name">Username</param>
        public void ShowNewGameStartNotification(string name, string gameName)
        {
            Console.WriteLine($"{name}, lets start the {gameName} game:");
            Console.WriteLine();
        }

        /// <summary>
        /// Get user input if he/she wants to start practice match
        /// </summary>
        /// <returns>bool - true for practice match else false</returns>
        public bool AskConfirmationAboutPracticeMatch()
        {
            Console.WriteLine("Would you like to play a practice match? Please enter Y or press enter to continue.");
            var input = Console.ReadKey();

            while (ConsoleKey.Y != input.Key && ConsoleKey.Enter != input.Key)
            {
                Console.WriteLine("Would you like to play a practice match? Please enter Y or press enter to continue.");
                input = Console.ReadKey();
            }

            if (ConsoleKey.Y == input.Key)
                return true;
            else
            {
                Console.WriteLine($"Please enter your guess!");
                return false;
            }
        }

        /// <summary>
        /// Show practice match message
        /// </summary>
        /// <param name="goal">string - goal value to print</param>
        public void ShowPracticeMatchMessage(string goal)
        {
            Console.WriteLine($"For practice, number is: {goal}");
            Console.WriteLine($"Please enter your guess!");
        }

        /// <summary>
        /// Get guess value entered by user after validation
        /// </summary>
        /// <returns>string - guess value entered by user</returns>
        public string ReadUserGuess()
        {
            var userGuess = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(userGuess))
            {
                Console.WriteLine("Please enter valid input!");
                userGuess = Console.ReadLine().Trim();
            }
            Console.WriteLine();
            return userGuess;
        }

        /// <summary>
        /// Show result based on user entered guess, show try again message if wrong guess.
        /// </summary>
        /// <param name="guess">guess result</param>
        /// <param name="isSuccess">successful flag to show correct message</param>
        public void ShowUserGuessResult(string guess, bool isSuccess)
        {
            Console.WriteLine($"Guess result: {guess}");
            Console.WriteLine((!isSuccess ? "Invalid guess! Please enter your guess again!" : string.Empty));
            Console.WriteLine();
        }

        /// <summary>
        /// Show successful guess message with number of attempts (guesscount) user made.
        /// </summary>
        /// <param name="guessCount">Count of retry attempts.</param>
        public void ShowSuccessfulGuessMessage(int guessCount)
        {
            Console.WriteLine($"Its Correct! It took { guessCount } guesses.");
        }

        /// <summary>
        /// Ask user if he wants to start new game or exit the game
        /// </summary>
        /// <returns>bool - true for new game or false to exit the game.</returns>
        public bool AskIfUserWantsToContinueMessage()
        {
            Console.WriteLine("Please press Y to start new game or press N to exit the game.");
            var input = Console.ReadKey();

            while (ConsoleKey.Y != input.Key && ConsoleKey.N != input.Key)
            {
                Console.WriteLine("Please press Y to start new game or press N to exit the game.");
                input = Console.ReadKey();
            }

            if (ConsoleKey.Y == input.Key)
                return true;
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Print all players track records including name, games played and average successful attempts
        /// </summary>
        /// <param name="playersData">List of PlayerData</param>
        public void PrintSummary(List<PlayerData> playersData)
        {
            Console.WriteLine("Player Records - ");
            foreach (PlayerData p in playersData)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
            }
        }

        /// <summary>
        /// Show game not available
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="gameName"></param>
        public void ShowSudokuNotAvailableMessage(string userName, string gameName)
        {
            Console.WriteLine($"{userName}, unfortunately {gameName} is not available in your region! Please select some other game from the options.");
            Console.WriteLine();
        }
    }
}
