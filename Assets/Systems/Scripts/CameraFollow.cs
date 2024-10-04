using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player or any object the camera will follow
    public Vector3 offset; // The offset at which the camera follows
    public float smoothSpeed = 0.25f; // The speed of the smooth follow
    public float driftFactor = 2f; // How far the camera drifts ahead of the player

    private Vector3 currentVelocity;
    private Vector3 oldTargetPosition;

    void Start()
    {
        oldTargetPosition = target.transform.position;
        offset = this.transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetDirection = (target.position - oldTargetPosition).normalized;

        // Check if the target has moved before applying the drift
        Vector3 desiredPosition;
        if (Vector3.Distance(target.position, oldTargetPosition) > 0.01f)
        {
            // Apply drift when the player is moving
            desiredPosition = target.position + offset + targetDirection * driftFactor;
        }
        else
        {
            // Follow without drift if the player is stationary
            desiredPosition = target.position + offset;
        }

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
        transform.position = smoothedPosition;

        // Update the old position for the next frame
        oldTargetPosition = target.position;
    }
}


