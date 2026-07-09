using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject menuPanel;

    public void ToggleMenu()
    {

        if (menuPanel != null)
        {
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive);
        }
    }
}
