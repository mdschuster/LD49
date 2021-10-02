using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mover : MonoBehaviour
{

    public enum ParticleState
    {
        FREE, CAPTURED
    }


    [Header("Stats")]
    public float speed;
    public ParticleState currentState;

    private Vector3 moveAmount;

    new private Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        currentState = ParticleState.FREE;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (currentState == ParticleState.FREE)
        {
            Vector3 pos = this.transform.position;
            pos.x += moveAmount.x * speed * Time.deltaTime;
            pos.y += moveAmount.y * speed * Time.deltaTime;
            this.transform.position = pos;
        }
    }

    public void setInitialMovement(Vector3 dir)
    {
        moveAmount = dir;
    }
}
