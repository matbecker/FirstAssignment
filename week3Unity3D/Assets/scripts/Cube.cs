using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	[SerializeField] GameObject cube;
	[SerializeField] int x;
	[SerializeField] int y;
	[SerializeField] int spacing;

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Bullet"))
		{
			Destroy(gameObject);
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
				Instantiate(cube, pos, transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

			
	}
}
