using UnityEngine;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelMenuPrefab;
    [SerializeField] private Transform _levelPanelLocation;
    [SerializeField] private Panel _panel;
    private GameDataManager _gameDataManager;

    private void Awake()
    {
        _gameDataManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
    }

    private void Start()
    {
        SpawnMenuLevelObjects();
        _panel.UpdatePanel();
    }

    private void SpawnMenuLevelObjects()
    {
        var levelAmount = _gameDataManager.LevelAmount;
        for(int levelNumber = 1; levelNumber < levelAmount + 1; levelNumber++)
        {
            var spawnedMenuLevel = Instantiate(_levelMenuPrefab, Vector3.zero, Quaternion.identity);
            spawnedMenuLevel.transform.SetParent(_levelPanelLocation);
            spawnedMenuLevel.name = $"Level {levelNumber}";
            spawnedMenuLevel.GetComponent<MenuLevel>().SetLevel(levelNumber);
        }
    }
}
