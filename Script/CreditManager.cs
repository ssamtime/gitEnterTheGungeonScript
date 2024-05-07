using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public Animator animator; // �ִϸ����� ������Ʈ�� ���� ����
    private float originalSpeed; // ������ �ִϸ��̼� �ӵ��� ����
    public float speedMultiplier = 2.0f; // �ӵ��� �����ϴ� ����
    private bool hasAnimationEnded = false; // �ִϸ��̼� ���� ���θ� üũ

    void Start()
    {
        // �ʱ� �ִϸ��̼� �ӵ� ����
        originalSpeed = animator.speed;
    }

    void Update()
    {
        // SPACE Ű�� ���� �ִ��� Ȯ��
        if (Input.GetKey(KeyCode.Space))
        {
            // �ִϸ��̼� �ӵ��� 2��� ����
            animator.speed = originalSpeed * speedMultiplier;
        }
        else
        {
            // SPACE Ű�� �������� ���� �ӵ��� ����
            animator.speed = originalSpeed;
        }

        // �ִϸ��̼��� �������� üũ
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) && !hasAnimationEnded)
        {
            hasAnimationEnded = true;
            LoadTitleScene();
        }
    }

    void LoadTitleScene()
    {
        SceneManager.LoadScene("Title"); // ���⿡ Ÿ��Ʋ ���� �̸��� ��������
    }
}