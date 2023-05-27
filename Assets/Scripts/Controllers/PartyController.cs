using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    // Attributes
    public AnimalController leader;
    private int leaderIndex = 0;
    public GameObject[] members; // Just so that w`e are keeping a list of prefabs

    [SerializeField] float partyRadius = 3f;

    // Events
    public delegate void OnAnimalChange(AnimalController newLeader);
    public event OnAnimalChange AnimalChanged;

    private void Start(){
        // Spawn in all of our party members
        #region Spawn Allies
        foreach(GameObject member in members){
            // Create a random spawn position for them within a radius.
            Vector2 point = Random.insideUnitCircle * partyRadius;
            Vector3 spawnPos = new Vector3(point.x, transform.position.y, point.y);

            // Random heading, just for funsies
            float heading = Random.Range(0, 360);
            Quaternion randomDirection = Quaternion.Euler(0, heading, 0);

            Instantiate(member, spawnPos, randomDirection);
        }
        #endregion

        leader = members[leaderIndex].GetComponent<AnimalController>();
    }

    private void SetLeader(){
        
    }
    

    private void CycleLeader() {
        this.leaderIndex = (this.leaderIndex + 1) % members.Length;
        this.leader = members[this.leaderIndex].GetComponent<AnimalController>();
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