using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class GiveOrderZone : MonoBehaviour
{
    public AudioClip correctOrderSound;
    public AudioClip wrongOrderSound;

    private AudioSource audioSource;
    public GameManager gameManager;
    public TMPro.TextMeshProUGUI customerOrderText;
    public float timeWait = 0f;
    public Transform customerSpawnPoint;
    public List<Transform> CustomerWaypoints = new List<Transform>();

    private Customer customer;
    private Plate givenOrder;
    private bool isOrderOk = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndCreateFirstCustomer());
        audioSource = GetComponent<AudioSource>();
    }

    // Coroutine to wait before spawning the first customer to avoid instant overlap
    IEnumerator WaitAndCreateFirstCustomer()
    {
        yield return new WaitForSeconds(timeWait);
        setNewCustomer(gameManager.SpawnNewCustomer(customerSpawnPoint, CustomerWaypoints));
    }

    // Set a new customer as the active one
    private void setNewCustomer(Customer cust)
    {
        customer = cust;
        // Set new customer order text
        customerOrderText.text = "Commande : " + string.Join(", ", customer.getOrder());
    }

    // Update is called once per frame
    void Update()
    {
        // Check the given order when available
        if (givenOrder != null && isOrderOk)
        {
            // When correct order given
            audioSource.PlayOneShot(correctOrderSound);

            // Unregister and destroy the given plate and customer
            gameManager.UnregisterItem(givenOrder.gameObject);
            Destroy(givenOrder.gameObject);
            Destroy(customer.gameObject);

            givenOrder = null;
            isOrderOk = false;

            // Add new score and time, then spawn a new customer
            gameManager.AddScore(1);
            gameManager.AddTime(10f);
            setNewCustomer(gameManager.SpawnNewCustomer(customerSpawnPoint, CustomerWaypoints));
        }
        else if (givenOrder != null && !isOrderOk)
        {
            // When wrong order given
            audioSource.PlayOneShot(wrongOrderSound);
            givenOrder = null;
            isOrderOk = false;
        }   
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        // Check for plate tag
        if(other.CompareTag("Plate"))
        {
            // Get the given plate
            givenOrder = other.GetComponent<Plate>();
            if(givenOrder != null)
            {
                // Compare the given order with the customer's order
                isOrderOk = customer.compareOrder(givenOrder);
            }
        }
    }
}