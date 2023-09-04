using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//몬스터 스텟, 보유 골드, 피격 이벤트

public class EnemyStats : Stats
{
    [SerializeField]
    private int dropGoldMin, dropGoldMax;
    [SerializeField]
    private GameObject enemy, enemyAI, atkobject, announce;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool superArmor;
    [SerializeField]
    private Image HPBar;

    private PlayerStats thePlayer;
    private bool gethit = false;
    private WaitForSeconds waittime = new WaitForSeconds(0.7f);

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerStats>();
        Wallet += Random.Range(dropGoldMin, dropGoldMax);
    }

    private void OnTriggerEnter(Collider other) // 피격 확인
    {
        if(!gethit)
        {
            gethit = true;
            Vector3 camera = Camera.main.WorldToScreenPoint(this.transform.position);
            GameObject prefab = Instantiate(announce);
            prefab.GetComponent<TextFloating>().TextSet((-thePlayer.ATK).ToString(), Color.blue, camera);

            if (CurrentHPChange(-thePlayer.ATK))
            {
                HPBar.fillAmount = CurrentHP / MaxHP;

                if (superArmor)
                {
                    StartCoroutine(ArmorHitCoroutine());
                }
                else 
                {
                    StartCoroutine(HitCoroutine());
                }
            }
            else 
            {
                StartCoroutine(DeadCoroutine());
            }
        }
    }

    IEnumerator HitCoroutine() // 피격 처리
    {
        enemyAI.SetActive(false);
        anim.SetTrigger("Hit");
        enemy.GetComponent<SoundDic>().PlaySound("Hit");
        yield return waittime;
        gethit = false;
        enemyAI.SetActive(true);
    }

    IEnumerator ArmorHitCoroutine() // 슈퍼 아머 피격 처리
    {
        enemy.GetComponent<SoundDic>().PlaySound("Hit");
        yield return waittime;
        gethit = false;
    }

    IEnumerator DeadCoroutine() // 사망 처리
    {
        HPBar.fillAmount = 0;
        enemyAI.SetActive(false);
        atkobject.SetActive(false);
        enemy.GetComponent<SoundDic>().PlaySound("Roar");
        anim.SetTrigger("Dead");
        anim.SetTrigger("Hit");
        thePlayer.Wallet += Wallet;
        yield return new WaitForSeconds(1.5f);

        Vector3 camera = Camera.main.WorldToScreenPoint(this.transform.position);
        GameObject prefab = Instantiate(announce);
        prefab.GetComponent<TextFloating>().TextSet("+ " + (Wallet).ToString() + " Gold", Color.cyan, camera);

        yield return new WaitForSeconds(3.0f);
        Destroy(enemy);
    }
}
