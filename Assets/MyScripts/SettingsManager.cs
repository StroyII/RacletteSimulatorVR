using UnityEngine;
using UnityEngine.XR.Hands.Samples.Gestures.DebugTools;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class SettingsManager : MonoBehaviour
{
    public GameObject player;
    public GameObject rayInteractor;
    public GameObject teleporationActivatorObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMovement();
    }

    private void SetMovement()
    {
        if (GameSettings.movementType == 0)
        {
            player.GetComponent<TeleportationProvider>().enabled = true;
            teleporationActivatorObject.GetComponent<TeleportationActivator>().enabled = true;
            rayInteractor.SetActive(true);
        }
        else
        {
            player.GetComponent<ContinuousMoveProvider>().enabled = true;
        }
    }
}
