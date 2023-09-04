using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̾ �ڵ� (������ ����)

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private GameObject fireZone;


    void Update()
    {
        if (this.transform.position.y <= 0)
        {
            GameObject prefab = Instantiate(fireZone);
            prefab.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
            Destroy(this.gameObject);
        }
    }
}
