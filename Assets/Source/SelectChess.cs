using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectChess : MonoBehaviour
{
    public Material highlightMaterial;
    public Material selectionMaterial;
    private  Material originalMaterialHighlight;
    private static Material originalMaterialSelection;


    private Transform highlight;
    public static Transform selectedChess;

    private RaycastHit raycastHit;


    public static Transform GetSelection()
    {
        return selectedChess;
    }

    private void HighlightWhenHoverChess()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            string input = highlight.gameObject.name;
            string[] parts = input.Split('_');
            Enum.TryParse(parts[0], out ChessColor color);
            
            
                if (highlight.CompareTag("Selectable") && highlight != selectedChess && (color == TurnSystem.TurnColor) && !SelectWaypoint.isMoving)
                {
                    if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                    {
                        originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                        highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                    }
                }
                else
                {
                    highlight = null;
                }
            

           
           
        }
    }
    
    private void HighlightWhenSelectChess()
    {
        // Selection
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (highlight)
            {
                if (selectedChess != null)
                {
                    selectedChess.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selectedChess = raycastHit.transform;
                if (selectedChess.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selectedChess.GetComponent<MeshRenderer>().material = selectionMaterial;
                }
                highlight = null;
            }
            else
            {
                if (selectedChess)
                {
                    selectedChess.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selectedChess = null;
                }
            }
        }
    }

    public static void ReturnOrigin()
    {
        if (selectedChess)
        {
            selectedChess.GetComponent<MeshRenderer>().material = originalMaterialSelection;
            selectedChess = null;
        }
    }
    

    void Update()
    {
        HighlightWhenHoverChess();
        HighlightWhenSelectChess(); 
        
    }

}