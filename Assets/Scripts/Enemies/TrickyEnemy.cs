using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyEnemy : Enemy {

    public float JumpForce;

    private void Start()
    {
        Create();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitForJump());
    }

    IEnumerator WaitForJump()
    {
        yield return new WaitUntil(() => tf.position.z <= -2.5f);
        Jump();
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * JumpForce);
        sm.PlayJump();
    }
}
