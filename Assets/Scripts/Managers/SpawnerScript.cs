using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {
    public float MaxX;
    public float SpawnEverySeconds;
    public float TrickySpawnChance;
    public float FastSpawnChance;
    public GameObject TrickyEnemy;
    public GameObject FastEnemy;
    public GameObject SadEnemy;

    public GameObject[] Bonus;
    public float BonusSpawnChance;

    private bool isPaused;
    private Transform tf;

	void Start () {
        tf = transform;
        StartCoroutine(Spawn());
	}

    public void Pause()
    {
        if(isPaused)
        {
            isPaused = false;
            StartCoroutine(Spawn());
        } else
        {
            isPaused = true;
            StopAllCoroutines();
        }
    }

    IEnumerator Spawn()
    {
        GameObject spawn;

        if(Random.value <= BonusSpawnChance)
        {
            Instantiate(Bonus[Random.Range(0,Bonus.Length)], new Vector3(Random.Range(-MaxX, MaxX), .5f, 30), new Quaternion(0, 180, 0, 0), tf);
        }

        float rand = Random.value;

        if (rand <= TrickySpawnChance)
            spawn = TrickyEnemy;
        else if (rand >= 1 - FastSpawnChance)
            spawn = FastEnemy;
        else
            spawn = SadEnemy;

        Instantiate(spawn, new Vector3(Random.Range(-MaxX, MaxX), .5f, 30), new Quaternion(0,180,0,0), tf);
        yield return new WaitForSeconds(SpawnEverySeconds);
        StartCoroutine(Spawn());
    }
}
