using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReactUI : MonoBehaviour
{
    public Frustum frustum;
    public GameObject slateUGUI;

    public Toggle rightToggle;
    public Toggle leftToggle;

    public InputField userNumberInput;
    public InputField armLengthInput;

    /// <summary>
    ///     버튼들 반응
    /// </summary>
    public void OnPressButton()
    {
        SoundManager.instance.PlayButton();
        //Debug.Log("press");
        if (SceneManager.GetSceneAt(1).name.Equals("UserInfo"))
        {
            switch (gameObject.name)
            {
                case "OK":
                    slateUGUI.SetActive(true);
                    gameObject.transform.parent.gameObject.SetActive(false);
                    break;

                case "Next":
                    PlayerPrefs.SetString("UserNumber", userNumberInput.text);

                    slateUGUI.SetActive(true);
                    gameObject.transform.parent.gameObject.SetActive(false);
                    break;

                case "Finish":
                    if(leftToggle.isOn)
                    {
                        PlayerPrefs.SetString("Hand", "Left Hand");
                    }
                    else if(rightToggle.isOn)
                    {
                        PlayerPrefs.SetString("Hand", "Right Hand");
                    }

                    PlayerPrefs.SetInt("ArmLength", int.Parse(armLengthInput.text));

                    //StartCoroutine(SurveyManager.instance.etriParameter.csvDirFileCheck(Application.persistentDataPath));
                    //SurveyManager.instance.etriParameter.csvDirFileCheck(Application.persistentDataPath);
                    RootManager.instance.LoadScene("Test");
                    break;
            }
        }
        else if(SceneManager.GetSceneAt(1).name.Equals("Test"))
        {
            switch (gameObject.name)
            {
                case "Next":
                    //slateUGUI.SetActive(true);
                    SessionManager.instance.b_CheckStartUI = true;

                    gameObject.transform.parent.gameObject.SetActive(false);
                    break;

                case "OK":
                    frustum.CreateCube();

                    //SessionManager.instance.etriParameter.csvDirFileCheck();
                    gameObject.transform.parent.gameObject.SetActive(false);
                    break;
            }
        }
        else if (SceneManager.GetSceneAt(1).name.Equals("Survey"))
        //else if(SceneManager.GetActiveScene().name.Equals("Survey"))
        {
            if (SurveyManager.instance.b_FinishSurvey)
            {
                switch (gameObject.name)
                {
                    case "Skip":
                        StartCoroutine(SurveyManager.instance.ReceiveSurvey(0));
                        //gameObject.transform.parent.gameObject.SetActive(false);
                        break;
                }
            }
            else if (!SurveyManager.instance.b_FinishSurvey)
            {
                StartCoroutine(SurveyManager.instance.ReceiveSurvey(int.Parse(gameObject.name)));
            }
        }
    }
}
