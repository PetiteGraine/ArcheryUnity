using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public int pointgagne = 10;
    public GameObject target;
    [SerializeField] private GameObject soundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject spawnedEffect = Instantiate(soundEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            Manager.instance.UpdateScore(pointgagne);
            Destroy(collision.gameObject);
            Instantiate(target);
            Destroy(gameObject);
        }
    }
}
