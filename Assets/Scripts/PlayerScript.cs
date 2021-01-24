using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //var speed: int = 5;
    public int speed = 5;
    
    //var collided_with: GameObject;
    GameObject collidedWith;

    //var drumBeat_pref: Transform;
    public GameObject drumbeatPrefab;

    //var musicSheet_texture: Texture;
    Texture musicSheetTexture;

    //var fire1_texture: Texture;
    public Texture fire1Texture;
    //var fire1_guiShow: boolean;
    bool fire1GUIShow;
    //var firstDot: boolean;
    bool firstDot;
    //var fire1_guiDistance: int = 0;
    int fire1GUIDistance = 0;
    //var fire1_guiCount: int = 0;
    int fire1GUICount = 0;

    //var textureFlip: double;
    float textureFlip;
    //var fire1Direction: boolean; 
    bool fire1Direction; //true is left, false is right

	Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        textureFlip = transform.localScale.x;
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Math.Abs(Input.GetAxis("Horizontal")) > 0)
		{
			print(true);
			//Vector 3 (x, y, z)
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") * -speed * Time.deltaTime, 0, 0));
			//Texture flip
			if (Input.GetKey("left"))
			{
				//transform.localScale.x = textureFlip;

				transform.localScale = new Vector3(textureFlip, transform.localScale.y, transform.localScale.z);
				fire1Direction = true;
			}
			//Texture flip
			if (Input.GetKey("right"))
			{
				//transform.localScale.x = -textureFlip;
				transform.localScale = new Vector3(-textureFlip, transform.localScale.y, transform.localScale.z);
				fire1Direction = false;
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
			if (fire1Direction == true)
			{
				//Instantiate(prefab, location, rotation)
				Instantiate(drumbeatPrefab, new Vector3(transform.position.x + 0.6f, transform.position.y + 0.1f, 6), Quaternion.identity);
				fire1GUIShow = true;
			}
			else if (fire1Direction == false)
			{
				Instantiate(drumbeatPrefab, new Vector3(transform.position.x - 0.6f, transform.position.y + 0.1f, 6), Quaternion.identity);
				fire1GUIShow = true;
			}
		}
	}
}
