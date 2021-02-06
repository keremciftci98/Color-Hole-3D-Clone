using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public LevelPass lp;
    public UIController uic;
    public ObjectCollect oc;
    public GameObject losePanel;

    [System.Serializable]
    public struct GOArray
    {
        public List<string> type;
        public List<Vector3> position;
    }

    public List<GOArray> part1;
    public List<GOArray> part2;

    public GameObject whiteCube;
    public GameObject shortCylinder;
    public GameObject longCylinder;
    public GameObject whiteRectangle;
    public GameObject redCube;
    public GameObject tempObjHolder;

    private void Start()
    {
        CloseLosePanel();
        for (int i = 0; i < lp.part1.Count; i++)
        {
            for (int j = 0; j < lp.part1[i].gameObjects.Count; j++)
            {
                part1[i].type.Add(lp.part1[i].gameObjects[j].name);
                part1[i].position.Add(lp.part1[i].gameObjects[j].transform.position);
            }
        }
        for (int i = 0; i < lp.part2.Count; i++)
        {
            for (int j = 0; j < lp.part2[i].gameObjects.Count; j++)
            {
                part2[i].type.Add(lp.part2[i].gameObjects[j].name);
                part2[i].position.Add(lp.part2[i].gameObjects[j].transform.position);
            }
        }
    }
    public void Restart()
    {
        Debug.Log("restarting");
        oc.gameLost = false;
        oc.whiteCollected = 0;
        CloseLosePanel();
        lp.blackHole.transform.position = lp.blackholePos[lp.currentLevel * 2];
        lp.myCamera.transform.position = lp.cameraPos[lp.currentLevel * 2];
        lp.whiteNum = lp.whiteNum1;
        lp.currentPart = 1;
        uic.slider1.value = 0;
        uic.slider2.value = 0;

        //destroy remaining objects
        for (int i = 0; i < lp.part1[lp.currentLevel].gameObjects.Count; i++)
        {
            Destroy(lp.part1[lp.currentLevel].gameObjects[i]);
        }
        for (int i = 0; i < lp.part2[lp.currentLevel].gameObjects.Count; i++)
        {
            Destroy(lp.part2[lp.currentLevel].gameObjects[i]);
        }
        foreach (Transform child in tempObjHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //instantiate objects        
        InstantiateObj(part1);
        InstantiateObj(part2);

    }

    public void OpenLosePanel()
    {
        losePanel.SetActive(true);
    }
    public void CloseLosePanel()
    {
        losePanel.SetActive(false);
    }
    void InstantiateObj(List<GOArray> givenPart)
    {
        for (int i = 0; i < givenPart[lp.currentLevel].type.Count; i++)
        {
            if (givenPart[lp.currentLevel].type[i] == "WhiteCube")
            {
                Instantiate(whiteCube, givenPart[lp.currentLevel].position[i], Quaternion.identity, tempObjHolder.transform);
            }
            else if (givenPart[lp.currentLevel].type[i] == "WhiteRectangle")
            {
                Instantiate(whiteRectangle, givenPart[lp.currentLevel].position[i], Quaternion.identity, tempObjHolder.transform);
            }
            else if (givenPart[lp.currentLevel].type[i] == "ShortCylinder")
            {
                Instantiate(shortCylinder, givenPart[lp.currentLevel].position[i], Quaternion.identity, tempObjHolder.transform);
            }
            else if (givenPart[lp.currentLevel].type[i] == "LongCylinder")
            {
                Instantiate(longCylinder, givenPart[lp.currentLevel].position[i], Quaternion.identity, tempObjHolder.transform);
            }
            else if (givenPart[lp.currentLevel].type[i] == "RedCube")
            {
                Instantiate(redCube, givenPart[lp.currentLevel].position[i], Quaternion.identity, tempObjHolder.transform);
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
