using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private AspectRatioManager _aspectRatioManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private int _selectedPoint;
    private int _ropesAnimated = 0;
    private const int _startingPoint = 0;
    private bool _variableRopeAnimationTime;
    public GameDataManager GameDataManager { get; private set; }
    public GameSettingsManager GameSettingsManager { get; private set; }
    public ResolutionChangeManager ResolutionChangeManager { get; private set; }
    public List<GameObject> SpawnedPoints { get; private set; }
    public List<GameObject> SpawnedRopes { get; private set; }
    public int PointLength { get; set; }
    [field: SerializeField] public int SelectedLevel { get; private set; }
    public int SelectedPoint
    {
        get { return _selectedPoint; }
        set
        {
            if (value == 1)
            {
                _scoreManager.StartCountingScore();
            }
            else if (value == PointLength)
            {
                _scoreManager.StopCountingScore();
            }
            _selectedPoint = value;
        }
    }

    private void Awake()
    {
        SpawnedRopes = new List<GameObject>();
        SelectedPoint = _startingPoint;
        GameDataManager = GameObject.Find("GameDataManager").GetComponent<GameDataManager>();
        GameSettingsManager = GameObject.Find("GameSettingsManager").GetComponent<GameSettingsManager>();
        ResolutionChangeManager = GameObject.Find("ResolutionChangeManager").GetComponent<ResolutionChangeManager>();
        ReadAndSetValues();
    }

    private void ReadAndSetValues()
    {
        _variableRopeAnimationTime = GameSettingsManager.VariableRopeSpeedTime;
        if (GameSettingsManager.SelectedLevel != 0)
        {
            SelectedLevel = GameSettingsManager.SelectedLevel;
        }
        SpawnedPoints = _spawnManager.SpawnPoints(GameDataManager, GameSettingsManager);
        ResolutionChangeManager.HandleResolutionChange(this);
    }

    private void Start()
    {
        EnablePointCollider(0);
        _aspectRatioManager.ModifyIntialFit();
    }

    public void ManageRopeSpawning(int spawnFrom, int spawnTo)
    {
        var spawnedRope = _spawnManager.SpawnRope(SpawnedPoints[spawnFrom - 1], SpawnedPoints[spawnTo - 1], spawnFrom, spawnTo);
        SpawnedRopes.Add(spawnedRope);
        _aspectRatioManager.FixRopeAspectRatio(spawnedRope);
        RopeAnimationHandler();
    }

    public void Update()
    {
        if (_ropesAnimated != SpawnedRopes.Count)
        {
            RopeAnimationHandler();
        }
        else if (SpawnedRopes.Count == PointLength)
        {
            var rope = SpawnedRopes[PointLength - 1].GetComponent<Rope>();
            if (rope.HasBeenAnimated == true)
            {
                _scoreManager.DisplayScore();
            }
        }
    }

    public void RopeAnimationHandler()
    {
        var currentlySpawnedRopesAmount = SpawnedRopes.Count;
        for (int ropeNumber = 0; ropeNumber < currentlySpawnedRopesAmount; ropeNumber++)
        {
            var rope = SpawnedRopes[ropeNumber].GetComponent<Rope>();
            var animateRope = rope.GetComponent<AnimateRope>();
            if (rope.IsInAnimation)
            {
                break;
            }
            else if (!rope.HasBeenAnimated)
            {
                rope.IsInAnimation = true;
                StartCoroutine(animateRope.AnimateRopeHeight(SpawnedRopes[ropeNumber], this, rope.AnimationTime, rope.SpeedModifier, _variableRopeAnimationTime));
                _ropesAnimated += 1;
                break;
            }
        }
    }

    public void EnablePointCollider(int pointNumber)
    {
        if (PointIsWithinList(pointNumber))
        {
            SpawnedPoints[pointNumber].GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    public void DisablePointCollider(int pointNumber)
    {
        if (PointIsWithinList(pointNumber))
        {
            SpawnedPoints[pointNumber].GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    public bool PointIsWithinList(int pointNumber)
    {
        if (pointNumber < PointLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
