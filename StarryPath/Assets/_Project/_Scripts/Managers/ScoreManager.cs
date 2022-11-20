using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _levelFinishedScreen;
    [SerializeField] private TextMeshProUGUI _scoreTMP;
    private GameSettingsManager _gameSettingsManager;
    private int _scoreAmplifier;
    private const int _scoreMultiplier = 5;
    private bool _countScore = false;
    private const int _maximumTimeLimitForScoring = 20;
    public float CurrentScore { get; private set; }

    private void Start()
    {
        _gameSettingsManager = _gameManager.GameSettingsManager;
        _scoreAmplifier = _gameSettingsManager.ScoreAmplifier;
    }

    private void Update()
    {
        if(_countScore)
        {
            CurrentScore -= Time.deltaTime;
        }
    }

    public void StartCountingScore()
    {
        CurrentScore = _maximumTimeLimitForScoring;
        _countScore = true;
    }

    public void StopCountingScore()
    {
        _countScore = false;
        if(CurrentScore > 0)
        {
            CurrentScore *= (int)(_scoreAmplifier * _scoreMultiplier);
        }
        else
        {
            CurrentScore = 0;
        }
    }

    public void DisplayScore()
    {
        _scoreTMP.text = ((int)CurrentScore).ToString();
        _levelFinishedScreen.SetActive(true);
    }
}
