using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Camera cam;
	public float speed;
	public Rigidbody rb;
	[SerializeField] Transform shootPoint;
	private float timer;
	[SerializeField] float shotDelay;
	private bool hasShot;
	[SerializeField] GameObject bullet;
	[SerializeField] Material material;
	[SerializeField] ParticleSystem ps;
	[SerializeField] int health;
	[SerializeField] Text healthText;

	// Use this for initialization
	void Start () 
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{

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
			InstantiateProjectile.velocity = transform.TransformDirection(Vector3.forward * speed);
			InstantiateProjectile.GetComponent<Bullet>().SetNextColor();
			Destroy(InstantiateProjectile.gameObject, 1f);
		}
		if (health <= 0)
		{
			PlayerDeath();
		}
		healthText.text = ("Health: " + health);


	
	}
	public void Damage(int dmg)
	{
		health -= dmg;
		Camera.instance.Shake(0.2f,0.2f);
	}
	void PlayerDeath()
	{
		//restart the level
		Application.LoadLevel(Application.loadedLevel);
	}
}
