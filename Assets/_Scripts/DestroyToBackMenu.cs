using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyToBackMenu : MonoBehaviour
{
    [SerializeField] private GameObject soundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject spawnedEffect = Instantiate(soundEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            SceneManager.LoadScene("Main menu");
        }
    }
}