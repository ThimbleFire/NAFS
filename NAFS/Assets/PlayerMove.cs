using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    public PlayerAnimator pAnimator;

    private void Update()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            velocity += Vector3.left * Time.smoothDeltaTime * walkSpeed;
        }
        if (Input.GetKey(KeyCode.D)) {
            velocity += Vector3.right * Time.smoothDeltaTime * walkSpeed;
        }
        if (Input.GetKey(KeyCode.W)) {
            velocity += Vector3.up * Time.smoothDeltaTime * walkSpeed;
        }
        if (Input.GetKey(KeyCode.S)) {
            velocity += Vector3.down * Time.smoothDeltaTime * walkSpeed;
        }

        transform.Translate(velocity);
        pAnimator.UpdateVelocity(velocity.normalized);
    }
}
