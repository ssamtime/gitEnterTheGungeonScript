using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Linq�� ����ϱ� ���� �߰�

public class AreaManager : MonoBehaviour
{
    public List<DoorManager> doorManagers; // �� �Ŵ��� ����Ʈ
    public List<GardManager> gardManagers; // ���� �Ŵ��� ����Ʈ
    public GameObject roomEnterManager; // �����Ϳ��� ����

    private bool isEnemyPresent;

    void Update()
    {
        // ���� Enemy �±׸� ���� �ڽ� ��ü�� �ִ��� Ȯ��
        bool enemyCurrentlyPresent = roomEnterManager.transform.Cast<Transform>().Any(child => child.tag == "Enemy");

        // Enemy �±׸� ���� ��ü�� ������ٸ� ���� ���� ���带 ��Ȱ��ȭ
        if (isEnemyPresent && !enemyCurrentlyPresent)
        {
            Debug.Log("���� üũ");
            foreach (var doorManager in doorManagers)
            {
                doorManager.OpenDoor();
            }
            foreach (var gardManager in gardManagers)
            {
                gardManager.OpenGard();
            }
        }

        // Enemy �±׸� ���� ��ü�� ���� ���¸� ����
        isEnemyPresent = enemyCurrentlyPresent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // RoomEnterManager ������Ʈ �ȿ��� Enemy �±׸� ���� �ڽ� ��ü�� �ϳ��� �ִ��� Ȯ��
            isEnemyPresent = roomEnterManager.transform.Cast<Transform>().Any(child => child.tag == "Enemy");

            // Enemy �±׸� ���� �ڽ� ��ü�� �ϳ��� ������ ���� �ݰ� ���带 �۵���Ŵ
            if (isEnemyPresent)
            {
                Debug.Log("���� üũ");
                foreach (var doorManager in doorManagers)
                {
                    doorManager.CloseDoor();
                }
                foreach (var gardManager in gardManagers)
                {
                    gardManager.CloseGard();
                }
            }
        }
    }
}

/*
public class AreaManager : MonoBehaviour
{
    public List<DoorManager> doorManagers; // �� �Ŵ������� ����Ʈ�� �����մϴ�.
    public List<GardManager> gardManagers; // �� �Ŵ������� ����Ʈ�� �����մϴ�.

    void Start()
    {
    }

    private void CloseAllGuards()
    {
        // �� ���带 Ȱ��ȭ�ϰ� 'Close' �ִϸ��̼��� ����մϴ�.
        r.gameObject.SetActive(true);
        UDGardAnimator.Play("UDGard_Close");

        DDGardAnimator.gameObject.SetActive(true);
        DDGardAnimator.Play("DDGard_Close");

        LTGardAnimator.gameObject.SetActive(true);
        LTGardAnimator.Play("LTGard_Close");

        RTGardAnimator.gameObject.SetActive(true);
        RTGardAnimator.Play("RTGard_Close");
    }

    private void OpenAllGuards()
    {
        // 'Up' �ִϸ��̼� ��� �� ���带 ��Ȱ��ȭ�մϴ�.
        StartCoroutine(DeactivateGuardAfterAnimation(UDGardAnimator, "UDGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(DDGardAnimator, "DDGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(LTGardAnimator, "LTGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(RTGardAnimator, "RTGard_Up"));
    }

    private IEnumerator DeactivateGuardAfterAnimation(Animator animator, string animationName)
    {
        // �ִϸ��̼� ���
        animator.Play(animationName);

        // �ִϸ��̼��� ���� ������ ��ٸ��ϴ�.
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // �ִϸ��̼��� ������ �ش� ���带 ��Ȱ��ȭ�մϴ�.
        animator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // BattleArea ���� ��� �ݶ��̴����� �˻��մϴ�.
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);

            foreach (var collider in colliders)
            {
                // "Enemy" �±��� ������Ʈ�� �ϳ��� ������ ���� �ݽ��ϴ�.
                if (collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("���� üũ");
                    foreach (var doorManager in doorManagers)
                    {
                        doorManager.CloseDoor();
                    }
                    CloseAllGuards(); // ��� ���带 Ȱ��ȭ�մϴ�.
                    break; // Enemy�� ã������ �� �̻� �ݺ��� �ʿ䰡 �����ϴ�.
                }
            }
        }
    }

    void Update()
    {
        // BattleArea ���� Enemy �±׸� ���� ������Ʈ�� ���ٸ� ���� ���ϴ�.
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Debug.Log("���� üũ");
            foreach (var doorManager in doorManagers)
            {
                doorManager.OpenDoor();
            }
            OpenAllGuards(); // ��� ���带 ��Ȱ��ȭ�մϴ�.
        }
    }
}
 */
/*
using UnityEngine;
// ���ӿ��� ���� ������ �����մϴ�.
public class AreaManager : MonoBehaviour
{
    DoorManager doorManager;
    GameObject[] enemies;
    GameObject[] doors;

    void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // ���� ���� ���� Ȯ���ϰ� ���� ������ ���� �ݽ��ϴ�.
    void Update()
    {
        CheckForEnemies();
    }

    void CheckForEnemies()
    {
        bool enemiesPresent = false;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemiesPresent = true;
                break;
            }
        }

        foreach (var door in doors)
        {
            DoorManager dm = door.GetComponent<DoorManager>();
            if (enemiesPresent)
            {
                dm.CloseDoor();
            }
            else
            {
                dm.OpenDoor();
            }
        }
    }
}
 */

/*
 



 */