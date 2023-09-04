using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� �ൿ ����

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

    private void OnTriggerEnter(Collider other) // �ν� ���� ����
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

    private void OnTriggerExit(Collider other) // �ν� ���� ��
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

    IEnumerator AggroCoroutine(Collider other) //�ν� �� �÷��̾�� �̵�, ����
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

    IEnumerator DeAggroCoroutine() // �� �� ��Ȱ��ȭ ���·� ����
    {
        anim.SetBool("Moving", false);
        anim.SetBool("Aggro", false);
        yield return new WaitForSeconds(5.0f);
        anim.SetTrigger("Hide");
        yield return new WaitForSeconds(3.0f);
        hpBar.SetActive(false);
    }
}
