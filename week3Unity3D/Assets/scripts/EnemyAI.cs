using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {


	public Player player;
	[SerializeField] float playerDistance;
	[SerializeField] float rotationDamping;
	[SerializeField] float moveSpeed;
	[SerializeField] float chaseCooldown;
	[SerializeField] GameObject bullet;
	[SerializeField] Transform lookPoint;
	[SerializeField] Rigidbody rb;
	[SerializeField] float speed;
	[SerializeField] float shotDelay;
	private Quaternion currentRot;
	private Vector3 currentPos;
	private float shotTimer;
	private bool stopChasing;
	private float timer;


	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
	}
	void LookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}
	void ChasePlayer()
	{
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
	void StopChasing()
	{
		stopChasing = true;
	}
	void ShootPlayer()
	{
		Rigidbody InstantiateProjectile = Instantiate(rb, currentPos, currentRot) as Rigidbody;
		InstantiateProjectile.velocity = transform.TransformDirection(new Vector3(0.0f,0.0f,speed));
	}
	// Update is called once per frame
	void Update () 
	{

		playerDistance = Vector3.Distance(player.transform.position, transform.position);
		currentRot = transform.rotation;
		currentPos = lookPoint.transform.position;
		if (playerDistance < 25.0f)
		{
			LookAtPlayer();
		}
		if (playerDistance < 15.0f && !stopChasing)
		{
			ChasePlayer();
		}

		if (playerDistance < 5.0f)
		{

			StopChasing();
			shotTimer += Time.deltaTime;
			if (shotTimer > shotDelay)
			{
				ShootPlayer();
				shotTimer = 0.0f;
			}
		}
		else
		{
			timer += Time.deltaTime;

			if (timer > chaseCooldown)
			{
				stopChasing = false;
				timer = 0.0f;
			}
		}
	
	}
}
