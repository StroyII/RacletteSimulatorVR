using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class CheeseWheel : MonoBehaviour
{
    public bool isReady = false;

    public int currentUse = 0;

    private Rigidbody rig;
    private XRGrabInteractable grab;
    private Renderer rend;


    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        rig = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    public void ClipToFurnace(Transform snapPoint)
    {
        grab.enabled = false;

        transform.position = snapPoint.position;
        transform.rotation = snapPoint.rotation;
        transform.SetParent(snapPoint);

        rig.isKinematic = true;
        rig.useGravity = false;

    }

    public void SetMaterial(Material mat)
    {
        rend.material = mat;
    }
}
