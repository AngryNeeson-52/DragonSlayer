using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� �ൿ ����

public class MushroomAI : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy, head;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float moveSpeed, moveDistance, attackRange;

    Coroutine aggro;
    WaitForSeconds waittime = new WaitForSeconds(1.0f);

    private void OnTriggerEnter(Collider other) // �ν� ���� ����
    {
        if (aggro != null)
        {
            StopCoroutine(aggro);
        }

        aggro = StartCoroutine(AggroCoroutine(other));
    }

    private void OnTriggerExit(Collider other) // �ν� ���� ��
    {
        if (aggro != null)
        {
            StopCoroutine(aggro);
        }

        anim.SetBool("Aggro", false);
    }

    IEnumerator AggroCoroutine(Collider other) // �νĽ� �ް�����, ����
    {
        anim.SetBool("Aggro", true);
        yield return waittime;

        while(true)
        {
            enemy.transform.LookAt(other.transform);
            while (Vector3.Distance(other.transform.position, enemy.transform.position) < moveDistance)
            {
                enemy.transform.LookAt(other.transform);
                if (Vector3.Distance(other.transform.position, enemy.transform.position) < attackRange)
                {
                    anim.SetBool("Moving", false);
                    head.SetActive(true);
                    anim.SetTrigger("Attack");
                    yield return waittime;
                    head.SetActive(false);
                    yield return waittime;
                }
                else
                {
                    anim.SetBool("Moving", true);
                    enemy.transform.position += -enemy.transform.forward * moveSpeed * Time.deltaTime;
                }
                yield return null;
            }
            
            anim.SetBool("Moving", false);

            yield return waittime;
        }
    }
}
