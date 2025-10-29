using UnityEngine;
using UnityEngine.UI;

public class UIChooseSettings : MonoBehaviour
{
    public Slider movementTypeSlider;

    // Start is called before the first frame update
    void Start()
    {
        movementTypeSlider.value = GameSettings.movementType;
    }
    
    // Update is called once per frame
    void Update()
    {
        GameSettings.movementType = (int)movementTypeSlider.value;          
    }
}
