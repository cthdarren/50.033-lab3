using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    public GameObject MenuToDisable; 
    public GameObject MenuToEnable; 

    public void Switch()
    {
        MenuToDisable.SetActive(false);
        MenuToEnable.SetActive(true);
    }
}
