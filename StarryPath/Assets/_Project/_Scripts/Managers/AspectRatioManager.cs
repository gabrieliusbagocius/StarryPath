using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private ResolutionChangeManager _resolutionChangeManager;
    private Vector2 _defaultResolution;
    private float _aspectRatio;
    private float _gameScreenRatio;
    private GameManager _gameManager;
    private GameSettingsManager _gameSettingsManager;
    private List<GameObject> _spawnedPoints;
    private List<GameObject> _spawnedRopes;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        _gameSettingsManager = _gameManager.GameSettingsManager;
        _resolutionChangeManager = _gameManager.ResolutionChangeManager;
        SubscribeToEvents();
        LoadGameSettingsValues();
        ReadAndSetValues();
    }

    private void ReadAndSetValues()
    {
        _spawnedPoints = _gameManager.SpawnedPoints;
        _spawnedRopes = _gameManager.SpawnedRopes;
    }

    private void SubscribeToEvents()
    {
        _resolutionChangeManager.ScreenSizeChangeEvent += Instance_ScreenSizeChangeEvent;
    }

    private void LoadGameSettingsValues()
    {
        _gameScreenRatio = _gameSettingsManager.GameScreenRatio;
    }

    private void Instance_ScreenSizeChangeEvent(float lastWidth, float lastHeight, float newWidth, float newHeight)
    {
        _aspectRatio = _resolutionChangeManager.AspectRatio;
        FitAllIntoNewResolution(lastWidth, lastHeight, newWidth, newHeight);
    }

    private void OnDestroy()
    {
        _resolutionChangeManager.ScreenSizeChangeEvent -= Instance_ScreenSizeChangeEvent;
    }

    public void ModifyIntialFit()
    {
        _aspectRatio = _resolutionChangeManager.AspectRatio;
        _defaultResolution = _resolutionChangeManager.DefaultResolution;
        FitAllIntoNewResolution(_defaultResolution.x, _defaultResolution.y, Screen.width, Screen.height);
        _gameScreenRatio = 1;
    }

    private void FitAllIntoNewResolution(float lastWidth, float lastHeight, float newWidth, float newHeight)
    {
        _mainCamera.orthographicSize = newHeight / 100 / 2;
        float changeRatioOfX = newWidth / lastWidth * _gameScreenRatio;
        float changeRatioOfY = newHeight / lastHeight * _gameScreenRatio;

        foreach (GameObject point in _spawnedPoints)
        {
            point.transform.position = new Vector2(point.transform.position.x * changeRatioOfX, point.transform.position.y * changeRatioOfY);
            point.transform.localScale = new Vector2(newHeight / _defaultResolution.y, newHeight / _defaultResolution.y);
        }

        foreach (GameObject rope in _spawnedRopes)
        {
            FixRopeAspectRatio(rope);
        }
    }

    public void FixRopeAspectRatio(GameObject spawnedRope)
    {
        spawnedRope.GetComponent<Rope>().UpdateRopeConnectionValues();

        var ropeManager = spawnedRope.GetComponent<Rope>();
        var ropeSpawnFromPosition = ropeManager.RopeStartPosition.transform.position;
        var ropeSpawnToPosition = ropeManager.RopeEndPosition.transform.position;
        var ropeSpriteRenderer = spawnedRope.GetComponent<SpriteRenderer>();
        var distance = Vector2.Distance(ropeSpawnFromPosition, ropeSpawnToPosition) / _aspectRatio;

        spawnedRope.GetComponent<Rope>().RopeHeight = distance;
        spawnedRope.transform.position = ropeSpawnFromPosition;
        spawnedRope.transform.up = ropeSpawnToPosition - ropeSpawnFromPosition;
        ropeSpriteRenderer.size = new Vector2(ropeSpriteRenderer.size.x, distance);
        spawnedRope.transform.localScale = new Vector3(_aspectRatio, _aspectRatio);
    }
}
