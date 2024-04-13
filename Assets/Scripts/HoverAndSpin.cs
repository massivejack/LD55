using UnityEngine;

public class HoverAndSpin : MonoBehaviour
{
    // Exposed parameters
    public float hoverRange = 0.5f; // Range of motion for hovering
    public float hoverFrequency = 1.0f; // Frequency of hovering
    public float hoverSpeed = 1.0f; // Speed of hovering
    public float spinSpeed = 30.0f; // Speed of spinning

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate vertical hover movement
        float hoverOffset = Mathf.Sin(Time.time * hoverFrequency) * hoverRange;
        Vector3 hoverPosition = startPosition + Vector3.up * hoverOffset;

        // Calculate spin
        float spinAngle = spinSpeed * Time.deltaTime;
        Quaternion spinRotation = Quaternion.Euler(0f, spinAngle, 0f);

        // Apply hover and spin
        transform.position = hoverPosition;
        transform.Rotate(Vector3.up, spinAngle);

        // Smooth rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * spinRotation, Time.deltaTime * smoothness);
    }
}
