using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Animator mainAnimator;
    public SpriteRenderer mainSprite;

    private Animator shadowAnimator;
    private SpriteRenderer shadowSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (mainAnimator == null)
        {
            Debug.LogWarning(
                "Object " + gameObject.name + "'s shadow is not referencing main animator."
            );
            mainAnimator = this.transform.parent.gameObject.GetComponentInChildren<Animator>();
        }

        if (mainSprite == null)
        {
            Debug.LogWarning(
                "Object " + gameObject.name + "'s shadow is not referencing main sprite."
            );
            mainSprite = this.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        shadowAnimator = GetComponent<Animator>();
        shadowSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mainAnimator.parameterCount; i++)
        {
            AnimatorControllerParameter shadowParamater = shadowAnimator.GetParameter(i);
            AnimatorControllerParameter mainParamater = mainAnimator.GetParameter(i);
            shadowAnimator.SetBool(shadowParamater.name, mainAnimator.GetBool(mainParamater.name));
        }

        shadowSprite.flipX = mainSprite.flipX;
    }
}
