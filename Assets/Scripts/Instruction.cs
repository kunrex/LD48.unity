using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;

    public void Enable()
    {
        foreach(Image image in images)
        {
            Color col = image.color;
            col.a = 0.5f;
            image.color = col;
        }

        foreach (Text text in texts)
        {
            Color col = text.color;
            col.a = 0.5f;
            text.color = col;
        }
    }

    public void Disable()
    {
        foreach (Image image in images)
        {
            Color col = image.color;
            col.a = 1f;
            image.color = col;
        }

        foreach (Text text in texts)
        {
            Color col = text.color;
            col.a = 1f;
            text.color = col;
        }
    }
}
