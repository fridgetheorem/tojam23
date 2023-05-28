using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private float _fireDuration = 0.6f;
    public void DestroyFire(){
        Destroy(gameObject.GetComponentInChildren<Collider>());
        StartCoroutine(
            FadeFire(_fireDuration)
        );
    }

    IEnumerator FadeFire(float fadeDuration)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color initialColor = spriteRenderer.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration){
            elapsedTime += Time.deltaTime;
            spriteRenderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
