using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class GiveOrderZone : MonoBehaviour
{

    public GameManager gameManager;
    public TMPro.TextMeshProUGUI customerOrderText;
    public float timeWait = 0f;
    public Transform customerSpawnPoint;
    public Transform customerFinalDest;

    private Customer customer;
    private Plate givenOrder;
    private bool isOrderOk = false;

    void Start()
    {
        StartCoroutine(WaitAndCreateFirstCustomer());
    }

    IEnumerator WaitAndCreateFirstCustomer()
    {
        yield return new WaitForSeconds(timeWait);
        setNewCustomer(gameManager.SpawnNewCustomer(customerSpawnPoint, customerFinalDest));
    }

    private void setNewCustomer(Customer cust)
    {
        customer = cust;
        customerOrderText.text = "Commande : " + string.Join(", ", customer.getOrder());
    }

    void Update()
    {
        if (givenOrder != null && isOrderOk)
        {
            gameManager.UnregisterItem(givenOrder.gameObject);
            Destroy(givenOrder.gameObject);
            Destroy(customer.gameObject);
            givenOrder = null;
            isOrderOk = false;
            gameManager.AddScore(1);
            gameManager.AddTime(10f);
            setNewCustomer(gameManager.SpawnNewCustomer(customerSpawnPoint, customerFinalDest));
        }
        else if (givenOrder != null && !isOrderOk)
        {
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
