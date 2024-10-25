using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    public GameObject Target;
    private void Awake()
    {
        Instantiate(Target);
        Instantiate(Target);
        Instantiate(Target);
        Instantiate(Target);
        Instantiate(Target);
    }
}
