using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject _healthObj;

    [SerializeField]
    private AnimalController _animal;

    [SerializeField]
    private PartyController _party;

    [SerializeField]
    public Animator specialAbility;

    [SerializeField]
    private AnimalController previousLeader;

    // Also be aware of the rankings

    // Start is called before the first frame update
    void Start()
    {
        _party = FindObjectOfType<PartyController>();
        if (_party == null)
        {
            Debug.LogWarning("No PartyController present in Scene");
        }

        // Subscribe to party swapping for this animal.
        if (_party)
        {
            _party.AnimalChanged += OnAnimalChanged;
        }

        if (_animal == null)
        {
            Debug.LogWarning("AnimalController not defined");
        }

        if (_healthObj == null)
        {
            Debug.LogWarning("Health Object not defined");
        }

        // Assign first animal as special ability (already default health bar).
        if (previousLeader == _animal) // Deselect its special ability.
        {
            specialAbility.SetBool("Selected", true);
        }
    }

    void OnAnimalChanged(AnimalController newLeader)
    {
        // Find animals ranking using the AnimalController.
        int i = _party.leaderIndex;
        int rank = 0;

        do
        {
            if (_party.members[i] == _animal)
            {
                break;
            }
            i += 1;
            i %= _party.members.Length; // To loop back to 0.
            rank += 1;
        } while (i != _party.leaderIndex);

        // Given it's rank (0 being leader) give it the appropriate transform.
        RectTransform rt = FindObjectOfType<Rankings>().transforms[rank];
        _healthObj.transform.localPosition = rt.localPosition;
        _healthObj.transform.localScale = rt.localScale;

        // Animate its special ability.
        if (newLeader == _animal) // Select its special ability.
        {
            specialAbility.SetBool("Selected", true);
        }
        else if (previousLeader == _animal) // Deselect its special ability.
        {
            specialAbility.SetBool("Selected", false);
        }
        // Otherwise don't do anything for the animals and track the new leader.
        previousLeader = newLeader;
    }

    public void Unsubscribe()
    {
        _party.AnimalChanged -= OnAnimalChanged;
    }

    // Update is called once per frame
    void Update() { }
}
