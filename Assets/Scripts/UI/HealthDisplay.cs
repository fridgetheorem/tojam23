using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider _display; // Health bar in the UI

    public Health _health; // Health bar in code

    // Start is called before the first frame update
    void Start()
    {
        if (_display == null || _health == null)
        {
            Debug.LogWarning("Display and values are not fully defined in inspector.");
        }

        // Subscribe to the delegates
        if (_health)
        {
            _health.Death += OnDeath;
            _health.HealthChanged += OnHealthChanged;

            // Record the max health of the animal.
            _display.maxValue = _health.health;
            _display.value = _health.health;
        }
    }

    void OnDeath()
    {
        Debug.Log("This animal has died.");
        // this.gameObject.SetActive(false);
        // Don't know what to do here
    }

    void OnHealthChanged(float oldHealth, float newHealth)
    {
        // TODO: Implement league like damage dealt
        _display.value = newHealth;
    }

    // Update is called once per frame
    void Update() { }
}
