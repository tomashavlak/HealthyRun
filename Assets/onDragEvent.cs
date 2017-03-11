using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onDragEvent : MonoBehaviour {

    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        /*EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);*/
        EventTrigger.Entry startDragging = new EventTrigger.Entry();
        startDragging.eventID = EventTriggerType.BeginDrag;
        startDragging.callback.AddListener((data) => { StartStopDragging(); });
        trigger.triggers.Add(startDragging);

        EventTrigger.Entry stopDragging = new EventTrigger.Entry();
        stopDragging.eventID = EventTriggerType.EndDrag;
        stopDragging.callback.AddListener((data) => { StartStopDragging(); });
        trigger.triggers.Add(stopDragging);
    }

    public void StartStopDragging()
    {
        Scroller.dragging = !Scroller.dragging;
        Debug.Log("Dragging START/STOP");
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        Debug.Log("OnPointerDownDelegate called.");
    }
}
