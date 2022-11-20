using UnityEngine;

public class ResolutionChangeManager : MonoBehaviour
{
    public delegate void ScreenSizeChangeEventHandler(float lastWidth, float lastHeight, float newWidth, float newHeight);
    public event ScreenSizeChangeEventHandler ScreenSizeChangeEvent;
    [ReadOnly, SerializeField] private Vector2 _defaultResolution;
    private Vector2 _lastScreenSize;
    private bool _fullScreen;
    private ScreenOrientation _orientation;
    public float AspectRatio { get; private set; }
    public Vector2 DefaultResolution
    {
        get { return _defaultResolution; }
        private set { _defaultResolution = value; }
    }

    public void HandleResolutionChange(GameManager gameManager)
    {
        var gameDataManager = gameManager.GameDataManager;
        var resolutionLength = gameDataManager.ResolutionLength;

        DefaultResolution = new Vector2(resolutionLength, resolutionLength);
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        AspectRatio = (Screen.width / _defaultResolution.x + Screen.height / _defaultResolution.y) / 3;
    }
    public void OnScreenSizeChange(float lastWidth, float lastHeight, float newWidth, float newHeight)
    {
        AspectRatio = (newWidth / _defaultResolution.x + newHeight / _defaultResolution.y) / 3;
        ScreenSizeChangeEvent?.Invoke(lastWidth, lastHeight, newWidth, newHeight);
    }

    public void HandleResolutionModeChange()
    {
        _fullScreen = !_fullScreen;
        if (_fullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void Awake()
    {
        _orientation = Screen.orientation;
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            _fullScreen = true;
        }
    }

    private void Update()
    {
        Vector2 screenSize = new(Screen.width, Screen.height);
        var newOrientation = Screen.orientation;
        if (_lastScreenSize != screenSize | _orientation != newOrientation)
        {
            _orientation = newOrientation;
            OnScreenSizeChange((int)_lastScreenSize.x, (int)_lastScreenSize.y, (int)screenSize.x, (int)screenSize.y);
            _lastScreenSize = screenSize;
        }
    }
}
