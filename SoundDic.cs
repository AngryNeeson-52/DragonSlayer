using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ȿ����, ����� ����

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundDic : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private Sound[] sounds;
    [SerializeField]
    private bool effectSound;
    [SerializeField]
    private float vol;

    private WaitForSeconds waittime = new WaitForSeconds(0.1f);

    public void SetVol(float effect, float field) // ���� ������
    {
        if (effectSound)
        {
            vol = effect;
            source.volume = vol;
        }
        else
        {
            vol = field;
            source.volume = vol;
        }
    }

    public void PlaySound(string name) // �⺻�Ҹ� ���
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                source.clip = sounds[i].clip;
                source.volume = vol;
                source.Play();
            }
        }
    }

    public void StopSound()
    {
        source.Stop();
    }

    public void PauseSound()
    {
        source.Pause();
    }

    public void UnPauseSound()
    {
        source.UnPause();
    }

    public void FadeOutSound(float time) // �⺻�Ҹ� - > 0 
    {
        StartCoroutine(FadeOutCoroutine(time));
    }

    IEnumerator FadeOutCoroutine(float time)
    {
        float temp = source.volume;

        for (float i = 0; i < time; i += 0.1f)
        {
            source.volume -= temp / (time * 10);
            yield return waittime;
        }

        source.volume = 0;
    }

    public void FadeInSound(float time) // 0 - > �⺻�Ҹ�
    {
        StartCoroutine(FadeInCoroutine(time));
    }

    IEnumerator FadeInCoroutine(float time)
    {
        source.volume = 0;

        for (float i = 0; i < time; i += 0.1f)
        {
            source.volume += vol / (time * 10);
            yield return waittime;
        }

        source.volume = vol;
    }
}
