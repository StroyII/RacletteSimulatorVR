using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    private GameManager gameManager;
    private List<IngredientTypes> order = new List<IngredientTypes>();

    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;

    public void Init(GameManager gm, List<Transform> points)
    {
        gameManager = gm;
        waypoints = points;
        order = gameManager.generateRandomOrder();
    }


    void Update()
    {
        if (waypoints.Count > 0 && waypoints != null && currentWaypointIndex < waypoints.Count)
        {
            Transform target = waypoints[currentWaypointIndex];
            if(Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                transform.LookAt(target);
            }
            else
            {
                currentWaypointIndex++;
            }
        }
    }

    public List<IngredientTypes> getOrder()
    {
        return order;
    }


    public bool compareOrder(Plate givenOrder)
    {
        List<IngredientTypes> ingredients = givenOrder.getIngredients();
        ingredients.Sort();
        order.Sort();
        return ingredients.SequenceEqual(order);
    }
}
