using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    public int Points = 10;
    public GameObject Target;
    [SerializeField] private GameObject _soundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            GameObject spawnedEffect = Instantiate(_soundEffect);
            spawnedEffect.transform.position = gameObject.transform.position;
            Manager.Instance.UpdateScore(Points);
            Destroy(collision.gameObject);
            Instantiate(Target);
            if (gameObject.transform.parent != null)
                Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}