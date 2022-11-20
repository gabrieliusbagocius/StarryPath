using UnityEditor;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private ScriptableObjectsManager _scriptableObjectsManager;
    private GameSettingsScriptableObject _gameSettings;
    public float GameScreenRatio { get; private set; }
    public float RopeSpeedModifier { get; private set; }
    public bool VariableRopeSpeedTime { get; private set; }
    public string InitialSceneName { get; private set; }
    public string LevelSceneName { get; private set; }
    public GameObject Point { get; private set; }
    public GameObject Rope { get; private set; }
    public int ScoreAmplifier { get; private set; }
    public int SelectedLevel { get; set; }

    private void Awake()
    {
        _gameSettings = _scriptableObjectsManager.GameSettings;
        SelectedLevel = 0;
        ReadGameSettingsValues();
        ChangePointPrefabDefaultValues();
        ChangeRopePrefabDefaultValues();
    }

    private void ReadGameSettingsValues()
    {
        GameScreenRatio = _gameSettings.gameSceenRatio;
        RopeSpeedModifier = _gameSettings.ropeSpeedModifier;
        VariableRopeSpeedTime = _gameSettings.variableRopeSpeedTime;
        InitialSceneName = _gameSettings.initialSceneName;
        LevelSceneName = _gameSettings.levelSceneName;
        Point = _gameSettings.point;
        Rope = _gameSettings.rope;
        ScoreAmplifier = _gameSettings.pointScoreAmplifier;
    }

    private void ChangePointPrefabDefaultValues()
    {
#if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(Point);
        GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);
        var gOManager = contentsRoot.GetComponent<Point>();
        gOManager.SetSprites(_gameSettings.defaultPointVisual, _gameSettings.selectedPointVisual);
        gOManager.TextAnimationTime = _gameSettings.pointTextAnimationTime;
        PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
        PrefabUtility.UnloadPrefabContents(contentsRoot);
#endif
    }

    private void ChangeRopePrefabDefaultValues()
    {
#if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(Rope);
        GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);
        var gOManager = contentsRoot.GetComponent<Rope>();
        gOManager.SetSprite(_gameSettings.ropeVisuals);
        gOManager.AnimationTime = _gameSettings.ropeAnimationTime;
        gOManager.SpeedModifier = _gameSettings.ropeSpeedModifier;
        PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
        PrefabUtility.UnloadPrefabContents(contentsRoot);
#endif
    }
}
