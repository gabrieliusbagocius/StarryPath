using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public void UpdatePanel()
    {
        var rectTransform = (RectTransform)transform;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - rectTransform.rect.height);
    }
}