using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFactoryScript : MonoBehaviour {
    
    public GameObject stone;
	List<GameObject> allEnemies = new List<GameObject>();
	public int difficulty;
	public AbstractEnemy getEnemy(){
		//enemy instantiation and selection logic
        return Instantiate(stone).GetComponent<AbstractEnemy>();

    }
	public void deleteEnemy(GameObject toBeDeleted) {
		allEnemies.Remove(toBeDeleted);
	}
	public List<GameObject> getAllEnemies() {
		return allEnemies;
	}
}
