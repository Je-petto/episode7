using System;
using UnityEngine;

public class Lerp1 : MonoBehaviour
{
    public Transform A;
    public Transform B;
    
    [Range(0f, 1f)] public float t;
    
    public bool isSlerp;

    // Lerp : Linear Interpolitan
    
    void Update()
    {
        if(isSlerp == false)
            MoveLerp();
        else
            MoveSlerp();
    }
    void MoveLerp()
    {
        t = Mathf.PingPong(Time.time, 1f);
        transform.position = Vector3.Lerp(A.position, B.position, t);

    }
    //곡선의 형태로 돌아감
    void MoveSlerp()
    {
        t = Mathf.PingPong(Time.time, 1f);
        transform.position = Vector3.Slerp(A.position, B.position, t);

        //Qaternion.RotateTowards()
    }
}
