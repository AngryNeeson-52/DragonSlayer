using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//보스룸 앞 드래곤 코고는 소리, 화면 흔들리는 이벤트

public class BossFront : MonoBehaviour
{
    [SerializeField]
    private float shakePower, shakeTime;
    [SerializeField]
    private bool onlyOne = true;
    [SerializeField]
    private GameObject dragon, fieldSound;

    private Camera theCamera;
    private ScreenEffect screenEffect;

    private Vector3 zeroPos;
    private Quaternion zeroRot;

    private void Start()
    {
        theCamera = FindObjectOfType<Camera>();
        screenEffect = FindObjectOfType<ScreenEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(onlyOne)
        {
            onlyOne = false;

            StartCoroutine(BossRoomFrontCoroutine());
        }
    }

    IEnumerator BossRoomFrontCoroutine()
    {
        screenEffect.ComeIn();
        fieldSound.GetComponent<SoundDic>().PauseSound();
        dragon.GetComponent<SoundDic>().PlaySound("Sleep");

        zeroPos = theCamera.transform.localPosition;
        zeroRot = theCamera.transform.localRotation;


        for(float i = 0; i <= shakeTime; i += Time.deltaTime)
        {
            theCamera.transform.localPosition = theCamera.transform.localPosition + Random.insideUnitSphere * shakePower;
            yield return null;
        }

        theCamera.transform.localPosition = zeroPos;
        theCamera.transform.localRotation = zeroRot;

        fieldSound.GetComponent<SoundDic>().FadeInSound(1.0f);
        fieldSound.GetComponent<SoundDic>().UnPauseSound();

        screenEffect.GetOut();

        Destroy(this.gameObject);
    }
}
