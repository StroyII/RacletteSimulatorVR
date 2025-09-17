using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Plate : MonoBehaviour
{
    public GameObject racletteFondue;
    public GameObject potatoPlate;
    public GameObject religeusePlate;
    public GameObject poivrePlate;

    private Renderer rend;
    private List<IngredientTypes> ingredients = new List<IngredientTypes>();
    private Rigidbody rig;
    private XRGrabInteractable grab;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rig = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);

        racletteFondue.SetActive(false);
        potatoPlate.SetActive(false);
        religeusePlate.SetActive(false);
        poivrePlate.SetActive(false);
    }
    public void AddIngredient(IngredientTypes ingredient)
    {
        if (!ingredients.Contains(ingredient))
        {
            ingredients.Add(ingredient);
            updateModel();
        }
    }

    public void updateModel()
    {
        if (ingredients.Contains(IngredientTypes.Fromage) && rend != null)
        {
            Debug.Log("FROMAGE");

            racletteFondue.SetActive(true);
        }

        if (ingredients.Contains(IngredientTypes.Patate) && rend != null)
        {
            Debug.Log("PATATE");

            potatoPlate.SetActive(true);
        }

        if (ingredients.Contains(IngredientTypes.Religieuse) && rend != null)
        {
            Debug.Log("RELIGIEUSE");

            religeusePlate.SetActive(true);
        }

        if (ingredients.Contains(IngredientTypes.Poivre) && rend != null)
        {
            Debug.Log("POIVRE");

            poivrePlate.SetActive(true);
            racletteFondue.SetActive(false);
        }
    }

    public void ClipToZone(Transform snapPoint)
    {
        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;




    }

    public void OnGrabbed(SelectEnterEventArgs args)
    {
        rig.isKinematic = false;
    }

    public List<IngredientTypes> getIngredients()
    {
        return ingredients;
    }
    
    public bool hasIngredient(IngredientTypes ingredient)
    {
        return ingredients.Contains(ingredient);
    }
}
