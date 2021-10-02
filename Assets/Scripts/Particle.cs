using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    private Vector3 moveAmount;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        float randomAngle = Random.Range(-180, 180) * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        moveAmount = dir.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        //moveAmount = dir.normalized;
        pos.x += moveAmount.x * Time.deltaTime;
        pos.y += moveAmount.y * Time.deltaTime;
        this.transform.position = pos;
    }
}
