using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private DataSettingsScriptableObject _dataSettings;
    [SerializeField] private Transform _spawnedPointHierarchyLocation;
    [SerializeField] private Transform _spawnedRopeHierarchyLocation;
    private GameManager _gameManager;
    private GameDataManager _gameDataManager;
    private GameSettingsManager _gameSettingsManager;
    private List<GameObject> _spawnedPoints = new();
    private int[] _levelData;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    public List<GameObject> SpawnPoints(GameDataManager gameDataManager, GameSettingsManager gameSettingsManager)
    {
        AdditionalInitializations(gameDataManager, gameSettingsManager);
        _levelData = _gameDataManager.GetLevelData(_gameManager.SelectedLevel);
        _gameManager.PointLength = _levelData.Length / 2;

        int pointNumber = 1;
        for (int selectedCoordinate = 0; selectedCoordinate < _levelData.Length; selectedCoordinate += 2)
        {
            float xCoordinate = _levelData[selectedCoordinate] - (_dataSettings.coordinatesEndAt - _dataSettings.coordinatesStartAt) / 2;
            float yCoordinate = _levelData[selectedCoordinate + 1] * -1 + (_dataSettings.coordinatesEndAt - _dataSettings.coordinatesStartAt) / 2;
            var spawnedPoint = Instantiate(_gameSettingsManager.Point, new Vector3(xCoordinate / 100, yCoordinate / 100, 0), Quaternion.identity);

            SetGOToParentLocation(spawnedPoint.transform, _spawnedPointHierarchyLocation);
            spawnedPoint.name = $"Point {pointNumber}";
            spawnedPoint.GetComponent<Point>().PointNumber = pointNumber;
            _spawnedPoints.Add(spawnedPoint);

            pointNumber++;
        }
        return _spawnedPoints;
    }

    private void AdditionalInitializations(GameDataManager gameDataManager, GameSettingsManager gameSettingsManager)
    {
        _gameDataManager = _gameManager.GameDataManager;
        _gameSettingsManager = _gameManager.GameSettingsManager;
    }

    public GameObject SpawnRope(GameObject spawnFrom, GameObject spawnTo, int pointFromNumber, int pointToNumber)
    {
        var spawnedRope = Instantiate(_gameSettingsManager.Rope, spawnFrom.transform.position, Quaternion.identity);
        spawnedRope.GetComponent<Rope>().SetRopeConnectionPoints(spawnFrom, spawnTo);
        SetGOToParentLocation(spawnedRope.transform, _spawnedRopeHierarchyLocation);
        spawnedRope.name = $"Rope connecting point: {pointFromNumber} to {pointToNumber}";
        return spawnedRope;
    }

    private void SetGOToParentLocation(Transform initialLocation, Transform newLocation)
    {
        if(newLocation)
        {
            initialLocation.SetParent(newLocation);
        }
    }
}

