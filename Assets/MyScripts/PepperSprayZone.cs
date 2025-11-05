using System.Collections.Generic;
using UnityEngine;

public class PepperSprayZone : MonoBehaviour
{
    // List of plates currently in the spray zone
    private List<Plate> plates = new List<Plate>();

    // When a plate enters the spray zone
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            Plate newplate = other.gameObject.GetComponent<Plate>();
            plates.Add(newplate);
        }
    }

    // When a plate exits the spray zone
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            Plate removedplate = other.gameObject.GetComponent<Plate>();
            if (plates.Contains(removedplate))
            {
                plates.Remove(removedplate);
            }
        }
    }

    // Apply pepper to all plates in the spray zone
    public void applyPepper()
    {
        foreach (Plate plate in plates)
        {
            // Only add pepper if the plate has cheese
            if (plate.hasIngredient(IngredientTypes.Fromage))
            {
                plate.AddIngredient(IngredientTypes.Poivre);
            }
        }
    }
}