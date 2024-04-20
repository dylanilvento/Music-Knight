using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //var speed: int = 5;
    public int speed = 5;

    bool facingLeft = true;

    GameObject collidedWith;

    public GameObject drumbeatPrefab;

    Texture musicSheetTexture;

    public Texture fire1Texture;
    bool fire1GUIShow;
    bool firstDot;
    int fire1GUIDistance = 0;
    int fire1GUICount = 0;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        if (Math.Abs(move) > 0)
        {
            //Vector 3 (x, y, z)
            transform.Translate(new Vector3(move * -speed * Time.deltaTime, 0, 0));

            //Texture flip
            if (move < 0 && !facingLeft)
            {
                FlipDirection();
                facingLeft = true;
            }

            if (move > 0 && facingLeft)
            {
                FlipDirection();
                facingLeft = false;
            }
        }
        //jump
        //not working correctly, may be due to fire1, only when on ground
        if (Input.GetButtonDown("Jump"))
        {
            //	    speed = 1;
            if (collidedWith != null)
            {
                if (collidedWith.tag == "Ground")
                {
                    rb.velocity = new Vector3(0, 3, 0);
                    return;
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject currDrumBeat;

            //facing left
            if (transform.localScale.x > 0f)
            {
                //Instantiate(prefab, location, rotation)
                currDrumBeat = Instantiate(
                    drumbeatPrefab,
                    new Vector3(transform.position.x + 0.6f, transform.position.y + 0.1f, 6),
                    Quaternion.identity
                );
                fire1GUIShow = true;
            }
            //facing right
            else if (transform.localScale.x < 0f)
            {
                currDrumBeat = Instantiate(
                    drumbeatPrefab,
                    new Vector3(transform.position.x - 0.6f, transform.position.y + 0.1f, 6),
                    Quaternion.identity
                );
                currDrumBeat.GetComponent<DrumBeatScript>().speed *= -1;
                fire1GUIShow = true;
            }
        }
    }

    void FlipDirection()
    {
        transform.localScale = new Vector3(
            transform.localScale.x * -1f,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}
