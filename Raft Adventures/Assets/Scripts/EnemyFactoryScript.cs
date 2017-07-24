using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFactoryScript : MonoBehaviour {
    
    public GameObject stone;
	List<GameObject> allEnemies = new List<GameObject>();
	public int difficulty;
	public GameObject getEnemy(){
		//enemy instantiation and selection logic

        GameObject thisEnemy = Instantiate(stone);
		allEnemies.Add(thisEnemy);
		return thisEnemy;

    }
	public void UpdateList() {
		allEnemies.RemoveAll(delegate (GameObject o) { return o == null; });//removes all null objects
	}
	public List<GameObject> getAllEnemies() {
		UpdateList();
		return allEnemies;
	}
}
