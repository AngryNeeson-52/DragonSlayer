using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  불장판 코드
public class FireZone : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ZoneCoroutine());
    }

    IEnumerator ZoneCoroutine()
    {
        yield return new WaitForSeconds(5.0f);

        Destroy(this.gameObject);
    }
}
