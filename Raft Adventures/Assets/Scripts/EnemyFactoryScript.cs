using UnityEngine;
using System.Collections;

public class EnemyFactoryScript : MonoBehaviour {
    
    public GameObject stone;

    public int difficulty;
    public void getEnemy(){
        //enemy instantiation and selection logic
        Instantiate(stone);
    }
}
