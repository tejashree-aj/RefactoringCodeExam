using FindTheBullGame_RefactoringProject.Inteface;
using FindTheBullGame_RefactoringProject.Inteface.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheBullGame_RefactoringProject.Implementation.Sudoku
{
    public class SudokuService : ISudokuService
    {
        private IUserInterface userInputService;
        public SudokuService(IUserInterface _userInputService)
        {
            userInputService = _userInputService;
        }

        /// <summary>
        /// Start Sudoku game
        /// </summary>
        /// <param name="userName"></param>
        public void StartGame(string userName)
        {
            // Show new game start notification message
            userInputService.ShowNewGameStartNotification(userName, "Sudoku");

            // Show new game start notification message
            userInputService.ShowSudokuNotAvailableMessage(userName, "Sudoku");
        }
    }
}
