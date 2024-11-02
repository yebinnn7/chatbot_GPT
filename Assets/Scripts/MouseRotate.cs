using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    //void Start()
    //void Update()
    //void LateUpdate()

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float x_offset = Input.GetAxis("Mouse X");
            float y_offset = Input.GetAxis("Mouse Y");

            float abs_x = Mathf.Abs(x_offset);
            float abs_y = Mathf.Abs(y_offset);

            if (abs_x > 0 || abs_y > 0)
            {
                if (abs_x > abs_y)
                {
                    this.transform.RotateAround(Vector3.zero, Vector3.up, 500 * x_offset * Time.deltaTime);
                }
                else
                {
                    this.transform.RotateAround(Vector3.zero, gameObject.transform.right, -500 * y_offset * Time.deltaTime);
                }
            }
        }

        transform.position += transform.forward * 500.0f * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");
    }



}