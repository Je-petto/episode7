using System.Collections.Generic;
using CustomInspector;
using UnityEngine;

public class Array1 : MonoBehaviour
{    
    // 배열 (Array)
    // 데이터 타입 (대표적인) int , float , string , bool
    // 배열 or 리스트 화 할 수 있다

    // () : 괄호 parenthesis
    // {} : brace
    // [] : braket


    [Space(20), HorizontalLine("변수 영역"), HideField] public bool _l0;
    
    public int[] numArray;
    //public int[] numArray2 = { 1, 3, 5, 7 };
    //public List<float> numList;

    public string[] nameArray;
    public string nameFind;



    [Space(20), HorizontalLine("버튼 영역"), HideField] public bool _l1;


    [Space(10), Button("ArrayGetValue", size = Size.medium), HideField] public bool _b0;
    void ArrayGetValue()
    {
        // 배열의 전체 길이 : (예) 5개 
        Debug.Log($"개수:GetLength = {numArray.Length}");

        // 배열의 특정 위치의 값을 가져온다 
        // index (시작:0 ~ 끝:4) 0,1,2,3,4
        Debug.Log($"값 GetValue 방식 - index{2} : {numArray.GetValue(2)}");
        Debug.Log($"값 [] 방식 - index{2} : {numArray[0]}");

        // GetValue(index) 와 [index] 방식 서로 같다
        //numArray.GetValue(3);
        //numArray[3];


        //Random.Range(0f, 1f);
        //Random.value * 10f;

           
    }


    [Button("ArrayLoop1", size = Size.medium), HideField] public bool _b1;
    void ArrayLoop1()
    {
        // 시작 ; 끝 ; 전체길이 ; 
        // 유한 루프로 전체 값 출력     

        // 전체 길이 Length

        // 초기값; 종료조건; 증감;
        int maxcount = numArray.Length;
        for( int a = 0; a < maxcount; a++ )
        {
            // a는 값 이 아니라 , 순번(index)
            Debug.Log($"index{a} : {numArray[a]}"); 
        }
    }


    [Button("ArrayLoop2", size = Size.medium), HideField] public bool _b2;
    void ArrayLoop2()
    {
        // 배열 자체를 넣어주면 , foreach 알아서 전체루프 1바퀴 돌린다.
        // 자동(Auto) : 

        int index=0;
        foreach(int a in numArray)
        {
            Debug.Log($"index{index} : {a}");

            index++;
        }
    }


    [Button("ArrayFind", size = Size.medium), HideField] public bool _b3;
    void ArrayFind()
    {
        // 배열 안에 특정 값을 찾기
        // 어떻게 배열 검색을 해서 찾을것인가 ?
        // nameFind 로 nameArray 에서 값을 찾기

        // 결과 : 찾은경우 => 순번 index:? 출력 => 루프탈출
        //        못찾은경우 => 해당값이 없음
       
        
        //1. for문으로 nameArray값을 전체 출력한다.
        //for : 인덱스
        //foreach : 값
        for(int i=0; i<nameArray.Length; i++)
        {
            //Debug.Log($"인덱스 {i} : 값 {nameArray[i]}");

            // 2. Cherry 랑 같은지 ? 다른지 ? 판단
            if (nameFind == nameArray[i])
            {
                //같으면
                Debug.Log($"찾았다 => index {i}");                
                
                //break;  // for루프만 탈출
                return;   // 함수 전체를 탈출
            }
            // else
            // {
            //     //다르면           
            //     Debug.Log($"다르다");
            // }
        }

        Debug.Log("못찾겠다!");
    }

    
    [Button("ArrayFind2", size = Size.medium), HideField] public bool _b4;
    void ArrayFind2()
    {
        // return 없이 처리하는 경우

        int found = -1;

        for(int i=0; i<nameArray.Length; i++)
        {
            //찾았다
            if (nameFind == nameArray[i])
            {
                found = i; 
                Debug.Log($"찾았다  index : {found}");
                break; // for문만 탈출
            }
        }

        // temp -1 이란 의미는 , 초기값 그대로 내려왔기때문에 => 못찾은거다
        if ( found == -1 )
            Debug.Log("못찾았다!");

    }


}