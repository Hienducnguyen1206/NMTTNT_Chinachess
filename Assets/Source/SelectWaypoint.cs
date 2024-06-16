using System;
using UnityEngine;

public class SelectWaypoint : MonoBehaviour
{

    public LayerMask waypointLayer;
    private float moveSpeed = 30.0f;
    private Vector3 targetPosition;
    public static bool isMoving = false;

    private Transform selectedObject;
    private  Transform currentObject;
 

  
    string[][][] board;

    private string selectedColor;
    private string selectedPiece;

    Vector2Int newPosition;

    public static event Action PlayerMove;

    void Start()
    {        
        board = Board.Instance.GetBoard();
    }

    private void Update()
    {
        CheckSelectedObjectNotNull();
        GetWaypointPosition();        
        MoveSelectedObjectToWayPoint();        
    }
   

    private void ProcessCurrentObjectName(string objectName)
    {
        string[] parts = objectName.Split('_');
        if (parts.Length >= 2)
        {
            selectedColor = parts[0];
            selectedPiece = parts[1];
        }
        else
        {
            Debug.Log("Object name does not contain an underscore or is improperly formatted.");
        }
    }

    private Vector2Int CheckChessPosition(string color, string piece)
    {
        string[][][] board = Board.Instance.GetBoard();

        for (int i = 0; i < board.Length; i++)  
        {
            for (int j = 0; j < board[i].Length; j++)  
            {
                if (board[i][j][0] == color && board[i][j][1] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        Debug.Log("Piece not found.");
        return new Vector2Int(-1, -1);
    }

    private Vector2Int ProcessObjectName(string name)
    {
        string[] parts = name.Split('_');
        if (parts.Length >= 3)
        {
            int x, y;
            if (int.TryParse(parts[1], out x) && int.TryParse(parts[2], out y))
            {
                return new Vector2Int(x, y);
            }
            else
            {
                Debug.Log("Error: Could not parse the coordinates.");
            }
        }
        else
        {
            Debug.Log("Error: Name format is incorrect or missing parts.");
        }
        return new Vector2Int(-1, -1);
    }

    private void CheckSelectedObjectNotNull()
    {
        selectedObject = SelectChess.GetSelection();
        if (selectedObject != null)
        {
            currentObject = selectedObject;
            ProcessCurrentObjectName(currentObject.name);
        }
    }

    private void MoveSelectedObjectToWayPoint()
    {
        if (isMoving && currentObject != null)
        {
           
            SelectChess.ReturnOrigin();
            currentObject.parent.position = Vector3.MoveTowards(currentObject.parent.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(currentObject.parent.position, targetPosition) < 0.001f)
            {
                currentObject.parent.position = targetPosition;
                isMoving = false;
                              
                Vector2Int oldPosition = CheckChessPosition(selectedColor, selectedPiece);
                Board.Instance.ChangePosition(board, oldPosition.x, oldPosition.y, newPosition.x, newPosition.y);

                PlayerMove?.Invoke();
                Board.Instance.PrintBoard();
                TurnSystem.changeTurn();
            }
        }
    } 

    private void GetWaypointPosition()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, waypointLayer))
            {
                if (hit.collider.CompareTag("Waypoint"))
                {
                    targetPosition = hit.collider.transform.position;
                    isMoving = true;
                    newPosition = ProcessObjectName(hit.collider.gameObject.name);

                }
            }
        }
    }












}
