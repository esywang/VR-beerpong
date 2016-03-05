using UnityEngine;
using System.Collections;

public class DeleteBall : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Table")
		{
			Destroy(this.gameObject);
		}
	}
}