using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RookScript : MonoBehaviour
{

	public float hori;
	public float vert;
	public float restart;
	public bool undo;
	public float speed = 2f;
	public float inputThreshold = 0.2f;
	public float stopDistance = 0.5f;
	public int numMoves;
	public Color paintColor;
	public static Stack<Vector3> prevPos = new Stack<Vector3>();


	private PlayerState playerState;
	private Direction inputDir;
	private Scene currentScene;

	enum Direction {UP, DOWN, LEFT, RIGHT, NONE};
	enum PlayerState {IDLE, MOVING};

    // Start is called before the first frame update
    void Start()
    {
    	currentScene = SceneManager.GetActiveScene();
        playerState = PlayerState.IDLE;
        inputDir = Direction.NONE;
        paintColor = Color.red;
        numMoves = 0;
        undo = false;
    }

    // Update is called once per frame
    void Update()
    {
    	
    	// get inputs
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        restart = Input.GetAxis("Jump");
        undo = Input.GetKeyDown(KeyCode.Q);

        // restart scene on spacebar
        if (restart > inputThreshold)
        {
        	SceneManager.LoadScene(currentScene.name);
        }

        // enable undo button presses
        if (undo)
        {
        	if (numMoves > 0)
        	{
        		Vector3 lastPos = prevPos.Pop();

        		while(Vector3.Distance(transform.position, lastPos) > 0.001f)
        		{
					// move towards last position	
        			transform.position = Vector3.MoveTowards(transform.position, lastPos, speed * Time.deltaTime);
        		}

        		// decrement numMoves
        		numMoves -= 1;
        	}
        	else
        	{
        		print("Can't undo");
        	}
        }

        // only act if input is entered
        if (hori < inputThreshold * -1 || 
        	hori > inputThreshold || 
        	vert < inputThreshold * -1 || 
        	vert > inputThreshold)
        {

	        // only get input when player isn't moving
	        if (playerState == PlayerState.IDLE)
	        {
	        	if (vert > inputThreshold)
	        	{
	        		inputDir = Direction.UP;
	        	}
	        	else if (vert < inputThreshold * -1)
	        	{
	        		inputDir = Direction.DOWN;
	        	}
	        	else if (hori > inputThreshold)
	        	{
	        		inputDir = Direction.RIGHT;
	        	}
	        	else if (hori < inputThreshold * -1)
	        	{
	        		inputDir = Direction.LEFT;
	        	}
	        }

	        // check if input direction is clear
	        if (checkDirClear(inputDir))
	        {
	        	// save current position into prevPos stack
	        	prevPos.Push(transform.position);

	        	// player is now moving
	        	playerState = PlayerState.MOVING;

	        	while (checkDirClear(inputDir))
	        	{
	        		// paint current tile
	        		if (checkFloorColor(paintColor))
	        		{
	        			paintFloor(paintColor);
	        		}
	        		
	        		// move until colliding with wall
	        		move(inputDir);
	        	}

	        	// update that player has moved
	        	numMoves += 1;

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
    		//print("Object at distance: " + hit.distance);
    		if (hit.distance <= stopDistance)
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
    		//print("No collision detected");
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

    	// actually move
    	transform.position += dirToMove * Time.deltaTime * speed;
    }

    // returns true if floor is of a paintColor
    bool checkFloorColor(Color paintColor)
    {
    	Vector3 towardsFloor = Vector3.down;
    	GameObject tileToPaint;

    	RaycastHit hit;
    	if (Physics.Raycast(transform.position, towardsFloor, out hit, 5f))
    	{
    		tileToPaint = hit.transform.gameObject;

    		Renderer rend = tileToPaint.GetComponent<Renderer>();

    		// check if tile is of color paintColor
    		if (rend.material.color != paintColor)
    		{
    			return true;
    		}
    		else
    		{
    			return false;
    		}
    	}
    	else
    	{
    		return false;
    	}
    }

    void paintFloor(Color paintColor)
    {
    	Vector3 towardsFloor = Vector3.down;
    	GameObject tileToPaint;

    	RaycastHit hit;
    	if (Physics.Raycast(transform.position, towardsFloor, out hit, 5f))
    	{
    		tileToPaint = hit.transform.gameObject;

    		Renderer rend = tileToPaint.GetComponent<Renderer>();
    		rend.material.color = paintColor;
    	}
    }
}
