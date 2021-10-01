using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{


    [Header("Player Data")]
    private int numNeutrons;
    private int numProtons;

    public float speed;

    private Vector3 moveAmount;

    new private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        moveAmount = Vector3.zero;
        numNeutrons = 0;
        numProtons = 1;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getMovement();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        movePlayer();
    }

    private void getMovement()
    {
        moveAmount.x = Input.GetAxisRaw("Horizontal");
        moveAmount.y = Input.GetAxisRaw("Vertical");
        moveAmount.Normalize();
    }

    private void movePlayer()
    {
        rigidbody.MovePosition(this.transform.position + moveAmount * speed * Time.deltaTime);
    }
}
