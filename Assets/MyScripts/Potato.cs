using UnityEngine;

public class Potato : MonoBehaviour
{
    public Material cookedMaterial;
    public bool isCooked = false;
    private Renderer rend;

    // Start is called before the first frame update
    void Start() 
    {
        rend = GetComponent<Renderer>();
    }

    // Method to cook the potato
    public void Cook()
    {
        // Change the material and state
        isCooked = true;
        if (rend != null && cookedMaterial != null)
        {
            rend.material = cookedMaterial;
        }
    }
}
