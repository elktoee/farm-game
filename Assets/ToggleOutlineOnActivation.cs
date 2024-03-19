using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(Outline))]
public class ToggleOutlineOnActivation : MonoBehaviour
{
    private Toggle toggle;
    private Outline outline;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        outline = GetComponent<Outline>();

        // Add listener to the toggle's onValueChanged event
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Enable or disable the outline based on the toggle state
        outline.enabled = isOn;

        // If outline is enabled, disable outlines on other objects
        if (isOn)
        {
            DisableOutlinesOnOtherObjects();
        }
    }

    private void DisableOutlinesOnOtherObjects()
    {
        // Get all other objects in the same parent panel
        ToggleOutlineOnActivation[] otherObjects = transform.parent.GetComponentsInChildren<ToggleOutlineOnActivation>(true);

        // Disable outlines on all other objects
        foreach (ToggleOutlineOnActivation obj in otherObjects)
        {
            if (obj != this)
            {
                obj.outline.enabled = false;
                obj.toggle.isOn = false; // Ensure toggle is off for other objects
            }
        }
    }
}
