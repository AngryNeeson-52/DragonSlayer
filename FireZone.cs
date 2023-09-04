using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �ڵ�
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
