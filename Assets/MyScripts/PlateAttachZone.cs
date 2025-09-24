using UnityEngine;

public class PlateAttachZone : MonoBehaviour
{

    public Plate plate;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potato"))
        {
            Potato potato = other.GetComponent<Potato>();
            if (potato != null && potato.isCooked)
            {
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
