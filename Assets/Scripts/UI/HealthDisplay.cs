using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider _display; // Health bar in the UI

    public Health _health; // Health bar in code

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (_display == null || _health == null)
        {
            Debug.LogWarning("Display and values are not fully defined in inspector.");
        }

        // Subscribe to the health changing for this animal.
        if (_health)
        {
            _health.Death += OnDeath;
            _health.HealthChanged += OnHealthChanged;

            // Record the max health of the animal.
            _display.maxValue = _health.maxHealth;
            _display.value = _health.health;
        }
    }

    void OnDeath() { }

    void OnHealthChanged(float oldHealth, float newHealth)
    {
        // TODO: Implement league like damage dealt
        _display.value = newHealth;

        if (sprite == null)
        {
            return;
        }

        if (oldHealth - newHealth > 0)
            AnimateHealthChanged(Color.red);
        else if (oldHealth - newHealth < 0)
            AnimateHealthChanged(Color.green);

        sprite.color = Color.white;
    }

    void AnimateHealthChanged(Color color)
    {
        StopCoroutine(ShowHealthChanged(color));
        StartCoroutine(ShowHealthChanged(color));
    }

    IEnumerator ShowHealthChanged(Color color)
    {
        yield return new WaitForSeconds(0.05f);
        sprite.color = color;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
    }

    public void Unsubscribe()
    {
        _health.Death -= OnDeath;
        _health.HealthChanged -= OnHealthChanged;
    }

    // Update is called once per frame
    void Update() { }

    void Destroy()
    {
        if (_health)
        {
            _health.Death -= OnDeath;
            _health.HealthChanged -= OnHealthChanged;
        }
    }
}
