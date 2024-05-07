using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GunController : MonoBehaviour
{
    // �� �ִϸ�����
    private Animator gunAnimator;

    // �ִϸ��̼�
    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string pilotGunReload = "PilotGunReload";

    string gameState;           //PlayerController ���� ������ �����Ұ�

    public float shootSpeed;    //ȭ�� �ӵ�
    public float shootDelay;    //�߻� ���� 0.5�� �ν����Ϳ� ������

    public int pilotGunBulletCount;     //PilotGunBullet�Ѿ� ����
    public int redGunBulletCount;     //�Ѿ� ����

    public int gunNumber;       //�� ���� �ĺ� ����
    public int gunMaxCount;     //������ �ִ� ���� ����    

    public GameObject pilotGunPrefab;       //���Ϸ� �� ������
    public GameObject redGunPrefab;         //���� �� ������
    public GameObject pilotGunBulletPrefab; //���Ϸ��Ѿ� ������
    public GameObject redGunBulletPrefab;   //�����Ѿ� ������

    public GameObject otherHandPrefab;  //�ѵ�� �ݴ��� �� ������
    public GameObject reloadBack;       //������ �� ����
    public GameObject reloadBar;        //������ ���� ����
    public GameObject reloadText;       //������ ������
    public GameObject blankBullet;      //����ź ������

    public GameObject pilotgunFireEffectPrefab; //

    GameObject gunObj;                  //�ѿ�����Ʈ
    GameObject gunGateObj;              //����
    GameObject LeftHandObj;             //�޼�
    GameObject RightHandObj;            //������
    GameObject ReloadBack;              //R������ ��������� �� ����
    GameObject ReloadBar;               //R������ ��������� ���� ����
    GameObject ReloadText;              //������ ����(3d legacy text)
    GameObject bulletObj;               //�Ѿ� ���ǹ��ȿ� �ȵ��� �����ص�
    GameObject pilotgunFireEffectObj;   //pilotGun �ѽ� ����Ʈ

    public bool canAttack;              //���� ������ �Ҷ� ���
    private bool isReloading;           //�����ϴ��� ���� �ȵǰ�
    bool isLeftHand;        //�޼��ִ���
    bool isRightHand;       //�������ִ���
    bool inlobby;           //PlayerController�� ���� �����ͼ� ������ ����
    bool isReloadText;       //������ ���� �����Ѱ����ҷ���

    TextMesh textMesh;
    Color textColor;

    PlayerController playerController;  //PlayerController�� �Լ�,���� ����ϱ����� �����ص�

    Vector3 mousePosition;

    public AudioClip pilotGunSound;
    public AudioClip redGunSound;
    public AudioClip reloadSound;
    public AudioClip BBSound;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        InstantiateGun();

        gunNumber = 1;
        gunMaxCount = 2;
        pilotGunBulletCount = 8;
        redGunBulletCount = 20;

        inlobby = GetComponent<PlayerController>().inlobby;

        // ����� �ҽ� ��������
        if (GameObject.Find("SeSoundPrefab") != null)
            audioSource = GameObject.Find("SeSoundPrefab").GetComponent<AudioSource>();
        else
            audioSource = GetComponent<AudioSource>();

        canAttack = true;
        isReloading = false;
        isReloadText = false;

        //Cursor.lockState = CursorLockMode.Confined;     // Ŀ�� ȭ������� ����������
    }

    // Update is called once per frame
    void Update()
    {
        gameState = GetComponent<PlayerController>().gameState;
        // gameover �϶��� �ƹ� �͵� ���� ����
        if (gameState == "gameover")
        {
            gunObj.GetComponent<SpriteRenderer>().enabled=false;
            return;
        }
        // ���콺 �� ��ũ�� ���� ���
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");

        // ���� ��ũ�� 
        if (scrollValue > 0.0f)
        {            
            gunNumber++;        // �ѹ�ȣ �ٲٱ�
            if (gunNumber > gunMaxCount)
                gunNumber = 1;
            Destroy(gunObj);    //����ִ��� ���ֱ�
            InstantiateGun();   //�� ����
            Destroy(ReloadText);    //������ ���������� ���ֱ�
            isReloadText = false;
        }
        else if (scrollValue < 0.0f)    // �Ʒ������� ��ũ��
        {
            gunNumber--;
            if (gunNumber <= 0)
                gunNumber = gunMaxCount;
            Destroy(gunObj);
            InstantiateGun();
            Destroy(ReloadText);    //������ ���������� ���ֱ�
            isReloadText = false;
        }

        SpriteRenderer gunSpr = gunObj.GetComponent<SpriteRenderer>();          //���� SpriteRenderer
        Transform childTransform = gunObj.transform.Find("PilotHand");          //�ڽĿ� �����ϱ�����
        SpriteRenderer handSpr = childTransform.GetComponent<SpriteRenderer>(); //���� SpriteRenderer
        PlayerController plmv = GetComponent<PlayerController>();               //player�� SpriteRenderer
        
        // playerController�� ���콺 ������ ���� ��������
        mousePosition = FindObjectOfType<PlayerController>().mousePosition; 

        // ��,��,�ѱ� ��ġ,ȸ��
        if (mousePosition .x> transform.position.x) // ���콺�� ĳ���� �����ʿ� ������
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = false;                                    //�������
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ - 90);                   //���콺�� ���� ��ȸ��          
            gunObj.transform.position = transform.position + new Vector3(0.35f, -0.2f, 0);          //����ġ ĳ���� ���������� new Vector3(0.2f, -0.15f, 0);

            gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//�ѱ� ��ġ
            if (plmv.angleZ < -45 && plmv.angleZ > -135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//�ؿ� ���� �ѱ���ġ ����
            else if (plmv.angleZ < 120 && plmv.angleZ > 60)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) + 0.3f);//���� ���� �ѱ���ġ ����

            childTransform.position = transform.position + new Vector3(0.35f, -0.2f, 0);            //����ġ
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //��ȸ�� x                        
            if(!isLeftHand)
            {
                isLeftHand = true;
                LeftHandObj = Instantiate(otherHandPrefab,
                    transform.position + new Vector3(-0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0)); //�޼� ����                
            }
            LeftHandObj.transform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);    //�޼� ĳ���Ϳ� �پ�ٴϰ�
            Destroy(RightHandObj);  //�ѿ� ���� �־ ���� �ִ� �� ����
            isRightHand = false;
            if (GetComponent<PlayerController>().inlobby)   //�κ񿡼� ��2��
            {
                if (!isRightHand)
                {
                    isRightHand = true;
                    RightHandObj = Instantiate(otherHandPrefab,
                        transform.position + new Vector3(0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0));  //������ ����
                }
                RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //������ ĳ���Ϳ� �پ�ٴϰ�
            }
        }
        else // ���콺�� ĳ���� ���ʿ� ������
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = true;                                     //����           
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ + 90);                     //���콺�� ���� ��ȸ��
            gunObj.transform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);         //����ġ ĳ���� ��������

            gunGateObj.transform.position = gunObj.transform.position + 
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//�ѱ� ��ġ
            if(plmv.angleZ<-45 && plmv.angleZ>-135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) -0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//�ؿ� ���� �ѱ���ġ ����
            else if (plmv.angleZ < -60 && plmv.angleZ > -120)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.3f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) + 0.5f);//���� ���� �ѱ���ġ ����


            childTransform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);           //����ġ
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //��ȸ�� x
            if (!isRightHand)                                                               
            {
                RightHandObj = Instantiate(otherHandPrefab,
                    transform.position + new Vector3(0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0));  //������ ����
                isRightHand = true;
            }
            RightHandObj.transform.position = transform.position + new Vector3(+0.35f, -0.2f, 0);   //������ ĳ���Ϳ� �پ�ٴϰ�
            Destroy(LeftHandObj);   //�ѿ� ���� �־ ���� �ִ� �� ����
            isLeftHand = false;

            if (GetComponent<PlayerController>().inlobby)   //�κ񿡼� ��2�� ����
            {
                if (!isLeftHand)
                {
                    LeftHandObj = Instantiate(otherHandPrefab,
                        transform.position + new Vector3(-0.35f, -0.2f, 0), Quaternion.Euler(0, 0, 0)); //�޼� ����
                    isLeftHand = true;
                }
                LeftHandObj.transform.position = transform.position + new Vector3(-0.35f, -0.2f, 0);    //�޼� ĳ���Ϳ� �پ�ٴϰ�
            }                                                
        }

        // �켱���� ����
        if (plmv.angleZ > 30 && plmv.angleZ < 150)  // ������
        {
            // ��,Ȱ �켱����
            gunSpr.sortingOrder = 1;
            handSpr.sortingOrder = 1;
            if(RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            gunSpr.sortingOrder = 5;    //ĳ���� OrderInLayer == 4
            handSpr.sortingOrder = 6;   //���� �Ѻ�����
            if (RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        if (GetComponent<PlayerController>().gameState == "falling")    //�������� ������
        {
            Destroy(RightHandObj);                                      //������ ����
            isRightHand = false;
            Destroy(LeftHandObj);                                       //�޼� ����
            isLeftHand = false;
        }
                
        if (GetComponent<PlayerController>().isDodging) // ������
        {
            Destroy(RightHandObj);                                      //������ ����
            isRightHand = false;
            Destroy(LeftHandObj);                                       //�޼� ����
            isLeftHand = false;
            if (gunObj)
            {
                gunObj.GetComponent<SpriteRenderer>().enabled = false;  //���콺 ������ �Է½� �ѾȺ��̰�
            }
        }
        else
        {
            if (gunObj)
            {
                gunObj.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButton(0) && canAttack&&!inlobby &&!isReloading)
        {            
            if(gunNumber ==1)
            {
                if(pilotGunBulletCount > 0) 
                {                    
                    // ���� Ű �Է� �� ������ ����
                    StartCoroutine(AttackWithDelay());
                    audioSource.PlayOneShot(pilotGunSound);
                }
            }
            else if(gunNumber ==2) 
            {
                if(redGunBulletCount > 0)
                {
                    // ���� Ű �Է� �� ������ ����
                    StartCoroutine(AttackWithDelay());
                    audioSource.PlayOneShot(redGunSound);
                }
            }            
        }

        // �Ѿ� �ٽ�����
        if (!isReloadText)
        {
            if (gunNumber ==1) 
            {
                if(pilotGunBulletCount == 0)
                {
                    // ������ ���� ����
                    ReloadText = Instantiate(reloadText, transform.position + new Vector3(-0.5f, 1.2f, 0), transform.rotation);
                    ReloadText.transform.SetParent(transform);  // �÷��̾� ����ٴϰ� �ڽ����� ����
                    isReloadText = true;

                    // �ؽ�Ʈ �����Ÿ��� �ϱ�
                    textMesh = ReloadText.GetComponent<TextMesh>();
                    textColor = textMesh.color;
                    Invoke("TextInVisible", 1f);
                }
            }
            else if (gunNumber == 2)
            {
                if (redGunBulletCount == 0)
                {
                    // ������ ���� ����
                    ReloadText = Instantiate(reloadText, transform.position + new Vector3(-0.5f, 1f, 0), transform.rotation);
                    ReloadText.transform.SetParent(transform);  // �÷��̾� ����ٴϰ� �ڽ����� ����
                    isReloadText = true;

                    // �ؽ�Ʈ �����Ÿ��� �ϱ�
                    textMesh = ReloadText.GetComponent<TextMesh>();
                    textColor = textMesh.color;
                    Invoke("TextInVisible", 1f);
                }
            }
        }


        // �Ѿ� �ٽ��� �� ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0) && canAttack && !inlobby)
        {
            if(gunNumber==1) 
            {
                if(pilotGunBulletCount == 0)
                    Reload();
            }
            else if (gunNumber == 2)
            {
                if (redGunBulletCount == 0)
                    Reload();
            }
        }

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonUp(0) && !inlobby &&!isReloading)
        {

            if (gunNumber == 1)
                gunAnimator.Play(pilotGunReturn);
            else if (gunNumber == 2)
                gunAnimator.Play("RedGunHold");
        }
        
        // RŰ������ ����
        if (Input.GetKeyDown(KeyCode.R) && !inlobby && !isReloading)
        {
            if (gunNumber == 1)
            {
                if (pilotGunBulletCount < 8)
                    Reload();
            }
            else if (gunNumber == 2)
            {
                if (redGunBulletCount < 20)
                    Reload();
            }
        }

        if(ReloadBar)
        {
            // ReloadBar�� ���������� �̵�
            float barspeed = 0.8f;
            ReloadBar.transform.Translate(Vector3.right * barspeed * Time.deltaTime, Space.Self);
        }        

        // Q������ ����ź ���
        if(Input.GetKeyDown(KeyCode.Q) && !inlobby && ItemData.hasBlankBullets>0)
        {
            audioSource.PlayOneShot(BBSound);
            ItemData.hasBlankBullets--;
            Vector2 blankBulletPos = new Vector2(transform.position.x -4.7f,transform.position.y - 4.7f);   //����ź ������ġ
            GameObject blankBulletInstance = Instantiate(blankBullet, blankBulletPos, transform.rotation);  //����ź ��ü����,��ü ����
            GameObject.Find("BlankBullet(Clone)").GetComponent<Animator>().Play("BlankBulletFire");
            Destroy(blankBulletInstance, 2.0f);

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");   //"EnemyBullet"�±��ִ� ������Ʈ�� ã��
            foreach (GameObject enemyBullet in enemyBullets)      //�迭�� �ϳ�����
            {
                Destroy(enemyBullet);
            }
        }
    }

    void FixedUpdate()
    {        
    }

    public void InstantiateGun()
    {
        // ���� �÷��̾� ��ġ�� ��ġ
        if (gunNumber == 1)
        {
            Vector3 pos = transform.position;
            gunObj = Instantiate(pilotGunPrefab, pos, Quaternion.identity);
            gunObj.transform.SetParent(transform);  //�÷��̾� ��ü�� �� ��ü�� �θ�� ����
        }
        if (gunNumber == 2)
        {
            Vector3 pos = transform.position;
            gunObj = Instantiate(redGunPrefab, pos, Quaternion.identity);
            gunObj.transform.SetParent(transform);  //�÷��̾� ��ü�� �� ��ü�� �θ�� ����
        }

        // ������ ��ġ�� ������Ʈ ��������
        Transform tr = gunObj.transform.Find("GunGate");
        gunGateObj = tr.gameObject;

        // �ִϸ����� ��������
        gunAnimator = gunObj.GetComponent<Animator>();

        // (�⺻) �ִϸ��̼� ����
        if (gunNumber == 1)
            gunAnimator.Play(pilotGunHold);
        else if (gunNumber == 2)
            gunAnimator.Play("RedGunHold");

        if (gunNumber == 1)
        {
            shootDelay = 0.5f;
        }
        else if (gunNumber == 2)
        {
            shootDelay = 0.2f;
        }
    }

    IEnumerator AttackWithDelay()
    {
        Attack();

        // ������ ���� 
        canAttack = false;
        yield return new WaitForSeconds(shootDelay);
        // ������ �Ŀ� �ٽ� ���� �������� ����
        canAttack = true;
    }

    // �Ѿ� �߻�
    public void Attack()
    {
        if(gunNumber == 1)
        {
            pilotGunBulletCount--;
            gunAnimator.Play(pilotGunFire, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����
        }
        else if(gunNumber == 2)
        {
            redGunBulletCount--;
            gunAnimator.Play("RedGunFire", 0, 0f);
        }
                    
        PlayerController playerCnt = GetComponent<PlayerController>();
        // ȸ���� ����� ����
        float angleZ = playerCnt.angleZ;        

        // �Ѿ� ���� ��ġ
        Vector3 pos = new Vector3(gunGateObj.transform.position.x,
                                          gunGateObj.transform.position.y,
                                          transform.position.z);
                
        // �Ѿ� ������Ʈ ���� (ĳ���� ���� �������� ȸ��)
        Quaternion r = Quaternion.Euler(0, 0, angleZ + 90 );     //�Ѿ� ��ü ����
        if(gunNumber ==1)
            bulletObj = Instantiate(pilotGunBulletPrefab, pos, r);
        else if(gunNumber==2)
            bulletObj = Instantiate(redGunBulletPrefab, pos, r);

        // �� �� ����Ʈ ����
        if (gunNumber == 1)
        {
            pilotgunFireEffectObj = Instantiate(pilotgunFireEffectPrefab, pos, r);
            Destroy(pilotgunFireEffectObj,0.5f);
        }            
        else if (gunNumber == 2)
        {
            // redgun�� ȿ�� �ȳ���
        }
        
        // -5~5���� �������� ���� ����
        int randomInt = Random.Range(-5, 5);

        // �Ѿ��� �߻��ϱ� ���� ���� ����
        Vector3 gateToMouseVec;
        float directionX;
        float directionY;

        // �Ѿ� ���콺 ������ �������� ���ư��� ����
        if (angleZ < 0 && angleZ > -90)         // 4��и�      
        {
            directionX = Mathf.Cos((angleZ-4 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ-4 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ < -135 && angleZ > -180) // 3��и� ����  
        {
            directionX = Mathf.Cos((angleZ  + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ  + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ < -90 && angleZ > -135) // 3��и� �Ʒ��� 
        {
            directionX = Mathf.Cos((angleZ+5 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ+5 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ > 60 && angleZ < 90)          // 1��и� ����    
        {
            directionX = Mathf.Cos((angleZ +8 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ +8 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ > 0 && angleZ < 60)          // 1��и� �Ʒ���   
        {
            directionX = Mathf.Cos((angleZ  + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ  + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ > 90 && angleZ < 135)        // 2��и� ����
        {
            directionX = Mathf.Cos((angleZ -3 + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ -3 + randomInt) * Mathf.Deg2Rad);
        }
        else if (angleZ >= 135 && angleZ < 180)    // 2��и� �Ʒ���
        {
            directionX = Mathf.Cos((angleZ  + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ  + randomInt) * Mathf.Deg2Rad);
        }
        else                                    // 0,180,90,-90
        {
            directionX = Mathf.Cos((angleZ + randomInt) * Mathf.Deg2Rad);
            directionY = Mathf.Sin((angleZ + randomInt) * Mathf.Deg2Rad);
        }
        gateToMouseVec = new Vector3(directionX, directionY) * shootSpeed;
        // ������ ������ �������� ȭ���� �߻�
        Rigidbody2D body = bulletObj.GetComponent<Rigidbody2D>();
        body.AddForce(gateToMouseVec, ForceMode2D.Impulse);

    }       

    void TextVisible()
    {
        textColor.a =1;
        if(ReloadText)
        {
            ReloadText.GetComponent<TextMesh>().color = textColor;
        }
        Invoke("TextInVisible", 1f);
    }
    void TextInVisible()
    {
        textColor.a = 0;
        if (ReloadText)
        {
            ReloadText.GetComponent<TextMesh>().color = textColor;
        }
        Invoke("TextVisible", 1f);
    }

    void Reload()
    {
        isReloading = true;
        canAttack = false;
        Destroy(ReloadText);
        isReloadText = false;

        audioSource.PlayOneShot(reloadSound);

        if (gunNumber == 1)
            gunAnimator.Play(pilotGunReload, 0, 0f);    // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����
        else if (gunNumber == 2)
            gunAnimator.Play("RedGunReload", 0, 0f);

        ReloadBack = Instantiate(reloadBack, transform.position + new Vector3(-0.1f, 0.8f, 0), transform.rotation);   //�丷�� ����            
        ReloadBar = Instantiate(reloadBar, transform.position + new Vector3(-0.5f, 0.84f, 0), transform.rotation);   //ª������ ����
        ReloadBack.transform.SetParent(transform);  //�÷��̾� ����ٴϰ�
        ReloadBar.transform.SetParent(transform);   //�÷��̾� ��ü�� �� ��ü�� �θ�� ����
        Destroy(ReloadBack, 1.3f);
        Destroy(ReloadBar, 1.3f);        //1.3�ʵ� ����
        Invoke("ChangeVariable", 1.3f);  //1.3�ʵ� isReloading=false ,canAttack =true�� �ٲٴ��Լ� ����
        if(gunNumber==1)
            pilotGunBulletCount = 8;    //�̰� �̺�Ʈ�Լ� �Ἥ ���� �߰��� ����� �����ȵǰ� ���� �ٵ� �ű� ������ɵ�
        else if(gunNumber == 2)
            redGunBulletCount = 20;
    }

    public void ChangeVariable()
    {
        isReloading = false;
        canAttack = true;
    }
}


/*

�ð��Ѹ°� ���� ��濡 GameOver ���� Ÿ��Ʋȭ�� or �����
-ui��ư����� sceneChange,,


�ѽ�� ȭ�� ��鸮��
(��鸲 ���� ���� �ɼ�)

�ణ �����Ѱŵ�
-
������ �ִ� źâ�� ���� ,�����ϸ� �����Ѿ˰�� ���

�Ҹ� ������ ���� - ź�����, ��Ʈ, ����ź 

�������� ,���Ҹ�


�����Ѱŵ�

�������߿� �ѹٲٸ� ���� ���, ���� ������ �Ѿ� ������
������ ��ο����°� ����Ʈ ���μ���,,

 */