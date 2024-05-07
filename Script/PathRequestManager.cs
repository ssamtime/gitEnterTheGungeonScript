using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>(); // ���� ������Ʈ���� ��ã�� ��û�� ������ ���� ť�� ����
    PathRequest currentPathRequest; //���� ó���� ��ã�� ��û

    static PathRequestManager instance;
    PathFinding pathFinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    //������Ʈ���� ��û�ϴ� �Լ�
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    //ť������� ��ã�� ��û�� ������ PathFinding �˰��� �����Լ�
    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    //��ã�Ⱑ �Ϸ�� ��û�� ó���ϰ� ������Ʈ���� �̵����۸�� �ݹ��Լ��� �����ϴ� �Լ�
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath=false;
        TryProcessNext();
    }
}

// ������Ʈ���� ��ã�� ��û������ ���� ����ü
struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public UnityAction<Vector3[], bool> callback;

    public PathRequest(Vector3 nStart, Vector3 nEnd, UnityAction<Vector3[], bool> nCallback)
    {
        pathStart = nStart;
        pathEnd = nEnd;
        callback = nCallback;
    }
}