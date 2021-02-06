using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public LevelPass lp;
    public GameObject startPanel;
    public GameObject winPanel;
    public Text levelText;
    public int level;
    public Slider slider1;
    public Slider slider2;
    // Start is called before the first frame update
    void Start()
    {
        startPanel.SetActive(true);
    }

    public void OpenStartPanel()
    {
        startPanel.SetActive(true);
    }
    public void CloseStartPanel()
    {
        startPanel.SetActive(false);
    }
    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }
    private void Update()
    {
        levelText.text = "Level " + level;

        slider1.maxValue = lp.whiteNum1;
        slider2.maxValue = lp.whiteNum2;

        if(lp.currentPart == 1)
        {
            slider1.value = lp.whiteCollected;
        }
        else if(lp.currentPart == 2)
        {
            slider2.value = lp.whiteCollected;
        }


        if (Input.GetMouseButtonDown(0))
        {
            CloseStartPanel();
        }
    }

}
