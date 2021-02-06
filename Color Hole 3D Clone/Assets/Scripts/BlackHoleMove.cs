using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleMove : MonoBehaviour
{
    public LevelPass lp;
    public Vector2 firstPressPos;
    public Vector2 secondPressPos;
    public Vector2 direction;
    public Vector3 objectFirstPos;
    public Vector3 targetPos;

    public float speed;
    public float distance;
    public float distanceMultiplier;

    public bool clicked;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            objectFirstPos = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            direction = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            distance = direction.magnitude;
            distance /= distanceMultiplier;

            direction.Normalize();

            direction *= distance;

            targetPos = new Vector3(objectFirstPos.x + direction.x, objectFirstPos.y, objectFirstPos.z + direction.y);

            if (distance > 0.05 && clicked)
            {
                rb.MovePosition(Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime));
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }
    }
}
