using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;
	public Rigidbody rb;
	[SerializeField] Transform shootPoint;
	private float timer;
	[SerializeField] float shotDelay;
	private bool hasShot;
	[SerializeField] GameObject bullet;
	[SerializeField] Material material;
	[SerializeField] ParticleSystem ps;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (hasShot)
		{
			ps.Play();
			timer += Time.deltaTime;
		}
		if (timer > shotDelay)
		{
			timer = 0.0f;
			hasShot = false;
		}

		if (Input.GetKey("z") && timer == 0.0f)
		{
			hasShot = true;
			Rigidbody InstantiateProjectile = Instantiate(rb, shootPoint.position, transform.rotation) as Rigidbody;
			InstantiateProjectile.velocity = transform.TransformDirection(new Vector3(0.0f,0.0f,speed));
			Destroy(InstantiateProjectile.gameObject, 1f);
		}

	
	}
}
