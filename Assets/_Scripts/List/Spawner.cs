using System.Collections.Generic;
using CustomInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //프히펩(Frefab) -> (Instantiate) ->리스트에서 여러 프리펩을 랜덤하게 스폰시키기

    [Title("Prop Spawner", underlined: true, fontSize = 14, alignment = TextAlignment.Center), HideField] public bool _t0;
    [Space(10), HorizontalLine("Prefabs", color: FixedColor.CherryRed), HideField] public bool _l0;

    // 다른 GameObject와 연결시키기 위해 만든 클래스
    [SerializeField] Transform propRoot;
    [SerializeField] List<GameObject> prefabs;
    [Space(10), HorizontalLine("배치 속성", color: FixedColor.CherryRed), HideField] public bool _l1;

    
    [SerializeField] LayerMask layerMask;
    [SerializeField, Range(1f,200f)] float radius; //반지름 -> 왜 Range? [SerializeField] float radius;
    //int : -21억 ~ 21억
    //uint ; 0 ~ 42억
    [SerializeField, AsRange(1, 1000)] Vector2 maxNumByRange; // X축 회전 범위
    //[SerializeField] float yOffset; Y 높이조절

    [Space(10), HorizontalLine("회전속성", color: FixedColor.CherryRed), HideField] public bool _l2;
    [SerializeField, AsRange(-90f, 90f)] Vector2 rotateXaxis; // X축 회전 범위
    [SerializeField, AsRange(-180f, 180f)] Vector2 rotateYaxis; // y축 회전 범위
    [SerializeField, AsRange(-90f, 90f)] Vector2 rotateZaxis; // Z축 회전 범위

    [Space(10), HorizontalLine("크기속성", color: FixedColor.CherryRed), HideField] public bool _l3;
    [SerializeField, AsRange(0.5f, 2f)] Vector2 scalerange; //크기 범위 설정

    //에디터에서 기즈모를 그릴 수 있는 공간(예약함수)
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        //해당 transform은 script가 연결된 Gameobject 자체의 transform이 된다
        Gizmos.DrawWireSphere(transform.position, radius);

        //Gizmos.DrawCube(transform.position, Vector3.one * radius);
    }

    [Space(15), HorizontalLine("Buttons", color: FixedColor.CherryRed), HideField] public bool _l4;

    [Button("Spawn"), HideField] public bool _b0;
    public void Spawn()
    {
        Vector3 rndpos = Random.insideUnitSphere * radius + transform.position;

        //Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity); - 좌표 입력 및 조종을 활용
        //랜덤한 위치에 prefab 생성
        //prop을 기준으로 생성됨
        //GameObject clone =  Instantiate(prefab, transform);

        // 여러개의 prefabs를 랜덤하게 뽑아내기
        // prefab는 GameObject
        // prefabs는 List<GameObjects>
        // 리스트 안에 있는 데이터 수 = prefabs.Count;

        //Checkpoint를 활용해서
        //Spawn 가능한지 ? 아닌지 ? 판단하고 생성
        Vector3 hitpoint;
        
        // 거짓 :  빈 공간이라는 뜻 -> 빈 공간이기에 아래 함수들이 실행되서는 안됨 -> 함수 탈출
        if (CheckHeight(rndpos, out hitpoint) == false)
            return;

        // 참 : 기존 계획대로 함수 실행

        
        
        // 리스트의 인덱스를 통한 값 가져오기
        // prefabs[0] = 리스트의 첫번재 값
        // prefabs[prefabs.Count-1] = 리스트의 마지막 값
        // 첫 값과 끝 값을 안다면 랜덤한 값을 뽑을 수 있음
        int rndcnt = Random.Range(0, prefabs.Count);

        GameObject clone = Instantiate(prefabs[rndcnt]);

        //float rndscl = Random.value;
        float rndscl = Random.Range(scalerange.x, scalerange.y);


        //좌표 값 float에 숫자를 곱하여 증폭 시킬 수 있다)
        //Vector2 (x, y)
        //Vector3 (x, y, z)
        //Vector4 (x, y, z, w)
        //Color (r, g, b, a)

        //Transform : 변형 - GameObject 내 변형시키거나 삭제할 수 없는 Inspector
        //주요기능:
        //1. 위치(position), 회전(rotation), 크기(scale)
        //2. 계층구조(Hierarchy), 부모-자식 (Parent-Child)

        //GameObject(더 상위계층) > Transform
        //생성 (Instantiate)
        //삭제 (Destroy(플레이모드), DestroyImmediate(에디터모드))
        
        //크기를 랜덤하게 조절한다
        clone.transform.localScale = new Vector3(rndscl, rndscl, rndscl);

        //위치를 랜덤하게 조절한다
        //= new Vector3(x,y,z) 활용해서 평면에 위치하게 한다
        //clone.transform.position = new Vector3(rndpos.x, hitpoint.y, rndpos.z); -> 얘는 왜 있지?
        clone.transform.position = hitpoint;
        
        //오브젝트를 랜덤하게 돌려 변형을 준다
        //clone.transform.rotation = Quaternion을 배운 후에 다룬다
        //rotateXaxis.x 최소값
        //rotateXaxis.y 최대값
        float rndrotX = Random.Range(rotateXaxis.x, rotateXaxis.y);
        float rndrotY = Random.Range(rotateZaxis.x, rotateZaxis.y);
        float rndrotZ = Random.Range(rotateZaxis.x, rotateZaxis.y);
        clone.transform.Rotate(new Vector3(rndrotX, rndrotY, rndrotZ));

        clone.transform.SetParent(propRoot);
    }

    [Button("SpawnLoop"), HideField] public bool _b1;
    void SpawnLoop()
    {
        int rnd = (int)Random.Range(maxNumByRange.x, maxNumByRange.y);
        for (int i = 0; i < rnd; i++)
        {
            Spawn();
        }
    }

    [Button("Despawn"), HideField] public bool _b2;
    public void Despawn()
    {
        //Destroy 플레이모드에서 삭제할 때
        //DestroyImmediate 에디터모드에서 삭제할 때

        //propRoot.childCount; 자식 수를 가져온다
        //propRoot 리스트 자료 구조형
        //DestroyImmediate()

        // 랜덤으로 제거됨
        // foreach (Transform t in propRoot) // 리스트에 뭐가 있든 한번씩 값을 가져오는 것 = foreach
        // {
        //     DestroyImmediate(t.gameObject);
        // }

        // foreach문 = 값 전달, 인덱스는 알아서 구해라
        // 리스트에 뭐가 있든 한번씩 값을 가져오는 것 = foreach
        // 랜덤 갯수 지우기
        // foreach (Transform t in propRoot) 
        // {
        //     //t는 나무, transform은 기즈모
        //     if (CheckInside(t.position, transform.position))
        //         DestroyImmediate(t.gameObject);
        // }

        // for문 = 인덱스 전달, 값은 알아서 구해라
        for (int i = propRoot.childCount-1; i >= 0; i--)
        {
            Transform child = propRoot.GetChild(i);
            if (CheckInside(child.position, transform.position))
                DestroyImmediate(child.gameObject);
        }

    }

    [Space(30), HideField] public bool _s1;

    // 원 안에 들어왔는지 검사하는 함수 - true or false이기 때문에 값이 bool로 나옴
    bool CheckInside(Vector3 A, Vector3 B)
    {
        // 두 점 사이의 거리 구하기
        float dist = Vector3.Distance(A, B);
        return dist <= radius;

        //원 사이의 거리가 반지름을 넘지 않음
        // if (dist <= radius)
        // {
        //     return true;
        // }
        // //원 사이의 거리가 반지름을 넘음
        // else
        // {
        //     return false;
        // }
    }
    // 생성할 오브젝트와 지형이 만나는 지점을 찾는다
    bool CheckHeight(Vector3 clonepoont, out Vector3 hitpoint)
    {
        //기준점: Gizmo -> Spawner의 위치를 기준으로 일정 높이 위에서 Ray를 Cast한다.
        
        RaycastHit hit;

        if (Physics.Raycast(clonepoont + Vector3.up * 30f, -Vector3.up, out hit, 1000.0f, layerMask))
        {
            //참 ? 충돌했다 => 충돌한 위치가 어디냐? => 그 위치에 오브젝트를 심는다
            
            //충졸 지점
            hitpoint = hit.point;
            return true;
        }
        //거짓 ? 충돌 안했다 => 패스
        hitpoint = Vector3.zero;
        return false;
            
    }

}
