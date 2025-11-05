using UnityEngine.UI;
using UnityEngine;
public class InputPseudo : MonoBehaviour
{
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.pseudo = "Player";
    }

    // Update is called once per frame    
    void Update()
    {
        if (inputField.text.Length > 0)
        {
            // Set the player pseudo from input field
            PlayerData.pseudo = inputField.text;
        }
        else
        {
            // Default pseudo if input is empty
            PlayerData.pseudo = "Player";
        }
    }
}
