using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Salud salud;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
