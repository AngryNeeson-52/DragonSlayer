using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 선인장 몬스터 행동 관련

public class CactusAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy, fist, hpBar;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float moveSpeed, moveDistance, attackRange;

    private Vector3 beforePos, dir;

    Coroutine aggro, deaggro;
    WaitForSeconds waittime = new WaitForSeconds(1.0f);

    private void OnTriggerEnter(Collider other) // 인식 범위 진입
    {
        if (aggro != null)
        {
            StopCoroutine(aggro);
        }

        if (deaggro != null)
        {
            StopCoroutine(deaggro);
        }

        aggro = StartCoroutine(AggroCoroutine(other));
    }

    private void OnTriggerExit(Collider other) // 인식 범위 퇴각
    {
        if (aggro != null)
        {
            StopCoroutine(aggro);
        }

        if (deaggro != null)
        {
            StopCoroutine(deaggro);
        }

        deaggro = StartCoroutine(DeAggroCoroutine());
    }

    IEnumerator AggroCoroutine(Collider other) //인식 시 플레이어에게 이동, 공격
    {
        anim.SetBool("Aggro", true);
        yield return waittime;
        hpBar.SetActive(true);

        while (true)
        {
            beforePos = enemy.transform.position;
            enemy.transform.LookAt(other.transform);

            while (Vector3.Distance(beforePos, enemy.transform.position) <= moveDistance)
            {
                dir = other.transform.position - enemy.transform.position;
                enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);

                if (Vector3.Distance(other.transform.position, enemy.transform.position) < attackRange)
                {
                    anim.SetBool("Moving", false);
                    fist.SetActive(true);
                    anim.SetTrigger("Attack");
                    yield return waittime;
                    fist.SetActive(false);
                    break;
                }
                else
                {
                    anim.SetBool("Moving", true);
                    enemy.transform.position += enemy.transform.forward * moveSpeed * Time.deltaTime;
                }
                yield return null;
            }
            anim.SetBool("Moving", false);

            yield return waittime;
            yield return waittime;
        }
    }

    IEnumerator DeAggroCoroutine() // 퇴각 시 비활성화 상태로 변경
    {
        anim.SetBool("Moving", false);
        anim.SetBool("Aggro", false);
        yield return new WaitForSeconds(5.0f);
        anim.SetTrigger("Hide");
        yield return new WaitForSeconds(3.0f);
        hpBar.SetActive(false);
    }
}
