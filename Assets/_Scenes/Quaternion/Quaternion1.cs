using UnityEngine;
using CustomInspector;

//Degree (일반 각도표현) , Radian(수학, 물리 각도표현 단위)
//0도 ~ 360도 , 1 pie(π) = 180도, 2π = 360도
//1 Radian = 57.3도
//3.14 Radian = 180도

// 이동 => Vector3로 처리
// 회전 => Quarternion으로 처리
// 크기 => Vector3로 처리

public class Quaternion1 : MonoBehaviour
{
    //Quarentation 용 속성
    [Space(10), HorizontalLine("Quarentation 속성"), HideField] public bool _l0;
    public float pitch;
    public float yaw;
    public float roll;
    [SerializeField] float rotatespeed = 10.0f;
    [Space(10), HorizontalLine("타겟 속성"), HideField] public bool _l1;

    public GameObject TargetObj;
    
    void Update()
    {
        // yaw = Input.GetAxis("Horizontal")* Time.deltaTime * rotatespeed;
        // pitch = Input.GetAxis("Vertical")* Time.deltaTime * rotatespeed;

        // Quaternion rotation = Quaternion.Euler(pitch, yaw, roll);

        // transform.rotation = transform.rotation * rotation;
        // transform.rotation *= rotation;

        LookRotationSmooth();
    }

    [Button("QuaternionElement", label = "쿼터니온 기본 속성"), HideField] public bool _b0;
    void QuaternionElement()
    {
        Debug.Log($"쿼터니온 identity = {Quaternion.identity}"); //쿼터니온 identity = (0.00000, 0.00000, 0.00000, 1.00000)
    }

    [Button("LookRotation"), HideField] public bool _b1;
    void LookRotation()
    {
        // Vector 뺄셈 : 방향 (Direction)

        //Forward벡터 : z+ : 전방 , 바라보는 방향
        //UP          : y+ :     , 위로 바라보는 방향
        //Right       : x+ :     , 오른쪽으로 바라보는 방향

        // A - B : B가 A를 바라보게 하는 값 (값 : Vector3)
        // B - A : A가 B를 바라보게 하는 값 (값 : Vector3)
        
        Vector3 lookdierction = TargetObj.transform.position - transform.position;

        //transform.rotation = Quaternion.LookRotation(lookdierction);
        transform.rotation = Quaternion.LookRotation(lookdierction);
    }

    void LookRotationSmooth()
    {
        // 타겟 위치 - 내 위치 => 내가 타겟을 바라보는 방향 Vector3
        Vector3 lookdir = TargetObj.transform.position - transform.position;

        // LookRotation : 내 캐릭터가 타겟을 바라보도록 Quaternion을 연산해준다
        Quaternion lookrot = Quaternion.LookRotation(lookdir);
        
        // RotateTowards : 기준 회전 quaternion에서 목표 회전 quaternion으로 자연스럽고 부드럽게 바라보도록 Quaternion을 연산해준다
        // RotateTowards( 기준 회전 quaternion, 목표 회전 quaternion, 단위 회전 속도 *Time.deltaTime(일정한 속도 유지 가능))
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookrot, rotatespeed * Time.deltaTime);

        // 타겟 위치 상관없이 y축으로만 움직이기
        // transform = object 자신의 transform
        // transform.rotation = 회전정보(타입 = Quaternion)
        // transform.rotation.eulerAngles 회원정보 => 오일러 앵글로 변환한 값
        // (Quaternion -> Vector3)
        float yAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0.0f, yAngle ,0.0f);
    }

}
