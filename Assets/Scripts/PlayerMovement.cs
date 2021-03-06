using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed;
    private int KeysFound;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 5;
        KeysFound = 0;
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

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            TileGenerator tileGenerator = GameObject.Find("WorldScripts").GetComponent<TileGenerator>();

            int xPosition = Convert.ToInt32(gameObject.transform.position.x);
            int yPosition = Convert.ToInt32(gameObject.transform.position.y);

            List<Vector2> positionsToCheck = new List<Vector2>();

            positionsToCheck.Add(new Vector2(xPosition, yPosition));
            positionsToCheck.Add(new Vector2(xPosition -1, yPosition));
            positionsToCheck.Add(new Vector2(xPosition -1, yPosition -1));
            positionsToCheck.Add(new Vector2(xPosition, yPosition -1));
            positionsToCheck.Add(new Vector2(xPosition +1, yPosition));
            positionsToCheck.Add(new Vector2(xPosition +1, yPosition+1));
            positionsToCheck.Add(new Vector2(xPosition, yPosition+1));
            positionsToCheck.Add(new Vector2(xPosition-1, yPosition+1));
            positionsToCheck.Add(new Vector2(xPosition+1, yPosition-1));

            foreach(Vector2 location in positionsToCheck)
            {
                tileGenerator.RemoveVoid(location);
                tileGenerator.GenerateGrass(location);
            }
            
            
        }

    }

    private void MoveInDiction(Vector2 direction, float deltaTime, float speed = 1)
    {
        transform.Translate(direction * deltaTime * speed, Space.World);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Portal" && KeysFound == 3)
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("level complete");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (col.gameObject.name == "Key")
        {
            KeysFound++;
            Destroy(col.gameObject);
        }
    }
}
