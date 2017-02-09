using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveGirl : Bonus {

    public float MovementSpeed;
    public float TimeBeforeExplosion;
    public float ExplosionRadius;

    private SkinnedMeshRenderer[] meshes;
    private Transform tf;
    private Rigidbody rb;
    private ParticleSystem particle;
    private ParticleSystem flames;
    private Vector3 Movement;
    private bool isCatched;
    private bool isLaunched;

	void Start () {
        Create();
        tf = transform;
        particle = tf.FindChild("Explosion").GetComponent<ParticleSystem>();
        Movement = Vector3.forward * Time.deltaTime * MovementSpeed;
        isCatched = false;
        isLaunched = false;
        meshes = tf.GetComponentsInChildren<SkinnedMeshRenderer>();
        rb = GetComponent<Rigidbody>();
        flames = transform.FindChild("Flames").GetComponent<ParticleSystem>();
    }
	
	void Update () {
        tf.Translate(Movement);
	}

    void Catch(Transform parent)
    {
        isCatched = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        Movement = new Vector3();
        tf.Rotate(0, 180, 0);
        tf.SetParent(parent);
        tf.localPosition = new Vector3(0,0,1.5f);
    }

    public override void Launch()
    {
        flames.Play();
        tf.SetParent(GameObject.Find("Spawner").transform);
        Movement = Vector3.forward * Time.deltaTime * MovementSpeed;
        StartCoroutine(TriggerExplosion());
        isLaunched = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        sm.PlayGirlOnFire();
    }

    void HideMesh()
    {
        foreach(SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
    }

    void ThrowNearbyEnemies()
    {
        var tab = Physics.OverlapSphere(tf.position, ExplosionRadius);
        foreach (var t in tab)
        {
            var enemy = t.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Ragdoll();
                enemy.ThrowCharacter();
            }
        }
    }

    public bool IsLaunched()
    {
        return isLaunched;
    }

    IEnumerator TriggerExplosion()
    {
        yield return new WaitForSeconds(TimeBeforeExplosion);
        Destroy(flames.gameObject);
        HideMesh();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        MovementSpeed = 0;
        particle.Play(true);
        sm.PlayExplosion();
        ThrowNearbyEnemies();
        Kill();
    }

    private void Kill()
    {
        StopAllCoroutines();
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isCatched)
        {
            Transform colTrans = collision.transform;
            if (colTrans.CompareTag("Timber"))
            {
                Catch(colTrans);
                colTrans.parent.GetComponent<PlayerController>().SetHasBonus(true);
            }
        }
    }
}
