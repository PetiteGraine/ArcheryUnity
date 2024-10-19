
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    public float lifeTime = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

}
