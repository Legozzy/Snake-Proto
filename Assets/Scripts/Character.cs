using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public float speed = 1.0f;

    private bool isRotating = false;
    [SerializeField] private float rotationDuration = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Keyboard.current.aKey.isPressed && !isRotating)
        {
            StartCoroutine(RotateSnake(90f));
        }
        else if (Keyboard.current.dKey.isPressed && !isRotating)
        {
            StartCoroutine(RotateSnake(-90f));
        }
    }

    IEnumerator RotateSnake(float angle)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);
        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;
    }
}
