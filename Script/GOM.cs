using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOM: MonoBehaviour
{
   // �ɼ��� �Ҵ�
    public GameObject OptionPrefab;
    public GameObject InoptionPrefab;

    public GameObject Playingbtn;  // ���ӽ���
    public GameObject Optionbtn;  // �ɼǹ�ư
    public GameObject Indexbtn;  // �������� ��ư
    public GameObject EXITbtn;  // �������� ��ư

    // Option Prefab�� �ν��Ͻ��� ������ ����
    private GameObject OptionInstance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ESC Ű�� ������ �ɼ�â�� �ߵ��� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �ɼ� Prefab�� ���� Ȱ��ȭ�Ǿ� �ִ� �� ���� �Ǵ�
            if (OptionInstance == null)
            {
                OpenInOption();
            }

            else
            {
                // ������ �Ⱥ��̰�
                HideOptionCanvas();
            }
        }
    }
    
    public void OpenInOption() 
    {

        // Option Prefab�� ȣ��
        OptionInstance = Instantiate(InoptionPrefab);

        // Option Prefab���� Canvas ������Ʈ ��������
        Canvas canvas = OptionInstance.GetComponent<Canvas>();

        if (canvas != null)
        {
            // Canvas�� Render Mode�� Screen Space - Camera�� ����
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            // ���� ���� Main Camera�� �����ͼ� Render Camera�� �Ҵ�
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                canvas.worldCamera = mainCamera;
            }
            else
            {
                //Debug.LogError("����ī�޶� ����");
            }
        }
        else
        {
            //Debug.LogError("�����տ� Canvas ������Ʈ�� ����");
        }

    }
    // ��ŸƮ ��ư Ŭ�� �̺�Ʈ
    public void PlayingButtonClicked()
    {
        Destroy(OptionInstance);
        OptionInstance = null;
    }

    // �ɼ� ��ư Ŭ�� �̺�Ʈ
    public void OptionButtonClicked()
    {
        InoptionPrefab.SetActive(false);

        // Option Prefab�� ȣ��
        OptionInstance = Instantiate(OptionPrefab);

        // Option Prefab���� Canvas ������Ʈ ��������
        Canvas canvas = OptionInstance.GetComponent<Canvas>();

        if (canvas != null)
        {
            // Canvas�� Render Mode�� Screen Space - Camera�� ����
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            // ���� ���� Main Camera�� �����ͼ� Render Camera�� �Ҵ�
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                canvas.worldCamera = mainCamera;
            }
            else
            {
                //Debug.LogError("����ī�޶� ����");
            }
        }
        else
        {
            //Debug.LogError("�����տ� Canvas ������Ʈ�� ����");
        }

        // ESC Ű�� ������ �ɼ�â�� �ߵ��� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InoptionPrefab.SetActive(true);
            HideOptionCanvas();

        }



    }

    // ���� ���� ��ư Ŭ�� �̺�Ʈ
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void HideOptionCanvas()
    {
        // �ɼ� Prefab�� �����մϴ�.
        Destroy(OptionInstance);
        OptionInstance = null;
    }
}
