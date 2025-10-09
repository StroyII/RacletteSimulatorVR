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
            Transform point = waypoints[currentWaypointIndex];
            Vector3 offset = new Vector3(0f,0f,0f);
            Vector3 target = point.position + offset;
            float distance = Vector3.Distance(transform.position, target);
            if (distance > 0.1f)
            {
                Vector3 direction = (target - transform.position).normalized;
                float step = moveSpeed * Time.deltaTime;
                if (step >= distance)
                {
                    transform.position = target;
                    currentWaypointIndex++;
                }
                else
                {
                    transform.position += direction * step;
                    transform.LookAt(target);
                }
            }
            else
            {
                transform.position = target;
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
