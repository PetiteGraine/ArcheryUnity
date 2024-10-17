using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 spawnArea;
    public GameObject target;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        gameObject.transform.position = spawnPosition + new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), Random.Range(-spawnArea.z, spawnArea.z));
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