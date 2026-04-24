using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Keyboard.current.wKey.isPressed) 
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * -speed * Time.deltaTime;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            transform.position += Vector3.up * -speed * Time.deltaTime;
        }
    }
}
