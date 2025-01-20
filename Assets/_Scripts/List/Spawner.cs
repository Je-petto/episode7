using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using CustomInspector;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //프히펩(Frefab) -> (Instantiate) ->리스트에서 여러 프리펩을 랜덤하게 스폰시키기

    [SerializeField] Transform propRoot;
    [SerializeField] GameObject prefab;
    [SerializeField] float radius; //지름
    [SerializeField] float yOffset; //Y 높이조절
    
    //에디터에서 기즈모를 그릴 수 있는 공간(예약함수)
    void OnDrawGizmos()
    {
        //Gizmos.color = Color.magenta;

        //Gizmos.DrawWireSphere(transform.position, radius * 2f);

        //Gizmos.DrawCube(transform.position, Vector3.one * radius);
    }

    [Button("Spawn"), HideField] public bool _b0;
    public void Spawn()
    {
        //Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity); - 좌표 입력 및 조종을 활용
        //랜덤한 위치에 prefab 생성
        GameObject clone =  Instantiate(prefab); //prop을 기준으로 생성됨
        //GameObject clone =  Instantiate(prefab, transform);
        
        //좌표 값 float에 숫자를 곱하여 증폭 시킬 수 있다)
        //Vector2 (x, y)
        //Vector3 (x, y, z)
        //Vector4 (x, y, z, w)
        //Color (r, g, b, a)
        Vector3 rndpos = Random.insideUnitSphere * radius + transform.position;

        clone.transform.position = rndpos;
        //변수 Range 만큼, 증폭하여 위치 이동
        //= new Vector3(x,y,z) 활용해서 평면에 위치하게 한다
        clone.transform.position = new Vector3(rndpos.x, yOffset, rndpos.z);
        clone.transform.SetParent(propRoot);

        //float rndscl = Random.value;
        float rndscl = Random.Range(0.6f, 1.2f);

        if (rndscl >= 0.6)
        {
            // 똑같은 값이 들어갈 수 있도록 Vector3 값에 넣는다
            clone.transform.localScale = new Vector3(rndscl, rndscl, rndscl);
        }

    }

    [Button("Despawn"), HideField] public bool _b1;
    public void Despawn()
    {
        //Destroy 플레이모드에서 삭제할 때
        //DestroyImmediate 에디터모드에서 삭제할 때

        //propRoot.childCount; 자식 수를 가져온다
        //propRoot 리스트 자료 구조형
        //DestroyImmediate()

        foreach(Transform t in propRoot) // 리스트에 뭐가 있든 한번씩 값을 가져오는 것 = foreach
        {
            DestroyImmediate(t.gameObject);
        }
        
    }
}
