using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Rankings : MonoBehaviour
{
    // Record health bar transforms
    public List<RectTransform> transforms = new List<RectTransform>();

    public List<GameObject> healthObj = new List<GameObject>();
    private PartyController _party;

    void Start()
    {
        _party = FindObjectOfType<PartyController>();

        int difference = (int)Mathf.Max(transforms.Count - _party.members.Length, 0);
        for (int i = 0; i < difference; i++)
        {
            // Removes the position associated with this health bar.
            transforms.RemoveAt(transforms.Count - 1);

            // Unsubscribe unused animal's events.
            healthObj[healthObj.Count - 1].GetComponent<AnimalDisplay>().Unsubscribe();
            healthObj[healthObj.Count - 1].GetComponent<AnimalDisplay>().Unsubscribe();

            // Disable the component.
            healthObj[healthObj.Count - 1].SetActive(false);

            // Finally remove the object from the list.
            healthObj.RemoveAt(healthObj.Count - 1);
        }
    }

    void Update() { }
}
