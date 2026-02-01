using UnityEngine;

public class BGFollow : MonoBehaviour
{
    [SerializeField] Transform targetCamera;
    [SerializeField] float z = 0f;

    void LateUpdate()
    {
        if (!targetCamera) return;
        var p = targetCamera.position;
        transform.position = new Vector3(p.x, p.y, z);
    }
}
