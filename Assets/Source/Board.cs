using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static Board instance;
    public static Board Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Board>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("Board");
                    instance = obj.AddComponent<Board>();
                }
            }
            return instance;
        }
    }

    private string[][][] board;

    public static List<(string Color, string Name)> BluechessPieces;
    public static List<(string Color, string Name)> RedchessPieces;

    public string[][][] GetBoard()
    {
        return board;
    }
    
    public void ChangePosition(string [][][] chessboard,int start_x, int start_y, int end_x, int end_y)
    {
        if (chessboard[start_x][start_y][0] == "")
        {
            Debug.LogError("No piece at the start position");
            return;
        }

        if (chessboard[end_x][end_y][0] != "")
        {
            string endPieceName = chessboard[end_x][end_y][0] + "_" + chessboard[end_x][end_y][1];
            GameObject endPieceObject = GameObject.Find(endPieceName);

            if (endPieceObject != null)
            {
                endPieceObject.SetActive(false);
            }
            else
            {
                Debug.LogError("GameObject not found for the piece at the end position: " + endPieceName);
            }
        }





        // Di chuyển quân cờ từ vị trí bắt đầu đến vị trí kết thúc
        chessboard[end_x][end_y] = chessboard[start_x][start_y];

        // Xóa quân cờ tại vị trí khởi đầu
        chessboard[start_x][start_y] = new string[] { "", "" };
    }
    private void InitializeBoard()
    {
        board = new string[9][][];

        for (int i = 0; i < 9; i++)
        {
            board[i] = new string[10][];
            for (int j = 0; j < 10; j++)
            {
                board[i][j] = new string[2];
                for (int k = 0; k < 2; k++)
                {
                    board[i][j][k] = "";
                }
            }
        }
    }
    private void SetupBlueChess()
    {
        board[0][0] = new string[] { "Blue", "Xe2" };
        board[8][0] = new string[] { "Blue", "Xe1" };

        board[1][0] = new string[] { "Blue", "Ma2" };
        board[7][0] = new string[] { "Blue", "Ma1" };

        board[2][0] = new string[] { "Blue", "Tuong1" };
        board[6][0] = new string[] { "Blue", "Tuong2" };

        board[3][0] = new string[] { "Blue", "Si1" };
        board[5][0] = new string[] { "Blue", "Si2" };

        board[4][0] = new string[] { "Blue", "Soai1" };

        board[1][2] = new string[] { "Blue", "Phao1" };
        board[7][2] = new string[] { "Blue", "Phao2" };

        board[0][3] = new string[] { "Blue", "Tot1" };
        board[2][3] = new string[] { "Blue", "Tot2" };
        board[4][3] = new string[] { "Blue", "Tot3" };
        board[6][3] = new string[] { "Blue", "Tot4" };
        board[8][3] = new string[] { "Blue", "Tot5" };
    }
    private void SetupRedChess()
    {
        board[0][9] = new string[] { "Red", "Xe1" };
        board[8][9] = new string[] { "Red", "Xe2" };

        board[1][9] = new string[] { "Red", "Ma1" };
        board[7][9] = new string[] { "Red", "Ma2" };

        board[2][9] = new string[] { "Red", "Tuong1" };
        board[6][9] = new string[] { "Red", "Tuong2" };

        board[3][9] = new string[] { "Red", "Si1" };
        board[5][9] = new string[] { "Red", "Si2" };

        board[4][9] = new string[] { "Red", "Soai1" };

        board[1][7] = new string[] { "Red", "Phao1" };
        board[7][7] = new string[] { "Red", "Phao2" };

        board[0][6] = new string[] { "Red", "Tot5" };
        board[2][6] = new string[] { "Red", "Tot4" };
        board[4][6] = new string[] { "Red", "Tot3" };
        board[6][6] = new string[] { "Red", "Tot2" };
        board[8][6] = new string[] { "Red", "Tot1" };
    }
    public void PrintBoard()
    {
        Debug.Log("Chess Board:");

        for (int y = 9; y >= 0; y--)
        {
            string rowString = "";
            for (int x = 0; x < 9; x++)
            {
                if (board[x][y][0] != "")
                {
                    rowString += "[" + board[x][y][0] + "," + board[x][y][1] + "]";
                }
                else
                {
                    rowString += "[  ]";
                }
            }
            Debug.Log(rowString);
        }
    }
    public Vector2Int SeacrhPosition(string color,string name)
    {
        for (int i = 0;i < 9;i++)
        {
            for(int j = 0;j < 10; j++)
            {
                if (board[i][j][0] == color && board[i][j][1] == name)
                {
                    return new Vector2Int(i,j);
                }
            }
        }
        return new Vector2Int(-1,-1);
    }

    public string getChessAtPosition(int x,int y)
    {
        return board[x][y][0] +"_"+board[x][y][1];
    }

    public List<(string,string)> GetBlueChessList()
    {
        return BluechessPieces;
    }

    public List<(string,string)> GetRedChessList()
    {
        return RedchessPieces;
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializeBoard();
        SetupBlueChess();
        SetupRedChess();
    }
    private void Start()
    {
        PrintBoard();

        BluechessPieces = new List<(string Color, string Name)>();
        RedchessPieces = new List<(string Color, string Name)>();

        // Thêm các quân cờ blue vào danh sách
        BluechessPieces.Add(("Blue", "Xe1"));
        BluechessPieces.Add(("Blue", "Xe2"));
        BluechessPieces.Add(("Blue", "Ma1"));
        BluechessPieces.Add(("Blue", "Ma2"));
        BluechessPieces.Add(("Blue", "Phao1"));
        BluechessPieces.Add(("Blue", "Phao2"));
        BluechessPieces.Add(("Blue", "Si1"));
        BluechessPieces.Add(("Blue", "Si2"));
        BluechessPieces.Add(("Blue", "Tuong1"));
        BluechessPieces.Add(("Blue", "Tuong2"));
        BluechessPieces.Add(("Blue", "Soai1"));
        BluechessPieces.Add(("Blue", "Tot1"));
        BluechessPieces.Add(("Blue", "Tot2"));
        BluechessPieces.Add(("Blue", "Tot3"));
        BluechessPieces.Add(("Blue", "Tot4"));
        BluechessPieces.Add(("Blue", "Tot5"));

        // Thêm các quân cờ red vào danh sách
        RedchessPieces.Add(("Red", "Xe1"));
        RedchessPieces.Add(("Red", "Xe2"));
        RedchessPieces.Add(("Red", "Ma1"));
        RedchessPieces.Add(("Red", "Ma2"));
        RedchessPieces.Add(("Red", "Phao1"));
        RedchessPieces.Add(("Red", "Phao2"));
        RedchessPieces.Add(("Red", "Si1"));
        RedchessPieces.Add(("Red", "Si2"));
        RedchessPieces.Add(("Red", "Tuong1"));
        RedchessPieces.Add(("Red", "Tuong2"));
        RedchessPieces.Add(("Red", "Soai1"));
        RedchessPieces.Add(("Red", "Tot1"));
        RedchessPieces.Add(("Red", "Tot2"));
        RedchessPieces.Add(("Red", "Tot3"));
        RedchessPieces.Add(("Red", "Tot4"));
        RedchessPieces.Add(("Red", "Tot5"));

    }
}
