using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    private TMPro.TextMeshProUGUI commandeText;
    private GameManager gameManager;
    private Transform finalDest;
    private List<IngredientTypes> order = new List<IngredientTypes>();

    void Update()
    {
        if (finalDest != null) {       
            if (Vector3.Distance(transform.position, finalDest.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, finalDest.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Init(GameManager gm, TMPro.TextMeshProUGUI text, Transform dest)
    {
        gameManager = gm;
        commandeText = text;
        finalDest = dest;
        generateOrder();
    }

    private void generateOrder()
    {
        order = gameManager.generateRandomOrder();
        commandeText.text = "Commande : " + string.Join(", ", order);
        Debug.Log("Nouvelle commande : " + string.Join(", ", order));
    }

    public bool compareOrder(Plate givenOrder)
    {
        List<IngredientTypes> ingredients = givenOrder.getIngredients();
        ingredients.Sort();
        order.Sort();
        return ingredients.SequenceEqual(order);
    }
}
