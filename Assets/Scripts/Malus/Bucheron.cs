using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucheron : Enemy {

    private GameObject Explosion;
    private SkinnedMeshRenderer[] meshes;
    private PlayerController player;

    void Start () {
        Create();
        player = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
        meshes = tf.GetComponentsInChildren<SkinnedMeshRenderer>();
        Explosion = tf.FindChild("Explosion").gameObject;
    }
	
	void Update () {
        Move();
    }

    private void Execute()
    {
        player.ReduceTimber();
        PlayExplosion();
    }

    private void HideMesh()
    {
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
    }

    private void PlayExplosion()
    {
        Movement = new Vector3();
        HideMesh();
        var particle = Explosion.GetComponent<ParticleSystem>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        particle.Play(true);
        sm.PlayExplosion();
        StopAllCoroutines();
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Timber"))
        {
            Execute();
        }
    }
}
