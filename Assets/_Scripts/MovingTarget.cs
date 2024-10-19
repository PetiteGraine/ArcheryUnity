using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public GameObject spawner;
    public GameObject target;
    public float startSize = 10;
    public float speed = 2;
    public float timeChasingPosMin = 1;
    public float timeChasingPosMax = 3;
    private float timePassed = 0;
    private float timeChasingPos;
    private Vector3 newPos;
    private Vector3 pos1;
    private Vector3 pos2;
    [SerializeField] private GameObject soundEffect;

    private void Start()
    {
        SetGameMode();
        Vector3 sizeVector = new Vector3(startSize, startSize, startSize);
        transform.localScale = sizeVector;
        float machin = transform.localScale.x * (1.15f / 8);
        float x = spawner.transform.localScale.x / 2 - machin;
        float y = spawner.transform.localScale.y / 2 - machin;
        float z = spawner.transform.localScale.z / 2;
        transform.position = spawner.transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), Random.Range(-z, z));
        timeChasingPos = Random.Range(timeChasingPosMin, timeChasingPosMax);
        pos1 = new Vector3(-spawner.transform.localScale.x / 2, transform.position.y, transform.position.z);
        pos2 = new Vector3(spawner.transform.localScale.x / 2, transform.position.y, transform.position.z);
        if (Random.Range(0, 1) == 1)
            newPos = pos1;
        else newPos = pos2;
    }

    void Update()
    {
        MoveUpdate();
        timePassed += Time.deltaTime;
        if (timePassed > timeChasingPos)
        {
            ChangeNewPos();
            timeChasingPos = Random.Range(timeChasingPosMin, timeChasingPosMax);
            timePassed = 0f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject spawnedEffect = Instantiate(soundEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            Destroy(collision.gameObject);
            Instantiate(target);
            Destroy(gameObject);
        }
    }

    private void MoveUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }

    private void ChangeNewPos()
    {
        if (newPos == pos1)
            newPos = pos2;
        else
            newPos = pos1;
    }

    private void SetGameMode()
    {

        switch (PlayerPrefs.GetString("mode"))
        {
            case "easy":
                SetEasyMode();
                break;
            case "medium":
                SetMediumMode();
                break;
            case "hard":
                SetHardMode();
                break;
            case "training":
                SetTrainingMode();
                break;
            default:
                SetTrainingMode();
                break;
        }
    }

    private void SetTrainingMode()
    {
        startSize = 10;
        speed = 0;
    }

    private void SetEasyMode()
    {
        startSize = 10;
        speed = 2;
        timeChasingPosMin = 1;
        timeChasingPosMax = 3;
    }

    private void SetMediumMode()
    {
        startSize = 8;
        speed = 3;
        timeChasingPosMin = 0.5f;
        timeChasingPosMax = 2;
    }

    private void SetHardMode()
    {
        startSize = 6;
        speed = 4;
        timeChasingPosMin = 0.3f;
        timeChasingPosMax = 1.5f;
    }
}