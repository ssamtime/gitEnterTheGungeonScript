using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject SoundOptionPrefab; // SoundOption2 �����տ� ���� ����
    public static GameObject soundOptionInstance; // ���� Ȱ��ȭ�� SoundOption2 �ν��Ͻ�

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (soundOptionInstance == null)
            {
                OpenSoundOption();
            }
            else
            {
                CloseSoundOption();
            }
        }
    }

    private void OpenSoundOption()
    {
        soundOptionInstance = Instantiate(SoundOptionPrefab);
        SetupCanvas(soundOptionInstance);
        PauseGame(); // ������ �Ͻ� ���� ���·� ����ϴ�.
    }

    public void CloseSoundOption()
    {
        if (soundOptionInstance != null)
        {
            Destroy(soundOptionInstance);
            soundOptionInstance = null;
        }
        ResumeGame(); // ������ �簳�մϴ�.
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // ������ �Ͻ� �����մϴ�.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // ������ �ð� �帧�� �������� �����մϴ�.
    }

    private void SetupCanvas(GameObject instance)
    {
        Canvas canvas = instance.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvas.sortingLayerName = "UI";
        }
        else
        {
            Debug.LogError("Canvas component is missing on the prefab!");
        }
    }
}
/*
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class OptionManager : MonoBehaviour
{
    public GameObject IngameOption;
    public GameObject SoundOption;

    private GameObject ingameOptionInstance;
    private GameObject soundOptionInstance;
    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (soundOptionInstance != null)
            {
                // SoundOption�� Ȱ��ȭ�� ���¿��� ESC�� ������, SoundOption�� ��Ȱ��ȭ�ϰ� IngameOption�� Ȱ��ȭ
                ToggleSoundOption(false);
                ToggleIngameOption(true);
            }
            else if (ingameOptionInstance == null)
            {
                // IngameOption�� Ȱ��ȭ���� �ʾ��� �� ESC�� ������, IngameOption Ȱ��ȭ
                ToggleIngameOption(true);
            }
            else
            {
                // IngameOption�� Ȱ��ȭ�� ���¿��� ESC�� ������, IngameOption�� ��Ȱ��ȭ
                ToggleIngameOption(false);
            }
        }
    }

    public void OpenSoundOption()
    {
        // SoundOption ����
        ToggleSoundOption(true);
        ToggleIngameOption(false);
    }

    public void CloseSoundOption()
    {
        // SoundOption �ݱ�
        ToggleSoundOption(false);
        ToggleIngameOption(true);
    }

    private void ToggleIngameOption(bool isActive)
    {
        if (isActive)
        {
            ingameOptionInstance = Instantiate(IngameOption);
            PauseGame();
            SetupCanvas(ingameOptionInstance);
        }
        else if (ingameOptionInstance != null)
        {
            Destroy(ingameOptionInstance);
            ingameOptionInstance = null;
            ResumeGame();
        }
    }

    private void ToggleSoundOption(bool isActive)
    {
        if (isActive)
        {
            soundOptionInstance = Instantiate(SoundOption);
            SetupCanvas(soundOptionInstance);
        }
        else if (soundOptionInstance != null)
        {
            Destroy(soundOptionInstance);
            soundOptionInstance = null;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

    private void SetupCanvas(GameObject instance)
    {
        if (instance != null)
        {
            Canvas canvas = instance.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
                canvas.sortingLayerName = "UI";
            }
            else
            {
                Debug.LogError("�����տ� Canvas ������Ʈ�� ����");
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
 */