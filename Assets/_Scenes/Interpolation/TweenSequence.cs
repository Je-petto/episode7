using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TweenSequence : MonoBehaviour
{

    public List<Vector3> targetPos;
    public List<Vector3> degree;
    
    void Start()
    {
        SequenceMoveWander();
    }
    
    void SequenceMove()
    {
        //Append : 시퀀스 뒤에 첨가, DoTween 함수를 바로 사용한다
        //AppendInterval : 시퀀스 뒤에 첨가, 지정한 시간만큼 지연시킨다
        //AppendCallback: 시퀀스 뒤에 첨가, 일반적인 함수를 람다형식으로 사용한다
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.Append(transform.DOMove(targetPos[0], 1f));
        seq.Append(transform.DOLocalRotate(degree[0], 0.25f));
        seq.Append(transform.DOMove(targetPos[1], 1f));
        seq.Append(transform.DOLocalRotate(degree[1], 0.25f));
        seq.Append(transform.DOMove(targetPos[2], 1f));
        seq.Append(transform.DOLocalRotate(degree[2], 0.25f));
        seq.Append(transform.DOMove(targetPos[3], 1f));
        seq.Append(transform.DOLocalRotate(degree[3], 0.25f));
        seq.SetLoops(-1);
    }

    void SequenceMoveLoop()
    {
        Sequence seq = DOTween.Sequence();
        // while(무한반복에 유리), for(유한반복에 유리 : index), for(유한반복에 유리 : value(값))
        int r = 1;
        foreach( Vector3 v in targetPos)
        {
            seq.Append(transform.DOMove(v, 1f));
            seq.Append(transform.DOLocalRotate(Vector3.up*90f*r, 0.25f));
            r++;
        }

        seq.SetLoops(-1);

    }

    //몬스터 Wander : 반지름 10 영역 안에서 패트롤 하도록 한다
    //Loop와 Random을 활용
    Vector3 rndpos;
    void SequenceMoveWander()
    {
        // Vector3 rndpos = Random.insideUnitSphere*10;
        // rndpos.y = 0.5f;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback( () => 
        {
            rndpos = Random.insideUnitSphere * 10f;
            rndpos.y = 0.5f;
            // 방향 구하기 : B - A => A 가 B를 바라보는 방향
            transform.rotation = Quaternion.LookRotation(rndpos - transform.position);
            transform.DOMove(rndpos, 1f);
        });        
        seq.AppendInterval(1f);
        seq.SetLoops(-1);
        
    }



    void Sequence1()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.AppendCallback(() => Debug.Log("1초"));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => Debug.Log("2초"));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => Debug.Log("3초"));

    }

}
