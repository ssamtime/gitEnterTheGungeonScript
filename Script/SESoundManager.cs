using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SESoundManager : MonoBehaviour
{
    public static SESoundManager Instance { get; private set; }

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ������Ʈ�� �� ��ȯ�ÿ��� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEvolumeValue");
    }
}
