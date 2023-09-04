using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//몬스터 공격 판정

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject dmgText;

    private Attack attack;


    private void Start()
    {
        attack = FindObjectOfType<Attack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 camera = Camera.main.WorldToScreenPoint(other.transform.position);
        GameObject prefab = Instantiate(dmgText);
        prefab.GetComponent<TextFloating>().TextSet((-damage).ToString(), Color.red, camera);

        attack.GetHIt(damage);
    }

}
