using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnButton : MonoBehaviour
{
    public AudioClip spawnSound;
    public GameManager gameManager;
    public Transform visualTarget;
    public Vector3 localAxis;
    public Transform spawnLocation;
    public GameObject objectSpawnedPrefab;
    public ParticleSystem spawnEffect;
    public float resetSpeed = 5f;

    private bool freeze = false;
    private AudioSource audioSource;
    private Vector3 initialLocalPosition;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<XRBaseInteractable>();
        initialLocalPosition = visualTarget.localPosition;
        
        // Instanciate interactable and add listeners for interactions with cube
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetBtn);
        interactable.selectEntered.AddListener(spawnObject);
    }

    // Method to make the button follow the poke interactor enters
    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            isFollowing = true;
            freeze = false;
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
        }
    }

    // Method to reset the button position when poke interactor exits
    public void ResetBtn(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    // Method to spawn the object when the button is pressed
    public void spawnObject(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            audioSource.PlayOneShot(spawnSound);
            GameObject newObject = Instantiate(objectSpawnedPrefab, spawnLocation.position, spawnLocation.rotation);
            Instantiate(spawnEffect, spawnLocation.position, spawnLocation.rotation);
            spawnEffect.Play();
            gameManager.RegisterItem(newObject);
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If frozen, do nothing
        if (freeze)
        {
            return;
        }

        // Update visual target position
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPosition, Time.deltaTime * resetSpeed);
        }
    }
}