using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frustum : MonoBehaviour
{
    public GameObject cubeObj;
    
    [HideInInspector] public Transform cubeTransform;

    public float armLength;

    //void Start()
    //{
        //StartCoroutine(CreateCube());
    //}

    public void CreateCube()
    {
        StartCoroutine(CreateCubeCo());
    }

    private Vector2 CalculateScreenSizeInWorldCoords()
    {
        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, armLength));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, armLength));
        Vector3 p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, armLength));

        float width = (p2 - p1).magnitude;
        float height = (p3 - p2).magnitude;

        Vector2 dimensions = new Vector2(width, height);

        return dimensions;
    }

    /// <summary>
    ///     카메라부터 팔 거리에 카메라 화각 기준 큐브 생성
    /// </summary>
    public IEnumerator CreateCubeCo()
    {
        SessionManager.instance.frustum = gameObject.GetComponent<Frustum>();

        yield return new WaitForSeconds(1.0f);

        float frustumHeight = 2.0f * 1f * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustWidth = frustumHeight * Camera.main.aspect;
        float distance = frustumHeight * 0.5f / Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        Vector2 dimensions = CalculateScreenSizeInWorldCoords();
        //Debug.Log("dimensions.x : " + dimensions.x + ", " + "dimensions.y : " + dimensions.y);

        GameObject obj = Instantiate(cubeObj);
        SessionManager.instance.cubeObj = obj;
        cubeTransform = obj.transform;
        
        //cubeTransform.localScale = new Vector3(frustWidth, frustumHeight, 2);
        cubeTransform.localScale = new Vector3(dimensions.x, dimensions.y, armLength);
        cubeTransform.rotation = Quaternion.identity;

        SessionManager.instance.CreateSpace(dimensions.x, dimensions.y, armLength);

        cubeTransform.position = new Vector3(0, 0, (armLength * 0.5f) + 0.3f);
        //Camera.main.nearClipPlane = 0.1f;
        //var Camera.main.fieldOfView = 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;
    }
}
