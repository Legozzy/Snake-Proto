using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    public List<Vector3> positionHistory = new List<Vector3>();
    public List<GameObject> bodySegments = new List<GameObject>();
    public GameObject fruitPrefab;
    public GameObject tailPrefab;
    public GameObject gameOverText;
    public GameObject restartButton;
    public AudioSource collectibleSound;
    public AudioSource levelMusic;

    [SerializeField] private float moveDelay = 0.5f;
    [SerializeField] private float cellSize = 0.2f;
    [SerializeField] private int gridSize = 22;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFruit();

        positionHistory.Add(transform.position);

        for (int i = 0; i < bodySegments.Count; i++)
        {
            Body_And_Tail segment = bodySegments[i].GetComponent<Body_And_Tail>();
            segment.index = i + 1;
            segment.head = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        while (timer >= moveDelay )
        {
            Movement();
            
            timer -= moveDelay;
        }

        HandleInput();
    }

    private void Movement()
    {
        transform.position += transform.forward * cellSize;

        positionHistory.Insert(0, transform.position);

        int requiredLength = bodySegments.Count + 1;

        if (positionHistory.Count > requiredLength )
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
        }
    }

    private void HandleInput()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            transform.Rotate(0, -90, 0);
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            transform.Rotate(0, 90, 0); ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);

            collectibleSound.Play();

            SpawnFruit();

            moveDelay -= 0.01f;

            Grow();
        }

        if (other.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
            GameOver();
        }

        if (other.gameObject.CompareTag("Tail"))
        {
            Destroy(gameObject);
            GameOver();
        }
    }

    private void GameOver()
    {
        restartButton.SetActive(true);
        gameOverText.SetActive(true);
        levelMusic.Stop();
    }

    private Vector3 GetRandomPos()
    {
        int x = Random.Range(-gridSize, gridSize);
        int z = Random.Range(-gridSize, gridSize);

        return new Vector3(x * cellSize, 0, z * cellSize);
    }

    private void SpawnFruit()
    {
        Vector3 fruitPos = GetRandomPos();
        Instantiate(fruitPrefab, fruitPos, Quaternion.identity);
    }

    private void Grow()
    {
        Vector3 spawnPos;

        if (bodySegments.Count > 0)
        {
            spawnPos = bodySegments[bodySegments.Count - 1].transform.position;
        } 
        else
        {
            spawnPos = transform.position - transform.forward * cellSize;
        }

        GameObject newSegment = Instantiate(tailPrefab, spawnPos, Quaternion.identity);

        Body_And_Tail segment = newSegment.GetComponent<Body_And_Tail>();

        segment.head = this;

        bodySegments.Add(newSegment);

        RecalculateIndexes();
    }

    private void RecalculateIndexes()
    {
        for (int i = 0; i < bodySegments.Count; i++)
        {
            Body_And_Tail segment = bodySegments[i].GetComponent<Body_And_Tail>();
            segment.index = i + 1;

        }
    }
}
