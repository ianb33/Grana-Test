using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    public Vector2 originalCellSize = new Vector2(100.0f, 50.0f); // The original cell size
    public Vector2 originalSpacing = new Vector2(100.0f, 50.0f);

    private GridLayoutGroup gridLayoutGroup;

    private Vector2 originalScreenSize; // The original screen size

    private void Awake()
    {
        // Get the GridLayoutGroup component
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        // Set the original screen size
        originalScreenSize = new Vector2(1440, 3088);
    }

    private void Start()
    {
        // Set the initial cell size based on the original cell size and the current screen size
        UpdateCellSize();
    }

    private void UpdateCellSize()
    {
        // Calculate the scale factor based on the ratio of the original screen size to the current screen size
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        Vector2 scaleFactor = originalScreenSize / currentScreenSize;

        // Calculate the new cell size by multiplying the original cell size by the inverse square root of the scale factor
        Vector2 newCellSize = originalCellSize / Mathf.Sqrt(Mathf.Max(scaleFactor.x, scaleFactor.y));

        Vector2 newSpacing = new Vector2(currentScreenSize.y / 23f, currentScreenSize.y / 23f);

        // Set the new cell size to the GridLayoutGroup
        gridLayoutGroup.cellSize = newCellSize;
        gridLayoutGroup.spacing = newSpacing;
    }

    private void Update()
    {
        // Check if the screen size has changed and update the cell size if needed
        if (Screen.width != originalScreenSize.x || Screen.height != originalScreenSize.y)
        {
            UpdateCellSize();
        }
    }
}
