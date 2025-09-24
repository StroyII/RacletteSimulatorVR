using UnityEngine;
using System.Collections.Generic;
public class WaterCooking : MonoBehaviour
{
    public float cookingTime = 5f;
    private List<Potato> potatoesInWater = new List<Potato>();
    private Dictionary<Potato, float> potatoTimers = new Dictionary<Potato, float>();

    private void OnTriggerEnter(Collider other)
    {
        Potato potato = other.GetComponent<Potato>();
        if (potato != null && !potato.isCooked && !potatoesInWater.Contains(potato))
        {
            potatoesInWater.Add(potato);
            potatoTimers[potato] = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Potato potato = other.GetComponent<Potato>();
        if (potato != null && potatoesInWater.Contains(potato))
        {
            potatoesInWater.Remove(potato);
            potatoTimers.Remove(potato);
        }
    }

    void Update()
    {
        // Pour chaque patate dans l'eau, incrémente le timer et cuit si besoin
        List<Potato> cookedPotatoes = new List<Potato>();
        foreach (Potato potato in potatoesInWater)
        {
            if (!potato.isCooked)
            {
                potatoTimers[potato] += Time.deltaTime;
                if (potatoTimers[potato] >= cookingTime)
                {
                    potato.Cook();
                    cookedPotatoes.Add(potato);
                }
            }
        }
        // Retire les patates cuites de la liste
        foreach (Potato potato in cookedPotatoes)
        {
            potatoesInWater.Remove(potato);
            potatoTimers.Remove(potato);
        }
    }
    
    
}
