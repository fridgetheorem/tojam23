using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{    
    private float damage;

    private int hurting;

    void Awake() {
        hurting = 0;
    }

    public bool IsHurting() {
        return (hurting > 0);
    }

    public void setDamage(float d) {
        damage = d;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "PlayerCharacter") {
            hurting++;
        }
    }

    void OnTriggerStay(Collider other){
        if (other.tag == "PlayerCharacter") {
            AnimalController damageable = other.gameObject.GetComponent<AnimalController>();
            if(damageable == null) return;
            damageable.BeDamaged(damage);
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "PlayerCharacter") {
            hurting--;
        }
    }
}
