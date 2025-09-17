using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Pepper : MonoBehaviour
{
    public GameObject sprayZoneObject;
    public ParticleSystem pepperParticles;
    PepperSprayZone sprayZone = null;
    private XRGrabInteractable grab;
    
    void Start()
    {   
        sprayZone = sprayZoneObject.GetComponent<PepperSprayZone>();
        grab = GetComponent<XRGrabInteractable>();
        grab.activated.AddListener(SprayPepper);
    }

    public void SprayPepper(ActivateEventArgs args)
    {
        sprayZone.applyPepper();


        pepperParticles.Play();
        
    }

}
