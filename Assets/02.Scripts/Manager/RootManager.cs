using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootManager : MonoBehaviour
{
    public static RootManager instance;

    public static SessionType sessionType = SessionType.None;

    [SerializeField] private string SceneToBeLoaded = "";

    public const int SelectSessionNumber = 9;
    public const int MoveSessionNumber = 6;
    public const int RotateSessionNumber = 12;
    public const int ScaleSessionNumber = 4;

    public const string comIP = "192.168.0.174";

    public static bool b_CheckWriteValue = true;
    public static bool b_FileCheck = false;

    void Awake()
    {
        if(RootManager.instance == null)
        {
            RootManager.instance = this;
        }
    }

    void Start()
    {
        PlayerPrefs.SetInt("CurrentSessionNumber", 1);
        PlayerPrefs.SetInt("FrameNum", 0);

        CheckSession();

        // 지울것
        //sessionType = SessionType.Move;
        //

        LoadStartScene("UserInfo");
    }

    public void CheckSession()
    {
        if (sessionType.Equals(SessionType.None))
        {
            sessionType = SessionType.Select;

            PlayerPrefs.SetString("SceneName", string.Format("{0}{1}{2}{3}", "S", (int)sessionType, "0", PlayerPrefs.GetInt("CurrentSessionNumber", 1)));
        }
    }

    private void LoadStartScene(string sceneName)
    {
        if (sceneName != "")
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void LoadScene(string sceneName)
    {
        LoadSceneCo(sceneName);
    }

    public void LoadSceneCo(string sceneName)
    {
        if (!string.IsNullOrWhiteSpace(sceneName))
        {
            StartCoroutine(LoadNewScene(sceneName));
        }
        else
        {
            //Debug.Log($"Unsupported scene name: {sceneName}");
        }
    }

    public static string lastSceneLoaded = "";

    /// <summary> 	
	/// 새로운 씬 불러오기
	/// </summary> 	
    private IEnumerator LoadNewScene(string sceneName)
    {
        if (SceneManager.sceneCount > 1)
        {
            lastSceneLoaded = SceneManager.GetSceneAt(1).name;

            //Debug.Log($"Last scene name: {lastSceneLoaded}");

            yield return new WaitForSeconds(0.25f);

            SceneManager.UnloadSceneAsync(lastSceneLoaded);
        }

        //Debug.Log($"New scene name: {sceneName}");
        lastSceneLoaded = sceneName;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
