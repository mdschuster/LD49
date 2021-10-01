using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToTarget : MonoBehaviour
{


    public GameObject target;
    private Vector3 targetPosition;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = this.transform.position;
        targetPosition = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = target.transform.position;
        currentPosition.x = targetPosition.x;
        currentPosition.y = targetPosition.y;
        currentPosition.z = this.transform.position.z;
        this.transform.position = currentPosition;
    }
}
