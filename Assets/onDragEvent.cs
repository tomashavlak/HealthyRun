using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onDragEvent : MonoBehaviour {

    // nastaví hlídání drag eventu
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry startDragging = new EventTrigger.Entry();
        startDragging.eventID = EventTriggerType.BeginDrag;
        startDragging.callback.AddListener((data) => { StartStopDragging(); });
        trigger.triggers.Add(startDragging);

        EventTrigger.Entry stopDragging = new EventTrigger.Entry();
        stopDragging.eventID = EventTriggerType.EndDrag;
        stopDragging.callback.AddListener((data) => { StartStopDragging(); });
        trigger.triggers.Add(stopDragging);
    }

    //metoda měnící status drag eventu
    public void StartStopDragging()
    {
        Scroller.dragging = !Scroller.dragging;
    }
}
