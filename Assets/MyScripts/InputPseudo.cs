using UnityEngine.UI;
using UnityEngine;
public class InputPseudo : MonoBehaviour
{
    public InputField inputField;

    void Start()
    {
        PlayerData.pseudo = "Player";
    }

    
    void Update()
    {
        if (inputField.text.Length > 0)
        {
            PlayerData.pseudo = inputField.text;
        }
        else
        {
            PlayerData.pseudo = "Player";
        }
    }
}
