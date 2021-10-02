using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{


    [Header("Player Data")]
    private int numNeutrons;
    private int numProtons;

    public Nucleus currentNucleus;

    public float speed;

    private Vector3 moveAmount;
    private float timeToDecay;
    private float decayTime;


    new private Rigidbody rigidbody;
    private List<Mover> children = new List<Mover>();

    private Dictionary<int, string> isotopeDict;

    // Start is called before the first frame update
    void Start()
    {
        isotopeDict = new Dictionary<int, string>();
        moveAmount = Vector3.zero;
        numNeutrons = 0;
        numProtons = currentNucleus.protons;
        rigidbody = GetComponent<Rigidbody>();
        Mover.nucleonAdded += addNucleon;
        setupDict();
    }

    // Update is called once per frame
    void Update()
    {
        getMovement();
        decay();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        movePlayer();
    }

    public void decay()
    {

        if (decayTime <= 0 && currentNucleus.decayType[numNeutrons] != "Stable")
        {
            print("Decay Called");

            string decayType = currentNucleus.decayType[numNeutrons];
            List<GameObject> nucleonsToChange = new List<GameObject>();
            switch (decayType)
            {
                case ("B-"):
                    break;
                case ("B+"):
                    break;
                case ("p"):
                    //find a proton
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "proton")
                        {
                            nucleonsToChange.Add(m.gameObject);
                            break;
                        }
                    }
                    break;
                case ("n"):
                    //find a neutron
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "neutron")
                        {
                            nucleonsToChange.Add(m.gameObject);
                            break;
                        }
                    }
                    break;
                case ("2p"):
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "proton")
                        {
                            nucleonsToChange.Add(m.gameObject);
                        }
                        if (nucleonsToChange.Count == 2) break;
                    }
                    break;
                case ("2n"):
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "neutron")
                        {
                            nucleonsToChange.Add(m.gameObject);
                        }
                        if (nucleonsToChange.Count == 2) break;
                    }
                    break;
                case ("3p"):
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "proton")
                        {
                            nucleonsToChange.Add(m.gameObject);
                        }
                        if (nucleonsToChange.Count == 3) break;
                    }
                    break;
                case ("3n"):
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "neutron")
                        {
                            nucleonsToChange.Add(m.gameObject);
                        }
                        if (nucleonsToChange.Count == 3) break;
                    }
                    break;
                case ("4p"):
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "proton")
                        {
                            nucleonsToChange.Add(m.gameObject);
                        }
                        if (nucleonsToChange.Count == 4) break;
                    }
                    break;
                case ("4n"):
                    foreach (Mover m in children)
                    {
                        if (m.nucleonType == "neutron")
                        {
                            nucleonsToChange.Add(m.gameObject);
                        }
                        if (nucleonsToChange.Count == 4) break;
                    }
                    break;
                default:
                    break;
            } //end of switch


            foreach (GameObject go in nucleonsToChange)
            {
                go.GetComponent<Mover>().decay();
                removeNucleon(go.GetComponent<Mover>());
            }
        }
        decayTime -= Time.deltaTime;
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

    public void addNucleon(Mover nucleon)
    {
        if (nucleon.nucleonType == "proton")
        {
            numProtons++;
            GameManager.instance().changeProtonText(numProtons);
            changeCurrentNucleus();
        }
        else if (nucleon.nucleonType == "neutron")
        {
            numNeutrons++;
            GameManager.instance().changeNeutronText(numNeutrons);
        }
        children.Add(nucleon);

        GameManager.instance().changeIsotopeText(currentNucleus.name + "-" + (numNeutrons + numProtons).ToString());

        if (numNeutrons > currentNucleus.neutrons.Count)
        {
            //decay neutron immediately
            decayTime = -1f;
        }
        else
        {
            decayTime = currentNucleus.decayTime[numNeutrons];
        }
    }

    public void removeNucleon(Mover nucleon)
    {
        if (nucleon.nucleonType == "proton")
        {
            numProtons--;
            GameManager.instance().changeProtonText(numProtons);
            changeCurrentNucleus();
        }
        else if (nucleon.nucleonType == "neutron")
        {
            numNeutrons--;
            GameManager.instance().changeNeutronText(numNeutrons);
        }
        children.Remove(nucleon);

        GameManager.instance().changeIsotopeText(currentNucleus.name + "-" + (numNeutrons + numProtons).ToString());

    }

    private void changeCurrentNucleus()
    {
        currentNucleus = GameManager.instance().getNucleus(numProtons);
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
