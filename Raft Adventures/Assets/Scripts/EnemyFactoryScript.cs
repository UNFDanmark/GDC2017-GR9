using UnityEngine;
using System.Collections;

public class EnemyFactoryScript : MonoBehaviour {
    
    public GameObject stone;

    public int difficulty;
    public AbstractEnemy getEnemy(){
        //enemy instantiation and selection logic
        return Instantiate(stone).GetComponent<AbstractEnemy>();
    }
}
