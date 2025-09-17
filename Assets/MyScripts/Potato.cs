using UnityEngine;

public class Potato : MonoBehaviour
{
    public Material cookedMaterial;
    public bool isCooked = false;
    private Renderer rend;

    void Start() 
    {
        rend = GetComponent<Renderer>();
    }

    public void Cook()
    {
        isCooked = true;
        if (rend != null && cookedMaterial != null)
        {
            rend.material = cookedMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
