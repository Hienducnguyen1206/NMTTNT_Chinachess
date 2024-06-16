﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckmateSystem : MonoBehaviour
{
  
    private string[][][] board;

    private List<(string Color, string Name)> BluechessPieces;
    private List<(string Color, string Name)> RedchessPieces;

    private List<Vector2Int> AllBlueMoveablePoints;
    private List<Vector2Int> AllRedMoveablePoints;

    void Start()
    {
        board = Board.Instance.GetBoard();
        
        BluechessPieces = Board.Instance.GetBlueChessList();
        RedchessPieces = Board.Instance.GetRedChessList();

        AllBlueMoveablePoints = new List<Vector2Int>();
        AllRedMoveablePoints = new List<Vector2Int>();
    
    }

    private  Vector2Int CheckChessPosition(string[][][] chessboard,string color, string piece)
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

    //Hàm lấy toàn bộ nước đi của quân Blue
    List<Vector2Int> GetAllBlueMoveablePoint(string[][][] Chessboard)
    {
        foreach (var pieces in BluechessPieces)
        {
            Vector2Int position = CheckChessPosition(Chessboard,pieces.Color, pieces.Name);
            if (position != new Vector2Int(-1, -1))
            {
                string type = pieces.Name.Substring(0, pieces.Name.Length - 1);
                List<Vector2Int> moveablePoints = FindMoveablePoints(position, pieces.Color, type, Chessboard);
                AllBlueMoveablePoints.AddRange(moveablePoints);
            }           
        }
        return AllBlueMoveablePoints;
    }

    // Hàm lấy tòan bộ nước đi của quân Red
    List<Vector2Int> GetAllRedMoveablePoint(string[][][] Chessboard)
    {
        foreach (var pieces in RedchessPieces)
        {
            Vector2Int position = CheckChessPosition(Chessboard,pieces.Color, pieces.Name);
            if (position != new Vector2Int(-1, -1)) 
            {
                string type = pieces.Name.Substring(0, pieces.Name.Length - 1);
                List<Vector2Int> moveablePoints = FindMoveablePoints(position, pieces.Color, type, Chessboard);
                AllRedMoveablePoints.AddRange(moveablePoints); 
            }
        }
        return AllRedMoveablePoints;
    }
    private void CheckRedMate()
    {
        Vector2Int NewRedKingPosition;
        
        string[][][] cloneboard;
        AllBlueMoveablePoints.Clear();
        cloneboard = CloneBoard(board);
       
        Vector2Int RedKingPosition = CheckChessPosition(cloneboard,"Red", "Soai1");
        List<Vector2Int> AllNewBlueMoveablePoint = new List<Vector2Int>();
        AllBlueMoveablePoints = GetAllBlueMoveablePoint(cloneboard);
        Vector2Int BlueKingPosition = CheckChessPosition(cloneboard, "Blue", "Soai1");
        if (AllBlueMoveablePoints.Contains(RedKingPosition))
        {
            bool RedKingCanProtected = false;
            cloneboard = CloneBoard(board);
            foreach (var piece in RedchessPieces)
            {            
                Vector2Int position = CheckChessPosition(cloneboard,piece.Color, piece.Name);
                if (position[0] != -1)
                {
                    string type = piece.Name.Substring(0, piece.Name.Length - 1);
                    List<Vector2Int> moveablepoints = FindMoveablePoints(position, piece.Color, type, board);
                    foreach (var moveablepoint in moveablepoints)
                    {   
                        
                        ChangeAndTestPosition(cloneboard,position.x,position.y,moveablepoint.x,moveablepoint.y);
                        AllNewBlueMoveablePoint.Clear();
                        AllNewBlueMoveablePoint = GetAllBlueMoveablePoint(cloneboard);
                        NewRedKingPosition = CheckChessPosition(cloneboard, "Red", "Soai1");                       
                        if(!AllNewBlueMoveablePoint.Contains(NewRedKingPosition) &&!CheckKingOpposite(NewRedKingPosition,BlueKingPosition,cloneboard,piece.Color)) {
                            RedKingCanProtected = true;
                            break;
                        }
                        cloneboard = CloneBoard(board);                     
                    }
                    cloneboard = CloneBoard(board);
                }
                if (RedKingCanProtected)
                {
                    break;
                }

            }
            if (RedKingCanProtected)
            {
                Debug.Log("Red King Can Protected");
            }
            else
            {
                Debug.Log("Red king is checkmate");
            }
        }
    }
    private void CheckBlueMate()
    {
        Vector2Int NewBlueKingPosition;
        string[][][] cloneboard;
        AllRedMoveablePoints.Clear();
        cloneboard = CloneBoard(board);
        Vector2Int BlueKingPosition = CheckChessPosition(cloneboard, "Blue", "Soai1");
        List<Vector2Int> AllNewRedMoveablePoint = new List<Vector2Int>();
        AllRedMoveablePoints = GetAllRedMoveablePoint(cloneboard);
        Vector2Int RedKingPosition = CheckChessPosition(cloneboard, "Red", "Soai1");
        if (AllRedMoveablePoints.Contains(BlueKingPosition))
        {
            bool BlueKingCanProtected = false;
            cloneboard = CloneBoard(board);
            foreach (var piece in BluechessPieces)
            {
                Vector2Int position = CheckChessPosition(cloneboard, piece.Color, piece.Name);
                if (position[0] != -1)
                {
                    string type = piece.Name.Substring(0, piece.Name.Length - 1);
                    List<Vector2Int> moveablepoints = FindMoveablePoints(position, piece.Color, type, board);
                    foreach (var moveablepoint in moveablepoints)
                    {
                        //Debug.Log(piece.Name + moveablepoint.x+" " + moveablepoint.y);
                        ChangeAndTestPosition(cloneboard, position.x, position.y, moveablepoint.x, moveablepoint.y);
                        AllNewRedMoveablePoint.Clear();
                        AllNewRedMoveablePoint = GetAllRedMoveablePoint(cloneboard);
                        NewBlueKingPosition = CheckChessPosition(cloneboard, "Blue", "Soai1");
                        if (!AllNewRedMoveablePoint.Contains(NewBlueKingPosition) && !CheckKingOpposite(NewBlueKingPosition,RedKingPosition,cloneboard,piece.Color))
                        {
                            BlueKingCanProtected = true;
                            break;
                        }
                        cloneboard = CloneBoard(board);
                    }
                    cloneboard = CloneBoard(board);
                }
                if (BlueKingCanProtected)
                {
                    break;
                }

            }
            if (BlueKingCanProtected)
            {
                Debug.Log("Blue King Can Protected");
            }
            else
            {
                Debug.Log("Blue king is checkmate");
            }
        }
    }
    private string[][][] CloneBoard(string[][][] originalBoard)
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
    public void PrintTestBoard(string[][][] Chessboard)
    {
        Debug.Log("Chess Board:");

        for (int y = 9; y >= 0; y--)
        {
            string rowString = "";
            for (int x = 0; x < 9; x++)
            {
                if (Chessboard[x][y][0] != "")
                {
                    rowString += "[" + Chessboard[x][y][0] + "," + Chessboard[x][y][1] + "]";
                }
                else
                {
                    rowString += "[  ]";
                }
            }
            Debug.Log(rowString);
        }
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
    void Update()
    {
        CheckRedMate();
        CheckBlueMate();
    }

}
