using UnityEngine;

public class raclageZoneFromage : MonoBehaviour
{
    public float scrapeCooldown = 1f;
    private float lastScrapeTime = -1f;
    public CheeseWheel cheese;
    private plateZone plateZone;

    void Start()
    {
        plateZone = FindFirstObjectByType<plateZone>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            RacletteKnife knife = other.GetComponent<RacletteKnife>();

            if (knife.isScraping && plateZone.currentPlate != null && cheese.isReady && (Time.time - lastScrapeTime) >= scrapeCooldown)
            {
                lastScrapeTime = Time.time;
                
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
