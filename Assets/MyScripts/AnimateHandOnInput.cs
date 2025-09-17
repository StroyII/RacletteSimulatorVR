using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator anim;

    
    void Update()
    {
        float triggerPinch = pinchAnimationAction.action.ReadValue<float>();
        float triggerGrip = gripAnimationAction.action.ReadValue<float>();

        anim.SetFloat("Grip", triggerGrip);
        anim.SetFloat("Trigger", triggerPinch);
    }
}
