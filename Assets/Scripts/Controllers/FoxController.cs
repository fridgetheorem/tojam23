using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : AnimalController
{
    [Header("Fox Properties")]
    public float dashDistance = 4;

    [SerializeField]
    private float _dashDuration = 0.5f;

    [SerializeField]
    private float _dashCooldown = 1f;
    private bool _canDash = true;

    //private bool _dashing = false;
    private Vector3 heading;

    private Vector3 originalScale;

    [SerializeField]
    private GameObject afterImage;

    new void Start()
    {
        base.Start(); // make sure base init stuff gets done
        originalScale = transform.localScale;
    }

    public override void Move(Vector3 vector, float speed)
    {
        base.Move(vector, speed);
        heading = vector;
    }

    public override void Interact()
    {
        DoDash();
    }

    void DoDash()
    {
        if (heading.magnitude > 0 && !_canDash)
            return;

        UpdateAnimator("Dashing", true);
        transform.localScale = new Vector3(originalScale.x, originalScale.y / 2f, originalScale.z);

        // Create an afterimage.
        CreateAfterImage();

        PlaySFX();

        _canDash = false;
        InputController._inputEnabled = false;
        StartCoroutine(Dash(heading.normalized * dashDistance, _dashDuration));
    }

    IEnumerator Dash(Vector3 direction, float length)
    {
        float afterImageLength = length / 7f;
        float afterImageElapsedTime = 0;

        float elapsedTime = 0;
        while (elapsedTime < length)
        {
            if (afterImageElapsedTime > afterImageLength)
            {
                CreateAfterImage();
                afterImageElapsedTime = 0;
            }
            Move(direction * (Time.deltaTime * length), dashDistance);
            elapsedTime += Time.deltaTime;
            afterImageElapsedTime += Time.deltaTime;
            yield return null;
        }
        OnDashEnd();
        yield return null;
    }

    private void OnDashEnd()
    {
        UpdateAnimator("Dashing", false);
        transform.localScale = originalScale;

        StartCoroutine(DestroyAfterImages(_dashDuration));

        InputController._inputEnabled = true;
        _canDash = false;
        party.followSpeed = dashDistance;
        StartCoroutine(DashCooldown(_dashCooldown));
    }

    IEnumerator DashCooldown(float length)
    {
        float elapsedTime = 0;
        while (elapsedTime < length)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _canDash = true;
        yield return null;
    }

    void CreateAfterImage()
    {
        GameObject newImage = Instantiate(afterImage, transform.position, transform.rotation);
        newImage.tag = "FoxAfterImage";

        SpriteRenderer newSprite = newImage.GetComponent<SpriteRenderer>();
        SpriteRenderer parentSprite = GetComponentInChildren<SpriteRenderer>();

        newSprite.sprite = parentSprite.sprite;
        newSprite.flipX = parentSprite.flipX;

        newSprite.color = new Color(1f, 1f, 1f, 0.5f);
    }

    IEnumerator DestroyAfterImages(float length)
    {
        GameObject[] images = GameObject.FindGameObjectsWithTag("FoxAfterImage");
        int count = images.Length;

        for (int i = 0; i < count; i++)
        {
            Destroy(images[i]);
            yield return new WaitForSeconds(length / count);
        }
        yield return null;
    }
}
