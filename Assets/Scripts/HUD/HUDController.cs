using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Yes why not
public class HUDController : MonoBehaviour
{
    private void ToggleByTimeline(bool state) {
        TransitionManager.instance.SetHUDVisibility(state);
    }

    private void OnEnable() {
        TimelineController.OnTimelineStatus += ToggleByTimeline;
    }

    private void OnDisable() {
        TimelineController.OnTimelineStatus -= ToggleByTimeline;
    }

    private void OnDestroy() {
        OnDisable();
    }

    public void ToggleCanvas(bool state) {
        TransitionManager.instance.SetHUDVisibility(state);
    }
}
