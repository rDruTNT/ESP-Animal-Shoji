using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    List<Pointer> extends = new List<Pointer>();
    private int _x, _y;

    public int x
    {
        get => _x;
    }

    public int y
    {
        get => _y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void point(int x, int y)
    {
        this._x = x;
        this._y = y;
        transform.position = new Vector3(-4.915f + y * 3.27f, 3.275f + x * -3.27f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public int getX()
    {
        return _x;
    }

    public int getY()
    {
        return _y;
    }

    public void extend(List<Vector2Int> pos)
    {
        foreach (Vector2Int loc in pos)
        {
            if (Main.isOutSideOfBoard(this._x + loc.x, this._y + loc.y)) continue;
            var pt = Instantiate(this, this.transform);
            pt.point(_x + loc.x, _y + loc.y);
            extends.Add(pt);
        }
    }

    public void extend(HashSet<int[]> pos)
    {
        foreach (int[] loc in pos)
        {
            if (Main.isOutSideOfBoard(this._x + loc[0], this._y + loc[1])) continue;
            var pt = Instantiate(this);
            extends.Add(pt);
            pt.point(this._x + loc[0], this._y + loc[1]);
        }
    }

    public void clearExtend()
    {
        foreach (Pointer p in extends) 
        {
            p.enabled = false;
            Destroy(p.gameObject);
        }
        extends.Clear();
    }
}
