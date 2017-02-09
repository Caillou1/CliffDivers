using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public float MovementSpeed;
    public float TimeBeforeDestroy;

    protected bool isPaused;
    protected Transform tf;
    protected Rigidbody rb;
    protected SoundManager sm;
    protected Vector3 Movement;

    private ScoreManager scoreManager;

	void Start ()
    {
        Create();
    }

    public void Create()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        tf = transform;
        rb = GetComponent<Rigidbody>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        Movement = Vector3.forward * Time.deltaTime * MovementSpeed;
        isPaused = false;
    }
	
	void Update () {
        if(!isPaused) Move();
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    protected virtual void Move()
    {
        tf.Translate(Movement);
    }

    public void Ragdoll()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    public void ThrowCharacter()
    {
        rb.AddForce(new Vector3(0, 1000, 1000));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Timber") || collision.transform.CompareTag("Player"))
        {
            sm.PlayPunch();
            scoreManager.AddScore(1);
            MovementSpeed = 0;
            Ragdoll();
            ThrowCharacter();
            Kill();
        }
    }

    protected void Kill()
    {
        StopAllCoroutines();
        Destroy(gameObject, TimeBeforeDestroy);
    }
}
