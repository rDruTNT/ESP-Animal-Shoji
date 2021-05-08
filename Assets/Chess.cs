using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
   
    [SerializeField]
    GameObject Pointer;
    [SerializeField]
    SpriteRenderer render;

    private Main.Turn _owner;
    public Main.Turn owner
    {
        get => _owner;
        set
        {
            transform.rotation = Quaternion.Euler(0, 0, (value == Main.Turn.Left) ? -90 : 90);
            _owner = value;
        }
    }

    private Type _type;
    public Type type
    {
        get => _type;
        set
        {
            render.sprite = ChessInfo.getInfo(value).texture;
            _type = value;
        }
    }
    private int x, y;
    
    private bool _onBoard = true;
    public bool isOnBoard
    {
        get => _onBoard;
    }
    public static Chess CreateChess(Chess perfab, int x, int y, Type type, Main.Turn turn)
    {
        Chess c = Object.Instantiate(perfab, new Vector3(), new Quaternion());
        c.startup(turn, type);
        c.setPosition(x, y);
        Main.board.Add(c);
        return c;
    }
    public enum Type
    {
        Chicken,
        Falcon,
        Giraffe,
        Elephant,
        Lion
    }
    void Start()
    {

    }
    // Start is called before the first frame update
   // void Start(Main.Turn owner, Type type)
    //{
      //  startup(owner, type);
    //}

    private void startup(Main.Turn owner, Type type)
    {
        this.owner = owner;
        this.type = type;
        ChessInfo info = ChessInfo.getInfo(type);

        foreach (int[] l in info.ways)
        {
            GameObject p = Instantiate(Pointer, new Vector3(l[0] * 0.7f, l[1] * -0.7f, 0), new Quaternion(0,0,0,0));
            p.transform.SetParent(this.transform);
            p.transform.localPosition = new Vector3(l[0] * 2.3f, l[1] * 2.3f, 0);
            //p.transform.localPosition = new Vector3(l[1] * 0.7f, l[0] * -0.7f, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Vector3 calPos(int x, int y)
    {
        return new Vector3(-4.915f + y * 3.27f, 3.275f + x * -3.27f, 0);
    }
    public void setPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = calPos(x,y);
        
    }

    public void setX(int x)
    {
        this.x = x;
        transform.position = new Vector3(-4.915f + y * 3.27f, 3.275f + x * -3.27f, 0);
    }

    public void setY(int y)
    {
        this.y = y;
        transform.position = new Vector3(-4.915f + y * 3.27f, 3.275f + x * -3.27f, 0);
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public bool reachable(int x, int y)
    {
        foreach (int[] way in getWays()) 
            if (x - this.x == way[0] && y - this.y == way[1])
                return true;
        return false;
    }

    public void move(int x, int y)
    {
        //reachable require, here uncheck
        Chess bc = Main.getChess(x, y);
        if(bc !=null)
            eat(bc);
        setPosition(x, y);


    }

    public void eat(Chess c)
    {
        //unmove
        Main.board.Remove(c);
        HashSet<Chess> pocket;
        if(owner == Main.Turn.Left)
        {
            pocket = Main.leftHands;
            
        } else
        {
            pocket = Main.rightHands;
        }
        pocket.Add(c);


    }

    public void place(int x,int y)
    {

    }
    public HashSet<int[]> getWays()
    {
        return ChessInfo.getInfo(type).ways;
    }

    
}
