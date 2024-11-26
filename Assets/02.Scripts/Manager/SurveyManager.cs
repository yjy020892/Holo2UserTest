using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class SurveyManager : MonoBehaviour
{
    WaitForSeconds cacheSec = new WaitForSeconds(1.0f);

    public static SurveyManager instance;
    public _EtriParameter etriParameter;
    SocketClient socketClient;

    public Text timerText;

    List<string> _scoreList = new List<string>();
    List<string[]> scoreList = new List<string[]>();

    public GameObject scorePanel;
    public GameObject restPanel;
    public GameObject finishPanel;

    private string path;
    private string folderPath;
    private string scoreFilePath;
    private string fileName;
    private string userNum;
    private string data;
    private string sceneName;

    int min = 2;
    float sec = 0.0f;

    public bool b_FinishSurvey = false;
    bool b_Timer = false;

    void Awake()
    {
        if(SurveyManager.instance == null)
        {
            SurveyManager.instance = this;
        }
    }

    void Start()
    {
        //etriParameter = GameObject.Find("_EtriParameter").GetComponent<_EtriParameter>();
        socketClient = GameObject.Find("Network").GetComponent<SocketClient>();
        CoreServices.DiagnosticsSystem.ShowProfiler = false;
        path = Application.persistentDataPath;
    }

    private bool CheckSessionNumber()
    {
        int num = PlayerPrefs.GetInt("CurrentSessionNumber");
        bool check = true;

        if (RootManager.sessionType.Equals(SessionType.Select))
        {
            //num = RootManager.SelectSessionNumber;
            if (!num.Equals(RootManager.SelectSessionNumber))
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", num + 1);
                //Debug.Log(PlayerPrefs.GetInt("CurrentSessionNumber"));
                check = true;
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", 1);
                RootManager.sessionType = SessionType.Move;
                check = false;
            }
        }
        else if (RootManager.sessionType.Equals(SessionType.Move))
        {
            //num = RootManager.MoveSessionNumber;
            if (!num.Equals(RootManager.MoveSessionNumber))
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", num + 1);
                //Debug.Log(PlayerPrefs.GetInt("CurrentSessionNumber"));
                check = true;
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", 1);
                RootManager.sessionType = SessionType.Rotate;
                check = false;
            }
        }
        else if (RootManager.sessionType.Equals(SessionType.Rotate))
        {
            //num = RootManager.RotateSessionNumber;
            if (!num.Equals(RootManager.RotateSessionNumber))
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", num + 1);
                //Debug.Log(PlayerPrefs.GetInt("CurrentSessionNumber"));
                check = true;
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", 1);
                RootManager.sessionType = SessionType.Scale;
                check = false;
            }
        }
        else if (RootManager.sessionType.Equals(SessionType.Scale))
        {
            //num = RootManager.ScaleSessionNumber;
            if (!num.Equals(RootManager.ScaleSessionNumber))
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", num + 1);
                //Debug.Log(PlayerPrefs.GetInt("CurrentSessionNumber"));
                check = true;
            }
            else
            {
                PlayerPrefs.SetInt("CurrentSessionNumber", 1);
                RootManager.sessionType = SessionType.Finish;
                check = false;
            }
        }

        return check;
    }

    public IEnumerator ReceiveSurvey(int score)
    {
        if(score.Equals(0))
        {
            yield return cacheSec;

            if (PlayerPrefs.GetInt("CurrentSessionNumber", 1) < 10)
            {
                PlayerPrefs.SetString("SceneName", string.Format("{0}{1}{2}{3}", "S", (int)RootManager.sessionType, "0", PlayerPrefs.GetInt("CurrentSessionNumber", 1)));
                //Debug.Log(PlayerPrefs.GetString("SceneName"));
            }
            else
            {
                PlayerPrefs.SetString("SceneName", string.Format("{0}{1}{2}", "S", (int)RootManager.sessionType, PlayerPrefs.GetInt("CurrentSessionNumber", 1)));
            }

            if (!RootManager.sessionType.Equals(SessionType.Finish))
            {
                //etriParameter.CSVsaveOn = false;
                //socketClient.SendMessageNet2("reset");

                //PlayerPrefs.SetInt("FrameNum", 0);

                //yield return new WaitForSeconds(5.0f);
                RootManager.b_FileCheck = false;
                RootManager.instance.LoadScene("Test");
            }
            else
            {
                finishPanel.SetActive(true);
                restPanel.SetActive(false);
            }
        }
        else
        {
            b_FinishSurvey = true;

            //etriParameter.CSVsaveOn = false;
            //string etri = "ETRI_";
            //userNum = PlayerPrefs.GetString("UserNumber", "001");
            //data = DateTime.Now.ToString("yyyyMMdd");
            sceneName = PlayerPrefs.GetString("SceneName", "S101");

            //fileName = string.Format("{0}{1}{2}{3}", etri, userNum, "_", data);
            //folderPath = Path.Combine(path, fileName);
            //scoreFilePath = Path.Combine(folderPath, fileName + "_" + "Score" + ".csv");

            _scoreList.Add(sceneName);
            _scoreList.Add(score.ToString());
            scoreList.Add(_scoreList.ToArray());

            //if (File.Exists(scoreFilePath))
            //{
                string[][] output = new string[scoreList.Count][];
                for (int i = 0; i < output.Length; i++)
                {
                    output[i] = scoreList[i];
                }
                int length = output.Length;
                string delimiter = ",";
                StringBuilder sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));

                //StreamWriter outStream = System.IO.File.CreateText(scoreFilePath);
                //outStream.WriteLine("Scene_Name,QoI_Score");
                //outStream.Close();

                socketClient.SendMessageNet3(sb.ToString());
                //File.AppendAllText(scoreFilePath, sb.ToString());
            //}

            //Debug.Log(score);
            

            yield return cacheSec;

            if (CheckSessionNumber())
            {
                if (PlayerPrefs.GetInt("CurrentSessionNumber", 1) < 10)
                {
                    PlayerPrefs.SetString("SceneName", string.Format("{0}{1}{2}{3}", "S", (int)RootManager.sessionType, "0", PlayerPrefs.GetInt("CurrentSessionNumber", 1)));
                    //Debug.Log(PlayerPrefs.GetString("SceneName"));
                }
                else
                {
                    PlayerPrefs.SetString("SceneName", string.Format("{0}{1}{2}", "S", (int)RootManager.sessionType, PlayerPrefs.GetInt("CurrentSessionNumber", 1)));
                }

                if (!RootManager.sessionType.Equals(SessionType.Finish))
                {
                    //etriParameter.CSVsaveOn = false;
                    socketClient.SendMessageNet2("reset");

                    //PlayerPrefs.SetInt("FrameNum", 0);

                    //yield return new WaitForSeconds(5.0f);
                    RootManager.b_FileCheck = false;
                    RootManager.instance.LoadScene("Test");
                }
                else
                {
                    socketClient.SendMessageNet2("reset");

                    finishPanel.SetActive(true);
                    restPanel.SetActive(false);
                }
            }
            else
            {
                socketClient.SendMessageNet2("reset");

                scorePanel.SetActive(false);
                restPanel.SetActive(true);

                StartCoroutine(Timer());
            }
        }
    }

    /// <summary>
    ///     휴식 타이머
    /// </summary>
    IEnumerator Timer()
    {
        while(true)
        {
            sec -= Time.deltaTime;

            if(min <= 0 && sec <= 0)
            {
                //RootManager.instance.LoadScene("Test");
                StartCoroutine(ReceiveSurvey(0));

                break;
            }
            else
            {
                if (sec <= 0)
                {
                    min--;
                    sec += 60;
                }
            }
            
            timerText.text = string.Format("{0:D1}:{1:D2}", min, (int)sec);

            yield return null;
        }
    }
}
