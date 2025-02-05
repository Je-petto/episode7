using UnityEngine;
using DG.Tweening;
using CustomInspector;

public class Tween1 : MonoBehaviour
{
    //DoTween : Update()가 아닌, 단발성 함수 호출

    public float duration;
    public Ease ease;

    [Space]
    public MeshRenderer meshrend;
    public Color meshcolor;

    private Vector3 initPos; //초기 위치 저장
    private Color initColor; //초기 컬러 저장
    void Start()
    {
        initPos = transform.position;
        initColor = meshrend.material.color;

    }

    [Button("MoveY"), HideField] public bool _b0;
    void MoveY()
    {
        KillMoveY();

        //Local 기준의 Vector 값으로 이동됨
        //LoopType.Incremental = 점진적으로 증가
        transform.position = initPos;
        transform.DOLocalMove(new Vector3(0f, 2f, 0f), duration)
                .SetLoops(1, LoopType.Yoyo)
                .SetEase(ease)
                .OnComplete(() => ChangeColor());
        
        //Y축으로만 이동하게 됨
        //ransform.DOLocalMoveY(2.0f, duration);
    }
    //         () => {}
    //function () {} - 둘이 같은 역할을 할 수 있음

    [Button("KillMoveY"), HideField] public bool _b1;
    void KillMoveY()
    {
        //DOTween.Kill : 현재 동작중인 Tween의 Thread를 지운다(삭제)
        DOTween.Kill(transform);
    }

    [Button("ChangeColor"), HideField] public bool _b2;
    void ChangeColor()
    {
        DOTween.Kill(meshrend.material);
        
        meshrend.material.color = initColor;
        meshrend.material.DOColor(meshcolor, duration)
                        .SetLoops(1, LoopType.Yoyo);
        
        // meshrend.material.DOFade(0f,duration)
        //                 .SetLoops(-1, LoopType.Yoyo);
    }


    void Backup()
    {
        // transform.DOLocalMoveX(1f, 1f)
        //         .SetLoops(-1, LoopType.Yoyo)
        //         .SetEase(Ease.InOutQuad);

        // 위아래 반복 움직이면서, 한방향으로 계속 회전하도록
        //transform.DOLocalMoveY
        //transform.DOLocalRotate
        // transform.DOLocalMoveY(1f, duration).SetLoops(-1, LoopType.Yoyo);
        // transform.DOLocalRotate(new Vector3(0f, 360f, 0f), 2.5f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

}
