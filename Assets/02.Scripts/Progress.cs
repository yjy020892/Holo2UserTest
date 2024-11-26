using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateProgress());
    }

    IEnumerator RotateProgress()
    {
        while(true)
        {
            transform.Rotate(Vector3.forward * -120.0f * Time.deltaTime);

            yield return null;
        }
    }
}
