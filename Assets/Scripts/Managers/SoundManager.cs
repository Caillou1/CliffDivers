using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Cry;
    public AudioClip Jump;
    public AudioClip Explosion;
    public AudioClip RobotCry;
    public AudioClip GirlOnFire;
    public AudioClip[] Punches;

    private static SoundManager instance = null;

    private AudioSource source;
	
	void Awake () {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
	}

    public void PlayGirlOnFire()
    {
        source.PlayOneShot(GirlOnFire);
    }

    public void PlayRobot()
    {
        source.PlayOneShot(RobotCry);
    }

    public void PlayCry()
    {
        source.PlayOneShot(Cry);
    }

    public void PlayPunch()
    {
        source.PlayOneShot(Punches[Random.Range(0, Punches.Length - 1)]);
    }

    public void PlayJump()
    {
        source.PlayOneShot(Jump);
    }

    public void PlayExplosion()
    {
        source.PlayOneShot(Explosion);
    }
}
