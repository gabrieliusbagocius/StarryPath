using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/SpawnGameSettings", order = 1)]

public class GameSettingsScriptableObject : ScriptableObject
{
    [Header("Main Settings")]
    public string initialSceneName;
    public string levelSceneName;
    public GameObject point;
    public GameObject rope;
    public Sprite defaultPointVisual;
    public Sprite selectedPointVisual;
    public Sprite backgroundVisual;
    public Sprite ropeVisuals;
    [Header("Score Settings")]
    public int pointScoreAmplifier = 1;
    [Header("Screen Coverage Settings")]
    public float gameSceenRatio = 0.95f;
    [Header("Point Animation Settings")]
    public float pointTextAnimationTime = 1f;
    [Header("Rope Gameplay Settings")]
    public float ropeAnimationTime = 1f;
    [Tooltip("If variable rope speed time = true the rope will not speed up to match selected rope animation time")]
    public bool variableRopeSpeedTime = true;
    [Tooltip("If variable rope speed time = true the rope animation speed will depend on this modifier and rope animation time will not be used")]
    public float ropeSpeedModifier = 10;
}
