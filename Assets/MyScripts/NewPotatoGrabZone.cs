using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;

public class NewPotatoGrabZone : MonoBehaviour
{
    public GameObject potatoPrefab;

    private XRDirectInteractor interactorInZone = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {    
            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor != null)
            {
                interactorInZone = interactor;
                interactor.selectEntered.AddListener(OnGrabInZone);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor != null && interactor == interactorInZone)
            {
                interactor.selectEntered.RemoveListener(OnGrabInZone);
                interactorInZone = null;
            }
        }
    }

    private void OnGrabInZone(SelectEnterEventArgs args)
    {
        if (interactorInZone != null && !interactorInZone.hasSelection)
        {
            GameObject newPotato = Instantiate(potatoPrefab, interactorInZone.transform.position, interactorInZone.transform.rotation);
            IXRSelectInteractable grab = newPotato.GetComponent<IXRSelectInteractable>();
            if (grab != null)
            {
                interactorInZone.StartManualInteraction(grab);
            }
        }
    }
}

