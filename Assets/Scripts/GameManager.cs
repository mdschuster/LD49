using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{

    //singleton part
    private static GameManager _instance = null;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager instance()
    {
        return _instance;
    }


    [Header("Player Information")]
    public GameObject playerPrefab;
    private GameObject player;

    [Header("Camera")]
    public CinemachineVirtualCameraBase vcam;

    [Header("UI Information")]
    public TMP_Text neutronValue;
    public TMP_Text protonValue;
    public TMP_Text isotopeValue;
    public TMP_Text decayText;

    [Header("Possible Nuclei")]
    public List<Nucleus> nuclei;

    [Header("Prefabs")]
    public GameObject betaNeutronPrefab;
    public GameObject betaProtonPrefab;
    public GameObject neutrinoPrefab;
    public GameObject electronPrefab;



    // Start is called before the first frame update
    void Start()
    {
        player = createPlayer();
        vcam.Follow = player.transform;
        neutronValue.text = "0";
        protonValue.text = "1";
        isotopeValue.text = "Hydrogen-1";
    }



    private GameObject createPlayer()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    public Nucleus getNucleus(int protons)
    {
        return nuclei[protons - 1];
    }

    public void changeProtonText(int value)
    {
        protonValue.text = value.ToString();
    }

    public void changeNeutronText(int value)
    {
        neutronValue.text = value.ToString();
    }

    public void changeIsotopeText(string value)
    {
        isotopeValue.text = value;
    }

    public void updateDecayText(float time, string decayType)
    {

        if (decayType != "Stable")
        {
            decayText.text = decayType.ToUpper() + " Decay in: " + time.ToString("00");
        }
        else
        {
            decayText.text = decayType.ToUpper();
        }
    }

    public GameObject getPlayer()
    {
        return player;
    }



}
