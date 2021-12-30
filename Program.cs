using FindTheBullGame_RefactoringProject.Implementation;
using FindTheBullGame_RefactoringProject.Implementation.BullGame;
using FindTheBullGame_RefactoringProject.Implementation.Sudoku;
using FindTheBullGame_RefactoringProject.Inteface;
using System;

namespace FindTheBullGame_RefactoringProject
{
    class MainClass
    {
        static IGameManager gameManagerService;
        static IUserInterface userInputService;
        public static void Main(string[] args)
        {
            try
            {
                userInputService = new UserInterface();

                // Show message to provide username
                var userName = userInputService.ShowMessageToEnterUsername();

                // ask user to select a game
                var gameSelection = userInputService.GetUserGameSelection();

                while (gameSelection != 0)
                {
                    // start user selected game (Create instance of interface based on criteria)
                    gameManagerService = gameSelection switch
                    {
                        1 => new FindBullService(userInputService),
                        2 => new SudokuService(userInputService),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    // Start game
                    gameManagerService.StartGame(userName);

                    // ask user to select a game
                    gameSelection = userInputService.GetUserGameSelection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found in the game!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}