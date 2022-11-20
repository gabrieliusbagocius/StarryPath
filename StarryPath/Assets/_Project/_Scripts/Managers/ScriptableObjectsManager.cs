using UnityEngine;

public class ScriptableObjects : MonoBehaviour
{
    [SerializeField] private DataSettingsScriptableObject _dataSettings;
    [SerializeField] private GameSettingsScriptableObject _gameSettings;

    public DataSettingsScriptableObject DataSettings
    {
        get { return _dataSettings; }
    }

    public GameSettingsScriptableObject GameSettings
    {
        get { return _gameSettings; }
    }
}
