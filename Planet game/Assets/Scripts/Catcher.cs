using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameController gameController;
    public GameObject particleSystemPrefab;

    private const string SphereTag = "Sphere";
    private const string ButtonATag = "ButtonA";
    private const string HomeButtonTag = "HomeButton";

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        switch (other.tag)
        {
            case SphereTag:
                HandleSphereCollision(other);
                break;
            case ButtonATag:
                gameController.ButtonAPressed();
                break;
            case HomeButtonTag:
                gameController.GoHome();
                break;
        }
    }

    private void HandleSphereCollision(Collider sphere)
    {
        // �������� ������� ���� ����� ������������
        Vector3 position = sphere.transform.position;

        // ���������� ���
        Destroy(sphere.gameObject);

        // ������� Particle System � ������� ����
        Instantiate(particleSystemPrefab, position, Quaternion.identity);

        // ���������� GameController
        gameController.BallHit();
    }
}
