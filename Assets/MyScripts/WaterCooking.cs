using UnityEngine;
using System.Collections.Generic;
public class WaterCooking : MonoBehaviour
{
    public float cookingTime = 5f;
    public AudioSource audioSourceSplash;
    public AudioSource audioReady;

    // List of the patatoes currently in the water
    private List<Potato> potatoesInWater = new List<Potato>();

    // Timers for each potato
    private Dictionary<Potato, float> potatoTimers = new Dictionary<Potato, float>();

    // Trigger when a potato enters the water
    private void OnTriggerEnter(Collider other)
    {
        audioSourceSplash.Play();
        Potato potato = other.GetComponent<Potato>();
        if (potato != null && !potato.isCooked && !potatoesInWater.Contains(potato))
        {
            // Add the potato to the list and initialize its timer
            potatoesInWater.Add(potato);
            potatoTimers[potato] = 0f;
        }
    }

    // Trigger when a potato exits the water
    private void OnTriggerExit(Collider other)
    {
        Potato potato = other.GetComponent<Potato>();
        if (potato != null && potatoesInWater.Contains(potato))
        {
            potatoesInWater.Remove(potato);
            potatoTimers.Remove(potato);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // For each potato in the water, update its timer
        List<Potato> cookedPotatoes = new List<Potato>();
        foreach (Potato potato in potatoesInWater)
        {
            if (!potato.isCooked)
            {
                potatoTimers[potato] += Time.deltaTime;
                if (potatoTimers[potato] >= cookingTime)
                {
                    // Lauch cooking
                    potato.Cook();
                    audioReady.Play();
                    cookedPotatoes.Add(potato);
                }
            }
        }
        
        // Remove cooked potatoes from the water list
        foreach (Potato potato in cookedPotatoes)
        {
            potatoesInWater.Remove(potato);
            potatoTimers.Remove(potato);
        }
    } 
}
