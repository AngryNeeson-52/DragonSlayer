using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//파이어볼 코드 (불장판 생성)

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
