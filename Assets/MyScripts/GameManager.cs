using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public enum IngredientTypes { Fromage, Religieuse, Patate, Poivre};
public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float porcentage = 50f;
    public float timeLeft = 100f;
    public int maxItems = 15;
    public GameObject player;
    public Transform endTpPoint;
    public GameObject[] customerPrefabs;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timeText;
    public GameOverManager gameOverManager;
    public GameObject leftRay;
    public GameObject rightRay;
    private bool isGameOver = false;
    private List<GameObject> items = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // Check for game over
        if (isGameOver) return;

        // Count down time
        timeLeft -= Time.deltaTime;
        timeText.text = "Temps restant : " + Mathf.Ceil(timeLeft).ToString() + "s";
        if (timeLeft <= 0)
        {
            finishGame();
        }
    }

    // Method to exectute the end of the game
    public void finishGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        player.transform.position = endTpPoint.position;
        player.transform.rotation = endTpPoint.rotation;

        ContinuousMoveProvider cont = player.GetComponent<ContinuousMoveProvider>();
        if (cont != null)
        {
            cont.enabled = false;
        }
        TeleportationProvider tp = player.GetComponent<TeleportationProvider>();
        if (tp != null)
        {
            tp.enabled = false;
        }

        leftRay.SetActive(true);
        rightRay.SetActive(true);

        PlayerData.score = score;
        gameOverManager.ShowGameOverMessage(score);
        gameOverManager.onGameOver();
    }
    
    // Instantiate and spawn a new customer
    public Customer SpawnNewCustomer(Transform customerSpawnPoint, List<Transform> waypoints)
    {
        int index = Random.Range(0, customerPrefabs.Length);
        // Craete the object at a specific position
        GameObject customerObj = Instantiate(customerPrefabs[index], customerSpawnPoint.position, customerSpawnPoint.rotation);
        Customer newCustomer = customerObj.GetComponent<Customer>();
        newCustomer.Init(this, waypoints);
        return newCustomer;
    }

    // Generate a random order for a customer
    public List<IngredientTypes> generateRandomOrder()
    {
        List<IngredientTypes> order = new List<IngredientTypes>();

        order.Add(IngredientTypes.Fromage);

        foreach (IngredientTypes ingredient in System.Enum.GetValues(typeof(IngredientTypes)))
        {
            // Skip the cheese as it's always included
            if (ingredient != IngredientTypes.Fromage)
            {
                // Porcentage is defined at 33 by default
                if (Random.value < porcentage / 100f)
                {
                    order.Add(ingredient);
                }
            }
        }

        return order;
    }

    // Method to add score
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        scoreText.text = "Score : " + score;
    }

    // Method to add time
    public void AddTime(float seconds)
    {
        timeLeft += seconds;
    }

    // Register an item to be tracked
    public void RegisterItem(GameObject item)
    {
        items.Add(item);
        if (items.Count > maxItems)
        {
            GameObject oldestItem = items[0];
            items.RemoveAt(0);
            Destroy(oldestItem);
        }
    }
    
    // Unregister an item
    public void UnregisterItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

}
