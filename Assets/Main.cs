using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Main : MonoBehaviour
{
    [SerializeField]
    private Chess chess;
    [SerializeField]
    Sprite chicken, falcon, giraffe, elephant, lion;
    
    public static HashSet<Chess> board = new HashSet<Chess>(), 
        leftHands = new HashSet<Chess>(), 
        rightHands = new HashSet<Chess>();
    
 

    public static Turn turn = Turn.Left;
    public enum Turn
    {
        Left,
        Right
    }
    // Start is called before the first frame update
    void Start()
    {
        
        chessInfoStartUp();
        resetBoard();
    }

    public void resetBoard()
    {
        Chess.CreateChess(chess, 0, 0, Chess.Type.Elephant, Turn.Left);
        Chess.CreateChess(chess, 1, 0, Chess.Type.Lion, Turn.Left);
        Chess.CreateChess(chess, 2, 0, Chess.Type.Giraffe, Turn.Left);
        Chess.CreateChess(chess, 1, 1, Chess.Type.Chicken, Turn.Left);

        Chess.CreateChess(chess, 2, 3, Chess.Type.Elephant, Turn.Right);
        Chess.CreateChess(chess, 1, 3, Chess.Type.Lion, Turn.Right);
        Chess.CreateChess(chess, 0, 3, Chess.Type.Giraffe, Turn.Right);
        Chess.CreateChess(chess, 1, 2, Chess.Type.Chicken, Turn.Right);

    }

    public void chessInfoStartUp()
    {
        ChessInfo c = new ChessInfo(Chess.Type.Chicken, chicken);
        c.ways.Add(new int[2] { 0, 1 });

        ChessInfo f = new ChessInfo(Chess.Type.Falcon, falcon);
        f.ways.Add(new int[2] { 0, 1 });
        f.ways.Add(new int[2] { 0, -1 });
        f.ways.Add(new int[2] { -1, 1 });
        f.ways.Add(new int[2] { 1, 1 });
        f.ways.Add(new int[2] { -1, 0 });
        f.ways.Add(new int[2] { 1, 0 });

        ChessInfo e = new ChessInfo(Chess.Type.Elephant, elephant);
        e.ways.Add(new int[2] { 1, 1 });
        e.ways.Add(new int[2] { -1, -1 });
        e.ways.Add(new int[2] { 1, -1 });
        e.ways.Add(new int[2] { -1, 1 });
        ChessInfo g = new ChessInfo(Chess.Type.Giraffe, giraffe);
        g.ways.Add(new int[2] { 0, 1 });
        g.ways.Add(new int[2] { 0, -1 });
        g.ways.Add(new int[2] { 1, 0 });
        g.ways.Add(new int[2] { -1, 0 });
        ChessInfo l = new ChessInfo(Chess.Type.Lion, lion);
        l.ways.Add(new int[2] { 0, 1 });
        l.ways.Add(new int[2] { 0, -1 });
        l.ways.Add(new int[2] { 1, 1 });
        l.ways.Add(new int[2] { 1, -1 });
        l.ways.Add(new int[2] { 1, 0 });
        l.ways.Add(new int[2] { -1, 0 });
        l.ways.Add(new int[2] { -1, -1 });
        l.ways.Add(new int[2] { -1, 1 });
    }
    // Update is called once per frame
    void Update()
    {
        if (turn == Turn.Right) turn = Turn.Left;
        else turn = Turn.Right;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int mouseX = (int)Mathf.Floor((mousePos.y - 5f) / -3.3f);
        int mouseY = (int)Mathf.Floor((mousePos.x + 6.5f) / 3.25f);

    }

    public static bool isOutSideOfBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >2 ||y>3) return true;
        return false;
    }

    public static bool hasChess(int x, int y)
    {
        foreach (Chess c in board)
        {
            if (c.getX() == x && c.getY() == y)
                return true;
        }
        return false;
    }

    public static Chess getChess(int x, int y)
    {
        foreach (Chess c in board)
        {
            if (c.getX() == x && c.getY() == y)
                return c;
        }
        return null;
    }

    

}
