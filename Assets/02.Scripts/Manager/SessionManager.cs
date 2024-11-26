using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using Microsoft.MixedReality.Toolkit.UI;
using MRTK.Tutorials.GettingStarted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SessionType
{
    None,
    Select,
    Move,
    Rotate,
    Scale,
    Finish
}

public enum RotateState
{
    None,
    CYaw90,
    CYaw180,
    CCYaw90,
    CCYaw180,
    CPitch90,
    CPitch180,
    CCPitch90,
    CCPitch180,
    CRoll90,
    CRoll180,
    CCRoll90,
    CCRoll180
}

public enum ScaleState
{
    None,
    EnLargeMainHand,
    EnLargeSubHand,
    ReduceMainHand,
    ReduceSubHand
}

public class SessionManager : MonoBehaviour
{
    WaitForSeconds cacheSec = new WaitForSeconds(2.0f);

    public static SessionManager instance = null;
    [HideInInspector] public RotateState rotateState = RotateState.None;
    [HideInInspector] public ScaleState scaleState = ScaleState.None;
    public Frustum frustum;
    public _EtriParameter etriParameter;
    SocketClient socketClient;

    public List<Transform> spacePoseList = new List<Transform>();
    public Text currentSessionText;
    public Text sessionText;

    public GameObject[] infoUGUI;
    public GameObject testPanel;
    public GameObject spaceObj;
    public GameObject guidePanel;
    public MeshRenderer guideMaterial;
    public Material selectMat;
    public Material moveMat;
    public Material[] rotateMat;
    public Material[] scaleMat;
    [HideInInspector] public GameObject cubeObj;
    GameObject coffeeCupObj;
    GameObject coffeeCupObjXRAY;

    int spaceCount;
    int spaceNum = 1;
    int currentSessionNumber;
    public int objectCount = 0;

    float xDivide3;
    float yDivide3;
    float zDivide3;

    [HideInInspector] public bool b_SetObject = false;
    [HideInInspector] public bool b_CheckPlacement = false;
    [HideInInspector] public bool b_CheckInfoUI = false;
    [HideInInspector] public bool b_CheckStartUI = false;

    private void Awake()
    {
        if(SessionManager.instance == null)
        {
            SessionManager.instance = this;
        }
    }

    void Start()
    {
        //Debug.Log("SessionManager Start");
        //StartCoroutine(Frustum.instance.CreateCube());
        CoreServices.DiagnosticsSystem.ShowProfiler = false;
        socketClient = GameObject.Find("Network").GetComponent<SocketClient>();

        currentSessionNumber = PlayerPrefs.GetInt("CurrentSessionNumber", 1);

        StartCoroutine(CoroutineUpdate());
    }

    /// <summary>
    ///     세션별 오브젝트가 배치될 공간 생성
    /// </summary>
    public void CreateSpace(float x, float y, float z)
    {
        // 지울것
        //RootManager.sessionType = SessionType.Move;
        //currentSessionNumber = 1;
        //

        if (RootManager.sessionType != SessionType.Scale)
        {
            xDivide3 = x / 3;
            yDivide3 = y / 3;
            zDivide3 = z / 3;
        }
        else if(RootManager.sessionType == SessionType.Scale)
        {
            xDivide3 = (x / 3) * 0.5f;
            yDivide3 = (y / 3) * 0.5f;
            zDivide3 = (z / 3) * 0.5f;
        }

        GameObject obj;
        Transform trans;
        int value;

        guidePanel.SetActive(true);

        #region 공간생성(Select)
        if (RootManager.sessionType == SessionType.Select)
        {
            spaceCount = 9;
            //currentSessionNumber = 9;
            guideMaterial.material = selectMat;

            switch (currentSessionNumber)
            {
                #region Session1
                case 1:
                    for(value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if(value.Equals(1))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session2
                case 2:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session3
                case 3:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session4
                case 4:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session5
                case 5:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session6
                case 6:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session7
                case 7:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session8
                case 8:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session9
                case 9:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                    #endregion
            }
        }
        #endregion
        #region 공간생성(Move)
        else if (RootManager.sessionType == SessionType.Move)
        {
            spaceCount = 18;
            guideMaterial.material = moveMat;

            switch (currentSessionNumber)
            {
                #region Session1 (수평이동 좌>우)
                case 1:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if(value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if(value.Equals(1))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(9))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }
                        else if (value.Equals(10))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(11))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }
                        // ----------------------
                        else if (value.Equals(12))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(13))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(14))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(15))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(16))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(17))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session2 (수평이동 우>좌)
                case 2:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }
                        else if (value.Equals(9))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(10))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(11))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        // ----------------------
                        else if (value.Equals(12))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(13))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(14))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(15))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(16))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(17))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session3 (수평이동 상>하)
                case 3:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }
                        else if (value.Equals(9))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(10))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(11))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        // ----------------------
                        else if (value.Equals(12))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(13))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(14))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }
                        else if (value.Equals(15))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(16))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(17))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session4 (수평이동 하>상)
                case 4:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, 0);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(9))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }
                        else if (value.Equals(10))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, 0);
                        }
                        else if (value.Equals(11))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, 0);
                        }
                        // ----------------------
                        else if (value.Equals(12))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(13))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(14))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(15))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }
                        else if (value.Equals(16))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(17))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session5 (수평이동 앞>뒤)
                case 5:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(9))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }
                        else if (value.Equals(10))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(11))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(12))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(13))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(14))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(15))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }
                        else if (value.Equals(16))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(17))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session6 (수평이동 뒤>앞)
                case 6:
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(0, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(-xDivide3, 0, -zDivide3);
                        }
                        else if (value.Equals(8))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }
                        else if (value.Equals(9))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(10))
                        {
                            trans.position = new Vector3(xDivide3, 0, zDivide3);
                        }
                        else if (value.Equals(11))
                        {
                            trans.position = new Vector3(xDivide3, 0, -zDivide3);
                        }
                        // ----------------------
                        else if (value.Equals(12))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(13))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(14))
                        {
                            trans.position = new Vector3(0, yDivide3, zDivide3);
                        }
                        else if (value.Equals(15))
                        {
                            trans.position = new Vector3(0, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(16))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(17))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion
            }
        }
        #endregion
        #region 공간생성(Rotate)
        else if(RootManager.sessionType == SessionType.Rotate)
        {
            spaceCount = 3;
            guideMaterial.material = rotateMat[currentSessionNumber - 1];

            switch (currentSessionNumber)
            {
                #region Session1 (Yaw 시계방향 90도 회전)
                case 1:
                    rotateState = RotateState.CYaw90;
                    
                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session2 (Yaw 시계방향 180도 회전)
                case 2:
                    rotateState = RotateState.CYaw180;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session3 (Pitch 시계방향 90도 회전)
                case 3:
                    rotateState = RotateState.CPitch90;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session4 (Pitch 시계방향 180도 회전)
                case 4:
                    rotateState = RotateState.CPitch180;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session5 (Roll 시계방향 90도 회전)
                case 5:
                    rotateState = RotateState.CRoll90;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session6 (Roll 시계방향 180도 회전)
                case 6:
                    rotateState = RotateState.CRoll180;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session7 (Yaw 반시계방향 90도 회전)
                case 7:
                    rotateState = RotateState.CCYaw90;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session8 (Yaw 반시계방향 180도 회전)
                case 8:
                    rotateState = RotateState.CCYaw180;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, -yDivide3, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, yDivide3, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session9 (Pitch 반시계방향 90도 회전)
                case 9:
                    rotateState = RotateState.CCPitch90;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session10 (Pitch 반시계방향 180도 회전)
                case 10:
                    rotateState = RotateState.CCPitch180;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, 0, 0);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(xDivide3, 0, 0);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session11 (Roll 반시계방향 90도 회전)
                case 11:
                    rotateState = RotateState.CCRoll90;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session12 (Roll 반시계방향 180도 회전)
                case 12:
                    rotateState = RotateState.CCRoll180;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(0, 0, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(0, 0, 0);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(0, 0, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion
            }
        }
        #endregion
        #region 공간생성(Scale)
        else if (RootManager.sessionType == SessionType.Scale)
        {
            spaceCount = 8;
            guideMaterial.material = scaleMat[currentSessionNumber - 1];

            switch (currentSessionNumber)
            {
                #region Session1 (주손으로 2배 크게)
                case 1:
                    scaleState = ScaleState.EnLargeMainHand;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        //
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session2 (반대손으로 2배 크게)
                case 2:
                    scaleState = ScaleState.EnLargeSubHand;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        //
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session3 (주손으로 1/2 작게)
                case 3:
                    scaleState = ScaleState.ReduceMainHand;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        //
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion

                #region Session4 (반대손으로 1/2 작게)
                case 4:
                    scaleState = ScaleState.ReduceSubHand;

                    for (value = 0; value < spaceCount; value++)
                    {
                        obj = Instantiate(spaceObj);
                        trans = obj.transform;
                        obj.name = string.Format("{0}{1}", "space", spaceNum);
                        spaceNum += 1;

                        if (value.Equals(0))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(1))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, -zDivide3);
                        }
                        else if (value.Equals(2))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, -zDivide3);
                        }
                        else if (value.Equals(3))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, -zDivide3);
                        }
                        //
                        else if (value.Equals(4))
                        {
                            trans.position = new Vector3(-xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(5))
                        {
                            trans.position = new Vector3(xDivide3, -yDivide3, zDivide3);
                        }
                        else if (value.Equals(6))
                        {
                            trans.position = new Vector3(-xDivide3, yDivide3, zDivide3);
                        }
                        else if (value.Equals(7))
                        {
                            trans.position = new Vector3(xDivide3, yDivide3, zDivide3);
                        }

                        trans.SetParent(frustum.cubeTransform);
                        spacePoseList.Add(trans);
                    }
                    break;
                #endregion
            }
        }
        #endregion

        b_SetObject = true;
    }

    private async void TransitionToScene(string sceneName)
    {
        IMixedRealitySceneSystem sceneSystem = MixedRealityToolkit.Instance.GetService<IMixedRealitySceneSystem>();
        //ISceneTransitionService transition = MixedRealityToolkit.Instance.GetService<ISceneTransitionService>();

        //await transition.DoSceneTransition(
        //    () => sceneSystem.LoadContent(sceneName)
        //    );
        
        //await sceneSystem.LoadContent(sceneName);
        if(sceneSystem.NextContentExists)
        {
            await sceneSystem.LoadNextContent();
        }
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            if(b_SetObject)
            {
                yield return cacheSec;

                switch(RootManager.sessionType)
                {
                    case SessionType.Select:
                        if (!objectCount.Equals(9))
                        {
                            coffeeCupObj = PooledObject.instance.GetPooledObject_Object();

                            coffeeCupObj.transform.position = spacePoseList[objectCount].position;
                            coffeeCupObj.SetActive(true);
                            SoundManager.instance.PlayObjectGenerate();

                            objectCount += 1;

                            //objectCount = 9;
                        }

                        else if (objectCount.Equals(9))
                        {
                            Destroy(cubeObj);

                            socketClient.SendMessageNet2("reset");
                            RootManager.b_FileCheck = false;
                            
                            RootManager.instance.LoadScene("Survey");
                        }
                        break;

                    case SessionType.Move:
                        if (!objectCount.Equals(18))
                        {
                            coffeeCupObj = PooledObject.instance.GetPooledObject_Object();

                            coffeeCupObj.transform.position = spacePoseList[objectCount].position;
                            coffeeCupObj.SetActive(true);
                            SoundManager.instance.PlayObjectGenerate();

                            coffeeCupObjXRAY = PooledObject.instance.GetPooledObject_Object_XRAY();

                            coffeeCupObjXRAY.transform.position = spacePoseList[objectCount + 1].position;
                            coffeeCupObjXRAY.SetActive(true);

                            coffeeCupObj.GetComponent<PartAssemblyController>().locationToPlace = coffeeCupObjXRAY.transform;

                            objectCount += 2;

                            //objectCount = 18;
                        }
                        else if (objectCount.Equals(18))
                        {
                            Destroy(cubeObj);

                            socketClient.SendMessageNet2("reset");
                            RootManager.b_FileCheck = false;

                            RootManager.instance.LoadScene("Survey");
                        }
                        break;

                    case SessionType.Rotate:
                        if (!objectCount.Equals(3))
                        {
                            coffeeCupObj = PooledObject.instance.GetPooledObject_Object();

                            coffeeCupObj.transform.position = spacePoseList[objectCount].position;
                            coffeeCupObj.SetActive(true);
                            SoundManager.instance.PlayObjectGenerate();

                            if (rotateState.Equals(RotateState.CYaw90) || rotateState.Equals(RotateState.CYaw180) || rotateState.Equals(RotateState.CCYaw90) || rotateState.Equals(RotateState.CCYaw180))
                            {
                                coffeeCupObj.GetComponent<BoundingBox>().ShowRotationHandleForX = false;
                                coffeeCupObj.GetComponent<BoundingBox>().ShowRotationHandleForZ = false;
                            }
                            else if(rotateState.Equals(RotateState.CPitch90) || rotateState.Equals(RotateState.CPitch180) || rotateState.Equals(RotateState.CCPitch90) || rotateState.Equals(RotateState.CCPitch180))
                            {
                                coffeeCupObj.GetComponent<BoundingBox>().ShowRotationHandleForY = false;
                                coffeeCupObj.GetComponent<BoundingBox>().ShowRotationHandleForZ = false;
                            }
                            else if (rotateState.Equals(RotateState.CRoll90) || rotateState.Equals(RotateState.CRoll180) || rotateState.Equals(RotateState.CCRoll90) || rotateState.Equals(RotateState.CCRoll180))
                            {
                                coffeeCupObj.GetComponent<BoundingBox>().ShowRotationHandleForX = false;
                                coffeeCupObj.GetComponent<BoundingBox>().ShowRotationHandleForY = false;
                            }

                            objectCount += 1;

                            //objectCount = 3;
                        }

                        else if (objectCount.Equals(3))
                        {
                            Destroy(cubeObj);

                            socketClient.SendMessageNet2("reset");
                            RootManager.b_FileCheck = false;

                            RootManager.instance.LoadScene("Survey");
                        }
                        break;

                    case SessionType.Scale:
                        if (!objectCount.Equals(8))
                        {
                            coffeeCupObj = PooledObject.instance.GetPooledObject_Object();

                            coffeeCupObj.transform.position = spacePoseList[objectCount].position;
                            coffeeCupObj.SetActive(true);
                            SoundManager.instance.PlayObjectGenerate();

                            objectCount += 1;

                            //objectCount = 8;
                        }

                        else if (objectCount.Equals(8))
                        {
                            Destroy(cubeObj);

                            socketClient.SendMessageNet2("reset");
                            RootManager.b_FileCheck = false;

                            RootManager.instance.LoadScene("Survey");
                        }
                        break;
                }

                b_SetObject = false;
            }

            if(!b_CheckInfoUI)
            {
                yield return cacheSec;

                if (RootManager.sessionType.Equals(SessionType.Select) && currentSessionNumber.Equals(1))
                {
                    infoUGUI[0].SetActive(true);
                }
                else if (RootManager.sessionType.Equals(SessionType.Move) && currentSessionNumber.Equals(1))
                {
                    infoUGUI[1].SetActive(true);
                }
                else if (RootManager.sessionType.Equals(SessionType.Rotate) && currentSessionNumber.Equals(1))
                {
                    infoUGUI[2].SetActive(true);
                }
                else if (RootManager.sessionType.Equals(SessionType.Scale) && currentSessionNumber.Equals(1))
                {
                    infoUGUI[3].SetActive(true);
                }
                else if(!currentSessionNumber.Equals(1))
                {
                    testPanel.SetActive(true);
                    b_CheckStartUI = true;
                }

                b_CheckInfoUI = true;
            }

            if(b_CheckStartUI)
            {
                //yield return cacheSec;

                testPanel.SetActive(true);
                currentSessionText.text = currentSessionNumber.ToString();
                
                switch(RootManager.sessionType)
                {
                    case SessionType.Select:
                        sessionText.text = RootManager.SelectSessionNumber.ToString();
                        break;

                    case SessionType.Move:
                        sessionText.text = RootManager.MoveSessionNumber.ToString();
                        break;

                    case SessionType.Rotate:
                        sessionText.text = RootManager.RotateSessionNumber.ToString();
                        break;

                    case SessionType.Scale:
                        sessionText.text = RootManager.ScaleSessionNumber.ToString();
                        break;
                }

                b_CheckStartUI = false;
            }

            if(b_CheckPlacement)
            {
                yield return cacheSec;

                PooledObject.instance.poolObjs_Object.Remove(coffeeCupObj);
                PooledObject.instance.poolObjs_Object_XRAY.Remove(coffeeCupObjXRAY);

                Destroy(coffeeCupObj);
                Destroy(coffeeCupObjXRAY);

                b_SetObject = true;
                b_CheckPlacement = false;
            }

            yield return null;
        }
    }
}
