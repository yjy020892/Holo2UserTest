using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;

#if WINDOWS_UWP
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
#endif
public class _EtriParameter : MonoBehaviour, IMixedRealityPointerHandler
{
    WaitForSeconds sec = new WaitForSeconds(0.1f);

    GazeProvider _GazeProvider;
    SocketClient socketClient;
    float timer = 0;

    [SerializeField]
    Headset _Headset;
    [SerializeField]
    TrackingState _TrackingState;
    [SerializeField]
    private MixedRealityPose _MixedRealityPose;
    [SerializeField]
    InteractableStates _InteractableStates;

    [Header("#frameNum")]
    private int FrameNum;

    [Header("#DateTime")]
    [SerializeField]
    private string Date;
    [SerializeField]
    private string Times;
    [SerializeField]
    private string FPS;
    float deltaTime = 0.0f;

    [Header("#Scene")]
    [SerializeField]
    private string Scene_Name;
    private string Scene_State = string.Empty;

    [Header("#Headset")]
    [SerializeField]
    private bool _Headset_TrackingState;
    [SerializeField]
    Transform _MainCamera;

    [Header("#Gaze")]
    [SerializeField]
    private string Gaze_GazeTarget;
    [SerializeField]
    bool GazeTargetOn = false;
    [SerializeField]
    bool PointerDragged = false;

    bool LeftHandCkeck = false;
    bool RightHandCkeck = false;

    [Header("#Object")]
    [SerializeField]
    GameObject InteractionObject;
    bool Interaction = false;

    [Header("#Save")]
    [SerializeField]
    _EtriFileCheck _EtriFileCheck;
    string csvDirPath;
    public bool CSVsaveOn = false;
    private List<string[]> T_csvValue = new List<string[]>();
    List<string> _csvValue = new List<string>();

    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
        //CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
    }
    private void OnDisable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
        //CoreServices.InputSystem?.UnregisterHandler<IMixedRealityHandJointHandler>(this);
    }
    void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        PointerDragged = false;
        InteractionObject = null;
    }
    void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData) { }
    void IMixedRealityPointerHandler.OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        PointerDragged = true;
        InteractionObject = eventData.Pointer.Result.CurrentPointerTarget;

    }
    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData) { }


    void Start()
    {
        FrameNum = 0;

        if (SceneManager.GetSceneAt(1).name.Equals("Test"))
        {
            PlayerPrefs.SetInt("FrameNum", 0);
        }
        
        socketClient = GameObject.Find("Network").GetComponent<SocketClient>();
        _EtriFileCheck = GameObject.Find("_EtriFileCheck").GetComponent<_EtriFileCheck>();
        _MainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        csvDirPath = Application.persistentDataPath;

        //#Hololens
        _TrackingState = _Headset.TrackingState;
        //# DateTime
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        //# Scene
        //Scene scene = SceneManager.GetActiveScene();
        Scene scene = SceneManager.GetSceneAt(1);
        Scene_Name = PlayerPrefs.GetString("SceneName", "S101");

        if (SceneManager.GetSceneAt(1).name.Equals("Test") || SceneManager.GetSceneAt(1).name.Equals("Survey"))
        {
            StartCoroutine("csvDirFileCheck", csvDirPath);
            //csvDirFileCheck(csvDirPath);
        }
    }
    
    public void csvDirFileCheck()
    {
        StartCoroutine(csvDirFileCheck(csvDirPath));
    }

    public IEnumerator csvDirFileCheck(string path){
        //yield return new WaitForSeconds(5.0f);

        yield return _EtriFileCheck.StartCoroutine("csvFileMaker", path);
        //_EtriFileCheck.csvFileMaker(path);
        CSVsaveOn = true;
    }

    void Update()
    {
        if (CSVsaveOn == true)
        {
            //# DateTime
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            FPS = "" + (float)System.Math.Round(fps, 3) * 0.5f;
            if (_TrackingState == TrackingState.Tracked) { _Headset_TrackingState = true; }
            else { _Headset_TrackingState = false; }
            if (CoreServices.InputSystem.GazeProvider.GazeTarget)
            {
                Gaze_GazeTarget = CoreServices.InputSystem.GazeProvider.GazeTarget.name;
                GazeTargetOn = true;
            }
            else
            {
                Gaze_GazeTarget = "-";
                GazeTargetOn = false;
            }

            timer += Time.deltaTime;
            //Debug.Log(timer);

            if(timer > 0.03f)
            {
                timer = 0;
                if(SceneManager.GetSceneAt(1).name.Equals("Test"))
                {
                    CSVsave();
                }
            }
        }
    }

    /// <summary>
    ///     csv파일에 저장
    /// </summary>
    void CSVsave() {
        _csvValue.Clear();
        T_csvValue.Clear();
        LeftHandCkeck = false;
        RightHandCkeck = false;

        //if(SceneManager.GetSceneAt(1).name.Equals("Survey"))
        //{
        
        FrameNum = PlayerPrefs.GetInt("FrameNum", 0);
        //}
        FrameNum++;
        PlayerPrefs.SetInt("FrameNum", FrameNum);

        Times = DateTime.Now.ToString("hh-mm-ss.fff");

        if (SceneManager.GetSceneAt(1).name.Equals("Test") || SceneManager.GetSceneAt(1).name.Equals("UserInfo"))
        {
            Scene_State = "S";
        }
        else if (SceneManager.GetSceneAt(1).name.Equals("Survey"))
        {
            Scene_State = "Q";
        }

        //Debug.Log(FrameNum);
        _csvValue = new List<string>(new string[]
        {
            FrameNum.ToString(),Date.ToString(),Times.ToString(),FPS,Scene_Name,Scene_State,
            //HeadSet
           //_Headset.Position.x.ToString(),_Headset.Position.y.ToString(),_Headset.Position.z.ToString(),
           //_Headset.Rotation.w.ToString(),_Headset.Rotation.x.ToString(),_Headset.Rotation.y.ToString(),_Headset.Rotation.z.ToString(),
           _MainCamera.position.x.ToString(),_MainCamera.position.y.ToString(),_MainCamera.position.z.ToString(),
           _MainCamera.rotation.w.ToString(),_MainCamera.rotation.eulerAngles.x.ToString(),_MainCamera.rotation.eulerAngles.y.ToString(),_MainCamera.rotation.eulerAngles.z.ToString(),
           _Headset_TrackingState.ToString(),

            //#Gaze
            CoreServices.InputSystem.GazeProvider.GazeOrigin.x.ToString(),
            CoreServices.InputSystem.GazeProvider.GazeOrigin.y.ToString(),
            CoreServices.InputSystem.GazeProvider.GazeOrigin.z.ToString(),
            CoreServices.InputSystem.GazeProvider.GazeDirection.x.ToString(),
            CoreServices.InputSystem.GazeProvider.GazeDirection.y.ToString(),
            CoreServices.InputSystem.GazeProvider.GazeDirection.z.ToString(),
            Gaze_GazeTarget,
            CoreServices.InputSystem.GazeProvider.HitNormal.x.ToString(),
            CoreServices.InputSystem.GazeProvider.HitNormal.y.ToString(),
            CoreServices.InputSystem.GazeProvider.HitNormal.z.ToString(),
            CoreServices.InputSystem.GazeProvider.HitPosition.x.ToString(),
            CoreServices.InputSystem.GazeProvider.HitPosition.y.ToString(),
            CoreServices.InputSystem.GazeProvider.HitPosition.z.ToString(),
            CoreServices.InputSystem.EyeGazeProvider.IsEyeCalibrationValid.ToString(),
            CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingDataValid.ToString(),
            CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingEnabled.ToString(),
            CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingEnabledAndValid.ToString(),
            CoreServices.InputSystem.EyeGazeProvider.Timestamp.ToString(),
        });
        
        var handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        Transform jointTransform;
        string t1 = "";
        foreach (TrackedHandJoint trackedHandJoint in Enum.GetValues(typeof(TrackedHandJoint)))
        {
            t1 = "";
            jointTransform = handJointService.RequestJointTransform(trackedHandJoint, Handedness.Left);
            t1 = jointTransform.name.Substring(15);
            if(LeftHandCkeck == true){
                _csvValue.Add(t1);
                _csvValue.Add(jointTransform.position.x.ToString());
                _csvValue.Add(jointTransform.position.y.ToString());
                _csvValue.Add(jointTransform.position.z.ToString());
                _csvValue.Add(jointTransform.rotation.eulerAngles.x.ToString());
                _csvValue.Add(jointTransform.rotation.eulerAngles.y.ToString());
                _csvValue.Add(jointTransform.rotation.eulerAngles.z.ToString());
                _csvValue.Add(jointTransform.rotation.w.ToString());
            }
            LeftHandCkeck = true;
        }
        foreach (TrackedHandJoint trackedHandJoint in Enum.GetValues(typeof(TrackedHandJoint)))
        {
            t1 = "";
            jointTransform = handJointService.RequestJointTransform(trackedHandJoint, Handedness.Right);
            t1 = jointTransform.name.Substring(15);
            if (RightHandCkeck == true)
            {
                _csvValue.Add(t1);
                _csvValue.Add(jointTransform.position.x.ToString());
                _csvValue.Add(jointTransform.position.y.ToString());
                _csvValue.Add(jointTransform.position.z.ToString());
                _csvValue.Add(jointTransform.rotation.eulerAngles.x.ToString());
                _csvValue.Add(jointTransform.rotation.eulerAngles.y.ToString());
                _csvValue.Add(jointTransform.rotation.eulerAngles.z.ToString());
                _csvValue.Add(jointTransform.rotation.w.ToString());
            }
            RightHandCkeck = true;
        }
        if (PointerDragged == true)
        {
            if(Interaction == false){
                Interaction = true;
                _csvValue.Add(DateTime.Now.ToString("hh-mm-ss.fff"));
                _csvValue.Add("-");
            }
            else{
                _csvValue.Add("-");
                _csvValue.Add("-");
            }

            if(InteractionObject != null)
            {
                _csvValue.Add(InteractionObject.name);
                _csvValue.Add(InteractionObject.transform.position.x.ToString());
                _csvValue.Add(InteractionObject.transform.position.y.ToString());
                _csvValue.Add(InteractionObject.transform.position.z.ToString());
                _csvValue.Add(InteractionObject.transform.rotation.eulerAngles.x.ToString());
                _csvValue.Add(InteractionObject.transform.rotation.eulerAngles.y.ToString());
                _csvValue.Add(InteractionObject.transform.rotation.eulerAngles.z.ToString());
                _csvValue.Add(InteractionObject.transform.rotation.w.ToString());
            }
            else
            {
                for(int i = 0; i < 7; i++)
                {
                    _csvValue.Add("null");
                }
            }
            
        }
        else
        {
            if (Interaction == true)
            {
                Interaction = false;
                _csvValue.Add("-");
                _csvValue.Add(DateTime.Now.ToString("hh-mm-ss.fff"));
            }
            else{
                _csvValue.Add("-");
                _csvValue.Add("-");
            }

            for (int i = 0; i < 7; i++)
            {
                _csvValue.Add("-");
            }
        }
        T_csvValue.Add(_csvValue.ToArray());

        string[][] output = new string[T_csvValue.Count][];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = T_csvValue[i];
        }
        int length = output.Length;
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        //Debug.Log(sb.ToString());
        if(RootManager.b_CheckWriteValue)
        {
            socketClient.SendMessageNet(sb.ToString());
        }
        else
        {
            socketClient.SendMessageNet1(sb.ToString());
        }
        
        
        //File.AppendAllText(_EtriFileCheck.filePath, sb.ToString());
    }
}
