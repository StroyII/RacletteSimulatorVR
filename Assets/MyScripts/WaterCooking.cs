using UnityEngine;

public class WaterCooking : MonoBehaviour
{
    public float cookingTime = 5f;
    private Potato activePotato = null;
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        Potato potato = other.GetComponent<Potato>();
        if (potato != null && !potato.isCooked)
        {
            activePotato = potato;
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Potato potato = other.GetComponent<Potato>();
        if (potato != null && potato == activePotato)
        {
            activePotato = null;
            timer = 0f;
        }
    }

    void Update()
    {
        if (activePotato != null && !activePotato.isCooked)
        {
            timer += Time.deltaTime;
            if (timer >= cookingTime)
            {
                activePotato.Cook();
                timer = 0f;
            }
        }
    }
    
    
}
