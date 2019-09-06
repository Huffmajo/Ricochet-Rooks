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
	public Color playerColor;
	public static Stack<Vector3> prevPos = new Stack<Vector3>();
	public GameObject canvasUI;
	public bool inControl;
	public int numMovesBest;
	public int numMovesFinal;
	public string medalReceived;
	public GameObject completeLevelUI;
	public GameObject alwaysUI;
	public GameObject shakee;
	public float magnitude;
	public float duration;

	private PlayerState playerState;
	private Direction inputDir;
	private Scene currentScene;

	enum Direction {UP, DOWN, LEFT, RIGHT, NONE};
	enum PlayerState {IDLE, MOVING};

    void Start()
    {
    	// initialize variables on start
    	currentScene = SceneManager.GetActiveScene();
        playerState = PlayerState.IDLE;
        inputDir = Direction.NONE;
        paintColor = Color.red;
        playerColor = Color.magenta;
        GetComponent<Renderer>().material.color = playerColor;
        inControl = true;
        numMoves = 0;
        numMovesBest = 999;
        undo = false;
        completeLevelUI.SetActive(false);
        magnitude = 0.1f;
		duration = 0.2f;
    }

    void Update()
    {
    	// limit player input if we need to
    	if (inControl)
    	{
    		// get inputs
	        hori = Input.GetAxis("Horizontal");
    	    vert = Input.GetAxis("Vertical");
        	restart = Input.GetAxis("Jump");
    	    undo = Input.GetKeyDown(KeyCode.Q);
    	}
    	
    	// check if level is completed
    	if (canvasUI.GetComponent<UIScript>().roundedPercentage == 100)
    	{
    		levelBeat();
    	}

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
        		
        			// decrement paintLevel
        			decrementPaintLevel();
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
	        		incrementPaintLevel();
	        		
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
	        	//print("Cannot move that direction");
	        	StartCoroutine(shake(shakee, magnitude, duration));
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
    		if (rend.material.color == paintColor)
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

    void incrementPaintLevel()
    {
    	Vector3 towardsFloor = Vector3.down;
    	GameObject tileToPaint;

    	RaycastHit hit;
    	if (Physics.Raycast(transform.position, towardsFloor, out hit, 5f))
    	{
    		tileToPaint = hit.transform.gameObject;

    		tileToPaint.GetComponent<TileScript>().paintLevel += 1;
    	}
    }

    void decrementPaintLevel()
    {
    	Vector3 towardsFloor = Vector3.down;
    	GameObject tileToPaint;

    	RaycastHit hit;
    	if (Physics.Raycast(transform.position, towardsFloor, out hit, 5f))
    	{
    		tileToPaint = hit.transform.gameObject;

    		tileToPaint.GetComponent<TileScript>().paintLevel -= 1;
    	}
    }

    void levelBeat()
    {
    	if (!completeLevelUI.activeSelf)
    	{
    		// set current number of moves 
	    	numMovesFinal = numMoves;

	    	if (numMovesFinal > canvasUI.GetComponent<UIScript>().silverPar)
	    	{
	    		medalReceived = "bronze";
	    	}
	    	else if(numMovesFinal > canvasUI.GetComponent<UIScript>().goldPar)
	    	{
	    		medalReceived = "silver";
	    	}
	    	else
	    	{
	    		medalReceived = "gold";
	    	}

	    	// overwrite best moves if current is better
	    	if (numMovesFinal < numMovesBest)
	    	{
	    		numMovesBest = numMovesFinal;

	    		// save best number of moves if it's changed
	    	}

	    	print("level Completed. " + medalReceived + "medal received!");

	    	// bring up level complete UI
	    	completeLevelUI.SetActive(true);

	    	// deactivate player movement
	    	inControl = false;
    	}
    }

    public IEnumerator shake(GameObject thingToShake, float magnitude, float duration)
    {
    	float x;
   		float y;
   		float z;

    	// retain objects initial position to return to after shaking
    	Vector3 originalPos = thingToShake.transform.position;

    	// shake for the entire duration
    	while (duration > 0)
    	{
    		print ("Duration: " + duration);
    		// get random values based on magnitude
    		x = Random.Range(duration * -1f, duration) + thingToShake.transform.position.x;
    		y = Random.Range(duration * -1f, duration) + thingToShake.transform.position.y;
    		z = Random.Range(duration * -1f, duration) + thingToShake.transform.position.z;
    		
    		thingToShake.transform.position = new Vector3(x, y, z);

    		// countdown duration
    		duration -= Time.deltaTime;

    		yield return null;
    	}

    	// return object back to initial position
    	thingToShake.transform.position = originalPos;
    }
}
