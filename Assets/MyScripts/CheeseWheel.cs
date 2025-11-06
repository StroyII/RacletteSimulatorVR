using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CheeseWheel : MonoBehaviour
{

    // If the cheese can be used
    public bool isReady = false;
    // How many time the cheese has been used
    public int currentUse = 0;
    
    private Renderer rend;

    // Start is called once before the first execution
    void Start()
    {
        // Set value of components
        rend = GetComponent<Renderer>();
    }

    // Set new material to the cheese
    public void SetMaterial(Material mat)
    {
        rend.material = mat;
    }
}