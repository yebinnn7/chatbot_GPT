using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //delta Time
        //ť�긦 ȸ�����Ѿ� ��
        this.transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
