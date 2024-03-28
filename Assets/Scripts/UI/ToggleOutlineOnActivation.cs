using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Outline))]
public class ToggleOutlineOnActivation : MonoBehaviour
{
    private Button toggle;
    private Outline outline;

    void Start()
    {
        toggle = GetComponent<Button>();
        outline = GetComponent<Outline>();

        // Add listener to the toggle's onValueChanged event
        toggle.onClick.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged()
    {
        // Enable or disable the outline based on the toggle state
        outline.enabled = true;

        // If outline is enabled, disable outlines on other objects
        DisableOutlinesOnOtherObjects();
        
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
            }
        }
    }
}
