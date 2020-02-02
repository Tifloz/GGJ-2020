﻿class ApplePowerUp : PowerUp
{
    protected override void PowerUpPayload()  // Checklist item 1
    {
        base.PowerUpPayload();
        playerBrain.Zap();      
    }
}