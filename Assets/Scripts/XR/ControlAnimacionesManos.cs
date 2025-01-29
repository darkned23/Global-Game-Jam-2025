using UnityEngine;
using UnityEngine.InputSystem;

public class ControlAnimacionesManos : MonoBehaviour
{
    public InputActionProperty trigger;
    public InputActionProperty grab;

    public Animator animator;

    void Start()
    {
        trigger.action.Enable();
        grab.action.Enable();
    }

    void Update()
    {
        animator.SetFloat("Trigger", trigger.action.ReadValue<float>());
        animator.SetFloat("Grab", grab.action.ReadValue<float>());
    }
}
