#pragma strict

var speed: int = 5;
var collided_with: GameObject;

var drumBeat_pref: Transform;

var musicSheet_texture: Texture;

var fire1_texture: Texture;
var fire1_guiShow: boolean;
var firstDot: boolean;
var fire1_guiDistance: int = 0;
var fire1_guiCount: int = 0;

var textureFlip: double;
var fire1Direction: boolean; //true is left, false is right

function Start () {
  textureFlip = transform.localScale.x;
}

function Update () {
	//horizontal movement
	if(Input.GetAxis("Horizontal")) {
		//Vector 3 (x, y, z)
		transform.Translate(Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0));
		//Texture flip
		if (Input.GetKey("left")) {
		  transform.localScale.x = textureFlip;
		  fire1Direction = true;
		}
		//Texture flip
		if (Input.GetKey("right")) {
		  transform.localScale.x = -textureFlip;
		  fire1Direction = false;
		}
	}
	//jump
	//not working correctly, may be due to fire1, only when on ground
	if(Input.GetButtonDown("Jump")) {
//	    speed = 1;
		if(collided_with != null) {
			if(collided_with.tag == "Ground") {
				rigidbody.velocity = Vector3(0,3,0);
				return;
			}
		}
	}
	
	if(Input.GetButtonDown("Fire1")) {
	 	if(fire1Direction == true){
			//Instantiate(prefab, location, rotation)
			Instantiate(drumBeat_pref, Vector3(transform.position.x + 0.6,transform.position.y + 0.1,6), Quaternion.identity);
			fire1_guiShow = true;
		}
		else if(fire1Direction == false) {
			Instantiate(drumBeat_pref, Vector3(transform.position.x - 0.6, transform.position.y + 0.1, 6), Quaternion.identity);
			fire1_guiShow = true;
		}
	}
//	if(Input.GetButtonUp("Fire1")) {
//		fire1_guiShow = false;
//	}
}

function OnCollisionEnter (col : Collision) {
	collided_with = col.gameObject;
}

function OnCollisionExit (col : Collision) {
	collided_with = null;
}

function OnGUI () {
	//Rect(x,y,width,height)
	GUI.DrawTexture(Rect(Screen.width/4, 10, Screen.width/2, 100), musicSheet_texture);
	
	//create dot on music sheet
	if(fire1_guiShow) {
		
		if (!firstDot) {
		  GUI.DrawTexture(new Rect(Screen.width/4 + 100, 130, 20, 20), fire1_texture);
		  firstDot = true;
		//fire1_guiShow = false;
		//fire1_guiDistance = fire1_guiDistance + 20;
		//return;
		}
		
		else {
		  GUI.DrawTexture(new Rect(Screen.width/4 + 150, 130, 20, 20), fire1_texture);
		
		}
	}
//	if(fire1_guiCount > 1) {
//		GUI.DrawTexture(Rect(Screen.width/4 + 100 + fire1_guiDistance, 130, 20, 20), fire1_texture);
//		fire1_guiDistance = fire1_guiDistance + 20;
//	}
}