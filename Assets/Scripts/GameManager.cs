using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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


    // Start is called before the first frame update
    void Start()
    {
        player = createPlayer();
        vcam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject createPlayer()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }



}
