using System.Collections.Generic;
using UnityEngine;

public enum IngredientTypes { Fromage, Religieuse, Patate, Poivre};
public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float porcentage = 33f;
    public float timeLeft = 100f;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "Temps restant : " + Mathf.Ceil(timeLeft).ToString() + "s";
        if (timeLeft <= 0)
        {
            Debug.Log("Temps écoulé ! Fin de la partie.");
            timeLeft = 0;
        }
    }

    public List<IngredientTypes> generateRandomOrder()
    {
        List<IngredientTypes> order = new List<IngredientTypes>();

        // Contient toujours le fromage
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

}
