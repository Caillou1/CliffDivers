using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerController : MonoBehaviour {
    public float MovementSpeed;
    public float JumpForce;

    private bool isPaused;
    private GameObject timber;
    private bool hasBonus;
    private Bonus bonus;
    private SoundManager sm;
    private Transform tf;
    private Rigidbody rb;
    private Text pause;
    private BlurOptimized blur;

    void Start()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        tf = transform;
        rb = GetComponent<Rigidbody>();
        hasBonus = false;
        StartCoroutine(WaitForBonus());
        timber = tf.FindChild("Timber").gameObject;
        isPaused = false;
        pause = GameObject.Find("Pause").GetComponent<Text>();
        blur = Camera.main.GetComponent<BlurOptimized>();
    }

    void Update()
    {
        if(IsGrounded())rb.velocity = new Vector3(Input.GetAxis("Horizontal") * MovementSpeed, 0, 0);

        if (Input.GetButtonDown("Jump") && !hasBonus)
        {
            Jump();
        }

        if(hasBonus && Input.GetButtonDown("Jump"))
        {
            LaunchBonus();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pause.enabled = false;
            PauseSpawner();
            PauseEnemies();
            blur.enabled = false;
        } else
        {
            isPaused = true;
            Time.timeScale = 0;
            pause.enabled = true;
            PauseEnemies();
            blur.enabled = true;
        }
    }

    void PauseSpawner()
    {
        GameObject.Find("Spawner").GetComponent<SpawnerScript>().Pause();
    }

    void PauseEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Suicide");
        foreach (var enemy in enemies)
        {
            var e = enemy.GetComponent<Enemy>();
            e.Pause();
        }
    }

    void Jump()
    {
        if(IsGrounded())
        {
            sm.PlayJump();
            rb.velocity = new Vector3();
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            rb.AddForce(Vector3.up * JumpForce);
            StartCoroutine(WaitForGround());
        }
    }

    void LaunchBonus()
    {
        hasBonus = false;
        StartCoroutine(WaitForBonus());
        bonus.Launch();
    }

    public void SetHasBonus(bool has)
    {
        hasBonus = has;
    }

    public void ReduceTimber()
    {
        timber.transform.localScale = new Vector3(timber.transform.localScale.x, timber.transform.localScale.y * 0.8f, timber.transform.localScale.z);
    }

    IEnumerator WaitForGround()
    {
        yield return new WaitForSeconds(.5f);
        yield return new WaitUntil(IsGrounded);
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        rb.velocity = new Vector3();
    }

    IEnumerator WaitForBonus()
    {
        yield return new WaitUntil(() => hasBonus);
        bonus = tf.FindChild("Timber").GetComponentInChildren<Bonus>();
    }

    bool IsGrounded()
    {
        return tf.position.y < .6;
    }
}
