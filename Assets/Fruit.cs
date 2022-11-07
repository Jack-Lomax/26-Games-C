using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatHeight;

    private Vector3 scaleOnStart;

    public bool isAlive {get; private set;} = true;

    [SerializeField] public bool isBug;



    float t;

    float yPosOnStart;

    void Start()
    {
        yPosOnStart = transform.position.y;
        scaleOnStart = transform.localScale;

        transform.localScale = Vector3.one * 0.001f;
        transform.DOScale(scaleOnStart, 0.5f).SetEase(Ease.OutSine);
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        t += Time.deltaTime * floatSpeed;

        transform.position = new Vector3(transform.position.x, yPosOnStart + Mathf.Sin(Time.time * Mathf.PI * 2 * floatSpeed) * floatHeight, transform.position.z);

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

        if(transform.localScale.magnitude == 0)
            Destroy(gameObject);

    }

    public void Despawn()
    {
        isAlive = false;
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        transform.DORotate(Vector3.zero, 0.3f, RotateMode.FastBeyond360).SetEase(Ease.InBack);
    }

}
