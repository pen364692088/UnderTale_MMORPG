using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private  EntityManager entityManager;
    private  CameraManager cameraManager;
    private  ItemManager itemManager;
    private MyScenceManager myScenceManager;
    
    private GameObject userInput;
    private GameObject passwordInput;
    private static bool inWorld = false;

    public bool isPC=false;
    public Player SelfPlayer;
    public InputData input=new InputData();
    public bool playerIsAlive = true;

    public static GameManager Instance {
        get {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public  void Awake()
    {
        _instance = this;
        entityManager = gameObject.AddComponent<EntityManager>();
        cameraManager = gameObject.AddComponent<CameraManager>(); 
        itemManager = gameObject.AddComponent<ItemManager>();
        myScenceManager = gameObject.AddComponent<MyScenceManager>();
        DontDestroyOnLoad(this);
    }

    
    public void InitWorld()
    {
       
        cameraManager.initCamera();
        entityManager.InitWorld();
        itemManager.init();
        inWorld = true;
    }
    public void Login()
    {
        userInput = GameObject.FindGameObjectsWithTag("GameController")[0];
        passwordInput = GameObject.FindGameObjectsWithTag("GameController")[0];
        
        PhotonEngine.Instance.Login(new userData(-1, userInput.GetComponent<InputField>().text, passwordInput.GetComponent<InputField>().text));


        GameObject.FindGameObjectWithTag("Console").GetComponent<Text>().text = "登录请求中.....";
    }
    public void Update()
    {
        if (inWorld)
        {
            cameraManager.seflUpdate();
            itemManager.seflUpdate();
        }
    }
    public void Look(GameObject obj)
    {
        cameraManager.Look(obj);
    }
    public RolerData getPlayerData()
    {
        return SelfPlayer.SelfData as RolerData;
    }
}
