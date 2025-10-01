using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
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

    public List<IngredientTypes> getOrder()
    {
        return order;
    }

    public void Init(GameManager gm, Transform dest)
    {
        gameManager = gm;
        finalDest = dest;
        order = gameManager.generateRandomOrder();
    }

    public bool compareOrder(Plate givenOrder)
    {
        List<IngredientTypes> ingredients = givenOrder.getIngredients();
        ingredients.Sort();
        order.Sort();
        return ingredients.SequenceEqual(order);
    }
}
