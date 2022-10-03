using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Transform playerCam;

    public LayerMask pickup;
    public Image button;
    public float pickupDistance;
    private Transform _selection;
    public bool attempt;
    public Image But;

    void Start() {attempt = true;}

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attempt);
        if (_selection != null)
        {
            attempt = true;
            var selectionRenderer = _selection.GetComponent<Renderer>();
            //selectionRenderer.material = defaultMaterial;
            // But.GetComponent<EButton>().Away();
            _selection = null;
        }

        RaycastHit hit;
        if(attempt && Physics.Raycast(playerCam.position, playerCam.forward, out hit, pickupDistance, pickup))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if(attempt && (selectionRenderer != null))
            {
                // But.GetComponent<EButton>().Near("Grab");
                //selectionRenderer.material = highlightMaterial;
            }
            _selection = selection;
            attempt = false;
        }
    }
}
