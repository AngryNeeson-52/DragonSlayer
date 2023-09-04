using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 조우시 관련 이벤트

public class BossIncount : MonoBehaviour
{
    [SerializeField]
    private GameObject producePos, dragon, fieldSound;
    [SerializeField]
    private float fadeSpeed;

    private DragonAI dragonAI;
    private Camera mainCamera;
    private ScreenEffect screenEffect;

    private Vector3 zeroPos;
    private Quaternion zeroRot;
    private bool incounted = true;

    private void Start()
    {
        dragonAI = FindObjectOfType<DragonAI>();
        mainCamera = FindObjectOfType<Camera>();
        screenEffect = FindObjectOfType<ScreenEffect>();
    }

    private void OnTriggerEnter(Collider other) // 보스 입장 트리거
    {
        if(incounted)
        {
            incounted = false;
            dragonAI.GetPlayer(other);
            fieldSound.GetComponent<SoundDic>().FadeOutSound(1.0f);

            StartCoroutine(CameraCoroutine(other));
        }
    }

    IEnumerator CameraCoroutine(Collider other) // 보스 카메라 연출
    {
        zeroPos = mainCamera.transform.localPosition;
        zeroRot = mainCamera.transform.localRotation;

        screenEffect.ComeIn();
        mainCamera.transform.rotation = Quaternion.LookRotation(producePos.transform.position - new Vector3(other.transform.position.x, 20, other.transform.position.z));
        
        while (Vector3.Distance(mainCamera.transform.position, producePos.transform.position) > 1)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, producePos.transform.position, fadeSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        dragon.GetComponent<SoundDic>().PlaySound("Roar");
        yield return new WaitForSeconds(2.5f);
        dragon.GetComponent<SoundDic>().PlaySound("Roar");
        screenEffect.GetOut();

        while (Vector3.Distance(mainCamera.transform.position, zeroPos) > 1)
        {
            mainCamera.transform.position = Vector3.Lerp(zeroPos, mainCamera.transform.position, 0.5f);
            yield return null;
        }

        mainCamera.transform.localPosition = zeroPos;
        mainCamera.transform.localRotation = zeroRot;

        fieldSound.GetComponent<SoundDic>().PlaySound("Boss");
        fieldSound.GetComponent<SoundDic>().FadeInSound(2.0f);
    }
}
