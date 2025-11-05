using UnityEngine;

public class Deadzone : MonoBehaviour
{
    public GameObject spawnPoint;
    
    // When an object enters the deadzone
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            // Reset player position to spawn point
            other.transform.position = spawnPoint.transform.position;
        } else {
            // Destroy other objects
            Destroy(other.gameObject);
        }
    }
}
