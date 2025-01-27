using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnCollisionEnter()
    {

    }
    void OnTriggerEnter()
    {
        Debug.Log("GameClear!");
    }
}
