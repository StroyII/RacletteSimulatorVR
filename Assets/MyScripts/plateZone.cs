using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class plateZone : MonoBehaviour
{
    public Plate currentPlate;
    private Renderer rend;
    private XRSocketInteractor socket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set component values
        rend = GetComponent<Renderer>();
        socket = GetComponent<XRSocketInteractor>();

        // Add listeners for when a plate is placed or removed
        socket.selectEntered.AddListener(OnPlatePlaced);
        socket.selectExited.AddListener(OnPlateRemoved);
    }   

    // When plate is placed in the zone
    private void OnPlatePlaced(SelectEnterEventArgs args)
    {
        Plate newPlate = args.interactableObject.transform.GetComponent<Plate>();
        if (newPlate != null)
        {
            currentPlate = newPlate;
            rend.enabled = false;
        }
    }

    // When plate is removed from the zone
    private void OnPlateRemoved(SelectExitEventArgs args)
    {
        currentPlate = null;
        rend.enabled = true;
    }

}
