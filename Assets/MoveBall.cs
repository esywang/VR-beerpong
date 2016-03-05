using UnityEngine;
using System.Collections;

public class MoveBall : MonoBehaviour {
	private bool dirRight = true;
	// Update is called once per frame

	void Update () {
		if (dirRight){
			Vector3 pos = transform.position;
			pos.x = pos.x + 0.03f;
			pos.z = pos.z - 0.03f;
			transform.position = pos;
		} else {
			Vector3 pos = transform.position;
			pos.x = pos.x - 0.03f;
			pos.z = pos.z + 0.03f;
			transform.position = pos;
		}

		if(transform.position.x >= -2) {
			dirRight = false;
		}

		if(transform.position.x <= -4) {
			dirRight = true;

		}
	}
}
