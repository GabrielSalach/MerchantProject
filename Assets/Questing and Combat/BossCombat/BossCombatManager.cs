using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BossCombatManager : MonoBehaviour
{ 
    List<Hero> heroParty;
    List<Enemy> bosses;

    [SerializeField] Transform heroPartyHolder;
    [SerializeField] Transform bossHolder;

    GameObject bossObject;
    GameObject heroObject;

    [SerializeField] BossQuestData questData;

    [SerializeField] GameObject skillPrefab;

    [SerializeField] RectTransform arrow;
    [SerializeField] TextMeshProUGUI messageBox;
    [SerializeField] GameObject playerInputUI;

    enum CombatState {Start, PlayerTurn, EnemyTurn, Win, Lose};
    CombatState combatState;

    bool movementComplete = false;

    void Start() {
        // Hero
        heroParty = new List<Hero>();
        //heroObject = new GameObject(CombatSceneLoader.instance.heroParty[0].heroName);
        heroObject = new GameObject("Mercenary");
        heroParty.Add(heroObject.AddComponent<Hero>());
        heroParty[0].ReadFromSavefile();
        heroObject.transform.parent = heroPartyHolder;
        heroObject.transform.localPosition = Vector3.zero;
        SpriteRenderer heroRenderer = heroObject.AddComponent<SpriteRenderer>();
        heroRenderer.sprite = heroParty[0].heroClass.classSprite;
        heroObject.transform.localScale = new Vector3(2,2,1);
        heroRenderer.sortingLayerName = "Characters";
        heroRenderer.flipX = true;

        // Boss
        bosses = new List<Enemy>();
        // BossQuestData questData = CombatSceneLoader.instance.questData;
        bossObject = new GameObject(questData.boss.enemyName);
        bosses.Add(bossObject.AddComponent<Enemy>());
        bosses[0].InitializeEnemy(questData.boss);
        bossObject.transform.parent = bossHolder;
        bossObject.transform.localPosition = Vector3.zero;
        SpriteRenderer bossRenderer = bossObject.AddComponent<SpriteRenderer>();
        bossRenderer.sprite = questData.boss.enemyArt;
        bossRenderer.flipX = true;
        bossObject.transform.localScale = new Vector3(4, 4, 1);
        bossRenderer.sortingLayerName = "Characters";
        
        GenerateSkills();

        combatState = CombatState.Start;
    }

    public void SwitchTurns() {
        if(combatState == CombatState.PlayerTurn) {
            combatState = CombatState.EnemyTurn;
        } else {
            combatState = CombatState.PlayerTurn;
        }
    }

    void Update() {
        StartCoroutine(Combat());
    }

    IEnumerator Combat() {
        switch(combatState) {
            case CombatState.Start:
                messageBox.gameObject.SetActive(true);
                messageBox.SetText(questData.boss.enemyName + " wants to fight !");
                yield return new WaitForSeconds(3f);
                combatState = CombatState.PlayerTurn;
                break;
            case CombatState.PlayerTurn:
                messageBox.gameObject.SetActive(false);
                playerInputUI.SetActive(true);
                if(movementComplete == false) {
                    LeanTween.value(arrow.gameObject, arrow.localPosition.x, heroObject.transform.position.x - 320 , 0.3f).setOnUpdate((float value) => MoveArrow(value));
                    movementComplete = true;
                }
                yield return null;
                break;
            case CombatState.EnemyTurn:
                playerInputUI.SetActive(false);
                messageBox.gameObject.SetActive(true);
                messageBox.SetText("Enemy Turn");
                if(movementComplete == true) {
                    LeanTween.value(arrow.gameObject, arrow.localPosition.x, bossObject.transform.position.x -320 , 0.3f).setOnUpdate((float value) => MoveArrow(value));
                    movementComplete = false;
                }
                yield return null;
                break;
            default: 
                yield return null;
                break;
        }
    }

    void GenerateSkills() {
        Transform skillHolder = playerInputUI.GetComponentInChildren<ContentSizeFitter>().transform;
        
        foreach(Skill skill in heroParty[0].skills) {
            Button skillBtn = Instantiate(skillPrefab, skillHolder.position, Quaternion.identity).GetComponent<Button>();
            skillBtn.transform.parent = skillHolder;
            SkillBuilder sb = skillBtn.GetComponent<SkillBuilder>();
            sb.GenerateSkillDisplay(skill);
        }
    }

    void MoveArrow(float position) {
        arrow.anchoredPosition = new Vector2(position, arrow.anchoredPosition.y);
    }
}
