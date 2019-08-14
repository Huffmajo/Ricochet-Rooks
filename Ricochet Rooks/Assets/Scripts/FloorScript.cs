using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{

	public Material paintColor;
	public GameObject player;

	private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
    	rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
    		rend.material.SetColor("_Color", Color.red);
    }
}
