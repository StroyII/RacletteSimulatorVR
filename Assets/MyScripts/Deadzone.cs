using UnityEngine;

public class Deadzone : MonoBehaviour
{
    public GameObject spawnPoint;
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = spawnPoint.transform.position;
        } else {
            Destroy(other.gameObject);
        }
    }
}
