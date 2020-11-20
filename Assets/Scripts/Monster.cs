using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]  // selecciona el parent , en este caso el monter al hacer click en él

public class Monster : MonoBehaviour
{

    [SerializeField] Sprite _deathSprite;
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die()) ;
            _hasDied=true;
        }
       
    }

    IEnumerator Die()
    {
        GetComponent<SpriteRenderer>().sprite = _deathSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if(_hasDied){
            return false;
        }

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if(bird != null)
        {
            return true;
        }

        if(collision.contacts[0].normal.y < -0.5)
        {
            return true;
        }


        return false;
    }

}
