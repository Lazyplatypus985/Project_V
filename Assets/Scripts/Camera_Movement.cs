using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera_Movement : MonoBehaviour
{
    public Transform target;

    [Header("Position")]
    public Vector3 offset;

    [Header("Deadzone")]
    public Vector2 deadzoneSize = new Vector2(2f, 1.5f);

    [Header("Smoothing")]
    public float smoothTime = 0.15f;

    [Header("Camera Bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastTargetPosition;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position based on target
        Vector3 desiredPosition = target.position + offset;

        Vector3 cameraPos = transform.position;
        Vector3 delta = desiredPosition - cameraPos;

        // Deadzone logic
        if (Mathf.Abs(delta.x) > deadzoneSize.x)
            cameraPos.x = desiredPosition.x - deadzoneSize.x * Mathf.Sign(delta.x);

        if (Mathf.Abs(delta.y) > deadzoneSize.y)
            cameraPos.y = desiredPosition.y - deadzoneSize.y * Mathf.Sign(delta.y);

        // Orthographic camera size
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Clamp camera within bounds
        cameraPos.x = Mathf.Clamp(
            cameraPos.x,
            minBounds.x + camWidth,
            maxBounds.x - camWidth
        );

        cameraPos.y = Mathf.Clamp(
            cameraPos.y,
            minBounds.y + camHeight,
            maxBounds.y - camHeight
        );

        // Smooth movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            cameraPos,
            ref velocity,
            smoothTime
        );

        lastTargetPosition = target.position;
    }

    // Gizmo visualization
    void OnDrawGizmos()
    {
        // Deadzone
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, deadzoneSize * 2f);

        // Camera bounds
        Gizmos.color = Color.yellow;
        Vector3 center = (minBounds + maxBounds) / 2f;
        Vector3 size = maxBounds - minBounds;
        Gizmos.DrawWireCube(center, size);
    }
}
