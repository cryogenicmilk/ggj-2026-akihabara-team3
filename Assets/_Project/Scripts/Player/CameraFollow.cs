using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    public Vector3 _offset;

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
