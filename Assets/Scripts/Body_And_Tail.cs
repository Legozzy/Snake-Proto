using UnityEngine;

public class Body_And_Tail : MonoBehaviour
{
    public GameObject head;
    public float dist;
    public float followSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, head.transform.position);
        if (dist > 0.15f)
        {
            transform.position = Vector3.MoveTowards(transform.position, head.transform.position, followSpeed * Time.deltaTime);
        }
        
    }
}
