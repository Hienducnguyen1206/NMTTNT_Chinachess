using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class GameState : MonoBehaviour
{
    private string[][][] board;
    private MinimaxAI minimaxAI;
   

    private void OnEnable()
    {
        SelectWaypoint.PlayerMove += ApplyPlayerMove;
    }

    private void OnDisable()
    {
        SelectWaypoint.PlayerMove -= ApplyPlayerMove;
    }

    private void Start()
    {
        board = Board.Instance.GetBoard();
        minimaxAI = new MinimaxAI(2, true); 
    }

    public void ApplyPlayerMove()
    {
      
            StartCoroutine(ApplyAIMove());
        
    }

    private IEnumerator ApplyAIMove()
    {
        Debug.Log("Apply AI");

        // Đợi một khoảng thời gian ngắn trước khi AI tính toán và thực hiện nước đi
        yield return new WaitForSeconds(0.5f);

        // Lấy nước đi tối ưu từ MinimaxAI với Alpha-Beta pruning
           Move bestMove = minimaxAI.GetBestMove(board);
           Debug.Log(bestMove.StartX+"-" + bestMove.StartY+":"+bestMove.EndX + "-" + bestMove.EndY);
        // Kiểm tra nếu bestMove không phải là null
           if (bestMove != null)
           {

            // Thực hiện nước đi trên giao diện

            string ChesstoMove = Board.Instance.getChessAtPosition(bestMove.StartX, bestMove.StartY);
           
        
            GameObject chess = GameObject.Find(ChesstoMove);
            string newposition = "Point_" + bestMove.EndX + "_" + bestMove.EndY;
            GameObject newPositiontomove = GameServices.FindInActiveObjectByName(newposition);
          
            if (newposition != null)
            {
                chess.transform.parent.position = newPositiontomove.transform.position;
            }
           

            //Thực hiện nước đi của AI trên bàn cờ ảo
            Board.Instance.ChangePosition(board, bestMove.StartX, bestMove.StartY, bestMove.EndX, bestMove.EndY);
            TurnSystem.changeTurn();              
        }
        
        Board.Instance.PrintBoard();
 // Chuyển lượt sang người chơi
    }
}
