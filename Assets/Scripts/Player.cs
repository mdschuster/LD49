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

    private Dictionary<int, string> isotopeDict;

    // Start is called before the first frame update
    void Start()
    {
        isotopeDict = new Dictionary<int, string>();
        moveAmount = Vector3.zero;
        numNeutrons = 0;
        numProtons = 1;
        rigidbody = GetComponent<Rigidbody>();
        Mover.nucleonAdded += addNucleon;
        setupDict();
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

    public void addNucleon(string type)
    {
        if (type == "proton")
        {
            numProtons++;
            GameManager.instance().changeProtonText(numProtons);
        }
        else if (type == "neutron")
        {
            numNeutrons++;
            GameManager.instance().changeNeutronText(numNeutrons);
        }

        string isotope = computeNewIsotope();
        GameManager.instance().changeIsotopeText(isotope + "-" + (numNeutrons + numProtons).ToString());
    }

    private string computeNewIsotope()
    {
        string value = "";
        isotopeDict.TryGetValue(numProtons, out value);
        return value;
    }



    private void setupDict()
    {
        isotopeDict.Add(1, "Hydrogen");
        isotopeDict.Add(2, "Helium");
        isotopeDict.Add(3, "Lithium");
        isotopeDict.Add(4, "Beryllium");
        isotopeDict.Add(5, "Boron");
        isotopeDict.Add(6, "Carbon");
        isotopeDict.Add(7, "Nitrogen");
        isotopeDict.Add(8, "Oxygen");
        isotopeDict.Add(9, "Fluorine");
        isotopeDict.Add(10, "Neon");
        isotopeDict.Add(11, "Sodium");
        isotopeDict.Add(12, "Magnesium");
        isotopeDict.Add(13, "Aluminum");
        isotopeDict.Add(14, "Silicon");
        isotopeDict.Add(15, "Phosphorus");
        isotopeDict.Add(16, "Sulfur");
        isotopeDict.Add(17, "Chlorine");
        isotopeDict.Add(18, "Argon");
        isotopeDict.Add(19, "Potassium");
        isotopeDict.Add(20, "Calcium");
        isotopeDict.Add(21, "Scandium");
        isotopeDict.Add(22, "Titanium");
        isotopeDict.Add(23, "Vanadium");
    }
}
