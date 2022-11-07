using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    void Update()
    {
        Vector3 targetPos = new Vector3(Mathf.Clamp(target.position.x, -4, 4), transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10);

        
    }
}
