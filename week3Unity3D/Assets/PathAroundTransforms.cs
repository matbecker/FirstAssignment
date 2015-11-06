using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathAroundTransforms : MonoBehaviour {

	[SerializeField] List<Transform> nodes;
	[SerializeField] float speed = 1f;
	int index = 0;
	Transform currentNode;
	
	// Use this for initialization
	void Start () {
		currentNode = nodes [0];
	}
	
	// Update is called once per frame
	void Update () {
		var dir = currentNode.position - transform.position;
		if (dir.magnitude <= 0.05) {
			GetNextNode();
			dir = currentNode.position - transform.position;
		}
		dir.Normalize ();
		transform.position += dir * speed * Time.deltaTime;
		
	}
	
	Transform GetNextNode(){
		index++;
		if (index >= nodes.Count) {
			index = 0;
		}
		currentNode = nodes [index];
		return currentNode;
	}
}
	
