using UnityEngine;

public class RacletteKnife : MonoBehaviour
{
    public bool isScraping = false;
    public float scrapeSpeedThreshold = 0.3f;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        if (speed > scrapeSpeedThreshold)
        {
            isScraping = true;
        }
        else
        {
            isScraping = false;
        }
    }
}
