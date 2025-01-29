using UnityEngine;

public class Borrar : MonoBehaviour
{
    public Animator animatorController;

    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animatorController != null)
        {
            animatorController.SetBool("Morir", true);
        }
    }
}