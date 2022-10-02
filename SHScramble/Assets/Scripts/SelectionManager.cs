using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Transform playerCam;

    public LayerMask pickup;
    public float pickupDistance;
    private Transform _selection;


    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        RaycastHit hit;
        if(Physics.Raycast(playerCam.position, playerCam.forward, out hit, pickupDistance, pickup))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if(selectionRenderer != null)
            {
                selectionRenderer.material = highlightMaterial;
            }
            _selection = selection;
        }
    }
}
