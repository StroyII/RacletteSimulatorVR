using UnityEngine;

public class PlateAttachZone : MonoBehaviour
{

    public Plate plate;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potato"))
        {
            Potato potato = other.GetComponent<Potato>();
            if (potato != null && potato.isCooked)
            {
                plate.AddIngredient(IngredientTypes.Patate);
                Destroy(other.gameObject);
            }
        }
    }
}
