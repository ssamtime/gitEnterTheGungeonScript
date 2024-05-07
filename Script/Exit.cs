using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ա� ��ġ ǥ��
public enum ExitDirection
{
    right,
    left,
    down,
    up
};

public class Exit : MonoBehaviour
{
    public string sceneName = "";               // �̵��� scene�̸�
    public int doorNumber = 0;                  // �� ��ȣ
    public ExitDirection direction = ExitDirection.down;    // �� ��ġ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RoomManager.ChangeScene(sceneName, doorNumber);
            //if (sceneName == "MainStage")
            //{
            //    GameObject player = GameObject.FindGameObjectWithTag("Player");
            //    Vector2 pos = player.transform.position;
            //    pos.y += 10;
            //}
        }
    }
}
