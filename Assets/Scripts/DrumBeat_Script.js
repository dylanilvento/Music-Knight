#pragma strict

var speed: int = 5;


function Start () {

}

function Update () {
	transform.Translate(Vector3(speed * Time.deltaTime, 0, 0));
}

function OnCollisionEnter (col : Collision) {
	if(col.gameObject.tag == "Ground"){
		Destroy(gameObject);
	}
}