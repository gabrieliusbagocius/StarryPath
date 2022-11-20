using System.Collections;
using TMPro;
using UnityEngine;

public class AnimateText : MonoBehaviour
{
    public IEnumerator FadeTextToZeroAlpha(TextMeshProUGUI textMeshProUGUI, float textAnimationTime)
    {
        textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, 1);
        while (textMeshProUGUI.color.a > 0.0f)
        {
            textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, textMeshProUGUI.color.a - (Time.deltaTime / textAnimationTime));
            yield return null;
        }
        yield return null;
    }
}
