using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 출입구 위치 표시
public enum ExitDirection
{
    right,
    left,
    down,
    up
};

public class Exit : MonoBehaviour
{
    public string sceneName = "";               // 이동할 scene이름
    public int doorNumber = 0;                  // 문 번호
    public ExitDirection direction = ExitDirection.down;    // 문 위치

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
