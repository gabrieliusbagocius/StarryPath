using TMPro;
using UnityEngine;

public class MenuLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _menuLevelTMP;
    [SerializeField] private SceneChangeManager _sceneChangeManager;
    public int LevelNumber { get; private set; }

    private void Awake()
    {
        _sceneChangeManager = GameObject.Find("SceneChangeManager").GetComponent<SceneChangeManager>();
    }

    public void SetLevel(int levelNumber)
    {
        LevelNumber = levelNumber;
        _menuLevelTMP.text = $"Level: {levelNumber}";
    }

    public void StartLevel()
    {
        _sceneChangeManager.StartLevel(LevelNumber);
    }

}
