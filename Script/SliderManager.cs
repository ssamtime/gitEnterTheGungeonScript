
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider Bgmslider;
    [SerializeField] private Slider SEslider;
    public Button targetButton; // �����Ϳ��� �Ҵ��� ��ư

    GameObject soundManager;
    GameObject seSoundManager;

    private void Start()
    {
        // ���� �ÿ� �����̴� �� �ε�
        Bgmslider.value = PlayerPrefs.GetFloat("BGMvolumeValue", 1f);
        SEslider.value = PlayerPrefs.GetFloat("SEvolumeValue", 1f);

        soundManager = GameObject.Find("SoundPrefab");
        seSoundManager = GameObject.Find("SeSoundPrefab");

        // ���� �� �̸� Ȯ��
        string sceneName = SceneManager.GetActiveScene().name;

        // �� �̸��� "Title"�� ��� ��ư ��Ȱ��ȭ, �׷��� ������ Ȱ��ȭ
        if (sceneName == "Title")
        {
            targetButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (soundManager != null)
        {
            soundManager.GetComponent<AudioSource>().volume = Bgmslider.value;
            PlayerPrefs.SetFloat("BGMvolumeValue", Bgmslider.value);
        }

        if (seSoundManager != null)
        {
            seSoundManager.GetComponent<AudioSource>().volume = SEslider.value;
            PlayerPrefs.SetFloat("SEvolumeValue", SEslider.value);
        }
    }

    public void SaveVolumeButton()
    {
        // �����̴� �� ����
        PlayerPrefs.SetFloat("BGMvolumeValue", Bgmslider.value);
        PlayerPrefs.SetFloat("SEvolumeValue", SEslider.value);

        // ���� �ɼ��� �ݽ��ϴ�.
        CloseSoundOption();
    }

    public void ResetVolumeButton()
    {
        // �����̴� ���� �⺻������ ����
        Bgmslider.value = 1f;
        SEslider.value = 1f;

        PlayerPrefs.SetFloat("BGMvolumeValue", 1f);
        PlayerPrefs.SetFloat("SEvolumeValue", 1f);
    }

    private void CloseSoundOption()
    {
        // �� ������Ʈ�� ���� �ɼ� UI��� ��Ȱ��ȭ�մϴ�.
        Destroy(this.gameObject);
        Time.timeScale = 1; // ������ �ð� �帧�� �������� �����մϴ�.
    }

    public void DeskTop()
    {
        // �� ������Ʈ�� ���� �ɼ� UI��� ��Ȱ��ȭ�մϴ�.
        Application.Quit();
    }

    public void BackTitle()
    {
        targetButton.gameObject.SetActive(true);
        Time.timeScale = 1; // ������ �ð� �帧�� �������� �����մϴ�.
        SceneManager.LoadScene("Title");
            
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider Bgmslider;
    [SerializeField] private Slider SEslider;


    private float originalBGMVolume; // ó�� ����� BGM ���� ��
    private float originalSEVolume;  // ó�� ����� SE ���� ��

    private void Start()
    {
        Bgmslider.value = 1f;
        SEslider.value = 1f;
        LoadValues();
    }

    public void saveVolumeButton()
    {
        float BGMvolumeValue = Bgmslider.value;
        float SEvolumeValue = SEslider.value;
        PlayerPrefs.SetFloat("BGMvolumeValue", BGMvolumeValue);
        PlayerPrefs.SetFloat("SEvolumeValue", SEvolumeValue);
        LoadValues();

       
        // ���⿡ �ɼ� â�� ��Ȱ��ȭ �ϴ� �ڵ带 �߰�
        gameObject.SetActive(false);
        TitleManager.Destroy(gameObject);
    }

    void LoadValues()
    {
        originalBGMVolume = PlayerPrefs.GetFloat("BGMvolumeValue", 1f); // �⺻ ���� 1
        originalSEVolume = PlayerPrefs.GetFloat("SEvolumeValue", 1f); // �⺻ ���� 1

        Bgmslider.value = originalBGMVolume;
        SEslider.value = originalSEVolume;
        AudioListener.volume = originalBGMVolume; // BGM ������ ����
    }

    public void CancelButton()
    {
        Bgmslider.value = originalBGMVolume;
        SEslider.value = originalSEVolume;
        AudioListener.volume = originalBGMVolume; // BGM ������ ����

        // ���⿡ �ɼ� â�� ��Ȱ��ȭ �ϴ� �ڵ带 �߰�
        gameObject.SetActive(false);
        TitleManager.Destroy(gameObject);
    }

    public void resetVolumeButton()
    {
        Bgmslider.value = 1f;
        SEslider.value = 1f;
        AudioListener.volume = 1f;
    }
}
 */