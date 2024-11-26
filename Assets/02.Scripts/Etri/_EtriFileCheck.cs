using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text;
using System.IO;
using System.Linq;



public class _EtriFileCheck : MonoBehaviour
{
    SocketClient socketClient;

    private List<string[]> TableName = new List<string[]>();
    List<string> tableTemp = new List<string>();
    [SerializeField]
    string[] csvFiles;
    [SerializeField]
    public string filePath;
    [SerializeField]
    string csvFileNumber;

    string fileName;
    string folderPath = string.Empty;
    string scoreFilePath;

    void Start()
    {
        socketClient = GameObject.Find("Network").GetComponent<SocketClient>();
    }

    public IEnumerator csvFileMaker(string path)
    {
        if (!RootManager.b_FileCheck)
        {
            RootManager.b_FileCheck = true;
            //yield return null;

            yield return new WaitForSeconds(0.5f);

            //Debug.Log("csvFileMaker");

            string etri = "ETRI_";
            string userNum = PlayerPrefs.GetString("UserNumber", "001");
            string data = DateTime.Now.ToString("yyyyMMdd");
            string sceneName = PlayerPrefs.GetString("SceneName", "S101");

            fileName = string.Format("{0}{1}{2}{3}", etri, userNum, "_", data);
            socketClient.SendMessageNet2(fileName);

            folderPath = Path.Combine(path, fileName);
            filePath = Path.Combine(folderPath, string.Format("{0}{1}{2}{3}", fileName, "_", sceneName, ".csv"));
            scoreFilePath = Path.Combine(folderPath, string.Format("{0}{1}{2}{3}", fileName, "_", "Score", ".csv"));

            if (RootManager.b_CheckWriteValue)
            {
                //Debug.Log("1");
                RootManager.b_CheckWriteValue = false;
            }
            else if (!RootManager.b_CheckWriteValue)
            {
                //Debug.Log("2");
                RootManager.b_CheckWriteValue = true;
            }

            //if (!string.IsNullOrEmpty(fileName) && socketClient != null)
            //{
            //Debug.Log("fileName : " + fileName);

            //}

            //Directory.CreateDirectory(folderPath);

            //List<string> inFiles = new List<string>();

            //foreach (string imageFile in Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".csv")))
            //{
            //    inFiles.Add(imageFile);
            //}

            //if (inFiles.Count > 0)
            //{
            //    string[] v = new string[inFiles.Count];
            //    for (int i = 0; i < inFiles.Count; i++)
            //    {
            //        v[i] = inFiles[i];
            //    }

            //    filePath = Path.Combine(folderPath, fileName + "_" + sceneName + ".csv");
            //}
            //else
            //{
            //    filePath = Path.Combine(folderPath, fileName + "_S101" +".csv");
            //}

            //filePath = Path.Combine(folderPath, fileName + "_" + sceneName + ".csv");

            //yield return new WaitForSeconds(0.1f);

            //if (!string.IsNullOrEmpty(sceneName) && socketClient != null)
            //{
            //Debug.Log("sceneName : " + sceneName);
            socketClient.SendMessageNet2(sceneName);
            //}

            //Debug.Log("filePath : " + filePath);

            //if (!File.Exists(filePath))
            //{
            //    using (TextWriter writer = File.CreateText(filePath))
            //    {
            //    }
            //}

            //tableTemp = new List<string>(new string[]
            //{
            //     "Frame_Number","Date","Time","FPS","Scene_Name","State",
            //     "Headset_Position_x","Headset_Position_y","Headset_Position_z","Headset_Rotation_x","Headset_Rotation_y","Headset_Rotation_z","Headset_Rotation_w",
            //     "Headset_TrackingState","Gaze_Origin_x","Gaze_Origin_y","Gaze_Origin_z","Gaze_Direction_x","Gaze_Direction_y","Gaze_Direction_z",
            //     "Gaze_Target","Gaze_HitNormal_x","Gaze_HitNormal_y","Gaze_HitNormal_z","Gaze_HitPosition_x","Gaze_HitPosition_y","Gaze_HitPosition_z",
            //     "Gaze_IsEyeCalibrationValid","Gaze_IsEyeTrackingDataValid","Gaze_IsEyeTrackingEnabled","Gaze_IsEyeTrackingEnabledAndValid","Gaze_Timestamp_Time",
            //});
            //for (int i = 0; i < 26; i++)
            //{
            //    tableTemp.Add("L_HandJoint" + i + "_TrackedHandJoint");
            //    tableTemp.Add("L_HandJoint" + i + "_Position_x");
            //    tableTemp.Add("L_HandJoint" + i + "_Position_y");
            //    tableTemp.Add("L_HandJoint" + i + "_Position_z");
            //    tableTemp.Add("L_HandJoint" + i + "_Rotation_x");
            //    tableTemp.Add("L_HandJoint" + i + "_Rotation_y");
            //    tableTemp.Add("L_HandJoint" + i + "_Rotation_z");
            //    tableTemp.Add("L_HandJoint" + i + "_Rotation_w");
            //}
            //for (int i = 0; i < 26; i++)
            //{
            //    tableTemp.Add("R_HandJoint" + i + "_TrackedHandJoint");
            //    tableTemp.Add("R_HandJoint" + i + "_Position_x");
            //    tableTemp.Add("R_HandJoint" + i + "_Position_y");
            //    tableTemp.Add("R_HandJoint" + i + "_Position_z");
            //    tableTemp.Add("R_HandJoint" + i + "_Rotation_w");
            //    tableTemp.Add("R_HandJoint" + i + "_Rotation_x");
            //    tableTemp.Add("R_HandJoint" + i + "_Rotation_y");
            //    tableTemp.Add("R_HandJoint" + i + "_Rotation_z");
            //}
            //tableTemp.Add("Object_Interaction_StartTime");
            //tableTemp.Add("Object_Interaction_EndTime");
            //tableTemp.Add("Object_Interaction_Target");
            //tableTemp.Add("Object_Position_x");
            //tableTemp.Add("Object_Position_y");
            //tableTemp.Add("Object_Position_z");
            //tableTemp.Add("Object_Rotation_x");
            //tableTemp.Add("Object_Rotation_y");
            //tableTemp.Add("Object_Rotation_z");
            //tableTemp.Add("Object_Rotation_w");

            //TableName.Add(tableTemp.ToArray());

            //string[][] output = new string[TableName.Count][];
            //for (int i = 0; i < output.Length; i++)
            //{
            //    output[i] = TableName[i];
            //}
            //int length = output.GetLength(0);
            //string delimiter = ",";
            //StringBuilder sb = new StringBuilder();
            //for (int index = 0; index < length; index++)
            //    sb.AppendLine(string.Join(delimiter, output[index]));
            //if (!File.Exists(filePath))
            //{
            //    //Debug.Log("없음");
            //    StreamWriter outStream = System.IO.File.CreateText(filePath);
            //    outStream.WriteLine(sb);
            //    outStream.Close();
            //}

            //if(!File.Exists(scoreFilePath))
            //{
            //    StreamWriter outStream = System.IO.File.CreateText(scoreFilePath);
            //    outStream.WriteLine("Scene_Name,QoI_Score");
            //    outStream.Close();
            //}

            //if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(1).name.Equals("Survey"))
            //{
                //socketClient.SendMessageNet2("score");
                //Debug.Log(filePath);
                //if (File.Exists(filePath))
                //{
                //    List<Dictionary<string, object>> csvData = CSVReader.Read(filePath);

                //    Debug.Log(csvData.Count);
                //    if (!csvData.Count.Equals(0))
                //    {
                //        for (int i = 0; i < csvData.Count; i++)
                //        {
                //            Debug.Log(csvData[csvData.Count - 1]["Frame_Number"]);
                //        }
                //    }
                //}W
                //else
                //{
                //    Debug.Log("no file");
                //}
            //}
        }



        yield return new WaitForSeconds(0.5f);
    }

    private string csvNumber(string[] inFiles){
        csvFiles = new string[inFiles.Length];
        for (int i = 0; i < inFiles.Length; i++)
        {
            csvFiles[i] = Path.GetFileNameWithoutExtension(inFiles[i]);
        }
        string lastFileName = (csvFiles[csvFiles.Length - 1].Substring(5, 8)).Remove(3); ;
        int n = (Convert.ToInt32(lastFileName)) + 1;
        string s = n.ToString();
        print(s.Length);
        if (s.Length == 1) { csvFileNumber = ("00" + s); }
        else if (s.Length == 2) { csvFileNumber = ("0" + s); }
        else if (s.Length == 3) { csvFileNumber = ("" + s); }
        return csvFileNumber;
    }


}
