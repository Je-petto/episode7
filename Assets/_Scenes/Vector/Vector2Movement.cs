using CustomInspector;
using UnityEngine;

public class Vector2Movement : MonoBehaviour
{

    //Input
    //Legacy (오래된 버전의 입력장치)
    // 키보드, 마우스, 모바일(터치) 입력을 처리한다
    //[SerializeField, ReadOnly] Vector2 movement;

    [SerializeField] float movespeed = 1.0f; //이동속도
    [SerializeField] float rotatespeed = 1.0f; //회전속도


    void Update()
    {

        // 버튼 누르면 : 참 => 실행
        // 버튼 안 누르면 : 거짓 => 미실행  

        // Unity Edit -> Projct Settings -> Input Manager -> Axes에서 네이밍 확인/네이밍은 변경 가능하기 변경 후 변경한 이름의 Axis로 가져와야 함 
        //GetButton 누른 상태인가? 뗀 상태인가      
        //if (Input.GetButton("Fire1"))
        //     Debug.Log("Fire1 클릭");

        //GetButtonDown 누를 때 한번 호출
        //if (Input.GetButtonDown("Jump"))
        //    Debug.Log("점프 down");

        //GetButtonUp 키에서 땔 때 한번 호출
        //if (Input.GetButtonUp("Jump"))
        //    Debug.Log("점프 up");

        // -1 ~ 1의 값이 나옴(방향을 활용 - Normalize와 연관성이 높음)
        float horz = Input.GetAxis("Horizontal");//W-S
        Debug.Log($"Horizontal = {horz}");

        float vert = Input.GetAxis("Vertical");//A-D
        Debug.Log($"Vertical = {vert}");

        //GameObject의 transform으로 오브젝트 조종
        // movement = new Vector2(horz, vert)


        //1) 마우스로 회전하고 키보드로 이동(나중에)

        //2) 키보드로 회전, 이동 모두 진행

        // 키보드로 회전 및 이동을 하기 위해서는 키를 분리해야 한다
        // horz 회전 담장, vert 이동 담당
        // horz a키 왼쪽 회전, d키 오늘쪽 회전
        // vert w키 앞으로, s키 뒤로 간다

        //Time.deltaTime -> 결과값을 어느 사양에서든 동일하게 만들어준다
        transform.Rotate(0f, horz * Time.deltaTime * rotatespeed, 0f);

        transform.Translate(/*horz * Time.deltaTime * movespeed*/0f, 0f, vert * Time.deltaTime * movespeed);




    }
}
