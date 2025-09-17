using UnityEngine;

public class Deadzone : MonoBehaviour
{
    public GameObject spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = spawnPoint.transform.position;
        } else {
            Destroy(other.gameObject);
        }
    }
}
