using System.Security.Cryptography;
using UnityEngine;

public class GiveOrderZone : MonoBehaviour
{

    public GameManager gameManager;

    private Customer customer;
    private Plate givenOrder;
    private bool isOrderOk = false;


    void Update()
    {
        if (givenOrder != null && isOrderOk)
        {
            Debug.Log("Commande correcte !");
            gameManager.UnregisterItem(givenOrder.gameObject);
            Destroy(givenOrder.gameObject);
            givenOrder = null;
            isOrderOk = false;
            gameManager.AddScore(1);
            gameManager.AddTime(10f);
            gameManager.SpawnNewCustomer();
        }
        else if (givenOrder != null && !isOrderOk)
        {
            Debug.Log("Commande incorrecte !");
            givenOrder = null;
            isOrderOk = false;
        }   
    }

    public void SetCustomer(Customer cust)
    {
        customer = cust;
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
