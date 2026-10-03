using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class MonsterLookAt : MonoBehaviour
{
    private Transform target;
    private Vector3 targetPosition;

    void Start()
    {
        target = Camera.main.transform;
    }

    void Update()
    {
        if (target != null)
        {
            targetPosition = new Vector3(
                target.position.x,
                transform.position.y,
                target.position.z
            );

            transform.LookAt(targetPosition);
        }
    }
}