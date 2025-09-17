using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class plateZone : MonoBehaviour
{
    public Plate currentPlate;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate"))
        {

            Plate newPlate = other.GetComponent<Plate>();
            if (currentPlate == null)
            {
                XRGrabInteractable plateGrab = newPlate.GetComponent<XRGrabInteractable>();
                if (!plateGrab.isSelected)
                {    
                    currentPlate = newPlate;
                    currentPlate.ClipToZone(transform);
                    rend.enabled = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plate"))
        {
            currentPlate = null;
            rend.enabled = true;         
        }
    }
}
