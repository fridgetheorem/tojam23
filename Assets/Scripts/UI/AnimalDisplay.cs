using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDisplay : MonoBehaviour
{
    public AnimalController animal;

    [SerializeField]
    public Animator specialAbility;

    public CanvasGroup canvas;

    // Start is called before the first frame update
    public void Start()
    {
        if (animal == null)
        {
            Debug.LogWarning("AnimalController not defined");
        }
    }

    public void HideHealthBar()
    {
        canvas.alpha = 0f;
    }

    public void ShowHealthBar()
    {
        canvas.alpha = 1f;
    }

    public void SwapDisplay(GameObject healthObj, Vector3 localPosition, Vector3 localScale)
    {
        healthObj.transform.localPosition = localPosition;
        healthObj.transform.localScale = localScale;
    }

    public void ShowSpecialAbility()
    {
        specialAbility.SetBool("Selected", true);
    }

    public void HideSpecialAbility()
    {
        specialAbility.SetBool("Selected", false);
    }
}
