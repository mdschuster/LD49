using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jitter : MonoBehaviour
{
    public float jitterAmount;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float randomAngle = Random.Range(-180, 180) * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
        dir = dir.normalized * jitterAmount;
        this.transform.localPosition = dir;
    }
}
