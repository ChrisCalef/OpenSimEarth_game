//-----------------------------------------------------------------------------
// Torque Game Engine
// Copyright (c) 2002 BraveTree Productions
// Copyright (c) 2001 GarageGames.Com
// Portions Copyright (c) 2001 by Sierra Online, Inc.
//-----------------------------------------------------------------------------

//~server/scripts/WarSparrow.cs

//-----------------------------------------------------------------------------
// WarSparrow Emitter profiles

datablock ParticleData(WarSparrowTrailParticle)
{
  dragCoefficient      = 1.5;
  gravityCoefficient   = 0;
  inheritedVelFactor   = 0.2;
  constantAcceleration = 0.0;
  lifetimeMS           = 3000;
  lifetimeVarianceMS   = 0;
  textureName          = "art/shapes/WarSparrow/splash";
  colors[0]     = "0.6 0.6 0.6 0.5";
  colors[1]     = "0.2 0.2 0.2 0";
  sizes[0]      = 0.6;
  sizes[1]      = 5;
};

datablock ParticleEmitterData(WarSparrowTrailEmitter)
{
  ejectionPeriodMS = 5;
  periodVarianceMS = 0;
  ejectionVelocity = 1;
  velocityVariance = 1.0;
  ejectionOffset   = 0.0;
  thetaMin         = 0;
  thetaMax         = 10;
  phiReferenceVel  = 0;
  phiVariance      = 360;
  overrideAdvances = false;
  particles = "WarSparrowTrailParticle";
};

//-----------------------------------------------------------------------------
// WarSparrow Audio Profiles
//
// To Use Sounds:
// Change replaceme.wav to the name of your sound.

datablock AudioProfile(WarSparrowThrustSound)
{
  fileName = "replaceme.wav";
  description = AudioDefaultLooping3d;
  preload = true;
};
datablock AudioProfile(WarSparrowEngineSound)
{
  fileName = "replaceme.wav";
  description = AudioDefaultLooping3d;
  preload = true;
};
datablock AudioProfile(WarSparrowSoftImpactSound)
{
  fileName = "replaceme.wav";
  description = AudioClose3d;
  preload = true;
};
datablock AudioProfile(WarSparrowHardImpactSound)
{
  fileName = "replaceme.wav";
  description = AudioClose3d;
  preload = true;
};
datablock AudioProfile(WarSparrowExitWaterSound)
{
  fileName = "replaceme.wav";
  description = AudioDefaultLooping3d;
  preload = true;
};
datablock AudioProfile(WarSparrowImpactSoftSound)
{
  fileName = "replaceme.wav";
  description = AudioClose3d;
  preload = true;
};
datablock AudioProfile(WarSparrowImpactMediumSound)
{
  fileName = "replaceme.wav";
  description = AudioClose3d;
  preload = true;
};
datablock AudioProfile(WarSparrowImpactHardSound)
{
  fileName = "replaceme.wav";
  description = AudioClose3d;
  preload = true;
};
datablock AudioProfile(WarSparrowWakeSound)
{
  fileName = "replaceme.wav";
  description = AudioClose3d;
  preload = true;
};
//-------------------------------------------------------------
//FlyerMachineGun Emitter Profiles

// Projectile trail emitter
datablock ParticleData(FlyerMachineGunSmokeParticle)
{

  textureName          = "art/shapes/WarSparrow/smokeParticle";
  dragCoeffiecient     = 0.0;
  gravityCoefficient   = -0.2;  // rises
  inheritedVelFactor   = 0.00;
  lifetimeMS           = 450;//300;   // time in ms
  lifetimeVarianceMS   = 150;   // ...more or less
  useInvAlpha = false;
  spinRandomMin = -30.0;
  spinRandomMax = 30.0;
  colors[0]     = "0.56 0.36 0.26 1.0";
  colors[1]     = "0.56 0.36 0.26 1.0";
  colors[2]     = "0 0 0 0";
  sizes[0]      = 0.25;
  sizes[1]      = 0.4;
  sizes[2]      = 0.6;
  times[0]      = 0.0;
  times[1]      = 0.3;
  times[2]      = 1.0;
};
datablock ParticleEmitterData(FlyerMachineGunSmokeEmitter)
{
  ejectionPeriodMS = 10;
  periodVarianceMS = 5;
  ejectionVelocity = 0.25;
  velocityVariance = 0.10;
  thetaMin         = 0.0;
  thetaMax         = 90.0;
  particles = FlyerMachineGunSmokeParticle;
};

//-----------------------------------------------------------------------------
// Weapon fire emitter
datablock ParticleData(FlyerMachineGunFireParticle)
{
  textureName          = "art/shapes/WarSparrow/smokeParticle";
  dragCoeffiecient     = 0.0;
  gravityCoefficient   = -0.1;  // rises
  inheritedVelFactor   = 0.3;
  lifetimeMS           = 200;   // Time in ms
  lifetimeVarianceMS   = 50;    // ...more or less
  useInvAlpha = false;
  spinRandomMin = -30.0;
  spinRandomMax = 30.0;
  colors[0]     = "1.0 1.0 1.0 0.0";
  //colors[1]     = "0.56 0.36 0.26 1.0";
  colors[2]     = "0 0 0 0";
  sizes[0]      = 0.2;
  sizes[1]      = 0.5;
  sizes[2]      = 1;
  times[0]      = 0.0;
  times[1]      = 0.3;
  times[2]      = 1.0;
};
datablock ParticleEmitterData(FlyerMachineGunFireEmitter)
{
  ejectionPeriodMS = 30;
  periodVarianceMS = 5;
  ejectionVelocity = 2;
  ejectionOffset   = 0.1;
  velocityVariance = 0.10;
  thetaMin         = 0.0;
  thetaMax         = 10.0;
  particles = FlyerMachineGunFireParticle;
};

//-----------------------------------------------------------------------------
// Projectile Explosion
datablock ParticleData(FlyerMachineGunExplosionParticle)
{
  dragCoefficient      = 2;
  gravityCoefficient   = 0.2;
  inheritedVelFactor   = 0.2;
  constantAcceleration = 0.0;
  lifetimeMS           = 750;
  lifetimeVarianceMS   = 150;
  textureName          = "art/shapes/WarSparrow/smokeParticle";
  colors[0]     = "0.56 0.36 0.26 1.0";
  colors[1]     = "0.56 0.36 0.26 1.0";
  colors[2]     = "0 0 0 0";
  sizes[0]      = 0.5;
  sizes[1]      = 1.0;
};
datablock ParticleEmitterData(FlyerMachineGunExplosionEmitter)
{
  ejectionPeriodMS = 7;
  periodVarianceMS = 0;
  ejectionVelocity = 1;
  velocityVariance = 1.0;
  ejectionOffset   = 0.0;
  thetaMin         = 0;
  thetaMax         = 60;
  phiReferenceVel  = 0;
  phiVariance      = 360;
  overrideAdvances = false;
  particles = "FlyerMachineGunExplosionParticle";
};
datablock ExplosionData(FlyerMachineGunExplosion)
{
  //explosionShape = "art/data/shapes/warsparrow/explosion.dts";
  particleEmitter = FlyerMachineGunExplosionEmitter;
  particleDensity = 50;
  particleRadius = 0.2;
  faceViewer     = true;
  explosionScale = "1 1 1";
  shakeCamera = true;
  camShakeFreq = "10.0 11.0 10.0";
  camShakeAmp = "1.0 1.0 1.0";
  camShakeDuration = 0.5;
  camShakeRadius = 10.0;
};

//-----------------------------------------------------------------------------
// Projectile Object
datablock ProjectileData(FlyerMachineGunProjectile)
{
  projectileShapeName = "art/shapes/WarSparrow/projectile.dts";
  directDamage        = 10;
  radiusDamage        = 10;
  damageRadius        = 0.5;
  explosion           = FlyerMachineGunExplosion;
  particleEmitter     = FlyerMachineGunSmokeEmitter;
  muzzleVelocity      = 60;
  velInheritFactor    = 1;
  armingDelay         = 0.3;
  lifetime            = 8000;
  fadeDelay           = 1500;
  bounceElasticity    = 0;
  bounceFriction      = 0;
  isBallistic         = true;
  gravityMod = 0.10;
  hasLight    = true;
  lightRadius = 1.5;
  lightColor  = "0.56 0.36 0.26 1.0";
};

function FlyerMachineGunProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
  // Apply damage to the object all shape base objects
  if (%col.getType() & $TypeMasks::ShapeBaseObjectType)
     %col.damage(%obj,%pos,%this.directDamage,"FlyerMachineGunBullet");

  // Radius damage is a support scripts defined in radiusDamage.cs
  radiusDamage(%obj,%pos,%this.damageRadius,%this.radiusDamage,"FlyerMachineGunBullet",0);
  
  //openSimEarth: This is temporary, and we need to find a better way to do things like this,
  //but for now just adding it here. When the object being collided into is named Skyscraper,
  //the explosions need to spit out people.
  echo("machine gun collision, " @ %col.getClassName() @ "  " @ %col.getName()); 
  if (%col.getName()$="Skyscraper")
   %col.spitM4s(%pos,%normal,1); 
}

//--------------------------------------------------------------------------
// Rifle shell that's ejected during reload.

datablock DebrisData(FlyerMachineGunShell)
{
  shapeFile = "art/shapes/WarSparrow/shell.dts";
  lifetime = 3.0;
  minSpinSpeed = 300.0;
  maxSpinSpeed = 400.0;
  elasticity = 0.5;
  friction = 0.2;
  numBounces = 5;
  staticOnMaxBounce = true;
  snapOnMaxBounce = false;
  fade = true;
};

//-------------------------------------------------------------
// VEHICLE CHARACTERISTICS
// To use emitters, explosions, and sounds not defined above:
// Define by creating profiles above
// Uncomment where they are used in the following datablock

datablock FlyingVehicleData(WarSparrow)
{

//Basic WarSparrow Information
  spawnOffset = "0 0 1";
  category = "Vehicles";
  shapeFile = "art/shapes/WarSparrow/WarSparrow.dts";
  //shapeFile = "art/shapes/fg_convert/Dragonfly/dragonfly2.dts";

  multipassenger = false;
  renderWhenDestroyed = false;
  mountPose[0] = sitting;
  drag = 0.15;
  density = 1.0;

  cameraMaxDist = 6;
  cameraOffset = 1.8;
  cameraRoll = true;// Roll the camera with the vehicle
  cameraDecay = 0.99;// Decay per sec. rate of velocity lag
  cameraLag = 0.05;

  minMountDist = 6;

  //explosion = WarSparrowExplosion;
  //explosionDamage = 0.5;
  //explosionRadius = 5.0;

  maxDamage = 90.00;
  DestroyedLevel = 90.00;
  isShielded = true;
  EnergyPerDamagePoint = 160;

  MaxEnergy = 280;    // Afterburner and any energy weapon pool
  MinDrag = 88;       // Linear Drag (eventually slows you down when not thrusting...constant drag)
  RotationalDrag = 10;// Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)
  RechargeRate = 0.0;

  maxAutoSpeed = 4;      // Autostabilizer kicks in when less than this speed. (meters/second)
  autoAngularForce = 100;  // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
  autoLinearForce = 300;   // Linear stabilzer force (this slows you down when autostabilizer kicks in)
  autoInputDamping = 0.85;// Dampen control input so you don't whack out at very slow speeds

  // Maneuvering Information
  MaxSteeringAngle = 4.2;      // Max radians you can rotate the wheel. Smaller number is more maneuverable.
  HorizontalSurfaceForce = 350;// Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
  VerticalSurfaceForce = 100;  // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
  ManeuveringForce = 4500;     // Horizontal jets (W,S,D,A key thrust)
  SteeringForce = 1500;        // Steering jets (force applied when you move the mouse)
  SteeringRollForce = 1280;     // Steering jets (how much you heel over when you turn)
  RollForce = 820;             // Auto-roll (self-correction to right you after you roll/invert)
  HoverHeight = 1.0;           // Height off the ground at rest
  CreateHoverHeight = 1.0;     // Height off the ground when created

  // Rigid body
  Mass = 500;           // Mass of the vehicle
  BodyFriction = 0;     // Don't mess with this.
  BodyRestitution = 0.8;// When you hit the ground, how much you rebound. (between 0 and 1)
  MinRollSpeed = 0;     // Don't mess with this.
  MinImpactSpeed = 0;   // If hit ground at speed above this then it's an impact. Meters/second
  SoftImpactSpeed = 10; // Sound hooks. This is the soft hit.
  HardImpactSpeed = 25; // Sound hooks. This is the hard hit.
  SpeedDamageScale = 0.04;

  collDamageThresholdVel = 20.0;
  collDamageMultiplier = 0.02;

  minTrailSpeed = 15; // The speed your contrail shows up at.
  TrailEmitter = WarSparrowTrailEmitter;
  TrailEmitterOffset = "0.0 -1.0 0.5";
  TrailEmitterHeight = 3.6;
  TrailEmitterFreqMod = 15.0;

  //General Sound Assignments
  //jetSound = WarSparrowThrustSound;
  //engineSound = WarSparrowEngineSound;
  //softImpactSound = WarSparrowSoftImpactSound;
  //hardImpactSound = WarSparrowHardImpactSound;


//------------------------------------------------------------
// Sound Velocities
// Velocities at which Water Impact sounds will be triggered

  SoftSplashSound = 15.0;
  MediumSplashSoundVelocity = 30.0;
  HardSplashSoundVelocity = 60.0;
  ExitSplashSoundVelocity = 20.0;

//-------------------------------------------------------------
// Water Impact Sound Assignments
// Uncomment these if you have added your own sounds above.
//-------------------------------------------------------------
  //exitingWater = VehicleExitWaterMediumSound;
  //impactWaterEasy = VehicleImpactWaterSoftSound;
  //impactWaterMedium = VehicleImpactWaterMediumSound;
  //impactWaterHard = VehicleImpactWaterHardSound;
  //waterWakeSound = VehicleWakeSound;

  //ExitWater = WarSparrowExitWaterSound;
  //ImpactSoft = WarSparrowImpactSoftSound;
  //ImpactMedium = WarSparrowImpactMediumSound;
  //ImpactHard = WarSparrowImpactHardSound;
  //Wake = WarSparrowWakeSound;
  //triggerDustHeight = 4.0;
  //dustHeight = 1.0;

  //damageEmitter[0] = WarSparrowSmallLightDamageSmoke;
  //damageEmitter[1] = WarSparrowSmallHeavyDamageSmoke;
  //damageEmitter[2] = WarSparrowDamageBubbles;
  //damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
  //damageLevelTolerance[0] = 0.3;
  //damageLevelTolerance[1] = 0.7;
  //numDmgEmitterAreas = 3;



  //splashEmitter[0] = WarSparrowFoamDropletsEmitter;
  //splashEmitter[1] = WarSparrowFoamEmitter;
  //shieldImpact = WarSparrowShieldImpact;
  //checkRadius = 5.5;
  //observeParameters = "1 10 10";
};

function VehicleData::onAdd(%data, %obj)
{
  //Parent::onAdd(%data, %obj);
  if((%data.sensorData !$= "") && (%obj.getTarget() != -1))
     setTargetSensorData(%obj.getTarget(), %data.sensorData);
  %obj.setRechargeRate(%data.rechargeRate);

  // set full energy
  %obj.setEnergyLevel(%data.MaxEnergy);
   if(%obj.disableMove)
     %obj.immobilized = true;
  if(%obj.deployed)
  {
     if($countDownStarted)
        %data.schedule(($Host::WarmupTime * 1000) / 2, "vehicleDeploy", %obj, 0, 1);
     else
     {
        $VehiclesDeploy[$NumVehiclesDeploy] = %obj;
        $NumVehiclesDeploy++;
     }
  }
  if(%obj.mountable || %obj.mountable $= "")
     %obj.mountable = "1";
  else
     %obj.mountable = "0";

  //if(%obj.mountable || %obj.mountable $= "")
     //%data.isMountable(%obj, true);
  //else
     //%data.isMountable(%obj, false);
 //%obj.setSelfPowered();
 //%data.canObserve = true;
 }

function FlyingVehicleData::create(%block)
{
    %obj = new FlyingVehicle() {
       dataBlock = %block;
    };
    return(%obj);
}

//-------------------------------------------------------------------------
// Add the Machine Gun to the Shape

function WarSparrow::onAdd(%this,%obj)
{
   parent::onAdd(%this,%obj);
   %obj.mountImage(FlyerMachineGunImage,0);
   %obj.setImageAmmo(0,1);
   %obj.nextWeaponFire = 1;
 }

 function WarSparrow::onDamage(%this, %obj)
 {
  Parent::onDamage(%this, %obj);
}



//--------------------------------------------------------------------------
/// FlyerMachineGun image which does all the work.  Images do not normally exist in
/// the world, they can only be mounted on ShapeBase objects.

datablock ShapeBaseImageData(FlyerMachineGunImage)
{
  // Basic Item properties
  shapeFile = "art/shapes/WarSparrow/weapon.dts";
  emap = true;

  // Specify mount point & offset for 3rd person, and eye offset
  // for first person rendering.
  mountPoint = 1;
  offset = "0 0 0";
  eyeOffset = "0.0 0.0 0.0";

  // When firing from a point offset from the eye, muzzle correction
  // will adjust the muzzle vector to point to the eye LOS point.
  // Since this weapon doesn't actually fire from the muzzle point,
  // we need to turn this off.
  correctMuzzleVector = true;

  // Add the WeaponImage namespace as a parent, WeaponImage namespace
  // provides some hooks into the inventory system.

  className = "WeaponImage";

   // Projectile && Ammo.
  projectile = FlyerMachineGunProjectile;
  projectileType = Projectile;
  fireTimeout = 300;
  casing = FlyerMachineGunShell;

  // Images have a state system which controls how the animations
  // are run, which sounds are played, script callbacks, etc. This
  // state system is downloaded to the client so that clients can
  // predict state changes and animate accordingly.  The following
  // system supports basic ready->fire->reload transitions as
  // well as a no-ammo->dryfire idle state.
  // Initial start up state

  stateName[0]                     = "Preactivate";
  stateTransitionOnLoaded[0]       = "Activate";
  stateTransitionOnNoAmmo[0]
       = "Activate";
   // Activating the gun.  Called when the weapon is first
  // mounted and there is ammo.
  stateName[1]                     = "Activate";
  stateTransitionOnTimeout[1]      = "Ready";
  stateTimeoutValue[1]             = 0.09;
  stateSequence[1]
                 = "Activate";
   // Ready to fire, just waiting for the trigger
  stateName[2]                     = "Ready";
  stateTransitionOnNoAmmo[2]       = "Activate";
  stateTransitionOnTriggerDown[2]  = "Fire";

   // Fire the weapon. Calls the fire script which does
  // the actual work.
  stateName[3]                     = "Fire";
  stateTransitionOnTimeout[3]      = "Reload";
  stateTimeoutValue[3]             = 0.09;
  stateFire[3]                     = true;
  stateRecoil[3]                   = LightRecoil;
  stateAllowImageChange[3]         = false;
  stateSequence[3]                 = "Fire";
  stateScript[3]                   = "onFire";
  stateEmitter[3]                  = FlyerMachineGunFireEmitter;
  stateEmitterTime[3]              = 0.13;

   // Play the reload animation, and transition into
  stateName[4]                     = "Reload";
  stateTransitionOnNoAmmo[4]       = "Activate";
  stateTransitionOnTimeout[4]      = "Ready";
  stateTimeoutValue[4]             = 0.01;
  stateAllowImageChange[4]         = false;
  stateSequence[4]                 = "Reload";
  stateEjectShell[4]               = true;

   // No ammo in the weapon, just idle until something
  // shows up. Play the dry fire sound if the trigger is
  // pulled.
  stateName[5]                     = "Activate";
  stateTransitionOnAmmo[5]         = "Reload";
  stateSequence[5]                 = "Activate";
  stateTransitionOnTriggerDown[5]  = "DryFire";

   // No ammo dry fire
  stateName[6]                     = "DryFire";
  stateTimeoutValue[6]             = 1.0;
  stateTransitionOnTimeout[6]      = "Activate";
};

//----------------------------------------------------------------

function Warsparrow::onTrigger(%data, %obj, %trigger, %state)
{
   if(%trigger == 0)
   {
      switch (%state) {
         case 0:
            %obj.fireWeapon = false;
            %obj.setImageTrigger(0, false);
            %obj.setImageTrigger(1, false);
         case 1:
            %obj.fireWeapon = true;
            if(%obj.nextWeaponFire == 0) {
               %obj.setImageTrigger(0, true);
               %obj.setImageTrigger(1, false);
            }
            else {
               %obj.setImageTrigger(0, false);
               %obj.setImageTrigger(1, true);
            }
      }
   }
}


function FlyerMachineGunImage::onFire(%data,%obj,%slot)
{
  Parent::onFire(%data,%obj,%slot);
  %obj.nextWeaponFire = 1;
  schedule(%data.fireTimeout, 0, "fireAgain", %obj);
}

function FlyerMachineGunImage::onTriggerDown(%this, %obj, %slot)
{
}

function FlyerMachineGunImage::onTriggerUp(%this, %obj, %slot)
{
}

function fireAgain(%obj)
{
  if(%obj.fireWeapon)
  {
     if(%obj.nextWeaponFire == 0)
     {
       %obj.setImageTrigger(0, true);
       %obj.setImageTrigger(1, false);
     }
     else
     {
       %obj.setImageTrigger(0, false);
       %obj.setImageTrigger(1, true);
     }
  }
  else
  {
    %obj.setImageTrigger(0, false);
    %obj.setImageTrigger(1, false);
  }
}

function ShapeBaseImageData::onFire(%data, %obj, %slot)
{
    %projectile = %data.projectile;
    // Determine initial projectile velocity based on the
    // gun's muzzle point and the object's current velocity
    %muzzleVector = %obj.getMuzzleVector(%slot);
    %objectVelocity = %obj.getVelocity();
    %muzzleVelocity = VectorAdd(
    	VectorScale(%muzzleVector, %projectile.muzzleVelocity),
    	VectorScale(%objectVelocity, %projectile.velInheritFactor));
    %vehicle = 0;
    // Create the projectile object
    %p = new (%data.projectileType)()
    {
      dataBlock        = %data.projectile;
      initialVelocity  = %muzzleVelocity;
      initialDirection = %obj.getMuzzleVector(%slot);
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
      vehicleObject    = %vehicle;
    };
    if (isObject(%obj.lastProjectile) && %obj.deleteLastProjectile)
       %obj.lastProjectile.delete();
       %obj.lastProjectile = %p;
   %obj.deleteLastProjectile = %data.deleteLastProjectile;
   MissionCleanup.add(%p);
   return %p;
}


//------------------------------------------------------------------


//This function should be moved to server/scripts/command.cs

function serverCmdAddFlyer(%client)
{
     if(%client.player$="")
       return;
     if(%client.player)
       if(%client.player.isMounted())
          return;
     %vehicle = new FlyingVehicle()
     {
        dataBlock = warsparrow;
     };
     echo("new warsparrow: " @ %vehicle @ " eyePoint " @ %vehicle.getEyePoint() );
     %vehicle.mountable = true;
     %vehicle.setEnergyLevel(60);
     %vehicle.setTransform( %client.player.getEyeTransform() );
     //%vehicle.schedule(5500, "playThread", $ActivateThread, "activate");
     %vehicle.mountObject($myPlayer,0);
     $myPlayer.mVehicle = %vehicle;
     MissionCleanup.add(%vehicle);

}

//Bind the "Cntl F" to spawn a New War Sparrow.
//This line should be moved to client/scripts/defaultbinds.cs

moveMap.bindCmd(keyboard, "ctrl f", "commandToServer(\'addflyer\');", "");
