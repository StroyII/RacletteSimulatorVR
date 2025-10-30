using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Pepper : MonoBehaviour
{
    public AudioClip spraySound;
    public GameObject sprayZoneObject;
    public ParticleSystem pepperParticles;
    PepperSprayZone sprayZone = null;
    private XRGrabInteractable grab;
    private AudioSource audioSource;
    
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        sprayZone = sprayZoneObject.GetComponent<PepperSprayZone>();
        grab = GetComponent<XRGrabInteractable>();
        grab.activated.AddListener(SprayPepper);
    }

    public void SprayPepper(ActivateEventArgs args)
    {
        sprayZone.applyPepper();
        audioSource.PlayOneShot(spraySound);
        pepperParticles.Play();
        
    }

}
