using FindTheBullGame_RefactoringProject.Models;
using System.Collections.Generic;

namespace FindTheBullGame_RefactoringProject.Inteface
{
    public interface IUserInterface
    {
        string ShowMessageToEnterUsername();

        int GetUserGameSelection();

        void ShowNewGameStartNotification(string name, string gameName);

        bool AskConfirmationAboutPracticeMatch();

        void ShowPracticeMatchMessage(string goal);

        string ReadUserGuess();

        void ShowUserGuessResult(string guess, bool isSuccess);

        void ShowSuccessfulGuessMessage(int guessCount);

        bool AskIfUserWantsToContinueMessage();

        void PrintSummary(List<PlayerData> playersData);
        void ShowSudokuNotAvailableMessage(string userName, string gameName);
    }
}
