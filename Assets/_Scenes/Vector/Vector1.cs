using CustomInspector;
using UnityEngine;

public class Vector1 : MonoBehaviour
{
    //float : Scalar(힘)
    //Vector : 힘 + 방향
    public Vector3 from = new Vector3(0f, 0f, 0f);
    public Vector3 to = new Vector3(0f, 0f, 0f);
    public float magnitude;

    public float force = 10f; 
    public Vector3 vA;
    public Vector3 vB;
    
    // 보여주기용 읽기전용
    [ReadOnly(disableStyle = DisableStyle.OnlyText)] public Vector3 AaddB;
    [ReadOnly(disableStyle = DisableStyle.OnlyText)] public Vector3 AminusB;
    [ReadOnly(disableStyle = DisableStyle.OnlyText)] public Vector3 AmulForce;
    [ReadOnly(disableStyle = DisableStyle.OnlyText)] public Vector3 AmulForceNormal;

    void DrawVector1()
    {
        //from은 원점, 새 힘과 방향이 적용된 벡터
        Gizmos.DrawLine(from, to);

        //벡터의 강도(힘)
        magnitude = to.magnitude;
    }

    // 예악함수 : 에디터에 기즈모를 그려라
    // 플레이 모드에는 적용이 되지 않음
    void OnDrawGizmos()
    {
        DrawVectorOperation();
    }

    //label 붙이면 함수 명이 아니라 붙인 레이블로 이름이 붙음
    [Button("VectorElement", label = "벡터 기본 속성"), HideField] public bool _b0;
    void VectorElement()
    {
        Debug.Log(Vector3.zero);
        Debug.Log(Vector3.one);
        
        // 유니티 좌표계 =  왼손 좌표계
        // 검지 = Forward Vector = Z+
        // 엄지 = Up Vector = Y+
        // 중지 = Right Vector = X+
        Debug.Log($"벡터 up : {Vector3.up}"); //벡터 up : (0.00, 1.00, 0.00)*
        Debug.Log($"벡터 down : {Vector3.down}"); //벡터 down : (0.00, -1.00, 0.00)
        Debug.Log($"벡터 forward : {Vector3.forward}"); // 벡터 forward : (0.00, 0.00, 1.00)* -> z축이 정면
        Debug.Log($"벡터 back : {Vector3.back}"); // 벡터 back : (0.00, 0.00, -1.00)
        Debug.Log($"벡터 right : {Vector3.right}"); // 벡터 right : (1.00, 0.00, 0.00)*
        Debug.Log($"벡터 left : {Vector3.left}"); // 벡터 left : (-1.00, 0.00, 0.00)
    }

    void DrawVectorElement()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Vector3.zero, Vector3.up);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.right);
    }

    void DrawVectorOperation()
    {
        //DrawVectorElement()
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Vector3.zero, vA);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, vB);

        AaddB = vA + vB;
        Gizmos.color = new Color(1f, 1f, 0f);
        Gizmos.DrawRay(Vector3.zero, AaddB);

        AminusB = vA - vB;
        Gizmos.color = new Color(0f, 1f, 0f);
        Gizmos.DrawRay(Vector3.zero, AminusB);

        // 벡터 간 곱셈, 나눗셈 불가능

        AmulForce = vA * force;
        Gizmos.color = new Color(1f, 0f, 1f);
        Gizmos.DrawRay(Vector3.zero, AmulForce);

        // 벡터의 정규화(Normalize) = -1 ~ 1 사이 값으로 만들어준다 => 방향을 알려주는데 사용한다
        AmulForceNormal = AmulForce.normalized;
        Gizmos.color = new Color(0f, 0f, 0f);
        Gizmos.DrawRay(Vector3.zero, AmulForceNormal);
    }
}
