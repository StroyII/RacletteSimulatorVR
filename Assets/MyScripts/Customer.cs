using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Customer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI commandeText;
    public GameManager gameManager;
    private List<IngredientTypes> order = new List<IngredientTypes>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generateOrder();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void generateOrder()
    {
        order = gameManager.generateRandomOrder();
        commandeText.text = "Commande : " + string.Join(", ", order);
    }

    public bool compareOrder(Plate givenOrder)
    {
        List<IngredientTypes> ingredients = givenOrder.getIngredients();
        ingredients.Sort();
        order.Sort();
        return ingredients.SequenceEqual(order);
    }
}
