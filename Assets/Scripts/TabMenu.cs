using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour
{
    [SerializeField] private int pageIndex = 0;

    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private List<Toggle> tabs = new List<Toggle>();
    [SerializeField] private List<CanvasGroup> pages = new List<CanvasGroup>();

    public UnityEvent<int> onPageIndexChanged;

    private void Initialize()
    {
        toggleGroup = GetComponentInChildren<ToggleGroup>();

        tabs.Clear();
        pages.Clear();

        tabs.AddRange(GetComponentsInChildren<Toggle>());
        pages.AddRange(GetComponentsInChildren<CanvasGroup>());
    }

    private void Awake()
    {
        foreach (var toggle in tabs)
        {
            toggle.onValueChanged.AddListener(CheckForTab);
            toggle.group = toggleGroup;
        }
    }

    private void OnDestroy()
    {
        foreach (var toggle in tabs)
        {
            toggle.onValueChanged.RemoveListener(CheckForTab);
        }
    }

    private void CheckForTab(bool isOn)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (!tabs[i].isOn) continue;
            pageIndex = i;
        }
        OpenPage(pageIndex);
    }

    private void OpenPage(int index)
    {
        CheckIndexInRange(index);
        for (int i = 0; i < pages.Count; i++)
        {
            bool isActivePage = (i == pageIndex);
            pages[i].alpha = isActivePage ? 1.0f : 0.0f;
            pages[i].interactable = isActivePage;
            pages[i].blocksRaycasts = isActivePage;
        }
        if (Application.isPlaying)
        {
            onPageIndexChanged?.Invoke(pageIndex);
        }
    }

    private void CheckIndexInRange(int index)
    {
        if (tabs.Count == 0 || pages.Count == 0)
        {
            Debug.Log("No tabs or pages found");
            return;
        }
        pageIndex = Mathf.Clamp(index, 0, pages.Count - 1);
    }
}
