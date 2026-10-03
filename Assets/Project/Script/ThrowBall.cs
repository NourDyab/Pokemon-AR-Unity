using UnityEngine;
using UnityEngine.InputSystem;


public class PokeballThrow : MonoBehaviour
{
    public GameObject pokeballPrefab;
    public Transform arCamera;
    public GameObject uiBall;

    public float forwardForce = 8f;
    public float upwardForce = 2f;

    void Update()
    {
        bool touch = Touchscreen.current != null &&
                     Touchscreen.current.primaryTouch.press.wasReleasedThisFrame;

        bool mouse = Mouse.current != null &&
                     Mouse.current.leftButton.wasReleasedThisFrame;

        if (touch || mouse)
        {
            Throw();
        }
    }

    void Throw()
    {
        if (uiBall != null)
            uiBall.SetActive(false);

        GameObject ball = Instantiate(
            pokeballPrefab,
            arCamera.position + arCamera.forward * 0.5f + Vector3.down * 0.2f,
            Quaternion.identity
        );

        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 throwDir = arCamera.forward + Vector3.up * 0.2f;

            rb.linearVelocity = throwDir.normalized * forwardForce;
            rb.angularVelocity = Random.insideUnitSphere * 5f;
        }
    }

    public void ResetUI()
    {
        if (uiBall != null)
            uiBall.SetActive(true);
    }
}