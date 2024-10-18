using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public GameObject spawner;
    public GameObject target;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        float machin = gameObject.transform.localScale.x * (1.15f / 8);
        float x = spawner.transform.localScale.x / 2 - machin;
        float y = spawner.transform.localScale.y / 2 - machin;
        float z = spawner.transform.localScale.z / 2;
        gameObject.transform.position = spawner.transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), Random.Range(-z, z));

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
}