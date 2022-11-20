using TMPro;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    private InputManager _inputManager;
    [SerializeField] private TextMeshProUGUI _textTMP;
    [SerializeField] private string _originalText;
    [SerializeField] private string _changedText;
    [SerializeField] private bool _workOnlyOnDesktop = true;
    private bool _originalTextIsSelected;

    private void Awake()
    {
        _inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        _textTMP.text = _originalText;
        _originalTextIsSelected = true;
        if(_workOnlyOnDesktop && _inputManager.IsDesktop)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Exit()
    {
        _inputManager.Exit();
    }

    public void ChangeResolutionMode()
    {
        _inputManager.ChangeResolutionMode();
        SwapTexts();
    }

    public void Continue()
    {
        _inputManager.ChangeLevel();
    }

    private void SwapTexts()
    {
        if(_originalTextIsSelected)
        {
            ChangeText(_changedText);
            _originalTextIsSelected = false;
        }
        else
        {
            ChangeText(_originalText);
            _originalTextIsSelected = true;
        }
    }

    private void ChangeText(string newText)
    {
        _textTMP.text = newText;
    }
}
