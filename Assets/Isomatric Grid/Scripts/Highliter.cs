using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highliter : MonoBehaviour
{
    public Color EmptytileColor;
    public Color FilledTileColor;
    public Color AreaNotAvailableColor;
    [SerializeField] private SpriteRenderer SRenderer;
    public void SetColor(bool isHighlite, bool isAreaAvailable)
    {
        if (isAreaAvailable)
        {
            if (isHighlite)
            {
                SRenderer.color = EmptytileColor;
            }
            else
            {
                SRenderer.color = FilledTileColor;
            }
        }
        else
        {
            SRenderer.color = AreaNotAvailableColor;
        }
    }
}
