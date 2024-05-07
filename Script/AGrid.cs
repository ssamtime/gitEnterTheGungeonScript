using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public List<ANode> path;
    ANode[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;


    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2; // ������ ���������� ������ ����
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //�׸��� ����
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //�׸��� ����

        CreateGrid();
    }

    void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        Vector3 worldPoint;

        grid = new ANode[gridSizeX, gridSizeY];

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapBox(worldPoint, new Vector2(nodeDiameter, nodeDiameter), 90, unwalkableMask));

                grid[x, y] = new ANode(walkable, worldPoint, x, y);
            }
        }
    }

    // ����� �ֺ� ���(8���)�� ã�� �Լ�
    public List<ANode> GetNeighbours(ANode node)
    {
        List<ANode> neighbours = new List<ANode>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; //�ڱ��ڽ��� ��� ��ŵ

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                // X,Y�� ���� Grid�����ȿ� ���� ���
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }

    // ����Ƽ�� WorldPosition���� ���� �׸������ ��带 ã�� �Լ�
    public ANode GetNodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null)
        {
            foreach(ANode n in grid)
            {
                Gizmos.color = (n.isWalkAble) ? Color.white : Color.red;

                //Ž���� path�� ���ǥ��
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
