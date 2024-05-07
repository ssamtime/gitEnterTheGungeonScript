using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D customCursor; // Inspector â���� �Ҵ��� ����� ���� Ŀ�� �̹���

    void Start()
    {        
        // Ŀ�� �̹����� ������ �̹����� ����
        Cursor.SetCursor(customCursor,new Vector2(7f,6f), CursorMode.Auto); // ���콺 Ŀ�� hotspot��ġ ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
