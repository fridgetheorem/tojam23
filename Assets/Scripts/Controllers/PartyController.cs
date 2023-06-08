using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum PartyBehaviour
{
    Follow,
    Independent
}

public class PartyController : MonoBehaviour
{
    // Attributes
    public AnimalController leader;
    private int _leaderIndex = 0;
    public int leaderIndex
    {
        get { return _leaderIndex; }
        private set { _leaderIndex = value; }
    }

    //public GameObject[] memberPrefabs; // Just so that we are keeping a list of prefabs
    public GameObject[] members; // The actual instantiated objects

    [SerializeField]
    float partyRadius = 2f;

    [SerializeField]
    float maxPartyRadius = 10f;

    // Events
    public delegate void OnAnimalChange(AnimalController newLeader);
    public event OnAnimalChange AnimalChanged;

    public static PartyController playerParty;

    [SerializeField]
    private PartyBehaviour _behaviour = PartyBehaviour.Follow;
    public PartyBehaviour behaviour
    {
        get { return _behaviour; }
        set { _behaviour = value; }
    }

    private float _followSpeed = 2f;
    public float followSpeed
    {
        get { return _followSpeed; }
        set { _followSpeed = value; }
    }

    private void Start()
    {
        //members = new List<GameObject>();
        // Spawn in all of our party members
        // #region Spawn Allies
        // foreach (GameObject member in memberPrefabs)
        // {
        //     // Create a random spawn position for them within a radius.
        //     Vector2 point = Random.insideUnitCircle * partyRadius;
        //     Vector3 spawnPos = new Vector3(point.x, transform.position.y, point.y);

        //     members.Add(Instantiate(member, spawnPos, Quaternion.identity));
        // }
        // #endregion
        playerParty = this;

        leader = members[leaderIndex].GetComponent<AnimalController>();
        foreach (var member in members)
        {
            member.GetComponent<AnimalController>().SetPartyAffiliation(this);
        }
        SetLeader();
    }

    private void SetLeader()
    {
        transform.SetPositionAndRotation(leader.transform.position, Quaternion.identity);
        transform.parent = leader.transform;

        // Re-enabling leader's virtual camera focuses it (since they all have equal priority.)
        leader.virtualCamera.gameObject.SetActive(false);
        leader.virtualCamera.gameObject.SetActive(true);
        setAnimalCollisions();
    }

    private void setAnimalCollisions()
    {
        // Members still collide with members
        foreach (GameObject member in members)
        {
            foreach (GameObject otherMember in members)
            {
                if (member != otherMember)
                    Physics.IgnoreLayerCollision(
                        member.gameObject.layer,
                        otherMember.gameObject.layer,
                        false
                    );
            }
        }

        // Leader doesn't collide with members
        foreach (GameObject member in members)
        {
            Physics.IgnoreLayerCollision(leader.gameObject.layer, member.gameObject.layer);
        }
    }

    public void CycleLeader()
    {
        this.leaderIndex = (this.leaderIndex + 1) % members.Length;
        this.leader = members[this.leaderIndex].GetComponent<AnimalController>();
        AnimalChanged?.Invoke(this.leader);
        SetLeader();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     CycleLeader();
        // }
        if (_behaviour == PartyBehaviour.Follow)
        {
            _followSpeed = leader.speed;
            MoveOthersCloser();
        }
    }

    public void Move(Vector2 movementInput)
    {
        Vector3 translatedMovement = new Vector3(movementInput.x, 0, movementInput.y);
        leader?.Move(translatedMovement, leader.speed);

        // Set each member animator state based on the most recent movement input
        foreach (GameObject member in members)
        {
            /*
            member.GetComponentInChildren<SpriteRenderer>().flipX = movementInput.x > 0;
            member.GetComponentInChildren<Animator>().SetBool("down", movementInput.y < 0);
            */
            //HACKY FIX FOR CAMERA, THIS IS IVNERTED!
            member.GetComponentInChildren<SpriteRenderer>().flipX = movementInput.x > 0;
            member.GetComponentInChildren<Animator>().SetBool("down", movementInput.y > 0);
        }
    }

    public void Interact()
    {
        leader.Interact();
    }

    // Move the other animals within the radius of the leader
    public void MoveOthersCloser()
    {
        for (int i = 0; i < members.Length; ++i)
        {
            GameObject member = members[i];
            if (i == leaderIndex)
                continue;

            // Get the relative position of the animal compared to me
            Vector3 distance = transform.position - member.transform.position;

            if (distance.magnitude > maxPartyRadius)
            {
                // Teleport them when they get too far
                TeleportPartyMembersToLeader();
            }
            else if (distance.magnitude > partyRadius)
            {
                member.GetComponent<AnimalController>().Move(distance, _followSpeed);
            }
            else if (distance.magnitude > partyRadius / 2)
            {
                // The party members slow down as they get closer,
                // just so that it looks a little more natural
                member.GetComponent<AnimalController>().Move(distance, _followSpeed / 3);
            }
        }
    }

    public void TeleportMemberToPosition(Vector3 position, int index)
    {
        TeleportMemberToPosition(position, members[index].GetComponent<AnimalController>());
    }

    public void TeleportMemberToPosition(Vector3 position, AnimalController member)
    {
        member.transform.position = position;
    }

    // Radius is an optional parameter which describes how far they have to be from the leader before we tp
    public void TeleportPartyMembersToLeader(float radius = 0f)
    {
        Debug.Log("Teleporting to:" + leader.transform.position);
        for (int i = 0; i < members.Length; ++i)
        {
            if (i == leaderIndex)
                continue;
            if (
                radius > 0f
                && (members[i].transform.position - leader.transform.position).magnitude < radius
            )
                continue;
            // Randomly within a small unit circle around the player
            // BUT NOT INSIDE
            Vector2 point = (Random.insideUnitCircle * partyRadius / 2) * 2; // Around world origin
            Vector3 teleportPos = new Vector3(
                leader.transform.position.x + point.x,
                leader.transform.position.y,
                leader.transform.position.z + point.y
            );
            TeleportMemberToPosition(teleportPos, i);
        }
    }

    public bool IsInParty(AnimalController animal)
    {
        for (int i = 0; i < members.Length; ++i)
        {
            if (members[i].GetComponent<AnimalController>() == animal)
                return true;
        }
        return false;
    }
}
