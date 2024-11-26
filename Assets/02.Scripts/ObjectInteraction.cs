using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjectInteraction : MonoBehaviour, IMixedRealityTouchHandler
{
    private bool b_touch = false;

    BoundingBox boundingBox;

    Transform tr;
    Vector3 posi;

    private string mainHandStr = string.Empty;
    float xScale = 0;

    bool b_CheckC90 = false;

    void OnEnable()
    {
        if(SessionManager.instance.b_SetObject)
        {
            boundingBox = gameObject.GetComponent<BoundingBox>();
            tr = gameObject.transform;
            posi = tr.position;
            
            if(RootManager.sessionType.Equals(SessionType.Rotate))
            {
                StartCoroutine(CheckRotateState());
            }
            else if(RootManager.sessionType.Equals(SessionType.Scale))
            {
                mainHandStr = PlayerPrefs.GetString("Hand");
                xScale = tr.localScale.x;

                StartCoroutine(CheckScaleState());
            }
        }
    }

    private void OnDisable()
    {
        b_touch = false;
    }

    /// <summary>
    ///     물체 선택 체크
    /// </summary>
    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {
        if(b_touch)
        {
            return;
        }
        
        if(RootManager.sessionType.Equals(SessionType.Select))
        {
            b_touch = true;
            //Debug.Log("touch");

            SoundManager.instance.PlayObjectFinish();
            PooledObject.instance.poolObjs_Object.Remove(gameObject);
            SessionManager.instance.b_SetObject = true;
            Destroy(gameObject);
        }

        //if(eventData.Handedness.IsRight())
        //{
        //    //GameObject obj = eventData.

        //    b_touch = true;
        //    Debug.Log("touch");
            

        //}
    }

    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        b_touch = false;
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        
    }

    public void ObjectInteractionSound()
    {
        SoundManager.instance.PlayObjectClick();
    }


    #region Check Rotate
    /// <summary>
    ///     물체 회전 체크
    /// </summary>
    IEnumerator CheckRotateState()
    {
        while(true)
        {
            //Debug.Log("Rotation : " + tr.localRotation.eulerAngles);

            switch(SessionManager.instance.rotateState)
            {
                case RotateState.CYaw90:
                    if (tr.rotation.eulerAngles.y >= 89f && tr.rotation.eulerAngles.y < 150/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if(tr.rotation.eulerAngles.y >= 150 && tr.rotation.eulerAngles.y < 359.0f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CYaw180:
                    if (tr.rotation.eulerAngles.y >= 179f && tr.rotation.eulerAngles.y < 230)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.y >= 230 && tr.rotation.eulerAngles.y < 359.0f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CPitch90:
                    if (tr.rotation.eulerAngles.x >= 89f && tr.rotation.eulerAngles.x < 150f/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.x >= 150f && tr.rotation.eulerAngles.x < 359.0f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CPitch180:
                    if (tr.rotation.eulerAngles.x >= 85.0f && tr.rotation.eulerAngles.x < 150.0f)
                    {
                        b_CheckC90 = true;
                    }
                    else if ((tr.rotation.eulerAngles.x >= 230.0f && tr.rotation.eulerAngles.x < 359.0f) && (tr.rotation.y.Equals(0) && tr.rotation.z.Equals(0)))
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }

                    if (tr.rotation.eulerAngles.x < 2f && b_CheckC90)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    break;

                case RotateState.CRoll90:
                    if (tr.rotation.eulerAngles.z <= 271f && tr.rotation.eulerAngles.z > 200.0f/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.z > 1.0f && tr.rotation.eulerAngles.z <= 200f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CRoll180:
                    if (tr.rotation.eulerAngles.z <= 181f && tr.rotation.eulerAngles.z > 110f/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.z > 1.0f && tr.rotation.eulerAngles.z <= 110f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CCYaw90:
                    if (tr.rotation.eulerAngles.y <= 271f && tr.rotation.eulerAngles.y > 200.0f/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.y > 1.0f && tr.rotation.eulerAngles.y <= 200f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CCYaw180:
                    if (tr.rotation.eulerAngles.y <= 181f && tr.rotation.eulerAngles.y > 110f/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.y > 1.0f && tr.rotation.eulerAngles.y <= 110f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CCPitch90:
                    if (tr.rotation.eulerAngles.x <= 271f && tr.rotation.eulerAngles.x > 200.0f/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.x > 1.0f && tr.rotation.eulerAngles.x <= 200f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CCPitch180:
                    //Debug.Log(tr.rotation.eulerAngles);

                    if (tr.rotation.eulerAngles.x <= 275.0f && tr.rotation.eulerAngles.x > 150.0f)
                    {
                        b_CheckC90 = true;
                    }
                    else if (tr.rotation.eulerAngles.x > 1.0f && tr.rotation.eulerAngles.x <= 150f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }

                    if (tr.rotation.eulerAngles.x > 358f && b_CheckC90)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    break;

                case RotateState.CCRoll90:
                    if (tr.rotation.eulerAngles.z >= 89f && tr.rotation.eulerAngles.z < 150/* || (tr.rotation.eulerAngles.y >= 210f && tr.rotation.eulerAngles.y < 271f)*/)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.z >= 150 && tr.rotation.eulerAngles.z < 359.0f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;

                case RotateState.CCRoll180:
                    if (tr.rotation.eulerAngles.z >= 179f && tr.rotation.eulerAngles.z < 230)
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    else if (tr.rotation.eulerAngles.z >= 230 && tr.rotation.eulerAngles.z < 359.0f)
                    {
                        tr.rotation = Quaternion.Euler(Vector3.zero);
                        tr.position = posi;
                    }
                    break;
            }

            yield return null;
        }
    }
    #endregion


    #region Check Scale
    /// <summary>
    ///     물체 축소/확대 체크
    /// </summary>
    IEnumerator CheckScaleState()
    {
        while(true)
        {
            switch (SessionManager.instance.scaleState)
            {
                case ScaleState.EnLargeMainHand:
                    //foreach (var controller in CoreServices.InputSystem.DetectedControllers)
                    //{
                    //    if (controller.InputSource.SourceName.Equals(mainHandStr))
                    //    {
                    //        if (!boundingBox.ShowScaleHandles)
                    //        {
                    //            boundingBox.ShowScaleHandles = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        boundingBox.ShowScaleHandles = false;
                    //    }
                    //}

                    if(tr.localScale.x >= (xScale * 2))
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    break;

                case ScaleState.EnLargeSubHand:
                    //foreach (var controller in CoreServices.InputSystem.DetectedControllers)
                    //{
                    //    if (!controller.InputSource.SourceName.Equals(mainHandStr))
                    //    {
                    //        if (!boundingBox.ShowScaleHandles)
                    //        {
                    //            boundingBox.ShowScaleHandles = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        boundingBox.ShowScaleHandles = false;
                    //    }
                    //}

                    if (tr.localScale.x >= (xScale * 2))
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    break;

                case ScaleState.ReduceMainHand:
                    //foreach (var controller in CoreServices.InputSystem.DetectedControllers)
                    //{
                    //    if (controller.InputSource.SourceName.Equals(mainHandStr))
                    //    {
                    //        if (!boundingBox.ShowScaleHandles)
                    //        {
                    //            boundingBox.ShowScaleHandles = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        boundingBox.ShowScaleHandles = false;
                    //    }
                    //}

                    if (tr.localScale.x <= (xScale * 0.5f))
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    break;

                case ScaleState.ReduceSubHand:
                    //foreach (var controller in CoreServices.InputSystem.DetectedControllers)
                    //{
                    //    if (!controller.InputSource.SourceName.Equals(mainHandStr))
                    //    {
                    //        if (!boundingBox.ShowScaleHandles)
                    //        {
                    //            boundingBox.ShowScaleHandles = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        boundingBox.ShowScaleHandles = false;
                    //    }
                    //}

                    if (tr.localScale.x <= (xScale * 0.5f))
                    {
                        SoundManager.instance.PlayObjectFinish();
                        PooledObject.instance.poolObjs_Object.Remove(gameObject);
                        SessionManager.instance.b_SetObject = true;
                        //Debug.Log("correct");
                        Destroy(gameObject);
                    }
                    break;
            }

            yield return null;
        }
    }
    #endregion
}
