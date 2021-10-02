using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{

    public float timeToDeath = 0;

    private float deathTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        deathTimer = timeToDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer <= 0)
        {
            Destroy(this.gameObject);
        }
        deathTimer -= Time.deltaTime;
    }
}

