using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	[SerializeField] GameObject cube;
	[SerializeField] int x;
	[SerializeField] int y;
	[SerializeField] int spacing;
	[SerializeField] bool collided;
	[SerializeField] Rigidbody rb;

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Bullet"))
		{
			collided = true;
		}
	}
	// Use this for initialization
	void Start () {

		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < x; j++)
			{
				Vector3 pos = transform.position;
				pos.x += i;
				pos.y += j;
				var box = Instantiate(cube, pos, transform.rotation);

				if (collided)
				{
					Destroy(box);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

			
	}
}
