using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	Rigidbody2D rg;
	Animator animator;

	public float speed = 3.0f;
	public float changeTime = 2.0f;

	[SerializeField]
	ParticleSystem smokeEffect;
	bool vertical;
	bool broken = true;
	int dirrection = 1;
	float timer;

    // Start is called before the first frame update
    void Start()
    {
		rg = GetComponent<Rigidbody2D>();
		timer = changeTime;
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!broken)
		{
			return;

		}
		timer -= Time.deltaTime;
		if (timer < 0)
		{
			dirrection = -dirrection;
			timer = changeTime;
		}
		Vector2 position = rg.position;
		if (vertical)
		{
			position.y += Time.deltaTime * speed * dirrection;
			animator.SetFloat("MoveX", 0);
			animator.SetFloat("MoveY", dirrection);
		}
		else
		{
			position.x += Time.deltaTime * speed * dirrection;
			animator.SetFloat("MoveY", 0);
			animator.SetFloat("MoveX", dirrection);
		}
	
		rg.MovePosition(position);
    }
	public void Fix()
	{
		broken = false;
		rg.simulated = false;
		animator.SetTrigger("Fixed");
		smokeEffect.Stop();
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		RubyController ruby = other.gameObject.GetComponent<RubyController>();
		if (ruby != null)
		{
			ruby.ChangeHealth(-1);
		}
	}

}
