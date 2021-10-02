using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mover : MonoBehaviour
{

    public enum ParticleState
    {
        FREE, CAPTURED, NUCLEUS, DECAY
    }


    [Header("Stats")]
    public float speed;
    public ParticleState currentState;

    public string nucleonType;

    private Vector3 moveAmount;

    new private Rigidbody rigidbody;

    public bool coreNucleon = false;

    public static event Action<Mover> nucleonAdded;


    // Start is called before the first frame update
    void Start()
    {
        currentState = ParticleState.FREE;
        rigidbody = GetComponent<Rigidbody>();
        if (this.transform.parent != null)
        {
            coreNucleon = true;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (coreNucleon)
        {
            this.transform.localPosition = Vector3.zero;
            return;
        }
        if (currentState == ParticleState.FREE)
        {
            Vector3 pos = this.transform.position;
            pos.x += moveAmount.x * speed * Time.deltaTime;
            pos.y += moveAmount.y * speed * Time.deltaTime;
            this.transform.position = pos;
        }
        else if (currentState == ParticleState.CAPTURED || currentState == ParticleState.NUCLEUS)
        {
            rigidbody.velocity = Vector3.zero;
            //this.transform.position = this.transform.parent.position;
            Vector3 dir = -this.transform.localPosition;
            moveAmount = dir.normalized;
            //rigidbody.MovePosition(this.transform.position + moveAmount * Time.deltaTime);
            //rigidbody.AddRelativeForce(dir.normalized * speed, ForceMode.Force);
            Vector3 pos = this.transform.localPosition;
            pos.x += moveAmount.x * speed * Time.deltaTime;
            pos.y += moveAmount.y * speed * Time.deltaTime;
            this.transform.localPosition = pos;

        }
        else if (currentState == ParticleState.DECAY)
        {
            //pick random direction
            Vector3 dir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0f) - this.transform.position;
            Vector3 pos = this.transform.position;
            //moveAmount = dir.normalized;
            pos.x += moveAmount.x * speed * Time.deltaTime;
            pos.y += moveAmount.y * speed * Time.deltaTime;
            this.transform.position = pos;
        }
    }

    public void setInitialMovement(Vector3 dir)
    {
        moveAmount = dir;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (currentState == ParticleState.FREE && other.CompareTag("Player") && currentState != ParticleState.DECAY)
        {
            currentState = ParticleState.CAPTURED;
            this.transform.SetParent(other.transform);
            nucleonAdded?.Invoke(this);

        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (currentState == ParticleState.CAPTURED && other.gameObject.CompareTag("nucleon") && currentState != ParticleState.DECAY)
        {
            //moveAmount = Vector3.zero;
            currentState = ParticleState.NUCLEUS;
            //rigidbody.isKinematic = true;
        }
    }

    public void decay()
    {
        currentState = ParticleState.DECAY;
        rigidbody.isKinematic = true;
        speed = 30;
        transform.parent = null;
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponentInChildren<Jitter>().enabled = false;
    }
}
