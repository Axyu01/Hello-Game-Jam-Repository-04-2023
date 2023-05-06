using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNote : InteractableObject
{
    [SerializeField]
    Canvas showedNoteCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if(showedNoteCanvas!=null)
            showedNoteCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            showedNoteCanvas.gameObject.SetActive(false);
        }
    }
    public override void onInteraction()
    {
        if (showedNoteCanvas != null)
        {
            showedNoteCanvas.gameObject.SetActive(true);
        }
    }
}
