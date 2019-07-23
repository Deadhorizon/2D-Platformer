using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	Rigidbody2D rg;
    // Start is called before the first frame update
    void Awake()
    {
		rg = GetComponent<Rigidbody2D>();
    }

	void Update()
	{
		if (transform.position.magnitude > 1000f)
		{
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	public void Launch(Vector2 direction, float force)
	{
		rg.AddForce(direction * force);
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		EnemyController enem_cont = other.collider.GetComponent<EnemyController>();
		if (enem_cont != null)
		{
			enem_cont.Fix();
		}
		Destroy(gameObject);
	}
}
