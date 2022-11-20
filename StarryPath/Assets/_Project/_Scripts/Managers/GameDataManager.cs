using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] ScriptableObjectsManager _scriptableObjectsManager;
    [SerializeField] private JSONReader _jSONReader;
    private JSONReader.ListOfLevels _listOfLevels;
    public int ResolutionLength { get; private set; }
    public int LevelAmount { get; private set; }
    private void Awake()
    {
        var dataSettings = _scriptableObjectsManager.DataSettings;
        ResolutionLength = dataSettings.coordinatesEndAt - dataSettings.coordinatesStartAt;
        _listOfLevels = _jSONReader.GetLoadedData(dataSettings.textAsset);
        LevelAmount = _listOfLevels.levels.Length;
    }

    public int[] GetLevelData(int level)
    {
        return _listOfLevels.levels[level - 1].level_data;
    }
}
