using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    public GameCanvasController canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.WinGame();
        }
    }
}
