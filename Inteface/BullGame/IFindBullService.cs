using FindTheBullGame_RefactoringProject.Models;
using System.Collections.Generic;

namespace FindTheBullGame_RefactoringProject.Inteface.BullGame
{
    public interface IFindBullService: IGameManager
    {
        string MakeGoal(int difficultyLevel = 4);

        string GetGuessResult(string userGuess, string goal, int difficultyLevel);

        List<PlayerData> GetPlayersData(string fileName = "result.txt");

        void SaveGuessResult(string userName, int guessCount, string fileName = "result.txt");
    }
}
