using System.Collections.Generic;
using UnityEngine;

public enum IngredientTypes { Fromage, Religieuse, Patate, Poivre};
public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float porcentage = 33f;
    public float timeLeft = 100f;
    public int maxItems = 15;
    public GameObject player;
    public Transform endTpPoint;
    public GameObject[] customerPrefabs;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timeText;

    private bool isGameOver = false;

    private List<GameObject> items = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = "Temps restant : " + Mathf.Ceil(timeLeft).ToString() + "s";
        if (timeLeft <= 0)
        {
            isGameOver = true;
            timeLeft = 0;
            player.transform.position = endTpPoint.position;
        }


    }

    public Customer SpawnNewCustomer(Transform customerSpawnPoint, Transform custommerFinalDest)
    {
        int index = Random.Range(0, customerPrefabs.Length);
        GameObject customerObj = Instantiate(customerPrefabs[index], customerSpawnPoint.position, customerSpawnPoint.rotation);
        Customer newCustomer = customerObj.GetComponent<Customer>();
        newCustomer.Init(this, custommerFinalDest);
        return newCustomer;
    }

    public List<IngredientTypes> generateRandomOrder()
    {
        List<IngredientTypes> order = new List<IngredientTypes>();

        order.Add(IngredientTypes.Fromage);

        foreach (IngredientTypes ingredient in System.Enum.GetValues(typeof(IngredientTypes)))
        {
            if (ingredient != IngredientTypes.Fromage)
            {
                if (Random.value < porcentage / 100f)
                {
                    order.Add(ingredient);
                }
            }
        }

        return order;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        scoreText.text = "Score : " + score;
    }

    public void AddTime(float seconds)
    {
        timeLeft += seconds;
    }

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
    
    public void UnregisterItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }

}
