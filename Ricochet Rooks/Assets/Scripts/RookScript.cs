using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookScript : MonoBehaviour
{

	public float hori;
	public float vert;
	public PlayerState playerState;

	enum Direction {UP, DOWN, LEFT, RIGHT};
	enum PlayerState {IDLE, MOVING};

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
    	Direction inputDir;

    	// get inputs
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        // only get input when player isn't moving
        if (playerState == PlayerState.IDLE)
        {
        	if (vert > 0.5)
        	{
        		inputDir = Direction.UP;
        	}
        	else if (vert < -0.5)
        	{
        		inputDir = Direction.DOWN;
        	}
        	else if (hori > 0.5)
        	{
        		inputDir = Direction.RIGHT;
        	}
        	else if (hori < -0.5)
        	{
        		inputDir = Direction.LEFT;
        	}
        }

        // check if input direction is clear
        if (checkDirClear(inputDir))
        {
        	// move until colliding with wall
        	move(inputDir);
        }
        else
        {
        	print("Cannot move that direction");
        }
    }

    // Check if the space immediately in provided direction is clear
    bool checkDirClear(Direction dir)
    {
    	Vector3 dirToCheck;

    	switch (dir)
    	{
    		case Direction.UP:
    			dirToCheck = Vector3.forward;
    			break;

    		case Direction.DOWN:
    			dirToCheck = Vector3.back;
    			break;

    		case Direction.LEFT:
    			dirToCheck = Vector3.left;
    			break;

    		case Direction.RIGHT:
    			dirToCheck = Vector3.right;
    			break;

    		default:
    			dirToCheck = Vector3.up;
    			break;
    	}

    	RaycastHit hit;
    	if (Physics.Raycast(transform.position, dirToCheck, out hit, 100f))
    	{
    		print("Object at distance: " + hit.distance);
    		if (hit.distance < 0.1)
    		{
    			return false;
    		}
    		else
    		{
    			return true;
    		}
    	}

    	// ray doesn't collide
    	else
    	{
    		print("No collision detected");
    		return true;
    	}
    }

    // Move in the provided direction until colliding with a solid
    void move(Direction dir)
    {
    	Vector3 dirToMove;

    	switch (dir)
    	{
    		case Direction.UP:
    			dirToMove = Vector3.forward;
    			break;

    		case Direction.DOWN:
    			dirToCheck = Vector3.back;
    			break;

    		case Direction.LEFT:
    			dirToCheck = Vector3.left;
    			break;

    		case Direction.RIGHT:
    			dirToCheck = Vector3.right;
    			break;

    		default:
    			dirToCheck = Vector3.up;
    			break;
    	}

    	while (checkDirClear(dir))
    	{

    	}	
    }
}
