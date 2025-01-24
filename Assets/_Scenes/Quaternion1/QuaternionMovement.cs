using UnityEngine;

public class QuaternionMovement : MonoBehaviour
{
    
    //메인 카메라의 Inspector 내 camera는 transform처럼 component로 활용 가능하다
    [SerializeField] Camera camMain;
    
    [SerializeField] float movespeed = 10.0f; //이동속도
    float horz;
    float vert;

    void OnDrawGizmos()
    {
        //Vector3 forward = Vector3.forward * vert;
        //Vector3 right = Vector3.right * horz;
        //Vector3 direction = forward + right;

        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(Vector3.zero, forward * 5f);

        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(Vector3.zero, right * 5f);
        
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(Vector3.zero, direction * 5f);

        //카메라용 기스모 그리기
        //camMain.transform.forward // 카메라의 전방 벡터
        //camMain.transform.right // 카메라의 오른쪽 벡터
        //camMain.transform.up // 카메라의 업 벡터
        Gizmos.DrawRay(Vector3.zero, camMain.transform.forward * 10f);
        
    }

    void Update()
    {
        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        //회전 처리
        UpdateRotation();
        //이동 처리
        UpdatePosition();

        
    }

        
    

    void UpdateRotation()
    {
        // horz에 들어오는 값 = (-1.0f ~ 1.0f) : -1f => 왼쪽, 0f = 센터, 1 => 오른쪽
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        // -1.xxf ~ 1.0f
        //Input.GetAxis()

        // -1f, 0, 1f
        //Input.GetAxisRaw()

        // 쿼터니온으로 회전하기
        //-1f => -180f, 1f= >180f
        //Quaternion rot = Quaternion.Euler(horz * 180f, 0f, 0f);

        Vector3 forward = camMain.transform.forward * vert;
        forward.y = 0.0f;
        // Vector (x오른쪽, y위, z전방)
        
        
        Vector3 right = Vector3.right * horz;
        Vector3 direction = forward + right;

        //forward.normalized => -1 ~ 1까지 단위벡터 : Direction (방향)
        //if (foward.sqrMagnitude == 0)
        //  return;
        if (direction.sqrMagnitude == 0)
            return;


        Quaternion lookrot = Quaternion.LookRotation(direction);
        transform.rotation = lookrot;

        //float yAngle = transform.rotation.eulerAngles.y;
        //transform.rotation = Quaternion.Euler(0.0f, yAngle ,0.0f);

        //Quaternion lookrot = Quaternion.LookRotation(foward); - 앞 뒤
        //Quaternion lookrot = Quaternion.LookRotation(right); - 옆
        //transform.rotation = lookrot;
        //LookRotation = 정면이 어디야라고 묻기

        //회전 처리
    }

    void UpdatePosition()
    {
        //이동처리
        transform.position += new Vector3(horz, 0f, vert) * movespeed * Time.deltaTime;
    }
    
}
