using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// »≠∏È ¿Ã∫•∆Æ

public class ScreenEffect : MonoBehaviour
{
    [SerializeField]
    private RectTransform up, down;
    [SerializeField]
    private Image whiteScreen;
    [SerializeField]
    private float fadespeed;
    [SerializeField]
    private Vector2 speed;

    private Coroutine effectcor;
    private Color temp;

    private PlayerStuff players;

    private void Start()
    {
        players = FindObjectOfType<PlayerStuff>();
    }


    public void ComeIn() // ∞À¿∫ ∂Ï ª˝º∫
    {
        if (effectcor != null)
        {
            StopCoroutine(effectcor);
        }
        effectcor = StartCoroutine(EffectINCoroutine());
    }

    public void GetOut() // ∞À¿∫ ∂Ï º“∏Í
    {
        if (effectcor != null)
        {
            StopCoroutine(effectcor);
        }
        effectcor = StartCoroutine(EffectOutCoroutine());
    }


    IEnumerator EffectINCoroutine()
    {
        players.mainCanvas.gameObject.SetActive(false);

        while (down.anchoredPosition.y <= down.rect.height * 2 / 5)
        {
            up.anchoredPosition -= speed * Time.deltaTime;
            down.anchoredPosition += speed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator EffectOutCoroutine()
    {
        while (up.anchoredPosition.y <= up.rect.height / 2)
        {
            up.anchoredPosition += speed * Time.deltaTime;
            down.anchoredPosition -= speed * Time.deltaTime;
            yield return null;
        }

        players.mainCanvas.gameObject.SetActive(true);
    }

    public void PortalEffect() // ∆˜≈ª «œæ· ¿‹ªÛ
    {
        if (effectcor != null)
        {
            StopCoroutine(effectcor);
        }
        effectcor = StartCoroutine(FadeCoroutine());
    }
    IEnumerator FadeCoroutine()
    {
        players.mainCanvas.gameObject.SetActive(false);
        temp = whiteScreen.color;
        temp.a = 0;
        while (temp.a <= 0.95f)
        {
            temp.a += fadespeed; 
            whiteScreen.color = temp; 
            yield return null;
        }
        while(temp.a >= 0)
        {
            temp.a -= fadespeed;
            whiteScreen.color = temp;
            yield return null;
        }
        players.mainCanvas.gameObject.SetActive(true);
    }
}
