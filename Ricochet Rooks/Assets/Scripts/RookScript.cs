using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookScript : MonoBehaviour
{

	public float hori;
	public float vert;
	public float speed = 2f;

	private PlayerState playerState;
	private Direction inputDir;

	enum Direction {UP, DOWN, LEFT, RIGHT, NONE};
	enum PlayerState {IDLE, MOVING};

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;
        inputDir = Direction.NONE;
    }

    // Update is called once per frame
    void Update()
    {
    	// debug info
    	print("PlayerState: " + playerState);

    	// get inputs
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        // only act if input is entered
        if (hori >= -0.5 && hori <= 0.5 && vert >= 0.5 && vert <= 0.5)
        {

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
	        	// player is now moving
	        	playerState = PlayerState.MOVING;

	        	// move until colliding with wall
	        	move(inputDir);

	        	// player has stopped again
		    	playerState = PlayerState.IDLE;
	        }
	        else
	        {
	        	print("Cannot move that direction");
	        }
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
    			dirToMove = Vector3.back;
    			break;

    		case Direction.LEFT:
    			dirToMove = Vector3.left;
    			break;

    		case Direction.RIGHT:
    			dirToMove = Vector3.right;
    			break;

    		default:
    			dirToMove = Vector3.up;
    			break;
    	}

    	while (checkDirClear(dir))
    	{
    		transform.position += dirToMove * speed;
    	}	
    }
}
