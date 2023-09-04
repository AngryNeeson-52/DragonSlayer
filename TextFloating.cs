using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//시스템 메시지 이동 & 파괴

public class TextFloating : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private float floatingDist;
    [SerializeField]
    private float floatingSpeed;

    public void TextSet(string info, Color color, Vector3 pos)
    {
        text.text = info;
        text.color = color;
        text.transform.position = pos;

        StartCoroutine(AnnounceCoroutine());
    }

    IEnumerator AnnounceCoroutine()
    {
        Vector3 startpos = text.transform.position;

        while (Vector3.Distance(startpos, text.transform.position) <= floatingDist)
        {
            text.transform.position += text.transform.up * floatingSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }
}
