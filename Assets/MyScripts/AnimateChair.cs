using UnityEngine;

public class AnimateChair : MonoBehaviour
{
    public float startAngle = 0f;    // Angle de départ en X
    public float endAngle = 30f;     // Angle d'arrivée en X
    public float duration = 4f;      // Durée d'un aller-retour complet
    
    private float time = 0f;

    void Update()
    {
        time += Time.deltaTime / duration;
        
        // Defines the interpolation factor
        float t = (Mathf.Sin(time * Mathf.PI * 2) + 1) * 0.5f;
        
        // Defines the angle
        float currentAngle = Mathf.Lerp(startAngle, endAngle, t);
        
        // Change the rotation
        transform.localRotation = Quaternion.Euler(currentAngle, 0f, 0f);
    }
}
