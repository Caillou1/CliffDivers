using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour {
    protected SoundManager sm;

    protected void Create()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public abstract void Launch();
}
