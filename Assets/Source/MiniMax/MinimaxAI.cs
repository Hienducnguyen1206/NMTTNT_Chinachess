using System;
using System.Collections.Generic;
using UnityEngine;

public class MinimaxAI
{
    private int maxDepth; // Độ sâu tối đa của thuật toán Minimax
    private bool isRedTurn = true;
    GameServices gameServices = new GameServices();

    public MinimaxAI(int depth, bool isRedTurn)
    {
        maxDepth = depth;
        this.isRedTurn = isRedTurn;
    }

    // Phương thức để lấy nước đi tối ưu từ trạng thái hiện tại của bàn cờ
    public Move GetBestMove(string[][][] board)
    {
        int bestValue = int.MinValue;
        Move bestMove = null;

        List<Move> legalMoves = GetLegalMoves(board, isRedTurn);
        foreach (var move in legalMoves)
        {
            string[][][] testboard = gameServices.CloneBoard(board);
            string[][][] newBoard = ApplyMove(testboard, move);
            int moveValue = Minimax(newBoard, maxDepth, int.MinValue, int.MaxValue, !isRedTurn);

            if (moveValue > bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
        }

        return bestMove;
    }

    // Thuật toán Minimax với cắt tỉa Alpha-Beta để đánh giá giá trị của các nước đi và lấy nước đi tối ưu
    private int Minimax(string[][][] board, int depth, int alpha, int beta, bool maximizingPlayer)
    {
        if (depth == 0 || IsGameOver(board))
        {
            return Evaluate(board);
        }

        if (maximizingPlayer)
        {
            int maxEval = int.MinValue;
            List<Move> legalMoves = GetLegalMoves(board, true); 
            foreach (var move in legalMoves)
            {
                string[][][] newBoard = ApplyMove(board, move);
                int eval = Minimax(newBoard, depth - 1, alpha, beta, false);
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha)
                    break; 
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            List<Move> legalMoves = GetLegalMoves(board, false); 
            foreach (var move in legalMoves)
            {
                string[][][] newBoard = ApplyMove(board, move);
                int eval = Minimax(newBoard, depth - 1, alpha, beta, true);
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha)
                    break; 
            }
            return minEval;
        }
    }

    // Phương thức để lấy danh sách các nước đi hợp lệ từ trạng thái hiện tại của bàn cờ
    private List<Move> GetLegalMoves(string[][][] board, bool isRedTurn)
    {
        List<Move> legalMoves = new List<Move>();

        if (isRedTurn)
        {
            legalMoves.AddRange(gameServices.GetAllRedMoveable(board));
        }
        else
        {
            legalMoves.AddRange(gameServices.GetAllBlueMoveable(board));
        }

        return legalMoves;
    }

    // Phương thức để áp dụng nước đi lên bàn cờ và trả về trạng thái mới của bàn cờ
    private string[][][] ApplyMove(string[][][] board, Move move)
    {
        gameServices.ChangePosition(board, move);
        return board;
    }

    // Phương thức để kiểm tra xem trò chơi đã kết thúc chưa
    private bool IsGameOver(string[][][] board)
    {
       
        return false;
    }

    // Phương thức để đánh giá giá trị của trạng thái hiện tại của bàn cờ
    private int Evaluate(string[][][] board)
    {
        int eval = 0;
        int cost = 0;

        for (int x = 0; x < board.Length; x++)
        {
            for (int y = 0; y < board[x].Length; y++)
            {
                if (board[x][y][0] == "")
                {
                    continue; // Bỏ qua các ô trống
                }
                else if (board[x][y][0] == "Red")
                {
                    switch (ChessCost.RemoveLastCharacter(board[x][y][1]))
                    {
                        case "Tot":
                            cost = ChessCost.GetTotCost(y, x);
                            Debug.Log(board[x][y][1]+ " " + cost);
                            break;
                        case "Xe":
                            cost = ChessCost.GetXeCost(y, x);
                            Debug.Log(board[x][y][1] + " " + cost);
                            break;
                        case "Phao":
                            cost = ChessCost.GetPhaoCost(y, x);
                            Debug.Log(board[x][y][1] + " " + cost);
                            break;
                        case "Ma":
                            cost = ChessCost.GetMaCost(y, x);
                            Debug.Log(board[x][y][1] + " " + cost);
                            break;
                        case "Tuong":
                            cost = ChessCost.GetTuongCost(y, x);
                            Debug.Log(board[x][y][1] + " " + cost);
                            break;
                        case "Si":
                            cost = ChessCost.GetSiCost(y, x);
                            Debug.Log(board[x][y][1] + " " + cost);
                            break;
                        case "Soai":
                            cost = ChessCost.GetSoaiCost(y, x);
                            Debug.Log(board[x][y][1] + " " + cost);
                            break;
                        default:
                            cost = 0; // Nếu không khớp với bất kỳ case nào
                            break;
                    }
                    eval += cost; // Cộng lượng giá vào biến eval
                }
            }
        }

        Debug.Log("Eval: " + eval); // In ra giá trị eval để debug
        return eval;
    }

}

// Lớp đại diện cho một nước đi
public class Move
{
    public int StartX { get; set; }
    public int StartY { get; set; }
    public int EndX { get; set; }
    public int EndY { get; set; }

    public Move(int startX, int startY, int endX, int endY)
    {
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
    }
}
