using UnityEngine;
using Deform;

public class QuaternionMovement : MonoBehaviour
{

    //메인 카메라의 Inspector 내 camera는 transform처럼 component로 활용 가능하다
    [SerializeField] Camera camMain;

    [SerializeField] float movespeed = 10.0f; //이동속도
    [SerializeField] float jumpPower = 5.0f; //이동속도
    [SerializeField] float jumpDuration = 1.0f; //점프지속시간
    float horz;
    float vert;
    public bool isJumping; // 점프 중인가?

    [SerializeField] SquashAndStretchDeformer deform;

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

    }

    void Update()
    {
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        //Jump = Input.GetButton("Jump");
        //Jump = Input.GetButtonDown("Jump");

        //회전 처리
        UpdateRotation();
        //이동 처리
        UpdatePosition();
        //점프 처리
        UpdateJump();


    }

    // 회전 처리
    void UpdateRotation()
    {
        // horz에 들어오는 값 = (-1.0f ~ 1.0f) : -1f => 왼쪽, 0f = 센터, 1 => 오른쪽
        // horz = Input.GetAxis("Horizontal");
        // vert = Input.GetAxis("Vertical");

        // -1.xxf ~ 1.0f
        //Input.GetAxis() -> 자연스러운 움직임

        // -1f, 0, 1f
        //Input.GetAxisRaw() -> 딱딱한 움직임

        // 쿼터니온으로 회전하기
        //-1f => -180f, 1f= >180f
        //Quaternion rot = Quaternion.Euler(horz * 180f, 0f, 0f);

        // Vector3 forward = camMain.transform.forward * vert;
        // forward.y = 0.0f;
        //수직 탑뷰에서 캐릭터가 앞뒤로 이동하지 않을 때
        Vector3 forward;
        if (camMain.transform.eulerAngles.x != 90)//일반적인 카메라 상황    
        {
            forward = camMain.transform.forward * vert;
            forward.y = 0f;
        }
        else    // x축 90도 세운상황
        {
            forward = camMain.transform.up * vert;
        }

        Vector3 right = camMain.transform.right * horz;
        right.y = 0f;
        Vector3 direction = forward + right;
        // Vector (x오른쪽, y위, z전방)

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
        Vector3 forward;
        if (camMain.transform.eulerAngles.x != 90) // 일반적인 상황
        {
            forward = camMain.transform.forward * vert;
            forward.y = 0.0f;
        }
        else// x축이 90인 상황 
        {
            forward = camMain.transform.up * vert;
        }

        Vector3 right = camMain.transform.right * horz;
        right.y = 0f;
        // normalized를 통해 값을 균일화
        Vector3 direction = (forward + right).normalized;

        //이동처리
        Vector3 movDir = direction * movespeed * Time.deltaTime;
        transform.position += movDir;

        //Debug.DrawRay(transform.position, movDir * 100f, Color.blue);
    }

    //public float DeltaTime;
    private float jumpStartTime;
    //private Vector3 jumpStartPosition;

    private float jumpChargedTime;

    public float jumpforceCharged = 0.0f;
    void UpdateJump()
    {

        //isJump = Input.GetButtonDown("Jump");
        //GetButtonDown 누른 시간만큼 충전, GetButtonUp 누룰 때 충전된 힘만큼 Jump 방법

        //점프를 한 번 눌렀을 때 다시 누를 때 점프가 적용이 안되도록 -> Jump를 누루고, 점프 중에는 '거짓'으로 만들기
        // 방법 : if (Input.GetButtonDown("Jump") && isJumping == true)

        if (Input.GetButtonDown("Jump"))
        {
            jumpChargedTime = Time.time;

            deform.Factor = -0.25f;

            //jumpStartPosition = transform.position;
        }

        // 포물선 방정식
        // 공식1 : (x - x * x)
        // 공식2 : y = x(1 -x)
        // y는 점프 높이, x는 시간변화량(점프 원점으로부터 원점까지의 변화량 - 퍼센트라고 생각하면 쉬움(0~1))
        // ex) 점프를 3초 동안 한다
        // 0 ~ 3초간 점프
        // [x = 0 , y = 0] , [x = 0.5, y = 0.25] [x = 1, y =0]

        // 시간
        //DeltaTime = Time.deltaTime;// 한 프레임 동안 걸린 시간
        // TimeNow = Time.time;// 현재 시간 -> 플레이한 시간
        //(현재시간 - 시작시간)/기준시간
        else if (Input.GetButtonUp("Jump") && isJumping == false)
        {
            
            jumpStartTime = Time.time;

            jumpforceCharged = jumpStartTime - jumpChargedTime;
            jumpforceCharged = Mathf.Clamp(jumpforceCharged, 1f, 3f);//Mathf.Clamp = 충전 값의 가능 범위 지정

            deform.Factor = 0.25f;
            isJumping = true;
            //jumpStartPosition = transform.position;
        }

        if (isJumping == true)
        {
            float percent = (Time.time - jumpStartTime) / jumpDuration; // 점프 포지션

            // percent < 1 = 표물선 안에서 작동 중(0 ~ 1)
            if (percent < 1)
            {
                deform.Factor = 1f;
                
                float jumpHeight = (percent - percent * percent) * (jumpPower * jumpforceCharged); //점프 높이(표물선 방정식)

                transform.position = new Vector3(transform.position.x, jumpHeight, transform.position.z);

            }

            else // 포물선을 벗어남(1~)
            {
                deform.Factor = 0f;
                
                isJumping = false;
            }
        }
    }

}
