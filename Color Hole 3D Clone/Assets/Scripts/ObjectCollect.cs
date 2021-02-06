using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollect : MonoBehaviour
{
    public int whiteCollected;
    public bool gameLost;
    private void Start()
    {
        whiteCollected = 0;
        gameLost = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "white")
        {
            whiteCollected++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "red")
        {
            gameLost = true;
            Debug.Log("collected red");
            Destroy(other.gameObject);
        }
    }
}
