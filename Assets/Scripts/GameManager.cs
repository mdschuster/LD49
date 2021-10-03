using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("Game Timer")]
    public TMP_Text GameTimer;
    public int timeSeconds = 120;
    private float timer;




    [Header("Game Over")]
    [System.NonSerialized]
    public bool gameOver;

    public TMP_Text stateText;
    public TMP_Text finalResultText;

    public GameObject gameOverCanvas;



    // Start is called before the first frame update
    void Start()
    {
        player = createPlayer();
        vcam.Follow = player.transform;
        neutronValue.text = "0";
        protonValue.text = "1";
        isotopeValue.text = "Hydrogen-1";
        timer = timeSeconds;
        gameOver = false;
        gameOverCanvas.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        GameTimer.text = timer.ToString("000");
        if (timer <= 0)
        {
            GameOver();
            return;
        }

        timer -= Time.deltaTime;
    }

    private void GameOver()
    {
        //stop spawning or disable player sphere collider and stop player from moving
        gameOver = true;
        gameOverCanvas.SetActive(true);
        Player p = player.GetComponent<Player>();
        int protons = p.currentNucleus.protons;
        int neutrons = p.getNeutrons();
        if (protons == 10 && (neutrons == 20 || neutrons == 21 || neutrons == 22))
        {
            stateText.text = "You've created a stable isotope of Neon!";
            finalResultText.text = "You Win";
        }
        else
        {
            stateText.text = "You couldn't get to stable isotope of Neon";
            finalResultText.text = "Game Over";
        }
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

    public void onMenuClick()
    {
        SceneManager.LoadScene("Menu");
    }



}
