using System;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Load : UIBase
{
    //显示进度的文本
    private GProgressBar loadBar;
    private GComponent loadCom;
    private GTextField loadText;

    //进度条的数值
    private float progressValue;
    //进度条
    //private Slider slider;
    //[Tooltip("下个场景的名字")]
    public string nextSceneName;

    private AsyncOperation async = null;

    private void Start()
    {
        loadCom = gameObject.GetComponent<UIPanel>().ui;
        loadBar = loadCom.GetChild("loadBar").asProgress;
        loadText = loadCom.GetChild("title").asTextField;

        nextSceneName = MyScenceManager.Instance.getNextScene();
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        SceneManager.sceneLoaded += MyScenceManager.Instance.getCallBack();
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            loadBar.value = Mathf.Floor(progressValue * 100);

            if (progressValue >= 1)
            {
                loadText.text = "触摸屏幕继续";
                loadCom.onTouchBegin.Add(() => {
                    async.allowSceneActivation = true;

                    //socketMsg smsg = new socketMsg(OpCode.WORLD, WorldCode.INTO, 3); //todo
                    //Dispatch(AreaCode.NET, 0, smsg);
                    //Dispatch(AreaCode.GAME, GameEvent.INTOWORLD, null);
                });


            }

            yield return null;
        }

    }


}
