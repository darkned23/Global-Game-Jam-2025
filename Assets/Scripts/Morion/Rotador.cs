using UnityEngine;

public class Rotador : MonoBehaviour
{
    public Vector3 vel;

    void Update()
    {
        transform.Rotate(vel * Time.deltaTime);
    }
}
