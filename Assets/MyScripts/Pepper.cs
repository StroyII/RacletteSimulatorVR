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
    
    // Start is called before the first frame update
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        sprayZone = sprayZoneObject.GetComponent<PepperSprayZone>();
        grab = GetComponent<XRGrabInteractable>();

        // Listener for grab inpot
        grab.activated.AddListener(SprayPepper);
    }

    // Method called when the spray is activated
    public void SprayPepper(ActivateEventArgs args)
    {
        // Apply pepper to all plates in the spray zone
        sprayZone.applyPepper();
        audioSource.PlayOneShot(spraySound);
        pepperParticles.Play();

    }

}
