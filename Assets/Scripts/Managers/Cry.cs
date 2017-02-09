using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cry : MonoBehaviour {

    private SoundManager sm;
    private ScoreManager scoreManager;

	void Start ()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Suicide"))
        {
            sm.PlayCry();
            other.GetComponent<Enemy>().Ragdoll();
            scoreManager.AddScore(-1);
            Destroy(other.gameObject, 1);
        }

        if(other.CompareTag("Bucheron"))
        {
            sm.PlayRobot();
            other.GetComponent<Enemy>().Ragdoll();
            scoreManager.AddScore(1);
            Destroy(other.gameObject, 1);
        }
    }
}
