using UnityEngine;

public class BowTeleportIfGrounded : MonoBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;

    private void LateUpdate()
    {
        if (gameObject.transform.position.y <= 0.25f)
        {
            gameObject.transform.position = Position;
            gameObject.transform.rotation = Rotation;
        }
    }
}
