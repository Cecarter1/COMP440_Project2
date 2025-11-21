using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target; // The BaseShapeManager GameObject or its parent
    public float smoothSpeed = 0.125f; // Controls the "lag smoothing"
    public Vector3 offset = new Vector3(0, 0, -10); // Standard 2D camera Z offset

    void LateUpdate()
    {
        if (target == null) return;

        // Determine the target position based on the Base Shape's position
        Vector3 targetPosition = target.position + offset;

        // Apply Vector3.Lerp for smooth, lag-like movement (3c)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Ensure the camera rotation matches the target's rotation for full tracking
        // (This makes the camera rotate with the base shape, which is essential for the Super Hexagon aesthetic)
        Quaternion targetRotation = target.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
    }
}
