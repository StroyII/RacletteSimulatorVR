using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnButton : MonoBehaviour
{
    public GameManager gameManager;
    public Transform visualTarget;
    public Vector3 localAxis;
    public Transform spawnLocation;
    public GameObject objectSpawnedPrefab;
    public ParticleSystem spawnEffect;
    public float resetSpeed = 5f;
    private bool freeze = false;

    private Vector3 initialLocalPosition;
    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    void Start()
    {
        initialLocalPosition = visualTarget.localPosition;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetBtn);
        interactable.selectEntered.AddListener(spawnObject);
    }

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

    public void ResetBtn(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void spawnObject(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            GameObject newObject = Instantiate(objectSpawnedPrefab, spawnLocation.position, spawnLocation.rotation);
            Instantiate(spawnEffect, spawnLocation.position, spawnLocation.rotation);
            spawnEffect.Play();
            gameManager.RegisterItem(newObject);
            freeze = true;
        }
    }

    void Update()
    {
        if (freeze)
        {
            return;
        }

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
