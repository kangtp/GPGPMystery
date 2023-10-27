using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_Info : MonoBehaviour
{
    // Start is called before the first frame update
    private int x_,y_;
    public void Set(int x, int y)
    {
        x_ = x;
        y_ = y;
    }

    public int get_X()
    {
        return x_;
    }

    public int get_Y()
    {
        return y_;
    }
}
