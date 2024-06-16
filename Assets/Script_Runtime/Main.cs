using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    Function function;

    [SerializeField] int limitedCount;

    [SerializeField] Vector2Int start;

    [SerializeField] Vector2Int end;

    List<Vector2Int> hinders = new List<Vector2Int>();
    RectCell result;

    void Awake() {
        function = new Function();
        result = new RectCell();

    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int pos = new Vector2Int((int)mousePos.x, (int)mousePos.y);
            start = pos;
        } else if (Input.GetMouseButtonDown(1)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int pos = new Vector2Int((int)mousePos.x, (int)mousePos.y);
            end = pos;
        } else if (Input.GetMouseButton(2)) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int pos = new Vector2Int((int)mousePos.x, (int)mousePos.y);
            hinders.Add(pos);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            int res;
            function.Astar(start, end, hinders, limitedCount, out res, out result);
        }

    }

    void OnDrawGizmos() {
        for (int i = 0; i < hinders.Count; i++) {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(hinders[i].x, hinders[i].y, 0), new Vector3(1, 1, 1));
        }
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(start.x, start.y, 0), new Vector3(1, 1, 1));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(new Vector3(end.x, end.y, 0), new Vector3(1, 1, 1));

        if (result != null) {
            RectCell temp = result;
            while (temp != null) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(new Vector3(temp.position.x, temp.position.y, 0), new Vector3(1, 1, 1));
                temp = temp.parent;
            }
        }
    }
}
