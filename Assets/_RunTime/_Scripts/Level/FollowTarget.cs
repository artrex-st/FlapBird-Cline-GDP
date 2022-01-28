using UnityEngine;

[ExecuteAlways]
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offSet;
    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x + _offSet, transform.position.y, transform.position.z);
    }
}
