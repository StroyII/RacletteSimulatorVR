using UnityEngine;

public class PlateAttachZone : MonoBehaviour
{
    public Plate plate;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // When triggered by a potato collider, check if it's cooked and add to plate
    void OnTriggerEnter(Collider other)
    {
        // Check for potato tag
        if (other.CompareTag("Potato"))
        {
            Potato potato = other.GetComponent<Potato>();
            if (potato != null && potato.isCooked)
            {
                // Add potato and destroy the potato object
                bool res = plate.AddIngredient(IngredientTypes.Patate);
                if (res)
                {    
                    gameManager.UnregisterItem(potato.gameObject);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}