using UnityEngine;
using CustomInspector;
using System.Collections.Generic;

public class TilemapGenerator : MonoBehaviour
{

    [Header("하이트맵 소스")]
    [SerializeField] Texture2D heightmap;
    [SerializeField] List<GameObject> tilePrefab;// 지형 프리팹
    [SerializeField] GameObject tileBasePrefab; 


    [Header("하이트맵 속성")]
    [SerializeField, Range(1f, 200f)] float heightRange;


    [Space(10), HorizontalLine("버튼"), HideField] public bool _l1;

    [Button("GetInfo"), HideField] public bool _b1;
    void GetInfo()
    {
        float w = heightmap.width;    // Horizontal
        float h = heightmap.height;   // Vertical

        // width , height 를 콘솔에 출력한다.
        Debug.Log($"Width = {w}, Height = {h}");


        // 1. 루프문으로 w,h 크기 만큼 반복한다.
        // 2. 반환값(Color) 을 콘솔에 출력 한다.

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Color col = heightmap.GetPixel(x, y);
                //Debug.Log($"컬러 r = {col}");
                Debug.Log($"컬러 g = {col.g}");
            }
        }


        // col ( r,g,b,a )
        // r : 레벨을 디자인    ( 등고선 표현 )
        // g : 나무 심기        ( 배경 프랍 배치 )
        // b : 몹 배치          ( 적 캐릭터 배치 )
        // a : START , GOAL     ( 시작점, 골인지점 )

    }

    [Button("BuildMap"), HideField] public bool _b2;
    void BuildMap()
    {
        // Width , Height 만큼 루프를 돌린다.
        // GetPixel() col.r 을 구한다
        // col.r 을 기준으로 Instantiate 할때 , y값을 설정 해준다.
        // Cube 월드를 만든다. (큐브, size Y 0.5f => 5.0f)

        float w = heightmap.width;    // Horizontal
        float h = heightmap.height;   // Vertical
        GameObject Empty = new GameObject("TileGroup");

        

        // GameObject clone = Instantiate(tilePrefab[rndcnt]);

        for (int x = 0; x < w; x++)
        {
            for (int z = 0; z < h; z++)
            {
                
                int rndcnt = Random.Range(0, tilePrefab.Count);

                Color col = heightmap.GetPixel(x, z);
                // col 범위 ( 최소 : 0.0f ~ 최대 : 1.0f )


                // RED 채널만 활용 : 등고선 표현
                // 컬러의 r채널을 높이값으로 활용 * heightRange 로 값을 증폭 (더 극명하게 보여주기 위해)
                float y = col.r * heightRange;

                GameObject contour = Instantiate(tilePrefab[rndcnt], new Vector3(x, y, z), Quaternion.identity);
                contour.transform.SetParent(Empty.transform);



                if (col.g != 0.0f)
                {
                    // Green 채널만 활용 : 나무를 심는다
                    GameObject trees = Instantiate(tileBasePrefab, new Vector3(x, y, z), Quaternion.identity);
                    trees.transform.SetParent(Empty.transform);
                }
            }
        }
    }
}
