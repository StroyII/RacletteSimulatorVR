using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;
    public HandData leftHandPose;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;


    void Start()
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(SetUpPose);
        grab.selectExited.AddListener(UnSetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetUpPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor)
        {
            HandData handdata = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handdata.animator.enabled = false;

            if (handdata.handType == HandData.HandModelType.Right)
            {
                SetHandDataValue(handdata, rightHandPose);
            }
            else
            {
                SetHandDataValue(handdata, leftHandPose);
            }


            SetHandData(handdata, finalHandPosition, finalHandRotation, finalFingerRotations);
        }
    }

    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor)
        {
            HandData handdata = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handdata.animator.enabled = true;

            SetHandData(handdata, startingHandPosition, startingHandRotation, startingFingerRotations);
        }
    }

    public void SetHandDataValue(HandData h1, HandData h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h2.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newFingersRotations)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newFingersRotations.Length; i++)
        {
            h.fingerBones[i].localRotation = newFingersRotations[i];
        }
    }

#if UNITY_EDITOR
    [MenuItem("Tools/Mirror Right Hand Pose")]
    public static void MirrorRightPose()
    {
        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
    }

#endif
    public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
    {
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
        mirroredPosition.x *= -1;

        Quaternion mirroredRotation = poseUsedToMirror.root.localRotation;
        mirroredRotation.y *= -1;
        mirroredRotation.z *= -1;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.localRotation = mirroredRotation;


        for (int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;

        }
    }
}
