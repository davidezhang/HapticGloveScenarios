using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the GameObject around its local Y-axis (or any axis you want)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
