using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;


public class plateZone : MonoBehaviour
{
    public Plate currentPlate;
    private Renderer rend;

    private XRSocketInteractor socket;

    void Start()
    {
        rend = GetComponent<Renderer>();
        socket = GetComponent<XRSocketInteractor>();

        socket.selectEntered.AddListener(OnPlatePlaced);
        socket.selectExited.AddListener(OnPlateRemoved);
    }   

    
    private void OnPlatePlaced(SelectEnterEventArgs args)
    {
        Plate newPlate = args.interactableObject.transform.GetComponent<Plate>();
        if (newPlate != null)
        {
            currentPlate = newPlate;
            rend.enabled = false;
        }
    }

    private void OnPlateRemoved(SelectExitEventArgs args)
    {
        currentPlate = null;
        rend.enabled = true;
    }

}
