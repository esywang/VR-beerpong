using UnityEngine;
using System.Collections;

public class DestroyBall : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		Destroy(this.gameObject);
	}
}