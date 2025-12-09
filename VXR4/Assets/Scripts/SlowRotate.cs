using UnityEngine;

public class rotating : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 90f;

    void Update()
    {
        // Rotate around Y axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
