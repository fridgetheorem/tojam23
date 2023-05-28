using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    // Attributes
    public AnimalController leader;
    private int leaderIndex = 0;
    public GameObject[] memberPrefabs; // Just so that we are keeping a list of prefabs
    private List<GameObject> members; // The actual instantiated objects

    [SerializeField] float partyRadius = 2f;

    [SerializeField] CinemachineVirtualCamera _virtualCamera;

    // Events
    public delegate void OnAnimalChange(AnimalController newLeader);
    public event OnAnimalChange AnimalChanged;

    private static float _followSpeed = 2f;
    public static float followSpeed {
        get { return _followSpeed; }
        set { _followSpeed = value; }
    }

    private void Start(){
        members = new List<GameObject>();
        // Spawn in all of our party members
        #region Spawn Allies
        foreach(GameObject member in memberPrefabs){
            // Create a random spawn position for them within a radius.
            Vector2 point = Random.insideUnitCircle * partyRadius;
            Vector3 spawnPos = new Vector3(point.x, transform.position.y, point.y);

            // Random heading, just for funsies
            float heading = Random.Range(0, 360);
            Quaternion randomDirection = Quaternion.Euler(0, heading, 0);

            members.Add(Instantiate(member, spawnPos, randomDirection));
        }
        #endregion

        leader = members[leaderIndex].GetComponent<AnimalController>();
        SetLeader();
    }

    private void SetLeader(){
        transform.SetPositionAndRotation(leader.transform.position, Quaternion.identity);
        transform.parent = leader.transform;
        _virtualCamera.Follow = leader.transform;

        setAnimalCollisions();
    }

    private void setAnimalCollisions(){
        // Members still collide with members
        foreach(GameObject member in members){
            foreach(GameObject otherMember in members){
                if (member != otherMember) Physics.IgnoreLayerCollision(member.gameObject.layer, otherMember.gameObject.layer, false);
            }
        }

        // Leader doesn't collide with members      
        foreach(GameObject member in members){
            Physics.IgnoreLayerCollision(leader.gameObject.layer, member.gameObject.layer);
        }
    }
    

    private void CycleLeader() {
        this.leaderIndex = (this.leaderIndex + 1) % members.Count;
        this.leader = members[this.leaderIndex].GetComponent<AnimalController>();
        AnimalChanged?.Invoke(this.leader);
        SetLeader();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            CycleLeader();
        }

        MoveOthersCloser();
    }

    public void Move(Vector2 movementInput){
        Vector3 translatedMovement = new Vector3(movementInput.x, 0, movementInput.y);
        leader?.Move(translatedMovement, leader.speed);
    }

    public void Interact(){
        leader.Interact();
    }

    // Move the other animals within the radius of the leader
    public void MoveOthersCloser(){
        for(int i = 0; i < members.Count; ++i){
            GameObject member = members[i];
            if(i == leaderIndex) continue;

            // Get the relative position of the animal compared to me
            Vector3 distance = transform.position - member.transform.position;

            if (distance.magnitude > partyRadius){
                member.GetComponent<AnimalController>().Move(distance, _followSpeed);
            }

        }
    }
}