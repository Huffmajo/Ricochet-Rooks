using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCube : MonoBehaviour
{
	public float maxRotate;
	public float minRotate;
	public float rotateX;
	public float rotateY;
	public float rotateZ;
	public bool self;

    // Start is called before the first frame update
    void Start()
    {
    	self = true;
        maxRotate = 2.0f;
		minRotate = -2.0f;
	
		GetComponent<Renderer>().material.color = Color.red;

		randomizeSpeeds();
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetAxis("Jump") > 0.2f)
    	{
    		randomizeSpeeds();
    	}

    	if (self)
    	{
    		transform.Rotate(rotateX, rotateY, rotateZ, Space.Self);	
    	}
        else
        {
        	transform.Rotate(rotateX, rotateY, rotateZ, Space.World);	
        }
    }

    void randomizeSpeeds()
    {
    	rotateX = Random.Range(minRotate, maxRotate);
    	rotateY = Random.Range(minRotate, maxRotate);
    	rotateZ = Random.Range(minRotate, maxRotate);
    }

    void reverseRotation()
    {
    	rotateX *= -1;
    	rotateY *= -1;
    	rotateZ *= -1;
    }
}
