using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;

    private CaterpillarMovement controller;

    float speedOnStart;

    void Start()
    {
        controller = GameObject.FindObjectOfType<CaterpillarMovement>();
        speedOnStart = followSpeed;
    }

    public void Init(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(!target) return;
        followSpeed = (controller.speed) + speedOnStart;

        transform.position = Vector3.Lerp(transform.position, target.position - target.TransformDirection(offset), followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}
