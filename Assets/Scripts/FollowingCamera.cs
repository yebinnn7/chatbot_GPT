using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject TargetObject;

    float Distance = 10f;
    float Height = 4f;

    Vector3 target_v3 = new Vector3();

    void LateUpdate()
    {
        Vector3 object_pos = TargetObject.transform.position;
        target_v3.x = object_pos.x;
        target_v3.y = object_pos.y + Height;
        target_v3.z = object_pos.z - Distance;

        this.transform.position = target_v3;
    }

} 