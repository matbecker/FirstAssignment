using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire1"))
		{
			Rigidbody InstantiateProjectile = Instantiate(rb, transform.position, transform.rotation) as Rigidbody;
			InstantiateProjectile.velocity = transform.TransformDirection(new Vector3(0f,0f,speed));
			Debug.Log ("Firing");
			Destroy(InstantiateProjectile.gameObject, 5f);
		}

	
	}
}
