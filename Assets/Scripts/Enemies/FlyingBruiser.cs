using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBruiser : Enemy
{
    public CircleCollider2D attackCollider;
	public Vector2 shadowOffset;
	public Material shadowMaterial;
	
	private GameObject shadowObject;
	private SpriteRenderer shadowRenderer;
	
	// Start is called before the first frame update
    void Start()
    {
        setGround(false);
		setFlying(true);
		attackCollider.enabled = false;
		
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		shadowObject = new GameObject("Shadow");
		shadowRenderer = shadowObject.AddComponent<SpriteRenderer>();
		shadowRenderer.sprite = renderer.sprite;
		shadowRenderer.material = shadowMaterial;
		shadowRenderer.sortingLayerName = renderer.sortingLayerName;
		shadowRenderer.sortingOrder = renderer.sortingOrder - 1;
    }

    void LateUpdate()
    {
        shadowObject.transform.position = transform.position + (Vector3)shadowOffset;
		shadowRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
    }
	
	public override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
		}
		if (collision.gameObject.tag == "Tower" && moving)
		{
			moving = false;
			StartCoroutine(attack());
		}
		if (collision.gameObject.tag == "Slum" && moving)
		{
			moving = false;
			StartCoroutine(attack());
		}
		if(collision.gameObject.tag == "Slime")
        {
			Destroy(collision.gameObject);
			poison();
        }
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Tower"){
			collider.gameObject.GetComponent<Health>().takeDamage(power);
		}
	}
	
	IEnumerator attack(){
		attackCollider.enabled = true;
		yield return new WaitForSeconds(0.1f);
		attackCollider.enabled = false;
		takeDamage(health);
	}
	
	void OnDestroy(){
		Destroy(shadowObject);
	}
}
