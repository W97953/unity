using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.12f;
    private Vector3 velocity = Vector3.zero;
    public Vector2 minPos, maxPos; // optional bounds

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
        newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);

        transform.position = newPos;
    }
}
