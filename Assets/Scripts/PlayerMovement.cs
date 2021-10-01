using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            MoveInDiction(Vector2.left, Time.deltaTime, playerSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveInDiction(Vector2.right, Time.deltaTime, playerSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveInDiction(Vector2.down, Time.deltaTime, playerSpeed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            MoveInDiction(Vector2.up, Time.deltaTime, playerSpeed);
        }
    }

    private void MoveInDiction(Vector2 direction, float deltaTime, float speed = 1)
    {
        transform.Translate(direction * deltaTime * speed, Space.World);

    }
}
