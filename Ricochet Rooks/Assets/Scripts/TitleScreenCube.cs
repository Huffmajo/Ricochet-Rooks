using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCube : MonoBehaviour
{
	public float maxRotate;
	public float minRotate;
	public float rotationSpeed;
	public float rotateX;
	public float rotateY;
	public float rotateZ;
	public int rotationStyle;

    // Start is called before the first frame update
    void Start()
    {
    	rotationStyle = 0;
    	rotationSpeed = 1.0f;
        maxRotate = 2.0f;
		minRotate = -2.0f;
	
		// paint cube red
		GetComponent<Renderer>().material.color = Color.red;

		randomizeSpeeds();
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Space))
    	{
    		if (rotationStyle >= 5)
    		{
    			rotationStyle = 0;
    		}
    		else
    		{
    			rotationStyle += 1;
    		}
    	}

    	switch (rotationStyle)
    	{
    		case 0:
    			rotateX = rotationSpeed;
    			rotateY = 0f;
    			rotateZ = 0f;
    			break;

    		case 1:
    			rotateX = 0f;
    			rotateY = rotationSpeed;
    			rotateZ = 0f;
    			break;

    		case 2:
    			rotateX = 0f;
    			rotateY = 0f;
    			rotateZ = rotationSpeed;
    			break;

    		case 3:
    			rotateX = rotationSpeed * -1;
    			rotateY = 0f;
    			rotateZ = 0f;
    			break;

    		case 4:
    			rotateX = 0f;
    			rotateY = rotationSpeed * -1;
    			rotateZ = 0f;
    			break;

			case 5:
    			rotateX = 0f;
    			rotateY = 0f;
    			rotateZ = rotationSpeed * -1;
    			break;

    		default:
    			rotateX = 0f;
    			rotateY = 0f;
    			rotateZ = 0f;
    			break;
    	}

   		transform.Rotate(rotateX, rotateY, rotateZ, Space.World);	
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
