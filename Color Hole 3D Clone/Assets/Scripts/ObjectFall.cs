using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    //public GameObject plane;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "white" || other.gameObject.tag == "red")
        {            
            other.gameObject.layer = LayerMask.NameToLayer("nocollusion");
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 30);
            //Debug.Log("entered name: " + other.name);
            //Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), plane.GetComponent<Collider>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "white" || other.gameObject.tag == "red")
        {            
            other.gameObject.layer = LayerMask.NameToLayer("yescollusion");
            //Debug.Log("exited name: " + other.name);
            //Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), plane.GetComponent<Collider>(), false);
        }
    }
}
