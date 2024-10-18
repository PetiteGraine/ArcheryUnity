using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public GameObject spawner;
    public GameObject target;
    public float speed = 3;
    public float timeChasingPosMin;
    public float timeChasingPosMax;
    private float timePassed = 0;
    private float timeChasingPos;
    private Vector3 newPos;
    private Vector3 pos1;
    private Vector3 pos2;

    [SerializeField]
    private AudioSource audioSource;


    private void Awake()
    {
        float machin = transform.localScale.x * (1.15f / 8);
        float x = spawner.transform.localScale.x / 2 - machin;
        float y = spawner.transform.localScale.y / 2 - machin;
        float z = spawner.transform.localScale.z / 2;
        transform.position = spawner.transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), Random.Range(-z, z));
        timeChasingPos = Random.Range(timeChasingPosMin, timeChasingPosMax);
    }

    private void Start()
    {
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
            audioSource.PlayOneShot(audioSource.clip, 1f);
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
}