using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//필드 브금 교체

public class SoundChange : MonoBehaviour
{
    [SerializeField]
    private bool selectMusic = true;
    [SerializeField]
    private GameObject fieldSound;
    [SerializeField]
    private string bgm1, bgm2;

    Coroutine musicChange;

    private void OnTriggerEnter(Collider other)
    {
        if (musicChange != null)
        {
            StopCoroutine(musicChange);
        }
        musicChange = StartCoroutine(MusicChangeCoroutine());
    }

    IEnumerator MusicChangeCoroutine()
    {
        if (selectMusic)
        {
            selectMusic = false;

            fieldSound.GetComponent<SoundDic>().FadeOutSound(2.0f);
            yield return new WaitForSeconds(3.0f);
            fieldSound.GetComponent<SoundDic>().PlaySound(bgm2);
        }
        else
        {
            selectMusic = true;

            fieldSound.GetComponent<SoundDic>().FadeOutSound(2.0f);
            yield return new WaitForSeconds(3.0f);
            fieldSound.GetComponent<SoundDic>().PlaySound(bgm1);
        }
    }
}
