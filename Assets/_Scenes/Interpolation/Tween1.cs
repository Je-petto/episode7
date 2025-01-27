using UnityEngine;
using DG.Tweening;

public class Tween1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        // transform.DOLocalMoveX(1f, 1f)
        //         .SetLoops(-1, LoopType.Yoyo)
        //         .SetEase(Ease.InOutQuad);
        
        // 위아래 반복 움직이면서, 한방향으로 계속 회전하도록
        //transform.DOLocalMoveY
        //transform.DOLocalRotate

        transform.DOLocalMoveY(1.5f, 2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad);

        transform.DOLocalRotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.InOutQuad);
    }

}
