using UnityEngine;
using UnityEngine.InputSystem;

public class HandsAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference triggerReference;
    [SerializeField] private Animator animator;

    private void Start() => animator = GetComponent<Animator>();

    void Update()
    {
        float gripValue = gripReference.action.ReadValue<float>();
        float triggerValue = triggerReference.action.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);
        animator.SetFloat("Trigger", triggerValue);
    }
}
