using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasController : MonoBehaviour
{
    // �ν����Ϳ��� ��Ʈ 0,1,2�� �̹��� �־�Ѱ�
    public Sprite heart0;
    public Sprite heart1;
    public Sprite heart2;

    // 1,2,3��° ��Ʈ ������ SpriteRenderer
    SpriteRenderer heartNo1Spr;
    SpriteRenderer heartNo2Spr;
    SpriteRenderer heartNo3Spr;
    SpriteRenderer blankBullet1Spr;
    SpriteRenderer blankBullet2Spr;
    SpriteRenderer blankBullet3Spr;

    Text keyCount;
    Text moneyBulletCount;

    // PlayerController ���� ������ �����Ұ�
    string gameState;                  
    bool inlobby;                         

    // Start is called before the first frame update
    void Start()
    {
        GameObject hearts = transform.Find("Hearts").gameObject;
        heartNo1Spr = hearts.transform.Find("HeartNo1").GetComponent<SpriteRenderer>();
        heartNo2Spr = hearts.transform.Find("HeartNo2").GetComponent<SpriteRenderer>();
        heartNo3Spr = hearts.transform.Find("HeartNo3").GetComponent<SpriteRenderer>();

        GameObject blankBullets = transform.Find("BlankBullets").gameObject;
        blankBullet1Spr = blankBullets.transform.Find("BlankBullet1").GetComponent<SpriteRenderer>();
        blankBullet2Spr = blankBullets.transform.Find("BlankBullet2").GetComponent<SpriteRenderer>();
        blankBullet3Spr = blankBullets.transform.Find("BlankBullet3").GetComponent<SpriteRenderer>();

        inlobby = GameObject.Find("Pilot").GetComponent<PlayerController>().inlobby;

        //keyCount = transform.Find("KeyCount").gameObject.GetComponent<Text>();
        //moneyBulletCount = transform.Find("MoneyBulletCount").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        gameState = GameObject.Find("Pilot").GetComponent<PlayerController>().gameState;
        // gameover �϶��� ĵ���� ��Ȱ��ȭ ���� �ƹ��͵� ���� ����
        if (gameState == "gameover" || inlobby)
        {
            if(gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
            return;
        }

        if (PlayerController.hp==6)
        {
            heartNo1Spr.sprite = heart2;
            heartNo2Spr.sprite = heart2;
            heartNo3Spr.sprite = heart2;
        }
        else if (PlayerController.hp == 5)
        {
            heartNo1Spr.sprite = heart2;
            heartNo2Spr.sprite = heart2;
            heartNo3Spr.sprite = heart1;
        }
        else if (PlayerController.hp == 4)
        {
            heartNo1Spr.sprite = heart2;
            heartNo2Spr.sprite = heart2;
            heartNo3Spr.sprite = heart0;
        }
        else if (PlayerController.hp == 3)
        {
            heartNo1Spr.sprite = heart2;
            heartNo2Spr.sprite = heart1;
            heartNo3Spr.sprite = heart0;
        }
        else if (PlayerController.hp == 2)
        {
            heartNo1Spr.sprite = heart2;
            heartNo2Spr.sprite = heart0;
            heartNo3Spr.sprite = heart0;
        }
        else if (PlayerController.hp == 1)
        {
            heartNo1Spr.sprite = heart1;
            heartNo2Spr.sprite = heart0;
            heartNo3Spr.sprite = heart0;
        }
        else if (PlayerController.hp == 0)
        {
            heartNo1Spr.sprite = heart0;
            heartNo2Spr.sprite = heart0;
            heartNo3Spr.sprite = heart0;
        }

        if (ItemData.hasBlankBullets == 0)
        {
            blankBullet1Spr.enabled = false;
            blankBullet2Spr.enabled = false;
            blankBullet3Spr.enabled = false;
        }
        else if (ItemData.hasBlankBullets == 1)
        {
            blankBullet1Spr.enabled = true;
            blankBullet2Spr.enabled = false;
            blankBullet3Spr.enabled = false;
        }
        else if (ItemData.hasBlankBullets == 2)
        {
            blankBullet1Spr.enabled = true;
            blankBullet2Spr.enabled = true;
            blankBullet3Spr.enabled = false;
        }
        else if (ItemData.hasBlankBullets == 3)
        {
            blankBullet1Spr.enabled = true;
            blankBullet2Spr.enabled = true;
            blankBullet3Spr.enabled = true;
        }

        // ���� ���� ���� �ؽ�Ʈ, �� �ؽ�Ʈ
        //keyCount.text = ItemData.hasKeys.ToString();
        //moneyBulletCount.text = ItemData.hasMoneyBullet.ToString();
    }

}
