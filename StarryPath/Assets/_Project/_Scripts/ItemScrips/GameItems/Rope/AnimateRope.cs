using System.Collections;
using UnityEngine;

public class AnimateRope : MonoBehaviour
{
    public IEnumerator AnimateRopeHeight(GameObject ropeGO, GameManager gameManager, float ropeAnimationTime, float ropeSpeedModifier, bool variableSpeed)
    {
        var rope = ropeGO.GetComponent<Rope>();
        var spriteRenderer = rope.GetComponent<SpriteRenderer>();
        var distance = rope.RopeHeight;
        spriteRenderer.enabled = true;
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, 0);

        while (spriteRenderer.size.y < distance)
        {
            distance = rope.RopeHeight;
            if (variableSpeed)
            {
                spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y + (Time.deltaTime * ropeSpeedModifier));
            }
            else
            {
                spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y + (Time.deltaTime * distance / ropeAnimationTime));
            }
            yield return null;
        }
        rope.GetComponent<Rope>().HasBeenAnimated = true;
        rope.GetComponent<Rope>().IsInAnimation = false;
    }
}
