using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameSettingsManager _gameSettingsManager;
    [SerializeField] private SceneChangeManager _sceneChangeManager;
    [SerializeField] private ResolutionChangeManager _resolutionChangeManager;
    [SerializeField] private Camera _camera;
    private Vector2 _pointerPosition;
    [field:SerializeField] public bool IsDesktop { get; private set; }

    private void Awake()
    {
        CheckWhatDeviceIsRunning();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void CheckWhatDeviceIsRunning()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            IsDesktop = true;
        }
        else
        {
            IsDesktop = false;
        }
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_pointerPosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Point"))
                {
                    hit.transform.gameObject.GetComponent<Point>().HandleHit();
                }
            }
        }
    }

    public void ChangeLevel()
    {
        _sceneChangeManager.ContinueToNextLevel();
    }

    public void SceneChanged()
    {
        _camera = Camera.main;
    }

    public void OnPointerAim(InputAction.CallbackContext context)
    {
        _pointerPosition = context.ReadValue<Vector2>();
    }

    public void Exit()
    {
        _sceneChangeManager.HandleExitLogic();
    }

    public void Exit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _sceneChangeManager.HandleExitLogic();
        }
    }
    public void ChangeResolutionMode()
    {
        _resolutionChangeManager.HandleResolutionModeChange();
    }
}
