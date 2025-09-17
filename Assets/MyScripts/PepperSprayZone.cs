using System.Collections.Generic;
using UnityEngine;

public class PepperSprayZone : MonoBehaviour
{
    private List<Plate> plates = new List<Plate>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            Plate newplate = other.gameObject.GetComponent<Plate>();
            plates.Add(newplate);
        }
    }

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

    public void applyPepper()
    {
        foreach (Plate plate in plates)
        {
            if (plate.hasIngredient(IngredientTypes.Fromage))
            {
                plate.AddIngredient(IngredientTypes.Poivre);
            }
        }
    }
}
