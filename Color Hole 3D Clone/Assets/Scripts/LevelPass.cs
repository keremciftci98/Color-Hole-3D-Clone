using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPass : MonoBehaviour
{
    public ObjectCollect oc;
    public BlackHoleMove bhm;
    public RestartLevel rl;
    public GameObject confetti;
    public UIController uic;
    public GameObject myCamera;
    public GameObject blackHole;
    public List<Vector3> cameraPos;
    public List<Vector3> blackholePos;

    [System.Serializable]
    public struct GOArray
    {
        public List<GameObject> gameObjects;
    }

    public List<GOArray> part1;
    public List<GOArray> part2;

    public int currentLevel;
    public int currentPart;

    public int whiteNum;
    public int whiteNum1;
    public int whiteNum2;
    public int whiteCollected;
    public bool partWon;
    public bool levelWon;
    public bool gameLost;
    public int currIndex;
    public float timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        confetti.SetActive(false);
        whiteCollected = 0;
        currentLevel = 0;
        currentPart = 1;
        whiteNum1 = part1[currentLevel].gameObjects.Count;
        whiteNum2 = part2[currentLevel].gameObjects.Count;

        int redNum1 = RedNumCount(part1[currentLevel].gameObjects);
        int redNum2 = RedNumCount(part2[currentLevel].gameObjects);

        Debug.Log("red num1: " + redNum1);
        whiteNum1 -= redNum1;
        Debug.Log("red num2: " + redNum2);
        whiteNum2 -= redNum2;
        whiteNum = whiteNum1;

        partWon = false;
        levelWon = false;
        gameLost = false;
        currIndex = 0;
        uic.level = currentLevel + 1;
    }

    // Update is called once per frame
    void Update()
    {
        gameLost = oc.gameLost;
        if (gameLost)
        {
            bhm.clicked = false;
            rl.OpenLosePanel();
        }
        else
        {
            LevelChecker();

            if (partWon)
            {
                MovePositionsPart();
            }
            if (levelWon)
            {
                uic.slider1.value = 0;
                uic.slider2.value = 0;
                confetti.SetActive(true);
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    MovePositionsLevel();
                }
            }
        }
    }

    void LevelChecker()
    {
        whiteCollected = oc.whiteCollected;
        if (whiteCollected == whiteNum)
        {
            if (currentLevel == 2 && currentPart == 2)
            {
                Debug.Log("game won");
                bhm.clicked = false;
                confetti.SetActive(true);
                uic.OpenWinPanel();
            }
            else
            {
                if (currentPart == 1)
                {
                    Debug.Log("part1 clear");
                    oc.whiteCollected = 0;
                    currentPart = 2;
                    whiteNum = whiteNum2;
                    partWon = true;
                }
                else if (currentPart == 2)
                {
                    Debug.Log("part2 clear");
                    bhm.clicked = false;
                    oc.whiteCollected = 0;
                    currentLevel++;
                    currentPart = 1;

                    whiteNum1 = part1[currentLevel].gameObjects.Count;
                    whiteNum2 = part2[currentLevel].gameObjects.Count;

                    int redNum1 = RedNumCount(part1[currentLevel].gameObjects);
                    int redNum2 = RedNumCount(part2[currentLevel].gameObjects);

                    Debug.Log("red num1: " + redNum1);
                    whiteNum1 -= redNum1;
                    Debug.Log("red num2: " + redNum2);
                    whiteNum2 -= redNum2;
                    whiteNum = whiteNum1;

                    levelWon = true;
                }
                currIndex = (currentLevel * 2) + (currentPart - 1);
            }
        }
    }
    void MovePositionsPart()
    {
        myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, cameraPos[currIndex], Time.deltaTime * 10);
        blackHole.GetComponent<Rigidbody>().isKinematic = true;
        blackHole.transform.position = Vector3.MoveTowards(blackHole.transform.position, blackholePos[currIndex], Time.deltaTime * 10);

        if (myCamera.transform.position == cameraPos[currIndex] && blackHole.transform.position == blackholePos[currIndex])
        {
            blackHole.GetComponent<Rigidbody>().isKinematic = false;
            partWon = false;
        }
        bhm.clicked = false;
    }
    void MovePositionsLevel()
    {
        myCamera.transform.position = cameraPos[currIndex];
        blackHole.transform.position = blackholePos[currIndex];
        levelWon = false;
        bhm.clicked = false;
        timer = 2;
        confetti.SetActive(false);
        uic.OpenStartPanel();
        uic.level = currentLevel + 1;
    }
    int RedNumCount(List<GameObject> givenPart)
    {
        int redNum = 0;
        for (int i = 0; i < givenPart.Count; i++)
        {
            if (givenPart[i].name == "RedCube")
            {
                redNum++;
            }
        }
        return redNum;
    }
}
