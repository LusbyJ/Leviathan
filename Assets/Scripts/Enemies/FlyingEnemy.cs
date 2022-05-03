using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
	public Vector2 shadowOffset;
	public Material shadowMaterial;
	
	private GameObject shadowObject;
	private SpriteRenderer shadowRenderer;
	
    // Start is called before the first frame update
    void Start()
    {
        setGround(false);
		setFlying(true);
		
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
	
	void OnDestroy(){
		Destroy(shadowObject);
	}
}
