using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarrierType
{
    PartySync,
    Deaths
}

public class Barrier : MonoBehaviour
{
    public BarrierType triggerOn;

    [Tooltip("Add all the enemies the player must kill for this barrier to dissapear.")]
    public List<EnemyController> enemies;

    public delegate void OnBarrierClear();
    public event OnBarrierClear EnemiesCleared;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerOn == BarrierType.Deaths)
        {
            if (enemies.Count == 0)
            {
                Debug.LogWarning("Barrier has no enemies to check death for.");
                return;
            }
            foreach (EnemyController enemy in enemies)
            {
                enemy.Death += UpdateEnemies;
            }
        }

        if (triggerOn == BarrierType.PartySync)
        {
            PartySyncZone _partySyncZone = FindObjectOfType<PartySyncZone>();
            if (_partySyncZone)
                _partySyncZone.PartySynced += ClearBarrier;
        }
    }

    void UpdateEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].health <= 0f)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if (enemies.Count == 0)
            ClearBarrier();
    }

    void ClearBarrier()
    {
        EnemiesCleared?.Invoke();
        StartCoroutine(DestroyBarrier());
    }

    IEnumerator DestroyBarrier()
    {
        Animator animator = GetComponent<Animator>();

        if (animator)
        {
            GameObject.FindGameObjectWithTag("FireSFX").GetComponent<AudioSource>().Play();
            animator.SetTrigger("ClearBarrier");
            yield return new WaitForSeconds(1.5f);
        }

        Destroy(this.gameObject);
    }

    void Destroy()
    {
        PartySyncZone _partySyncZone = FindObjectOfType<PartySyncZone>();
        if (_partySyncZone)
            _partySyncZone.PartySynced -= ClearBarrier;
    }

    // Update is called once per frame
    void Update() { }
}
