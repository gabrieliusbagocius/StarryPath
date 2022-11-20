using System;
using System.Collections.Generic;

public static class LevelDataHelper
{
    private static List<string> _message = new List<string>();

    public static List<string> GetGameDataStatus(JSONReader.ListOfLevels listOfLevels)
    {
        int amountOfLevels = listOfLevels.levels.Length;

        for(int selectedLevelNumber = 0; selectedLevelNumber < amountOfLevels; selectedLevelNumber++)
        {
            CheckForLengthMismatch(listOfLevels, selectedLevelNumber);
        }

        return _message;
    }

    private static void CheckForLengthMismatch(JSONReader.ListOfLevels listOfLevels, int levelNumber)
    {
        var levelData = listOfLevels.levels[levelNumber].level_data;
        if (levelData.Length % 2 > 0)
        {
            listOfLevels.levels[levelNumber] = null;
            _message.Add($"The amount of points X are not equal to points Y in Level: {levelNumber}");
        }
    }
}
