using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyToBackMenu : MonoBehaviour
{
    [SerializeField] private GameObject _soundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject spawnedEffect = Instantiate(_soundEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            SceneManager.LoadScene("Main menu");
        }
    }
}
