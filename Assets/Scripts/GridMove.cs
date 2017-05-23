using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {
	private bool isMoving;
	public float gridSize = 1f;
	public float moveSpeed = 3f;

	void Start () {
		isMoving = false;
	}

	void Update () {
		if (!isMoving) {
			Vector3 destination = Vector3.zero;
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			if (input.x != 0) {
				destination = transform.position + new Vector3 (gridSize * input.x, 0, 0);
			} else if (input.y != 0) {
				destination = transform.position + new Vector3 (0, 0, gridSize * input.y);
			}

			if (destination != Vector3.zero) {
				StartCoroutine (Move (transform, destination));
			}
		}
	}

	IEnumerator Move(Transform transform, Vector3 destination) {
		isMoving = true;
		while (transform.position != destination) {
			transform.position = Vector3.MoveTowards (transform.position, destination, moveSpeed * Time.deltaTime);
			yield return null;
		}

		transform.position = destination;
		isMoving = false;
		yield return 0;
	}
}
