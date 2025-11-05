using UnityEngine;

public class UIButtonOpenTab : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject panelToClose;

    // Method to open specified tab and close another
    public void OpenTab()
    {
        panelToOpen.SetActive(true);
        panelToClose.SetActive(false);
    }
}
