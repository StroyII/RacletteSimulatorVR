using UnityEngine;

public class raclageZoneFromage : MonoBehaviour
{
    // Cooldown beetween scrapes
    public float scrapeCooldown = 1f;
    private float lastScrapeTime = -1f;

    public CheeseWheel cheese;
    private plateZone plateZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the plate zone in the scene
        plateZone = FindFirstObjectByType<plateZone>();
    }

    // When triggred by the knife while scraping, add cheese to the plate
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            RacletteKnife knife = other.GetComponent<RacletteKnife>();

            // Ckeck if knife is scraping and cooldown is over
            if (knife.isScraping && plateZone.currentPlate != null && cheese.isReady && (Time.time - lastScrapeTime) >= scrapeCooldown)
            {
                lastScrapeTime = Time.time;

                // Add cheese or religieuse if other not already present
                if (plateZone.currentPlate.hasIngredient(IngredientTypes.Fromage))
                {
                    plateZone.currentPlate.AddIngredient(IngredientTypes.Religieuse);
                }
                else
                {
                    plateZone.currentPlate.AddIngredient(IngredientTypes.Fromage);
                    cheese.currentUse++;
                }
            }
        }
    }
}
