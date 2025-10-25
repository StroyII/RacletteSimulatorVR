using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plate : MonoBehaviour
{
    // Different models on the plate depending on ingredients
    public GameObject racletteFondue;
    public GameObject potatoPlate;
    public GameObject religeusePlate;
    public GameObject poivrePlate;

    private List<IngredientTypes> ingredients = new List<IngredientTypes>();
    private Renderer rend;
    private Rigidbody rig;
    private XRGrabInteractable grab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set value of components
        rend = GetComponent<Renderer>();
        rig = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        // Disable all models at start
        racletteFondue.SetActive(false);
        potatoPlate.SetActive(false);
        religeusePlate.SetActive(false);
        poivrePlate.SetActive(false);
    }
    
    // Function to add ingredient to the plate
    public bool AddIngredient(IngredientTypes ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
            updateModel();
            return true;
        }
        else
        {
            return false;
        }
    }

    // Function to update the plate model based on ingredients added
    public void updateModel()
    {
        if (ingredients.Contains(IngredientTypes.Fromage) && rend != null)
        {
            racletteFondue.SetActive(true);
        }

        if (ingredients.Contains(IngredientTypes.Patate) && rend != null)
        {
            potatoPlate.SetActive(true);
        }

        if (ingredients.Contains(IngredientTypes.Religieuse) && rend != null)
        {
            religeusePlate.SetActive(true);
        }

        if (ingredients.Contains(IngredientTypes.Poivre) && rend != null)
        {
            poivrePlate.SetActive(true);
            racletteFondue.SetActive(false);
        }
    }

    // Get list of ingredients on the plate
    public List<IngredientTypes> getIngredients()
    {
        return ingredients;
    }
    
    // Check if plate has a specific ingredient
    public bool hasIngredient(IngredientTypes ingredient)
    {
        return ingredients.Contains(ingredient);
    }
}
