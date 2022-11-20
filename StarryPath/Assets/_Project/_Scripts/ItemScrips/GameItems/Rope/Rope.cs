using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public GameObject RopeStartPosition { get; set; }
    [field:SerializeField] public Vector3 StartVector { get; private set; }
    public GameObject RopeEndPosition { get; set; }
    [field: SerializeField] public Vector3 EndVector { get; private set; }
    public bool HasBeenAnimated { get; set; }
    public float RopeHeight { get; set; }
    public bool IsInAnimation { get; set; }
    [field: SerializeField] public float AnimationTime { get; set; }
    [field: SerializeField] public float SpeedModifier { get; set; }


    private void Awake()
    {
        _spriteRenderer.enabled = false;
        IsInAnimation = false;
        HasBeenAnimated = false;
    }

    public void SetRopeConnectionPoints(GameObject startPosition, GameObject endPosition)
    {
        RopeStartPosition = startPosition;
        RopeEndPosition = endPosition;
        UpdateRopeConnectionValues();
    }

    public void UpdateRopeConnectionValues()
    {
        StartVector = RopeStartPosition.transform.position;
        EndVector = RopeEndPosition.transform.position;
    }

    public void SetSprite(Sprite ropeVisuals)
    {
        _spriteRenderer.sprite = ropeVisuals;
    }
}
