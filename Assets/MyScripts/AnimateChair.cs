using UnityEngine;

public class AnimateChair : MonoBehaviour
{
    public float startAngle = 0f;   
    public float endAngle = 30f;     
    public float duration = 4f;  
    
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
