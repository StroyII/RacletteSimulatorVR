using UnityEngine;

public class UIButtonOpenTab : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject panelToClose;

    public void OpenTab()
    {
        panelToOpen.SetActive(true);
        panelToClose.SetActive(false);
    }
}
