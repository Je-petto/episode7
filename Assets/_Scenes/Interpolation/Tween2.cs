using UnityEngine;
using DG.Tweening;
using CustomInspector;


public class Tween2 : MonoBehaviour
{
    
    //Cube가 위아래 바운스 -> 반복, Ease 사용
    //회전 -> 한방향으로 일정한 속도로 계속 회전
    //스케일 조절  -> 1.5배 커졌다가, 원위치가 되도록
    public float duration;
    public Ease ease;
    [Space]
    public MeshRenderer meshrend;
    public Color meshcolor;
    [Space]
    public RotateMode rotatemode;

    private Vector3 initPos; //초기 위치 저장
    private Color initColor; //초기 컬러 저장
    void Start()
    {
        initPos = transform.position;
        initColor = meshrend.material.color;

        // Start : GameObject가 시작할 때 한 번 호출
        // 용도 : 초기값 설정 해준다 
        Debug.Log("Start");
    }
    // OnDestroy : GameObeject가 끝날때 (삭제될 때) 한 번 호출
    // 용도 : 사용한 리소스들 청소해준다 (삭제)
    void OnDestroy()
    {
        DOTween.KillAll();
        
        Debug.Log("OnDestroy");
    }

    [Button("CompositedMove"), HideField] public bool _b0;
    void CompositedMove()
    {
        //Clear
        DOTween.Kill(transform);
        //위아래
        transform.position = initPos;
        transform.DOLocalMoveY(3.0f, duration).SetLoops(-1, LoopType.Yoyo);
        //Scale 조절 반복
        transform.DOScale(1.5f, duration).SetLoops(-1, LoopType.Yoyo);
        //컬러 조절 반복
        initColor = meshrend.material.color;
        meshrend.material.DOColor(meshcolor, duration).SetLoops(-1, LoopType.Yoyo);
        //회전 반복
        transform.DOLocalRotate(Vector3.up * 360f, duration, rotatemode)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(ease);

        //new Vector3(0f, 1f, 0f) = Vector3.up

    }

}
