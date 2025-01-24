using CustomInspector;
using UnityEngine;

public class Vector3Rotate : MonoBehaviour
{

    public float pitch;//x축 회전 : Pitch , Vertical(w,s)
    public float yaw;//y축 회전 : Yaw , Vertical(a,d)
    public float roll;//z축 회전 : Roll
    [SerializeField] float rotatespeed = 10.0f; //회전속도

    void Update()
    {
        yaw = Input.GetAxis("Horizontal")* Time.deltaTime * rotatespeed;
        pitch = Input.GetAxis("Vertical")* Time.deltaTime * rotatespeed;
        
        transform.Rotate(new Vector3(pitch, yaw, roll));

        
    }
    
    [Button("GimballockRotate", label = "짐벌락"), HideField] public bool _b0;
    void GimballockRotate()
    {

    }
}
