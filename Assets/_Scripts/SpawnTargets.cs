using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    public GameObject target;
    private void Awake()
    {
        Instantiate(target);
        Instantiate(target);
        Instantiate(target);
        Instantiate(target);
        Instantiate(target);
    }
}