using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grass : MonoBehaviour
{
    bool canRotate = true;

    public void Rotate()
    {
        if(!canRotate) return;
        transform.DOKill(true);
        transform.DOPunchRotation(transform.right * 50, .5f);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        canRotate = false;
        yield return new WaitForSeconds(0.5f);
        canRotate = true;
    }
}
