using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.ToUpper() == "PLAYER")
        {
            SceneManager.LoadScene("EndingCredit");
        }
    }
}
