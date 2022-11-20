using UnityEngine;

public class JSONReader : MonoBehaviour
{
    [SerializeField] public ListOfLevels listOfLevels;

    [System.Serializable]
    public class Levels
    {
        public int[] level_data;
    }

    [System.Serializable]
    public class ListOfLevels
    {
        public Levels[] levels;
    }

    public ListOfLevels GetLoadedData(TextAsset textAsset)
    {
        listOfLevels = JsonUtility.FromJson<ListOfLevels>(textAsset.text);
        DisplayGameDataStatus();
        return listOfLevels;
    }

    private void DisplayGameDataStatus()
    {
        var message = LevelDataHelper.GetGameDataStatus(listOfLevels);
        var messageLength = message.Count;

        if (messageLength != 0)
        {
            for (int selectedMessageNumber = 0; selectedMessageNumber < messageLength; selectedMessageNumber++)
            {
                Debug.LogError(message[selectedMessageNumber]);
            }
        }
    }
}
