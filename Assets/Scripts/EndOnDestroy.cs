using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOnDestroy : MonoBehaviour
{
    // the health script destroys the object when its health reaches zero
    void OnDestroy() {
        FindObjectOfType<GameManager>().EndGame();  
    }

}
