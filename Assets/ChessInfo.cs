using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessInfo
{
    private static HashSet<ChessInfo> chesses = new HashSet<ChessInfo>();

    Chess.Type type;
    public Sprite texture;
    public HashSet<int[]> ways = new HashSet<int[]>();

    public ChessInfo(Chess.Type type, Sprite texture)
    {
        
        this.texture = texture;
        this.type = type;
        chesses.Add(this);
    }

    public static ChessInfo getInfo(Chess.Type type)
    {

        foreach (ChessInfo i in chesses)
        {
            if (i.type == type)
                return i;
        }
        return null;
    }
}
