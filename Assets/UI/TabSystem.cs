using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [System.Serializable]
    public class Tab {
        public Button tabButton;
        public GameObject tabView;
    }

    [SerializeField] List<Tab> tabs;
    Tab defaultTab;

    void Awake() {
        foreach(Tab tab in tabs) {
            tab.tabButton.onClick.AddListener(delegate {OpenTab(tab);});
        }
        defaultTab = tabs[0];
        OpenTab(defaultTab);
    }


    public void OpenTab(Tab activeTab) {
        foreach(Tab tab in tabs) {
            tab.tabView.SetActive(false);
        }
        activeTab.tabView.SetActive(true);
    }
}
