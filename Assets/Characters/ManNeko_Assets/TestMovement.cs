using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    [SerializeField] private float speed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float dir = Input.GetAxis("Vertical");

        Vector3 vector = new ( dir, 0f, 0f);

        if (dir != 0)
        {
            rigidbody.velocity = speed * Time.deltaTime * vector;
            animator.SetBool("isRunning", true);
        }
        else
            animator.SetBool("isRunning", false);
    }
}
