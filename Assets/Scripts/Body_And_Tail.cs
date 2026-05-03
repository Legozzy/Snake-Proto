using UnityEngine;

public class Body_And_Tail : MonoBehaviour
{
    public int index;
    public Head head;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (head.positionHistory.Count > index)
        {
            transform.position = head.positionHistory[index];
        }   
    }
}
