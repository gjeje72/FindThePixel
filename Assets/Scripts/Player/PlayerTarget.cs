using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTarget : MonoBehaviour
{
    public GameObject WinMenu;

    // Method used when player find the pixel.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
            WinMenu.SetActive(true);
    }
}
