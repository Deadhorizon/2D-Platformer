using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RubyController : MonoBehaviour
{
	public int maxHealth = 5;
	public float timeInvincible = 2.0f;
	public float speed = 3.0f;
	public int health
	{
		get
		{
			return currentHealth;
		}
	}

	bool isInvincible;
	float invincibleTimer;
	int currentHealth;


	Vector2 lookDirection = new Vector2(1, 0);
	Animator animator;
	Rigidbody2D rg;
	public GameObject projectilePref;
    // Start is called before the first frame update
    void Start()
    {
		rg = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector2 move = new Vector2(horizontal, vertical);
		if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
		{
			lookDirection.Set(move.x, move.y);
			lookDirection.Normalize();
		}
		animator.SetFloat("Look X", lookDirection.x);
		animator.SetFloat("Look Y", lookDirection.y);
		animator.SetFloat("Speed", move.magnitude);
		Vector2 pos = rg.position;
		pos += move * speed * Time.deltaTime;
		rg.MovePosition(pos);
		if (isInvincible)
		{
			invincibleTimer -= Time.deltaTime;
			if (invincibleTimer < 0) isInvincible = false;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			Launch();
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			RaycastHit2D hit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

			if (hit.collider != null)
			{
				NPC сharacter = hit.collider.GetComponent<NPC>();
				if (сharacter != null)
				{
					сharacter.DisplayDialog();
					Debug.Log("We found him!");
				}

			}
		}
	}
	public void ChangeHealth(int amount)
	{
		if (amount < 0)
		{
			animator.SetTrigger("Hit");
			if (isInvincible) return;
			isInvincible = true;
			invincibleTimer = timeInvincible;
		}
		currentHealth=Mathf.Clamp(currentHealth+amount,0,maxHealth);
		UIHPBar.instance.SetValue(currentHealth /(float) maxHealth);
	}
	void Launch()
	{
		GameObject projectileObject = Instantiate(projectilePref, rg.position+Vector2.up*0.5f, Quaternion.identity);
		Projectile projectile = projectileObject.GetComponent<Projectile>();
		projectile.Launch(lookDirection, 300);
		animator.SetTrigger("Launch");
	}

}
