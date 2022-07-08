using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class holding runtime game data, is present in all scenes.
public class GameData : MonoBehaviour
{

    public static GameData instance;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    [SerializeField] List<CharacterUnit> allCharacters;
    public List<CharacterUnit> GetAllCharacters() {
        return allCharacters;
    }
}
