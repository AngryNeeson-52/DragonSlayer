using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ż ���� �̺�Ʈ, ��Ż �۵� �ڵ�

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject toPortal, producePos, fieldSound;
    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private string bgmSelect;

    private PlayerStuff players;
    private Camera mainCamera;
    private ScreenEffect screenEffect;

    private Vector3 zeroPos;
    private Quaternion zeroRot;

    private bool portalCool = false;

    private void Start()
    {
        players = FindObjectOfType<PlayerStuff>();
        mainCamera = FindObjectOfType<Camera>();
        screenEffect = FindObjectOfType<ScreenEffect>();
    }

    private void OnTriggerEnter(Collider other) // ��Ż �̵�
    {
        if (portalCool)
        {
            portalCool = false;
            screenEffect.PortalEffect();
            players.player.transform.position = toPortal.transform.position;
            players.player.transform.rotation = toPortal.transform.rotation;
            fieldSound.GetComponent<SoundDic>().PlaySound(bgmSelect);
            Destroy(this.gameObject);
        }
    }

    private void OnEnable() // ��Ż ����
    {
        StartCoroutine(PortalCoroutine());
    }


    IEnumerator PortalCoroutine()
    {
        yield return new WaitForSeconds(4.0f);

        screenEffect.ComeIn();

        zeroPos = mainCamera.transform.localPosition;
        zeroRot = mainCamera.transform.localRotation;

        mainCamera.transform.rotation = Quaternion.LookRotation(this.transform.position - producePos.transform.position);

        while (Vector3.Distance(mainCamera.transform.position, producePos.transform.position) > 1)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, producePos.transform.position, fadeSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(3.0f);
        screenEffect.GetOut();

        while (Vector3.Distance(mainCamera.transform.position, zeroPos) > 1)
        {
            mainCamera.transform.position = Vector3.Lerp(zeroPos, mainCamera.transform.position, 0.5f);
            yield return null;
        }

        mainCamera.transform.localPosition = zeroPos;
        mainCamera.transform.localRotation = zeroRot;

        portalCool = true;
    }

}
