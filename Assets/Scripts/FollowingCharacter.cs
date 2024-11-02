using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCharacter : MonoBehaviour
{
    public Transform Target;

    private float SmoothTime = 0.3f;
    private Vector3 Offset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Offset = this.transform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + (Target.transform.forward * Offset.z) + (Vector3.up * Offset.y);

        this.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        Vector3 lookAtPosition = Target.position + new Vector3(0, 2, 0);

        this.transform.LookAt(lookAtPosition);
    }

}