using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyScenceManager : MonoBehaviour
{
    private static MyScenceManager _instance = null;

    public static MyScenceManager Instance {
        get {
            if (_instance == null)
            {
                _instance = new MyScenceManager();
            }
            return _instance;
        }
    }

    private string nextScene = "";
    private string nowScene = "";

    private UnityEngine.Events.UnityAction<Scene, LoadSceneMode> CallBack;
    void Awake()
    {
        _instance = this;
        nowScene = "welcome";
    }
   
    public void Load(string name, UnityEngine.Events.UnityAction<Scene, LoadSceneMode> callBack)
    {
        nextScene = name;
        CallBack = callBack;
        SceneManager.LoadScene("load");
        //SceneManager.sceneLoaded += ;
        
        //Dispatch(AreaCode.UI, UIEvent.LOADSCENE, null);

    }
    public UnityEngine.Events.UnityAction<Scene, LoadSceneMode> getCallBack()
    {
        return CallBack;
    }
    public string getNowScene()
    {
        return nowScene;
    }
    public string getNextScene()
    {
        return nextScene;
    }
    public void setNextScene(string next)
    {
        nextScene = next;
    }
    public void setNowScene(string now)
    {
        nowScene = now;
    }
}
