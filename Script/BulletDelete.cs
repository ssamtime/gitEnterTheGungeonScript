using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDelete : MonoBehaviour
{
    public float deleteTime = 3.0f; //�Ҹ꿡 �ɸ��� �ð�

    public GameObject bulletBombPrefab; //�Ѿ� ������ �ִϸ��̼ǰ��� ������

    GameObject bulletBombObj;       //���������� ���� ��ü

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);    //�Ѿ��� 3�ʵ� �ڵ��Ҹ�
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag.ToLower() == "enemy")
        {
            // ���� ������ �Ѿ� ������ �ִϸ��̼� ���� ��ü����
            bulletBombObj = Instantiate(bulletBombPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(bulletBombObj, 1.0f);
            //�̰� ���׹̿� �޾ƾߵǷ���

            Destroy(gameObject);
        }
    }
}
