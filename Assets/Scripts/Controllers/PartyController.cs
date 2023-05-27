using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimalController))]

public class PartyController : MonoBehaviour
{
    // Attributes
    public AnimalController leader;
    public int leaderIndex;
    public AnimalController[] members;

    // Events
    public delegate void OnAnimalChange(AnimalController newLeader);
    public event OnAnimalChange AnimalChanged;

    private void CycleLeader() {
        this.leaderIndex = (this.leaderIndex + 1) % members.Length;
        this.leader = members[this.leaderIndex];
        AnimalChanged?.Invoke(this.leader);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            CycleLeader();
        }
    }
}