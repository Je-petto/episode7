using System;
using UnityEngine;

public class Lerp1 : MonoBehaviour
{
    public Transform A;
    public Transform B;
    
    [Range(0f, 1f)] public float t;
    
    public bool isSlerp;

    void Update()
    {
        if(isSlerp == false)
            MoveLerp();
        else
            MoveSlerp();
    }
    void MoveSlerp()
    {
        t = Mathf.PingPong(Time.time, 1f);
        transform.position = Vector3.Slerp(A.position, B.position, t);
    }
    void MoveLerp()
    {
        t = Mathf.PingPong(Time.time, 1f);
        transform.position = Vector3.Lerp(A.position, B.position, t);

    }
}
