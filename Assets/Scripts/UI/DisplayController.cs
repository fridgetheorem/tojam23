using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [Header("Party Info")]
    [SerializeField]
    protected PartyController _party;

    [SerializeField]
    private bool _partySynced = false;

    [SerializeField]
    private PartySyncZone _partySyncZone;

    public DialogueTrigger DeathDialogue;

    // Record health bar transforms
    [Header("")]
    public List<RectTransform> transforms = new List<RectTransform>();

    [SerializeField]
    public List<GameObject> healthObj = new List<GameObject>();

    [SerializeField]
    public List<AnimalDisplay> displays = new List<AnimalDisplay>();

    void Start()
    {
        // Subscribe to party swapping for this animal.
        _party = FindObjectOfType<PartyController>();
        if (_party)
        {
            _party.AnimalChanged += OnAnimalChanged;
        }

        // Subscribe to party syncing.
        if (_partySyncZone)
        {
            _partySyncZone.PartySynced += OnPartySynced;
        }

        // Allow for death.
        for (int i = 0; i < transforms.Count; i++)
        {
            healthObj[i].GetComponent<Health>().Death += OnDeath;
        }

        int difference = (int)Mathf.Max(transforms.Count - _party.members.Length, 0);
        for (int i = 0; i < difference; i++)
        {
            // Removes the position associated with this health bar.
            transforms.RemoveAt(transforms.Count - 1);

            // Disable the component.
            healthObj[healthObj.Count - 1].SetActive(false);

            // Finally remove the object from the list.
            healthObj.RemoveAt(healthObj.Count - 1);
        }

        // Hide other health bars if party is unsynced on start.
        if (!_partySynced)
        {
            for (int i = 1; i < transforms.Count; i++)
            {
                displays[i].HideHealthBar();
            }
        }

        // Assign first animal as special ability (already default health bar).
        displays[0].ShowHealthBar();
        displays[0].ShowSpecialAbility();
    }

    void OnDeath()
    {
        DeathDialogue.TriggerDialogue();
    }

    void OnPartySynced()
    {
        _partySynced = true;
        for (int i = 1; i < transforms.Count; i++)
        {
            displays[i].ShowHealthBar();
        }
    }

    void OnAnimalChanged(AnimalController newLeader)
    {
        // Cycle animals until party leader is 0.
        while (displays[0].animal != newLeader)
        {
            AnimalDisplay tempDisplay = displays[0];
            GameObject tempHealthObj = healthObj[0];

            displays.RemoveAt(0);
            healthObj.RemoveAt(0);

            displays.Add(tempDisplay);
            healthObj.Add(tempHealthObj);
        }

        // Swap each health bar with its associated rankings.
        for (int i = 0; i < transforms.Count; i++)
        {
            displays[i].SwapDisplay(
                healthObj[i],
                transforms[i].localPosition,
                transforms[i].localScale
            );
        }

        // Cycling each animal at a time means the last ranked animal was the previous leader.
        displays[0].ShowSpecialAbility();
        displays[displays.Count - 1].HideSpecialAbility();

        // Hide the other health bars.
        if (!_partySynced)
        {
            displays[0].ShowHealthBar();
            displays[displays.Count - 1].HideHealthBar();
        }
    }

    public void Destroy()
    {
        if (_party)
        {
            _party.AnimalChanged -= OnAnimalChanged;
        }
        for (int i = 0; i < transforms.Count; i++)
        {
            healthObj[i].GetComponent<Health>().Death -= OnDeath;
        }
    }
}
