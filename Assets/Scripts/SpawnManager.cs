using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [Header("Things to Spawn")]
    //these will spawn for the player to "collect"
    public GameObject neutron;
    public GameObject proton;

    //these will spawn during decay,
    public GameObject electron;
    public GameObject neutrino;


    [Header("Spawn Stats")]
    public float timeToSpawn;
    private float spawnTime;
    public GameObject spawnCenter;
    public float spawnOffset;


    public float particleTimeToSpawn;
    private float particleSpawnTime;


    // Start is called before the first frame update
    void Start()
    {
        spawnTime = timeToSpawn;
        particleSpawnTime = particleTimeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance().gameOver == false)
        {
            spawnNucleons();
            spawnParticles();
        }
    }

    private void spawnNucleons()
    {
        if (spawnTime <= 0)
        {
            //pick random direction from spawn center (angle) and turn it into a heading
            float randomAngle = Random.Range(-180, 180) * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);

            //normalize and extend vector to far away
            dir = dir.normalized * spawnOffset;

            //shift by center of spawnCenter object
            Vector3 spawnPos = spawnCenter.transform.position + dir;

            //instantiate neutron or proton
            float randNum = Random.Range(0f, 1f);
            GameObject toSpawn = (randNum < 0.5f) ? neutron : proton;
            GameObject go = Instantiate(toSpawn, spawnPos, Quaternion.identity);
            go.GetComponent<Mover>().nucleonType = (randNum < 0.5f) ? "neutron" : "proton";


            //set movement direction of newly spawned particle
            Vector3 randomOffset = new Vector3(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f), 0f);
            go.GetComponent<Mover>().setInitialMovement(-(dir.normalized + randomOffset).normalized);


            spawnTime = timeToSpawn;
        }
        spawnTime -= Time.deltaTime;
    }


    public void spawnParticles()
    {
        if (particleSpawnTime <= 0)
        {

            float randomAngle = Random.Range(-180, 180) * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);

            //normalize and extend vector to far away
            dir = dir.normalized * spawnOffset;

            //shift by center of spawnCenter object
            Vector3 spawnPos = spawnCenter.transform.position + dir;

            //instantiate neutron or proton
            float randNum = Random.Range(0f, 1f);
            GameObject toSpawn = (randNum < 0.3f) ? electron : neutrino;
            GameObject go = Instantiate(toSpawn, spawnPos, Quaternion.identity);


            //set movement direction of newly spawned particle
            Vector3 randomOffset = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f);

            particleSpawnTime = particleTimeToSpawn;
        }


        particleSpawnTime -= Time.deltaTime;
    }
}
