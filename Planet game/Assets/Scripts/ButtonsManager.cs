using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public GameObject buttonA;
    public GameObject buttonB;
    public GameObject homeButton;

    public void ColliderTurn()
    {
        buttonA.GetComponent<SphereCollider>().enabled = false;
        buttonB.GetComponent<SphereCollider>().enabled = false;
        homeButton.GetComponent<SphereCollider>().enabled = true;
    } 
    public void ColliderReturn()
    {
        buttonA.GetComponent<SphereCollider>().enabled = true;
        buttonB.GetComponent<SphereCollider>().enabled = true;
        homeButton.GetComponent<SphereCollider>().enabled = false;
    }    
    public void HideButtons()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
    }
    public void HideHomeButton()
    {
        homeButton.SetActive(false);
    }
    public void ReturnButtons()
    {
        buttonB.SetActive(true);
        buttonA.SetActive(true);
    }
    public void ReturnHomeButton()
    {
        homeButton.SetActive(true);
    }
}
