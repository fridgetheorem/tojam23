using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField]
    private float _slowSpeed = 0.5f;

    [Header("Damage Ticking Parameters")]
    [SerializeField]
    private List<AnimalController> animalsInZone = new List<AnimalController>();

    [SerializeField]
    private List<float> timeInZone = new List<float>();

    [SerializeField]
    private List<float> totalTimeInZone = new List<float>();

    public float _dotTick = 1f;

    public float _dotBaseDamage = 1f;

    public float _dotIncrease = 1.1f;

    // Start is called before the first frame update

    void Start() { }

    // Entering the slowzone will add this animal to the DoT damage.
    private void OnTriggerEnter(Collider other)
    {
        AnimalController animal = other.gameObject.GetComponent<AnimalController>();
        if (animal != null && AnimalIndex(animal) == -1 && other.gameObject.tag != "Enemy")
        {
            animalsInZone.Add(animal);
            timeInZone.Add(0f);
            totalTimeInZone.Add(0f);
        }
    }

    // Staying in the slowzone will make the animal slowed.
    private void OnTriggerStay(Collider other)
    {
        AnimalController animal = other.gameObject.GetComponent<AnimalController>();
        if (animal != null && other.gameObject.tag != "Enemy")
            animal.speed = _slowSpeed;
    }

    // Leaving the slowzone will remove this animal from taking DoT damage.
    private void OnTriggerExit(Collider other)
    {
        AnimalController animal = other.gameObject.GetComponent<AnimalController>();

        if (animal != null && other.gameObject.tag != "Enemy")
        {
            animal.speed = animal.originalSpeed;

            int i = AnimalIndex(animal);
            if (i == -1)
                return;
            animalsInZone.RemoveAt(i);
            timeInZone.RemoveAt(i);
            totalTimeInZone.RemoveAt(i);
        }
    }

    private int AnimalIndex(AnimalController target)
    {
        for (int i = 0; i < animalsInZone.Count; i++)
        {
            if (animalsInZone[i] == target)
                return i;
        }
        return -1;
    }

    void Update()
    {
        for (int i = 0; i < timeInZone.Count; i++)
            if (timeInZone[i] > _dotTick)
            {
                animalsInZone[i].BeDamaged(
                    _dotBaseDamage * Mathf.Pow(_dotIncrease, totalTimeInZone[i])
                );
                totalTimeInZone[i] += 1;
                timeInZone[i] = 0f;
            }
            else
                timeInZone[i] += Time.deltaTime;
    }
}
