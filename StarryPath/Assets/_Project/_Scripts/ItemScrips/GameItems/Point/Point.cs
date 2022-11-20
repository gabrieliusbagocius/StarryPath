using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _pointNumberTMP;
    [SerializeField] private AnimateText _animateText;
    [SerializeField] private Sprite _defaultSprite, _selectedSprite;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _pointNumber;
    [field:SerializeField] public float TextAnimationTime { get; set; }

    public int PointNumber
    {
        get { return _pointNumber; }
        set
        {
            _pointNumber = value;
            _spriteRenderer.sortingOrder = _gameManager.PointLength + 1 - PointNumber;
            _pointNumberTMP.text = value.ToString();
        }
    }

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        ChangeSprite(_defaultSprite);
    }

    public void SetSprites(Sprite defaultSprite, Sprite selectedSprite)
    {
        _defaultSprite = defaultSprite;
        _selectedSprite = selectedSprite;
    }

    public void HandleHit()
    {
        if (PointNumber == _gameManager.SelectedPoint + 1)
        {
            StartCoroutine(_animateText.FadeTextToZeroAlpha(_pointNumberTMP, TextAnimationTime));
            _gameManager.DisablePointCollider(PointNumber - 1);
            _gameManager.EnablePointCollider(PointNumber);
            _gameManager.SpawnedPoints[PointNumber - 1].GetComponent<SpriteRenderer>().sortingOrder = 0;
            _gameManager.SelectedPoint = PointNumber;
            ChangeSprite(_selectedSprite);
            if (PointNumber > 1)
            {
                _gameManager.ManageRopeSpawning(PointNumber - 1, PointNumber);
            }
            if (PointNumber == _gameManager.PointLength)
            {
                _gameManager.ManageRopeSpawning(PointNumber, 1);
            }
        }
    }

    public void ResetPoint()
    {
        ChangeSprite(_defaultSprite);
    }

    private void ChangeSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
