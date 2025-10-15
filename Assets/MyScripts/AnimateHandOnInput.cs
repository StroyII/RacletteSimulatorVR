using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    // Different Input Action for the animations to start
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    // The animator of the hand
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        // Get the value of the input actions
        float triggerPinch = pinchAnimationAction.action.ReadValue<float>();
        float triggerGrip = gripAnimationAction.action.ReadValue<float>();

        // Set the animations with the values
        anim.SetFloat("Grip", triggerGrip);
        anim.SetFloat("Trigger", triggerPinch);
    }
}
