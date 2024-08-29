using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Animation circlesAnimation;
    public GameObject buttons;
    public Animation volumeAnimation;
    public TileSpawner tileSpawner;
    public AudioSource audioSource;
    public Text scoreText; // ��� ����������� ������� ����������
    public Image circleFill; // ��� ���������� �������� �����
    public int totalBalls = 257;

    private int ballsHit = 0;
    private int bestScore = 0;
    private bool hasExited = false;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateUI();
        hasExited = false; // ������������ ��������� ��������
    }

    private void Update()
    {
        // �������� ���������� �����
        if (!audioSource.isPlaying && ballsHit > 0 && !hasExited)
        {
            Exit();
            GoHome();
        }
    }

    public void BallHit()
    {
        volumeAnimation.Play("VolumePlus");
        ballsHit++;
    }

    private void UpdateUI()
    {
        float percentage = Mathf.Min((float)bestScore / totalBalls * 100, 100); // ����������� �� 100%
        scoreText.text = $"{(int)percentage}%";
        circleFill.fillAmount = percentage / 100;
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    public void Exit()
    {
        // ����� �� ����
        circlesAnimation.Stop();

        if (ballsHit > bestScore)
        {
            bestScore = ballsHit;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        UpdateUI();

        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
        {
            Destroy(sphere);
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // ������������� hasExited � true ����� ������
        hasExited = true;
        ResetGame();
    }

    private void ResetGame()
    {
        // ����� ��������� ����
        ballsHit = 0;
    }

    public void ButtonAPressed()
    {
        foreach (TileSpawner spawner in FindObjectsOfType<TileSpawner>())
        {
            spawner.ButtonATouched();
        }

        buttons.GetComponent<Animation>().Play("Hider");
        circlesAnimation.Play("CirclesAnim");
        hasExited = false; // ���������� ��� ������� �� ������
    }

    public void GoHome()
    {
        buttons.GetComponent<Animation>().Play("Returner");
        Exit();

        foreach (TileSpawner spawner in FindObjectsOfType<TileSpawner>())
        {
            spawner.ArrayReturner();
        }
    }
}
