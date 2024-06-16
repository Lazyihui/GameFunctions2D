using System;
using System.Collections.Generic;
using UnityEngine;


public class RectCell{
    public float hCost;

    public float gCost;

    public float fCost;

    public Vector2Int position;


    public RectCell parent;


    public void Init(RectCell parent,Vector2Int position, float hCost, float gCost)
    {
        this.position = position;
        this.parent = parent;
        this.hCost = hCost;
        this.gCost = gCost;
        this.fCost = hCost + gCost;
    }

}