using System;
using System.Collections.Generic;
using UnityEngine;

public class GameServices
{

    private string[][][] cloneboard;
    public List<Move> GetAllBlueMoveable(string[][][] board)
    {  
        List<Move> result = new List<Move>();
        foreach(var pieces in Board.BluechessPieces) {
            Vector2Int position = CheckChessPosition(board, pieces.Color, pieces.Name);
            if (position != new Vector2Int(-1, -1))
            {
                string type = pieces.Name.Substring(0, pieces.Name.Length - 1);
                List<Vector2Int> PremoveablePoints = FindMoveablePoints(position, pieces.Color, type, board);
                List<Vector2Int> moveablePoints = CheckMoveablePoints(PremoveablePoints, "Blue", pieces.Name, position,board);
                foreach (var moveablePoint in moveablePoints)
                {
                    Move move = new Move(position.x,position.y,moveablePoint.x,moveablePoint.y);
                    result.Add(move);
                }
            }

        }
        return result;      
   }

    public List<Move> GetAllRedMoveable(string[][][] board)
    {
        List<Move> result = new List<Move>();
        foreach (var pieces in Board.RedchessPieces)
        {
            Vector2Int position = CheckChessPosition(board, pieces.Color, pieces.Name);
            if (position != new Vector2Int(-1, -1))
            {
                string type = pieces.Name.Substring(0, pieces.Name.Length - 1);
                List<Vector2Int> PremoveablePoints = FindMoveablePoints(position, pieces.Color, type, board);
                List<Vector2Int> moveablePoints = CheckMoveablePoints(PremoveablePoints, "Red", pieces.Name, position, board);
                foreach (var moveablePoint in moveablePoints)
                {
                    Move move = new Move(position.x, position.y, moveablePoint.x, moveablePoint.y);
                    result.Add(move);
                }
            }
        }
        return result;
    }




    private Vector2Int CheckChessPosition(string[][][] chessboard, string color, string piece)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (chessboard[i][j][0] == color && chessboard[i][j][1] == piece)
                {

                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    List<Vector2Int> FindMoveablePoints(Vector2Int position, string color, string type, string[][][] Chessboard)
    {

        List<Vector2Int> moveablePoints = new List<Vector2Int>();


        switch (type)
        {
            case "Xe":
                for (int i = position.y + 1; i < 10; i++)
                {
                    if (Chessboard[position.x][i][0] == "")
                        moveablePoints.Add(new Vector2Int(position.x, i));
                    else
                    {
                        if (Chessboard[position.x][i][0] != color)
                            moveablePoints.Add(new Vector2Int(position.x, i));
                        break;
                    }
                }

                for (int i = position.y - 1; i >= 0; i--)
                {
                    if (Chessboard[position.x][i][0] == "")
                        moveablePoints.Add(new Vector2Int(position.x, i));
                    else
                    {
                        if (Chessboard[position.x][i][0] != color)
                            moveablePoints.Add(new Vector2Int(position.x, i));
                        break;
                    }
                }

                for (int i = position.x - 1; i >= 0; i--)
                {
                    if (Chessboard[i][position.y][0] == "")
                        moveablePoints.Add(new Vector2Int(i, position.y));
                    else
                    {
                        if (Chessboard[i][position.y][0] != color)
                            moveablePoints.Add(new Vector2Int(i, position.y));
                        break;
                    }
                }

                for (int i = position.x + 1; i < 9; i++)
                {
                    if (Chessboard[i][position.y][0] == "")
                        moveablePoints.Add(new Vector2Int(i, position.y));
                    else
                    {
                        if (Chessboard[i][position.y][0] != color)
                            moveablePoints.Add(new Vector2Int(i, position.y));
                        break;
                    }
                }
                break;
            case "Phao":
                bool hasJumped = false;
                for (int i = position.y + 1; i < 10; i++)
                {
                    if (Chessboard[position.x][i][0] == "")
                    {
                        if (!hasJumped)
                            moveablePoints.Add(new Vector2Int(position.x, i));
                    }
                    else
                    {
                        if (!hasJumped)
                            hasJumped = true;
                        else
                        {
                            if (Chessboard[position.x][i][0] != color)
                                moveablePoints.Add(new Vector2Int(position.x, i));
                            break;
                        }
                    }
                }


                hasJumped = false;
                for (int i = position.y - 1; i >= 0; i--)
                {
                    if (Chessboard[position.x][i][0] == "")
                    {
                        if (!hasJumped)
                            moveablePoints.Add(new Vector2Int(position.x, i));
                    }
                    else
                    {
                        if (!hasJumped)
                            hasJumped = true;
                        else
                        {
                            if (Chessboard[position.x][i][0] != color)
                                moveablePoints.Add(new Vector2Int(position.x, i));
                            break;
                        }
                    }
                }


                hasJumped = false;
                for (int i = position.x - 1; i >= 0; i--)
                {
                    if (Chessboard[i][position.y][0] == "")
                    {
                        if (!hasJumped)
                            moveablePoints.Add(new Vector2Int(i, position.y));
                    }
                    else
                    {
                        if (!hasJumped)
                            hasJumped = true;
                        else
                        {
                            if (Chessboard[i][position.y][0] != color)
                                moveablePoints.Add(new Vector2Int(i, position.y));
                            break;
                        }
                    }
                }


                hasJumped = false;
                for (int i = position.x + 1; i < 9; i++)
                {
                    if (Chessboard[i][position.y][0] == "")
                    {
                        if (!hasJumped)
                            moveablePoints.Add(new Vector2Int(i, position.y));
                    }
                    else
                    {
                        if (!hasJumped)
                            hasJumped = true;
                        else
                        {
                            if (Chessboard[i][position.y][0] != color)
                                moveablePoints.Add(new Vector2Int(i, position.y));
                            break;
                        }
                    }
                }
                break;
            case "Ma":

                int[][] moveOffsets = new int[][]
                {
                new int[] {-1, -2, 0, -1},
                new int[] {1, -2, 0, -1},
                new int[] {-1, 2, 0, 1},
                new int[] {1, 2, 0, 1},
                new int[] {-2, -1, -1, 0},
                new int[] {2, -1, 1, 0},
                new int[] {-2, 1, -1, 0},
                new int[] {2, 1, 1, 0}
                };

                foreach (int[] offset in moveOffsets)
                {
                    int targetX = position.x + offset[0];
                    int targetY = position.y + offset[1];
                    int blockX = position.x + offset[2];
                    int blockY = position.y + offset[3];

                    if (blockX >= 0 && blockX < 9 && blockY >= 0 && blockY < 10 && targetX >= 0 && targetX < 9 && targetY >= 0 && targetY < 10)
                    {
                        if (Chessboard[blockX][blockY][0] == "")
                        {
                            if (Chessboard[targetX][targetY][0] == "" || Chessboard[targetX][targetY][0] != color)
                                moveablePoints.Add(new Vector2Int(targetX, targetY));
                        }
                    }
                }
                break;
            case "Tuong":
                int[][] moves = new int[][]
                {
        new int[] { 2, 2 }, new int[] { 2, -2 },
        new int[] { -2, 2 }, new int[] { -2, -2 }
                };

                int minY = color == "Red" ? 5 : 0;
                int maxY = color == "Red" ? 9 : 4;

                foreach (var move in moves)
                {
                    int targetX = position.x + move[0];
                    int targetY = position.y + move[1];
                    int checkX = position.x + move[0] / 2;
                    int checkY = position.y + move[1] / 2;


                    if (targetX >= 0 && targetX < 9 && targetY >= minY && targetY <= maxY)
                    {

                        if (Chessboard[checkX][checkY][0] == "")
                        {

                            if (Chessboard[targetX][targetY][0] != color)
                            {

                                moveablePoints.Add(new Vector2Int(targetX, targetY));
                            }
                        }
                    }
                }
                break;

            case "Si":

                int[][] Smoves = new int[][]
                {
        new int[] { 1, 1 }, new int[] { 1, -1 },
        new int[] { -1, 1 }, new int[] { -1, -1 }
                };


                int SminX = 3;
                int SmaxX = 5;
                int SminY = color == "Red" ? 7 : 0;
                int SmaxY = color == "Red" ? 9 : 2;

                foreach (var Smove in Smoves)
                {
                    int targetX = position.x + Smove[0];
                    int targetY = position.y + Smove[1];


                    if (targetX >= SminX && targetX <= SmaxX && targetY >= SminY && targetY <= SmaxY)
                    {
                        if (Chessboard[targetX][targetY][0] != color)
                        {

                            moveablePoints.Add(new Vector2Int(targetX, targetY));
                        }
                    }
                }
                break;

            case "Tot":

                int forward = color == "Blue" ? 1 : -1;


                int riverCross = color == "Blue" ? 4 : 5;


                int nextY = position.y + forward;
                if (nextY >= 0 && nextY < 10)
                {
                    if (Chessboard[position.x][nextY][0] != color)
                    {
                        moveablePoints.Add(new Vector2Int(position.x, nextY));
                    }
                }


                if ((color == "Blue" && position.y > riverCross) || (color == "Red" && position.y < riverCross))
                {

                    if (position.x > 0)
                    {
                        if (Chessboard[position.x - 1][position.y][0] != color)
                        {
                            moveablePoints.Add(new Vector2Int(position.x - 1, position.y));
                        }
                    }

                    if (position.x < 8)
                    {
                        if (Chessboard[position.x + 1][position.y][0] != color)
                        {
                            moveablePoints.Add(new Vector2Int(position.x + 1, position.y));
                        }
                    }
                }
                break;
            case "Soai":

                int TminY = color == "Blue" ? 0 : 7;
                int TmaxY = color == "Blue" ? 2 : 9;
                int TminX = 3;
                int TmaxX = 5;


                if (position.y > TminY && Chessboard[position.x][position.y - 1][0] != color)
                {
                    moveablePoints.Add(new Vector2Int(position.x, position.y - 1));
                }

                if (position.y < TmaxY && Chessboard[position.x][position.y + 1][0] != color)
                {
                    moveablePoints.Add(new Vector2Int(position.x, position.y + 1));
                }

                if (position.x > TminX && Chessboard[position.x - 1][position.y][0] != color)
                {
                    moveablePoints.Add(new Vector2Int(position.x - 1, position.y));
                }

                if (position.x < TmaxX && Chessboard[position.x + 1][position.y][0] != color)
                {
                    moveablePoints.Add(new Vector2Int(position.x + 1, position.y));
                }
                break;
        }
        return moveablePoints;
    }

    public List<Vector2Int> CheckMoveablePoints(List<Vector2Int> PreMoveablePoints, string color, string piece, Vector2Int position, string[][][] board)
    {
        if (PreMoveablePoints == null)
        {
            // Xử lý trường hợp PreMoveablePoints là null, có thể trả về một danh sách trống hoặc thực hiện hành động phù hợp khác.
            return new List<Vector2Int>();
        }


        List<Vector2Int> lastMoveablePoints = new List<Vector2Int>(PreMoveablePoints);
        Vector2Int kingPosition;
        Vector2Int enemyKingPosition;
        foreach (var moveablePoint in PreMoveablePoints)
        {
            cloneboard = CloneBoard(board);

            ChangeAndTestPosition(cloneboard, position.x, position.y, moveablePoint.x, moveablePoint.y);
            kingPosition = CheckChessPosition(cloneboard, color, "Soai1");
           
            if (color == "Red")
            {

                List<Vector2Int> AllBlueMoveablePoints;
                AllBlueMoveablePoints = GetAllBlueMoveablePoint(cloneboard);
                enemyKingPosition = CheckChessPosition(cloneboard, "Blue", "Soai1");
                if (AllBlueMoveablePoints.Contains(kingPosition) || CheckKingOpposite(kingPosition, enemyKingPosition, cloneboard, color))
                {
                    lastMoveablePoints.Remove(moveablePoint);
                }
            }
            else if (color == "Blue")
            {
                List<Vector2Int> AllRedMoveablePoints;
             
                AllRedMoveablePoints = GetAllRedMoveablePoint(cloneboard);
                enemyKingPosition = CheckChessPosition(cloneboard, "Red", "Soai1");
                if (AllRedMoveablePoints.Contains(kingPosition) || CheckKingOpposite(kingPosition, enemyKingPosition, cloneboard, color))
                {
                    lastMoveablePoints.Remove(moveablePoint);
                }
            }
        }
        return lastMoveablePoints;
    }


    public bool CheckKingOpposite(Vector2Int kingPosition, Vector2Int enemyKingPosition, string[][][] chessboard, string color)
    {
        if (kingPosition.x == enemyKingPosition.x)
        {
            if (color == "Red")
            {
                for (int i = kingPosition.y - 1; i > enemyKingPosition.y; i--)
                {
                    if (chessboard[kingPosition.x][i][0] != "")
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = kingPosition.y + 1; i < enemyKingPosition.y; i++)
                {
                    if (chessboard[kingPosition.x][i][0] != "")
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        else
        {
            return false;
        }
    }

    public string[][][] CloneBoard(string[][][] originalBoard)
    {
        string[][][] clone = new string[originalBoard.Length][][];

        for (int i = 0; i < originalBoard.Length; i++)
        {
            clone[i] = new string[originalBoard[i].Length][];
            for (int j = 0; j < originalBoard[i].Length; j++)
            {
                clone[i][j] = new string[originalBoard[i][j].Length];
                Array.Copy(originalBoard[i][j], clone[i][j], originalBoard[i][j].Length);
            }
        }
        return clone;
    }

    public void ChangeAndTestPosition(string[][][] chessboard, int start_x, int start_y, int end_x, int end_y)
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
        }
        chessboard[end_x][end_y] = chessboard[start_x][start_y];
        chessboard[start_x][start_y] = new string[] { "", "" };
    }

    public List<Vector2Int> GetAllBlueMoveablePoint(string[][][] Chessboard)
    {
        List<Vector2Int> AllBlueMoveablePoints = new List<Vector2Int>();
        foreach (var pieces in Board.BluechessPieces)
        {
            Vector2Int position = CheckChessPosition(Chessboard, pieces.Color, pieces.Name);
            if (position != new Vector2Int(-1, -1))
            {
                string type = pieces.Name.Substring(0, pieces.Name.Length - 1);
                List<Vector2Int> moveablePoints = FindMoveablePoints(position, pieces.Color, type, Chessboard);
                AllBlueMoveablePoints.AddRange(moveablePoints);
            }
        }
        return AllBlueMoveablePoints;
    }

    public List<Vector2Int> GetAllRedMoveablePoint(string[][][] Chessboard)
    {
        List<Vector2Int> AllRedMoveablePoints = new List<Vector2Int>();
        foreach (var pieces in Board.RedchessPieces)
        {
            Vector2Int position = CheckChessPosition(Chessboard, pieces.Color, pieces.Name);
            if (position != new Vector2Int(-1, -1))
            {
                string type = pieces.Name.Substring(0, pieces.Name.Length - 1);
                List<Vector2Int> moveablePoints = FindMoveablePoints(position, pieces.Color, type, Chessboard);
                AllRedMoveablePoints.AddRange(moveablePoints);
            }
        }
        return AllRedMoveablePoints;
    }

    public void ChangePosition(string[][][] chessboard,Move move)
    {
        if (chessboard[move.StartX][move.StartY][0] == "")
        {
            
            return;
        }
        // Di chuyển quân cờ từ vị trí bắt đầu đến vị trí kết thúc
        chessboard[move.EndX][move.EndY] = chessboard[move.StartX][move.StartY];

        // Xóa quân cờ tại vị trí khởi đầu
        chessboard[move.StartX][move.StartY] = new string[] { "", "" };
    }

    public static void PrintBoard(string[][][] board)
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


    public static  GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None)
            {
                if (obj.name == name)
                {
                    return obj.gameObject;
                }
            }
        }
        return null;
    }
}
