using System.Security.Cryptography;
using UnityEngine;

public class GiveOrderZone : MonoBehaviour
{

    public Customer customer;
    public GameManager gameManager;

    private Plate givenOrder;


    private bool isOrderOk = false;
    void Start()
    {

    }

    void Update()
    {
        if (givenOrder != null && isOrderOk)
        {
            Debug.Log("Commande correcte !");
            Destroy(givenOrder.gameObject);
            givenOrder = null;
            isOrderOk = false;
            gameManager.AddScore(1);
            gameManager.AddTime(10f);
        }
        else if (givenOrder != null && !isOrderOk)
        {
            Debug.Log("Commande incorrecte !");
            givenOrder = null;
            isOrderOk = false;
        }   
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Plate"))
        {
            givenOrder = other.GetComponent<Plate>();
            if(givenOrder != null)
            {
                isOrderOk = customer.compareOrder(givenOrder);
            }
        }
    }
}
