using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardManager : MonoBehaviour
{
    private Animator animator;

    public string openGardName; // ���� ���� �ִϸ��̼� �̸�
    public string closeGardName; // ���� ���� �ִϸ��̼� �̸�

    AudioSource audioSource;

    public AudioClip GuardOpenSound;
    public AudioClip GuardCloseSound;

    void Start()
    {
        // ����� �ҽ� ��������
        if (GameObject.Find("SeSoundPrefab") != null)
            audioSource = GameObject.Find("SeSoundPrefab").GetComponent<AudioSource>();
        else
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        Debug.Log("���� ���� ������");
    }

    public void CloseGard()
    {
            gameObject.SetActive(true);
        Debug.Log("���� ���� ������");
            animator.Play(closeGardName);
        audioSource.PlayOneShot(GuardCloseSound);
    }

    public void OpenGard()
    {
            animator.Play(openGardName);
            gameObject.SetActive(false);
        audioSource.PlayOneShot(GuardOpenSound);
    }
}
