using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTeleportIfGrounded : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;

    private void LateUpdate()
    {
        if (gameObject.transform.position.y <= 0.25f)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
        }
    }
}
