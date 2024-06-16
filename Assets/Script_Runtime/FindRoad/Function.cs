using System;
using System.Collections.Generic;
using UnityEngine;



public class Function {
    public HashSet<RectCell> openSet = new HashSet<RectCell>();

    public HashSet<RectCell> closeSet = new HashSet<RectCell>();

    public HashSet<Vector2Int> closeSetKey = new HashSet<Vector2Int>();

    Vector2Int[] neighbors = new Vector2Int[] {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };

    public int Astar(Vector2Int start, Vector2Int end, List<Vector2Int> hinders, int limitedCount, out int res, /*最后一个位置*/ out RectCell result) {
        // -1 死路结束
        // 1 走到了
        // -2 超出边界


        // 找到次数
        int resultCount = 0;
        res = 0;

        if (start == end) {
            res = 1;
            result = new RectCell();
            return resultCount;
        }

        openSet.Clear();
        closeSet.Clear();
        closeSetKey.Clear();

        for (int i = 0; i < hinders.Count; i++) {
            Vector2Int hind = hinders[i];
            closeSetKey.Add(hind);
        }

        RectCell startCell = new RectCell();
        startCell.Init(null, start, 0, 0);
        openSet.Add(startCell);

        RectCell endCell = new RectCell();

        while (res == 0) {

            res = ProcessCell(end, ref endCell);
            resultCount += 1;
            if (resultCount >= limitedCount) {
                res = -2;
                break;
            }
        }

        result = endCell;
        if (res == -1) {
            Debug.Log("死路结束");
        } else if (res == -2) {
            Debug.Log("超出边界");
        }



        return resultCount;
    }

    public int ProcessCell(Vector2Int end, ref RectCell result) {
        // 1 走到了
        if (openSet.Count == 0) {
            return -1;
        }

        RectCell cur = FindMinFCostCell();

        openSet.Remove(cur);
        closeSet.Add(cur);
        closeSetKey.Add(cur.position);

        for (int i = 0; i < 4; i++) {
            Vector2Int neighborPos = cur.position + neighbors[i];

            if (neighborPos == end) {
                Debug.Log("找到了");
                result = cur;
                return 1;
            }

            if (closeSetKey.Contains(neighborPos)) {
                continue;
            } else {
                RectCell tem = new RectCell();
                tem.parent = cur;
                tem.position = neighborPos;
                tem.hCost = 10;
                tem.gCost = H_Manhattan(neighborPos, end);
                tem.fCost = tem.hCost + tem.gCost;
                openSet.Add(tem);
            }

        }
        return 0;
    }

    public RectCell FindMinFCostCell() {
        RectCell minCell = null;
        foreach (var cell in openSet) {
            if (minCell == null || cell.fCost < minCell.fCost) {
                minCell = cell;
            }
        }

        return minCell;

    }

    // g值的算法
    float H_Manhattan(Vector2Int cur, Vector2Int end) {
        return 10 * (Mathf.Abs(cur.x - end.x) + Mathf.Abs(cur.y - end.y));
    }


}