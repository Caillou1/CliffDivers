using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadEnemy : Enemy
{
    public float TimeBeforeNewMovement;

    private void Start()
    {
        Create();
        Movement = Vector3.forward * MovementSpeed * Time.deltaTime;
        StartCoroutine(FindNewDirection());
        StartCoroutine(CheckForLimit());
    }

    IEnumerator FindNewDirection()
    {
        yield return new WaitForSeconds(TimeBeforeNewMovement);
        Movement = new Vector3(UnityEngine.Random.Range(-1, 2) * MovementSpeed * Time.deltaTime, 0, Time.deltaTime * MovementSpeed);
        StartCoroutine(FindNewDirection());
    }

    IEnumerator CheckForLimit()
    {
        yield return new WaitUntil(() => tf.position.x < -4.5 || tf.position.x > 4.5);

        StopCoroutine(FindNewDirection());

        if (tf.position.x < -4.5)
            Movement = new Vector3(-MovementSpeed/2 * Time.deltaTime, 0, Time.deltaTime * MovementSpeed);
        if (tf.position.x > 4.5)
            Movement = new Vector3(MovementSpeed/2 * Time.deltaTime, 0, Time.deltaTime * MovementSpeed);
        StartCoroutine(WaitForCheckLimit());
        StartCoroutine(FindNewDirection());
    }

    IEnumerator WaitForCheckLimit()
    {
        yield return new WaitUntil(() => tf.position.x > -4.5 && tf.position.x < 4.5);
        StartCoroutine(CheckForLimit());
    }

    protected override void Move()
    {
        tf.Translate(Movement);
    }
}
