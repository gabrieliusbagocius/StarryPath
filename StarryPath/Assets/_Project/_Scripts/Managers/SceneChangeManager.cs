using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private String _sceneName;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameSettingsManager _gameSettingsManager;
    [SerializeField] private GameDataManager _gameDataManager;
    private string _initialSceneName;
    private string _levelSceneName;
    private int _levelAmount;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _sceneName = SceneManager.GetActiveScene().name;
        _initialSceneName = _gameSettingsManager.InitialSceneName;
        _levelSceneName = _gameSettingsManager.LevelSceneName;
        _levelAmount = _gameDataManager.LevelAmount;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _sceneName = scene.name;
        _inputManager.GetComponent<InputManager>().SceneChanged();
    }

    public void StartLevel(int levelNumber)
    {
        _gameSettingsManager.SelectedLevel = levelNumber;
        ChangeScene(_gameSettingsManager.LevelSceneName);
    }

    public void ContinueToNextLevel()
    {
        if(_gameSettingsManager.SelectedLevel > 0)
        {
            _gameSettingsManager.SelectedLevel += 1;
            HandleNextLevelCheck(_gameSettingsManager.SelectedLevel);
        }
    }

    public void HandleNextLevelCheck(int selectedLevel)
    {
        if (selectedLevel <= _levelAmount)
        {
            ChangeScene(_gameSettingsManager.LevelSceneName);
        }
        else
        {
            ChangeScene(_initialSceneName);
        }
    }

    public void ChangeScene(string newSceneName)
    {
        SceneManager.LoadScene(newSceneName);
    }

    public void HandleExitLogic()
    {
        if (_sceneName == _levelSceneName)
        {
            ChangeScene(_initialSceneName);
        }
        else
        {
            Application.Quit();
        }
    }
}
