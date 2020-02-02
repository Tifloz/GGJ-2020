﻿/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using UnityEngine;
using UnityEngine.EventSystems;
 
public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public string powerUpExplanation;
    public string powerUpQuote;
    [Tooltip ("Tick true for power ups that are instant use, eg a health addition that has no delay before expiring")]
    public bool expiresImmediately;
    public GameObject specialEffect;
    public AudioClip soundEffect;
    
    protected BaseEntity playerBrain;

    protected SpriteRenderer spriteRenderer;

    protected enum PowerUpState
    {
        InAttractMode,
        IsCollected,
        IsExpiring
    }

    protected PowerUpState powerUpState;

    protected virtual void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    protected virtual void Start ()
    {
        powerUpState = PowerUpState.InAttractMode;
    }
    
    protected virtual void OnTriggerEnter2D (Collider2D other)
    {
        PowerUpCollected (other.gameObject);
    }

    protected virtual void PowerUpCollected (GameObject gameObjectCollectingPowerUp)
    {
        if (gameObjectCollectingPowerUp.tag != "Player")
        {
            return;
        }
        if (powerUpState == PowerUpState.IsCollected || powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsCollected;
        
        playerBrain = gameObjectCollectingPowerUp.GetComponent<BaseEntity> ();
        
        gameObject.transform.parent = playerBrain.gameObject.transform;
        gameObject.transform.position = playerBrain.gameObject.transform.position;

        // Collection effects
        PowerUpEffects ();           

        // Payload      
        PowerUpPayload ();

        // Send message to any listeners
        foreach (GameObject go in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IPowerUpEvents> (go, null, (x, y) => x.OnPowerUpCollected (this, playerBrain));
        }

        spriteRenderer.enabled = false;
    }

    protected virtual void PowerUpEffects ()
    {
        if (specialEffect != null)
        {
            Instantiate (specialEffect, transform.position, transform.rotation, transform);
        }

        //if (soundEffect != null)
        //{
        //    MainGameController.main.PlaySound (soundEffect);
        //}
    }

    protected virtual void PowerUpPayload ()
    {
        Debug.Log ("Power Up collected, issuing payload for: " + gameObject.name);

        if (expiresImmediately)
        {
            PowerUpHasExpired ();
        }
    }

    protected virtual void PowerUpHasExpired ()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsExpiring;

        foreach (GameObject go in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IPowerUpEvents> (go, null, (x, y) => x.OnPowerUpExpired (this, playerBrain));
        }
        Debug.Log ("Power Up has expired, removing after a delay for: " + gameObject.name);
        DestroySelfAfterDelay ();
    }

    protected virtual void DestroySelfAfterDelay ()
    {
        Destroy (gameObject, 10f);
    }
}

