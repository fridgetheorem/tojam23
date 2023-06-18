using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LynxController : AnimalController
{
    public float screamRadius = 5;

    public Animator screechImage;

    [SerializeField]
    private float _screechCooldown = 1f;
    private bool _canScreech = true;

    // Start is called before the first frame update

    public override void Interact()
    {
#if DEBUG
        Debug.Log("Screech!");
#endif

        if (!_canScreech)
            return;

        DoScream();
    }

    IEnumerator ScreechCooldown(float length)
    {
        float elapsedTime = 0;
        while (elapsedTime < length)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _canScreech = true;
        yield return null;
    }

    void DoScream()
    {
        if (screechImage != null)
            screechImage.SetTrigger("TriggerScreech");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, screamRadius);
        if (hitColliders.Length == 0)
            return;

        // Probably wanna play some sound
        PlaySFX();

        // We hit some collider
        // Do something with this information
        foreach (Collider collider in hitColliders)
        {
            // If the layer is Fire, remove it
            if (collider.gameObject.layer == LayerMask.NameToLayer("Fire"))
            {
                collider.gameObject.GetComponentInParent<Fire>().DestroyFire();
            }
        }

        _canScreech = false;
        StartCoroutine(ScreechCooldown(_screechCooldown));
    }
}
