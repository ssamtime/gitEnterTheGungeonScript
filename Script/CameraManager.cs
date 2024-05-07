using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Rect screenRect = Rect.zero;
    string gameState;        //PlayerController ���� ������ �����Ұ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameState = GameObject.Find("Pilot").GetComponent<PlayerController>().gameState;

        if (gameState == "gameover")
        {
            transform.position = player.transform.position;     //gameover�Ǹ� player�� ī�޶� ��ġ �̵�
            return;                                             //gameover�� �ؿ� ���ǹ��� ���� x            
        }
        if (player != null)
        {            
            Vector2 pos = Vector2.zero;
            Vector2 playerPos = new Vector2(player.transform.position.x,player.transform.position.y);
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
            
            if (screenRect.Contains(mousePos))
            {
                pos = Camera.main.ScreenToWorldPoint(mousePos);
            }
            transform.position = Vector2.Lerp(playerPos, pos, 0.5f);
        }
    }
}
