using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class RacletteFurnacae : MonoBehaviour
{
    public float cookTime = 5f;
    public Material cookedMat;
    public int maxUses = 5;

    public AudioSource audioSourceCooking;
    public AudioSource audioSourceReady;

    public CheeseWheel currentCheese;
    private XRSocketInteractor socket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set component values
        socket = GetComponent<XRSocketInteractor>();

        // Add listeners for when a cheese is placed or removed
        socket.selectEntered.AddListener(OnCheesePlaced);
        socket.selectExited.AddListener(OnCheeseRemoved);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the cheese has reached max uses
        if (currentCheese != null && currentCheese.currentUse >= maxUses)
        {
            Destroy(currentCheese.gameObject);
            currentCheese = null;
        }    
    }

    // When cheese is placed in the furnace
    private void OnCheesePlaced(SelectEnterEventArgs args)
    {
        CheeseWheel newCheese = args.interactableObject.transform.GetComponent<CheeseWheel>();

        if (newCheese != null && currentCheese == null)
        {
            currentCheese = newCheese;
            audioSourceCooking.Play();
            StartCoroutine(CookCheese(currentCheese));
        }
    }

    // When cheese is removed from the furnace
    private void OnCheeseRemoved(SelectExitEventArgs args)
    {
        if (currentCheese != null && args.interactableObject.transform == currentCheese.transform)
        {
            StopAllCoroutines(); // stop la cuisson si on retire le fromage avant la fin
            audioSourceCooking.Stop();
            currentCheese = null;
        }
    }

    // Coroutine to cook the cheese over time
    IEnumerator CookCheese(CheeseWheel cheese)
    {
        yield return new WaitForSeconds(cookTime);

        if (cheese != null && currentCheese != null && cheese == currentCheese)
        {
            cheese.isReady = true;
            cheese.SetMaterial(cookedMat);
        }

        audioSourceCooking.Stop();
        
        audioSourceReady.Play();
    }
}
