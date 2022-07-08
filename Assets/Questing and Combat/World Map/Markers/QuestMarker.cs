using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMarker : MonoBehaviour
{
    // Defines the state of the marker
    Quest.QuestState markerState;
    public QuestData linkedQuestData;
    public Quest linkedQuest;
    [SerializeField] GameObject closeLOD, farLOD, graphicsHolder;
    [SerializeField] Sprite chestIcon, swordsIcon;
    [SerializeField] Image statusIcon;
    [SerializeField] Material grayscaleMaterial;
    [SerializeField] TextMeshProUGUI questNameText;
    [SerializeField] Button questButton;

    public TextMeshProUGUI timer;

    void Start() {
        Image closeLODIcon = closeLOD.transform.Find("Image").GetComponent<Image>();
        Image farLODIcon = farLOD.transform.Find("Image").GetComponent<Image>();
        closeLODIcon.sprite = linkedQuestData.questIcon;
        farLODIcon.sprite = linkedQuestData.questIcon;
        questNameText.SetText(linkedQuestData.questName);
    }

    public void LoadCloseLOD() {
        farLOD.SetActive(false);
        closeLOD.SetActive(true);
    }

    public void LoadFarLOD() {
        closeLOD.SetActive(false);
        farLOD.SetActive(true);
    }

    public void OpenQuestPreviewWindow() {
        QuestPreviewWindow.instance.OpenWindow();
        QuestPreviewWindow.instance.SetQuestData(linkedQuestData);
        UnitFinderWindow.instance.confirmSelectionButton.onClick.RemoveAllListeners();
        SetUnitSelectionQuest();
    }

    public void SetUnitSelectionQuest() {
        if(linkedQuestData.GetType() != typeof (MaterialQuestData)) {
            UnitFinderWindow.instance.confirmSelectionButton.onClick.AddListener(delegate {
                List<Hero> heroParty = UnitFinderWindow.instance.GetSelectedHeroes();
                
                MoneyBag.instance.RemoveMoney(linkedQuestData.goldCost);
                if(heroParty != null) {
                    linkedQuest.StartQuest(heroParty);
                }
            });
        } else {
            Debug.Log("MaterialQuest");
            UnitFinderWindow.instance.confirmSelectionButton.onClick.AddListener(delegate {
                List<Hero> heroParty = UnitFinderWindow.instance.GetSelectedHeroes();
                MoneyBag.instance.RemoveMoney(linkedQuestData.goldCost);
                if(heroParty != null) {
                    MaterialQuestRunPrompt.instance.SetQuest((MaterialQuest)linkedQuest, heroParty);
                    UnitFinderWindow.instance.OpenPopup(MaterialQuestRunPrompt.instance);
                }
            });
        }
    }

    public void OpenQuestResultsWindow() {
        QuestResultsWindow.instance.SetQuest(linkedQuest);
        QuestResultsWindow.instance.OpenWindow();
    }

    public void SwitchState(Quest.QuestState newState) {
        switch(newState) {
            case Quest.QuestState.Disabled:
                statusIcon.enabled = false;
                graphicsHolder.SetActive(false);
                timer.gameObject.SetActive(false);
                break;
            case Quest.QuestState.Unavailable:
                graphicsHolder.SetActive(true);
                statusIcon.enabled = false;
                closeLOD.GetComponent<Image>().material = grayscaleMaterial;
                closeLOD.transform.Find("Image").GetComponent<Image>().material = grayscaleMaterial;
                farLOD.GetComponent<Image>().material = grayscaleMaterial;
                farLOD.transform.Find("Image").GetComponent<Image>().material = grayscaleMaterial;
                questNameText.material = grayscaleMaterial;
                questButton.enabled = false;
                timer.gameObject.SetActive(false);
                break;
            case Quest.QuestState.Available:
                graphicsHolder.SetActive(true);
                statusIcon.enabled = false;
                closeLOD.GetComponent<Image>().material = null;
                closeLOD.transform.Find("Image").GetComponent<Image>().material = null;
                farLOD.GetComponent<Image>().material = null;
                farLOD.transform.Find("Image").GetComponent<Image>().material = null;
                questNameText.material = null;
                questButton.enabled = true;
                questButton.onClick.RemoveAllListeners();
                questButton.onClick.AddListener(delegate {OpenQuestPreviewWindow();});
                timer.gameObject.SetActive(false);
                break;
            case Quest.QuestState.Ongoing:
                statusIcon.enabled = true;
                graphicsHolder.SetActive(true);
                statusIcon.sprite = swordsIcon;
                questButton.enabled = false;
                timer.gameObject.SetActive(true);
                break;
            case Quest.QuestState.RewardsReady:
                statusIcon.enabled = true;
                questButton.enabled = true;
                statusIcon.sprite = chestIcon;
                timer.gameObject.SetActive(false);
                questButton.onClick.RemoveAllListeners();
                questButton.onClick.AddListener(delegate {OpenQuestResultsWindow();});
                break;
        }
    }

}
