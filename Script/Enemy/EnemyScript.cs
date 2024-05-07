using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int hp = 3;                      //�� ü��
    public float speed = 1.0f;              //�� �ӵ�
    public int roomNumber = 1;    //��ġ�� ��ġ
    public float attackDistance = 10;    //���� �Ÿ�

    //�ִϸ��̼� ���
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackingAnime = "EnemyAttaking";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    public string mjscAnime = "EnemyMjsc";

    public GameObject bulletPrefab;         //�Ѿ�
    public float shootSpeed = 5.0f;         //�Ѿ� �ӵ�

    //���� & ���� �ִϸ��̼�
    string nowAnimation = "";
    string oldAnimation = "";

    //�Էµ� �̵� ��
    float axisH;
    float axisV;
    Rigidbody2D rbody;

    //Ȱ��ȭ ����
    bool isActive = false;
    bool isAttack = false;  //���� ����
    bool isAct = true;
    bool isHit = false;

    List<GameObject> bulletStats; //�Ѿ��� ǥ���� �ѹ��� ��� ���� List�� ����
    int count = 0; //�Ѿ� ���� ī��Ʈ

    int bolletShape = 0;
    private const int NshapeMaxBullet = 30;
    private const int RshapeMaxBullet = 22;
    private const int PshapeMaxBullet = 32;

    //R���� �Ѿ� ���󰡴� ����
    int rDelay = 0;

    bool RShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        bulletStats = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            isActive = true;

            //�÷��̾���� �Ÿ� Ȯ��
            Vector3 plpos = player.transform.position;
            float dist = Vector2.Distance(transform.position, plpos);

            if (!isAttack)
            {
                //�÷��̾ ���� �ȿ� �ְ� ���� ���� �ƴ� ���
                if (dist <= attackDistance)
                {
                    isAttack = true;

                    axisH = 0.0f;
                    axisV = 0.0f;
                    nowAnimation = attackAnime;
                }
                //�÷��̾ �ν� ������ ��� ���
                else if (dist > attackDistance)
                {
                    //�÷��̾���� �Ÿ��� �������� ������ ���ϱ�
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rad = Mathf.Atan2(dy, dx);
                    float angleZ = rad * Mathf.Rad2Deg;

                    nowAnimation = idleAnime;

                    //�̵� ����
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;
                }
            }
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            if (!isAttack)
            {
                //���� �̵���Ű��
                rbody.velocity = new Vector2(axisH, axisV);
            }
            else
            {
                if (isAttack)
                {
                    rbody.velocity = Vector2.zero;

                    GameObject player = GameObject.FindGameObjectWithTag("Player");

                    if (player != null)
                    {
                        if (!isAct)
                        {
                            switch (bolletShape)
                            {
                                case 0:
                                    #region [ N ���� �߻� ]
                                    if (count < NshapeMaxBullet)
                                    {
                                        //Debug.Log("Charge" + count);
                                        isAct = true;
                                        float objX = 0, objY = 0;

                                        if (count < 10)
                                        {
                                            objX = transform.position.x - 1;
                                            objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                                        }
                                        else if (count < 20)
                                        {
                                            objX = transform.position.x + (float)(-1 + (0.2 * (count % 10)));
                                            objY = transform.position.y + (float)(1 - (0.2 * (count % 10)));
                                        }
                                        else
                                        {
                                            objX = transform.position.x + 1;
                                            objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                                        }

                                        Vector3 bulletTran = new Vector3(objX, objY);

                                        //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
                                        Quaternion r = Quaternion.Euler(0, 0, 0);
                                        GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);

                                        if (bullet != null) bulletStats.Add(bullet);

                                        count++;

                                        isAct = false;
                                    }
                                    #endregion
                                    break;
                                case 1:
                                    #region [ R ���� �߻� ]
                                    if (count < RshapeMaxBullet)
                                    {
                                        //Debug.Log("Charge" + count);
                                        isAct = true;
                                        float objX = 0, objY = 0;

                                        float minX = -0.5f, maxX = 0.5f;
                                        float minY = -0.75f, maxY = 0.75f;

                                        if (count < 5)
                                        {
                                            objX = transform.position.x + minX;
                                            objY = transform.position.y + (float)(minY + (((maxY - minY) / 5.0f) * count));
                                        }
                                        else if(count < 8)
                                        {
                                            objX = transform.position.x + (float)(minX + ((minX + 1.25f) / 3) * (count - 5));
                                            objY = transform.position.y + maxY;
                                        }
                                        else if (count < 14)
                                        {
                                            objX = transform.position.x - 0.6f + (Mathf.Cos((0 + (float)Math.Truncate(Math.Abs((count - 10.5f)))) * 0.3f));
                                            objY = transform.position.y + maxY - (maxY * ((float)(count - 8) / 6));

                                            //Debug.Log(objY);
                                        }
                                        else if (count < 17)
                                        {
                                            objX = transform.position.x - (float)(0.2f * (count - 14));
                                            objY = transform.position.y;
                                        }
                                        else
                                        {
                                            objX = transform.position.x + (float)((0.5f / 6.0f) * (count - 17));
                                            objY = transform.position.y - (float)((1.0f / 6.0f) * (count - 17));
                                        }

                                        Vector3 bulletTran = new Vector3(objX, objY);

                                        //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
                                        Quaternion r = Quaternion.Euler(0, 0, 0);
                                        GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);

                                        if (bullet != null) bulletStats.Add(bullet);

                                        count++;

                                        isAct = false;
                                    }
                                    #endregion
                                    break;
                                case 2:
                                    #region [ �� ���� �߻� ]
                                    if (count < PshapeMaxBullet)
                                    {
                                        //Debug.Log("Charge" + count);
                                        isAct = true;
                                        float objX = 0, objY = 0;

                                        float minY = -1.2f, maxY = 1.2f;

                                        if (count < 22)
                                        {
                                            float rad = (float)((count * 360.0f / 22.0f) * Math.PI / 180.0f);

                                            objX = transform.position.x + (float)Math.Sin(rad);
                                            objY = transform.position.y + (float)Math.Cos(rad);
                                        }
                                        else
                                        {
                                            float newCount = count - 22;
                                            float remainBulletCnt = PshapeMaxBullet - 22 - 1;

                                            //Debug.Log(maxY - ((maxY - minY) * (newCount / remainBulletCnt)));

                                            objX = transform.position.x;
                                            objY = transform.position.y + (float)maxY - ((maxY - minY) * (newCount / remainBulletCnt));
                                        }

                                        Vector3 bulletTran = new Vector3(objX, objY);

                                        //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
                                        Quaternion r = Quaternion.Euler(0, 0, 0);
                                        GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);

                                        if (bullet != null) bulletStats.Add(bullet);

                                        count++;

                                        isAct = false;
                                    }
                                    #endregion
                                    break;
                            }
                        }
                        else if(RShoot)
                        {
                            //Debug.Log("Shoot1");

                            if (rDelay == 0)
                            {
                                var bs = bulletStats[0];

                                float dx = player.transform.position.x - bs.transform.position.x;
                                float dy = player.transform.position.y - bs.transform.position.y;

                                //��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
                                float rad = Mathf.Atan2(dy, dx);

                                //������ ����(���ʺй�)�� ��ȯ
                                float angle = rad * Mathf.Rad2Deg;

                                bs.transform.rotation = Quaternion.Euler(0, 0, angle);

                                float x = Mathf.Cos(rad);
                                float y = Mathf.Sin(rad);
                                Vector3 v = new Vector3(x, y) * shootSpeed;

                                //Debug.Log("Shoot2");

                                Rigidbody2D rbody = bs.GetComponent<Rigidbody2D>();
                                rbody.AddForce(v, ForceMode2D.Impulse);

                                //Debug.Log("Shoot3");
                                bulletStats.Remove(bs);

                                if (bulletStats.Count == 0)
                                {
                                    count = 0;
                                    RShoot = false;
                                    nowAnimation = attackFinishAnime;
                                }
                            }
                            else if(rDelay == 4) rDelay = -1;

                            rDelay++;
                        }
                    }
                }
            }
            //�ִϸ��̼� �����ϱ�
            if (nowAnimation != oldAnimation)
            {
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }

    //����
    void Attack()
    {
        //�÷��̾� ������ ��������
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            #region [ ���� �߻� �ּ� ]
            ////�� �� ����� �Ÿ��� ���ϱ�
            //float dx = player.transform.position.x - transform.position.x;
            //float dy = player.transform.position.y - transform.position.y;

            ////��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
            //float rad = Mathf.Atan2(dy, dx);

            ////������ ����(���ʺй�)�� ��ȯ
            //float angle = rad * Mathf.Rad2Deg;

            ////�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
            //Quaternion r = Quaternion.Euler(0, 0, angle);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, r);
            //float x = Mathf.Cos(rad);
            //float y = Mathf.Sin(rad);
            //Vector3 v = new Vector3(x, y) * shootSpeed;

            ////�Ѿ� �߻�
            //Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            //rbody.AddForce(v, ForceMode2D.Impulse);
            #endregion

            bolletShape = UnityEngine.Random.Range(0, 3);
            //bolletShape = 2;

            isAct = false;
        }
    }

    void AttackAnimationEnd()
    {
        isAct = true;
        isAttack = false;
    }

    void InBulletCharge()
    {
        nowAnimation = attackingAnime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //������ ȭ���� �¾��� ���
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp--; //ü�� ����

            //ü���� 0 ���ϰ� �Ǵ� ���� ���ó��
            if (hp <= 0)
            {
                rbody.velocity = Vector2.zero;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Animator>().Play(deadAnime);

                foreach (var bs in bulletStats)
                {
                    Destroy(bs);
                }

                Destroy(gameObject, 1);
            }
            else
            {
                if(!isHit)StartCoroutine(Blink());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private IEnumerator Blink()
    {
        isHit = true;

        //Debug.Log("blink");
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = new Color(1, 1, 1, 1);

        playerSprite.color = new Color(1, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

        playerSprite.color = defaultColor;

        isHit = false;
    }

    private void BulletShoot()
    {
        if (bolletShape == 0)
        {
            #region [ N ��� ������ �÷��̾�� �߻� ]
            if (count == NshapeMaxBullet)
            {
                //Debug.Log("Shoot!");
                isAct = true;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                foreach (var bs in bulletStats)
                {
                    if (bs != null)
                    {
                        float dx = player.transform.position.x - bs.transform.position.x;
                        float dy = player.transform.position.y - bs.transform.position.y;

                        //��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
                        float rad = Mathf.Atan2(dy, dx);

                        //������ ����(���ʺй�)�� ��ȯ
                        float angle = rad * Mathf.Rad2Deg;

                        bs.transform.rotation = Quaternion.Euler(0, 0, angle);

                        float x = Mathf.Cos(rad);
                        float y = Mathf.Sin(rad);
                        Vector3 v = new Vector3(x, y) * shootSpeed;

                        Rigidbody2D rbody = bs.GetComponent<Rigidbody2D>();
                        rbody.AddForce(v, ForceMode2D.Impulse);
                    }
                }

                bulletStats.Clear();
                count = 0;

                nowAnimation = attackFinishAnime;
            }
            #endregion
        }
        else if(bolletShape == 1)
        {
            #region [ R ��� �Ѱ��� �߻� ]
            if (count == RshapeMaxBullet)
            {
                //Debug.Log("Shoot!");
                RShoot = true;

                isAct = true;
            }
            #endregion
        }
        else if (bolletShape == 2)
        {
            #region [ �� ��� ������ �÷��̾�� �߻� ]
            if (count == PshapeMaxBullet)
            {
                //Debug.Log("Shoot!");
                isAct = true;

                foreach (var bs in bulletStats)
                {
                    if (bs != null)
                    {
                        float dx = transform.position.x - bs.transform.position.x;
                        float dy = transform.position.y - bs.transform.position.y;

                        //��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
                        float rad = Mathf.Atan2(dy, dx);

                        //������ ����(���ʺй�)�� ��ȯ
                        float angle = rad * Mathf.Rad2Deg;

                        bs.transform.rotation = Quaternion.Euler(0, 0, angle);

                        float x = Mathf.Cos(rad) * -1;
                        float y = Mathf.Sin(rad) * -1;
                        Vector3 v = new Vector3(x, y) * shootSpeed;

                        Rigidbody2D rbody = bs.GetComponent<Rigidbody2D>();
                        rbody.AddForce(v, ForceMode2D.Impulse);
                    }
                }

                bulletStats.Clear();
                count = 0;

                nowAnimation = attackFinishAnime;
            }
            #endregion
        }
    }
}