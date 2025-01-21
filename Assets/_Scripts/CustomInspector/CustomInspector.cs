using UnityEngine;
using CustomInspector;

public class InspectorTutorial : MonoBehaviour
{    
    [Title("인스펙터 꾸미기", underlined: true, fontSize = 15, alignment = TextAlignment.Center), HideField] public bool _t0;


    [Space(10), HorizontalLine("세부 속성", color:FixedColor.CherryRed), HideField] public bool _l0;

    public int testNum1;
    [Range(0, 5)] public int testNum2;    
    [Range(0.0f, 10.0f)] public float testNum3;


    [RichText(true)]
    public string testString;

    [Multiline(lines:4)]
    public string testString2;

    [TextArea(minLines: 2, maxLines: 10)]
    public string testString3;


    [Space(20), HorizontalLine(color: FixedColor.Orange), HideField] public bool _l1;

    [Space(20), ReadOnly(DisableStyle.OnlyText)] public string testReadOnly = "ReadOnly 테스트" ;

    [Space(20), HorizontalLine(color: FixedColor.Purple), HideField] public bool _l2;

    [Space(20), Preview(Size.big)] public Sprite sprite;

    [Space(20), HorizontalLine(color: FixedColor.Green), HideField] public bool _l3;



    [Space(30), Button("Method1", size = Size.medium), HideField] public bool _b0;
    [Button("Method2", size = Size.medium), HideField] public bool _b1;

    

    void Method1()
    {
        Debug.Log("메소드1 테스트");
    }

    void Method2()
    {
        Debug.Log("메소드2 테스트");
    }

    [Space(20), Button("Method3", true)] public int inputNum;
    void Method3(int n)
    {
        Debug.Log($"입력한 숫자 {n}");
    }

    [Space(50), HideField] public bool _s1;
}