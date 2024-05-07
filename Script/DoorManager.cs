
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInside = false;

    public string openAnimationName; // ���� ���� �ִϸ��̼� �̸�
    public string closeAnimationName; // ���� ���� �ִϸ��̼� �̸�

    AudioSource audioSource;

    public AudioClip DOSound;

    void Start()
    {
        animator = GetComponent<Animator>();

        // ����� �ҽ� ��������
        if (GameObject.Find("SeSoundPrefab") != null)
            audioSource = GameObject.Find("SeSoundPrefab").GetComponent<AudioSource>();
        else
            audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPlayerInside)
        {
            isPlayerInside = true;
            animator.Play(openAnimationName);
            audioSource.PlayOneShot(DOSound);
        }
    }

    public void CloseDoor()
    {
        if (isPlayerInside)
        {
            animator.Play(closeAnimationName);
        }
    }

    public void OpenDoor()
    {
        if (isPlayerInside)
        {
            animator.Play(openAnimationName);
            audioSource.PlayOneShot(DOSound);
        }
    }
}

/*
// ���ӿ��� ���� ���� ���带 �����մϴ�.
public class DoorManager : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator guardAnimator; // ���� ������ �ִϸ�����
    bool isDoorOpen = false;
    bool isGuardActive = false; // ���� ������ Ȱ�� ����
    public string openDoorName; // ���� ���� �ִϸ��̼� �̸�
    public string openGardName; // ���� ���� �ִϸ��̼� �̸�
    public string closeDoorName; // ���� ���� �ִϸ��̼� �̸�
    public string closeGardName; // ���� ���� �ִϸ��̼� �̸�

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        // ���� �ִϸ����͸� ������. ���� ���� ������Ʈ�� �ִϸ����� ������Ʈ�� �����ؾ� ��.
        //guardAnimator = transform.Find("Gard").GetComponent<Animator>();

        // ���� ������ �� ���� �ִ� ���¸� ����
        doorAnimator.Play(closeDoorName);

    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            OpenDoor();
            if (isGuardActive) // ���� ���尡 Ȱ��ȭ�Ǿ� �ִٸ�, �������� �ִϸ��̼��� �ߴ�
            {
                LowerGuard(false);
            }
        }
    }

    public void OpenDoor()
    {
        doorAnimator.Play(openDoorName);
        isDoorOpen = true;
    }

    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            doorAnimator.Play(closeDoorName);
            isDoorOpen = false;
            if (!isGuardActive) // ���� ������ ���� ���带 Ȱ��ȭ
            {
                LowerGuard(true);
            }
        }
    }

    // ���� ���带 �����ų� �ø��� �޼���
    void LowerGuard(bool lower)
    {
        if (lower)
        {
            guardAnimator.Play(closeGardName);
            isGuardActive = true;
        }
        else
        {
            guardAnimator.Play(openGardName);
            isGuardActive = false;
        }
    }
}

 */
/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInside = false;

    public string openAnimationName; // ���� ���� �ִϸ��̼� �̸�
    public string closeAnimationName; // ���� ���� �ִϸ��̼� �̸�

    void Start()
    {
        //Debug.Log("�ִϸ����� �غ����");
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPlayerInside)
        {
            //Debug.Log("�� ���� �Ǵ�");
            isPlayerInside = true;
            animator.Play(openAnimationName);
        }
    }

    public void CloseDoor()
    {
        if (isPlayerInside)
        {
            //Debug.Log("�� �ݴ� �޼���");
            animator.Play(closeAnimationName);
        }
    }

    public void OpenDoor()
    {
        if (isPlayerInside)
        {
            //Debug.Log("�� ���� �޼���");
            animator.Play(openAnimationName);
        }
    }
}
 */