using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private GameObject soundEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject spawnedEffect = Instantiate(soundEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            Manager.instance.UpdateScore(10);
            Destroy(collision.gameObject);
            Instantiate(target);
            Destroy(gameObject);
        }
    }
}
