using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

public class List1 : MonoBehaviour
{
    //public int[]// 배열형태 // 사용비율: 1
    // 배열형태는 접근이 빠르다
    public List<int> datas = new List<int>(); // 리스트 형태 사용비율: 9
    public List<string> dataStrs = new List<string>();
    public List<GameObject> dataObjs = new List<GameObject>();

    void AddData()
    {
        //datas.Add(값) //추가
        //datas.Clear() //전체삭제
        //datas.Remove(값) //1개 삭제
        //datas.Find(값) // 특정 값을 찾는다
        //datas.Sort() // 정렬(오름차순, 내림차순)
        //datas.Count // 총 개수
        //datas.ForEach() // 
    }

    //버튼은 메소드와 같은 이름값이어야한다.
    //true를 넣는 경우는 메소드에 값[=메소드(여기에 들어가는 게 값)]을 넣을건지 아닐지
    //넣을거면 아래와 같이 표시
    //버튼 생성 시 값이 들어가냐 없느냐로 구별하여 제작
    [Space(10), Button("AddData", true)] public int inputNum;
    void AddData(int n)
    {
        //입력한 숫자를 datas 리스트에 넣는다 = 리스트에 추가한다
        datas.Add(n);

        //Debug.Log($"입력한 숫자{n}");
    }
    [Space(10), Button("RemoveData", true)] public int removeNum;
    void RemoveData(int n)
    {
        //입력한 숫자를 datas 리스트에서 뺀다 = 리스트에서 삭제한다
        datas.Remove(n);
        //datas.RemoveAll(); 조건에 맞는것만 전체 삭제

        //Debug.Log($"입력한 숫자{n}");
    }
    [Space(10), Button("RemoveAtData", true)] public int removeIndex;
    void RemoveAtData(int i)
    {

        datas.RemoveAt(i);
        //datas.RemoveAt(값); 순번(index)에 해당하는 것만 삭제
        // i에 해당하는 데이터가 있는지?
        // 참 : 지워라, 거짓 : pass
        // datas.Contains(값)
        if (datas.Exists(a => a == i) == false)
            return;

        //Debug.Log($"입력한 숫자{n}");
    }
    [Button("ClearData", size = Size.medium), HideField] public bool _b1;
    void ClearData()
    {
        //datas 리스트에서 완전 삭제
        datas.Clear();
    }
    [Button("SortData", size = Size.medium), HideField] public bool _b2;
    void SortData()
    {
        //기본 정렬
        datas.Sort();
    }
    [Button("ShowAllDataFor", size = Size.medium), HideField] public bool _b3;
    void ShowAllDataFor()
    {
        //리스트의 모든 데이터를 출력해보기
        //반복문        
        // 배열.Length는 배열의 총 데이터 수
        //datas.Count는 리스트 안의 총 데이터 수
        for (int i = 0; i < datas.Count; i++)
        {
            Debug.Log($"{i} : {datas[i]}");
        }

    }
    [Button("ShowAllDataWhile", size = Size.medium), HideField] public bool _b4;
    void ShowAllDataWhile()
    {
        int i = 0;
        while (i < datas.Count)
        {
            Debug.Log($"{i} : {datas[i]}");
            i++;
        }
    }
    [Button("ShowAllDataForEach", size = Size.medium), HideField] public bool _b5;
    void ShowAllDataForEach()
    {
        // Foreach의 존재 이유 => 리스트를 위해 만들어졌다
        // in 리스트를 넣으면, 알아서 1바퀴 값을 전달해준다
        // 따라서 증감 표현을 해줄 필요가 없다
        // 초기값 설정 필요없다
        // index를 표현하고 싶으면 알아서 추가하여 표현
        foreach(var v in datas)
            Debug.Log(v);

        datas.ForEach(v => Debug.Log(v));

    }

}
