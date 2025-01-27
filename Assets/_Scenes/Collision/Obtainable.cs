using UnityEngine;
using UnityEngine.Events;

public class Obtainable : MonoBehaviour
{
    [SerializeField] UnityEvent OnEventCoin;
    
    Vector3 _temp;
    
    // 충돌체 처음 진입했을 때
    void OnTriggerEnter(Collider col)
    {
        Debug.Log($"TriggerEnter{col.name}");
        
        if (col.tag != "Item")//태그가 아이템인 오보젝트만 먹어서 삭제
            return;
        
        _temp = col.transform.localScale;
        //Destroy(col.gameObject); 오브젝트 삭제
        col.transform.localScale = _temp * 3.0f;
    }

    // 충돌체가 머물고 있을 때
    void OnTriggerStay(Collider col)
    {
    }
    
    // 충돌체가 탈출했을 때
    void OnTriggerExit(Collider col)
    {
        if (col.tag != "Item")//태그가 아이템인 오보젝트만 먹어서 삭제
            return;
        col.transform.localScale = _temp;
    }
    // void OnCollisionEnter()
    // {
    //     Debug.Log("Collision +1");

    //     Destroy(gameObject);
    // }
    // new Vector는 완전히 새로운 값으로 받아야할 때
    //
}
