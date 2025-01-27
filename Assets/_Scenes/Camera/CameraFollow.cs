using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float movespeed = 1.0f;
    [SerializeField] Vector3 camoffset;// 타겟 위치에서 얼마만큼 떨어트릴 것이냐

    public bool islerp;

    void Update()
    {
        //if(islerp)
            MoveLerp();
        //else
        //MoveTowards();
    }

    // Camera inspector -> projection ->field of View로 배경과 카메라 사이의 원근감 조절 가능
    // MoveLerp : A에서 B로 간다 (등가속운동)
    void MoveLerp()
    {
        // 사전 카메라 움직임 제어
        Vector3 targetPos = new Vector3(camoffset.x, camoffset.y, target.position.z + camoffset.z);
        
        transform.position = Vector3.Lerp(transform.position, targetPos, movespeed * Time.deltaTime);
        
        // 사후 카메라 움직임 제어
        //transform.position = new Vector3(camoffset.x, camoffset.y, transform.position.z);
    }

    // MoveTowards : Target 위치로 간다 (등속운동)
    // void MoveTowards()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, target.position + camoffset, movespeed * Time.deltaTime);

    // }
    
}
