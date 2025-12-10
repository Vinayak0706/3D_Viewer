using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image toolsButtonImage;
    public Sprite toolsOpenSprite;
    public Sprite toolsCloseSprite;

    public GameObject toolsPanel;

    private bool isToolsPanelOpen = false;


    public void ToggleToolsPanel()
    {
        if (isToolsPanelOpen == false)
        {
            toolsButtonImage.sprite = toolsOpenSprite;
            toolsPanel.SetActive(true);
            isToolsPanelOpen = true;
        }
        else
        {
            toolsButtonImage.sprite = toolsCloseSprite;
            toolsPanel.SetActive(false);    
            isToolsPanelOpen = false;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
