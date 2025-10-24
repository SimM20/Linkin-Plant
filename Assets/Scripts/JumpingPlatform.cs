using UnityEngine;
using System.Collections;

public class JumpingPlatform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpDuration = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponent<CharacterController>();
        if (controller != null)
            StartCoroutine(ApplyImpulse(controller));
    }

    private void OnTriggerStay(Collider other)
    {
        var controller = other.GetComponent<CharacterController>();
        if (controller != null)
            controller.Move(Vector3.up * jumpForce * Time.deltaTime);
    }


    private IEnumerator ApplyImpulse(CharacterController controller)
    {
        float timer = 0f;
        while (timer < jumpDuration)
        {
            controller.Move(Vector3.up * jumpForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
