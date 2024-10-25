using UnityEngine;

public class DestroySound : MonoBehaviour
{
    public float LifeTime = 1;
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }
}
