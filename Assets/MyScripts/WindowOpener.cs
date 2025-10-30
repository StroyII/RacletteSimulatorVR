using UnityEngine;

public class WindowOpener : MonoBehaviour
{
    public GameObject windowToOpen;
    public GameObject objectReference;

    // Update is called once per frame
    void Update()
    {
        windowToOpen.SetActive(objectReference.activeInHierarchy);
    }
}
