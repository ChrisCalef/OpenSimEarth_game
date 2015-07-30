//-----------------------------------------------------------------------------
// Copyright (c) 2014 Guy Allard
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Base frequency for behavior tree ticks (in milliseconds)
//-----------------------------------------------------------------------------
$BotTickFrequency = 100;

// ----------------------------------------------------------------------------
// make the local player invisible to the AI
//-----------------------------------------------------------------------------
function setGodMode(%val)
{
   LocalClientConnection.player.isGod = %val;
}

// don't damage an invincible player
function DefaultPlayerData::damage(%this, %obj, %sourceObject, %position, %damage, %damageType)
{
   if(%obj.isGod)
      return;
   
   Parent::damage(%this, %obj, %sourceObject, %position, %damage, %damageType);
}

//-----------------------------------------------------------------------------
// bot datablock
//-----------------------------------------------------------------------------
datablock PlayerData(BadBotData : DefaultPlayerData)
{
   // max visible distance
   VisionRange = 40;
   
   // vision field of view
   VisionFov = 120;
   
   // max range to look for items
   findItemRange = 20;
   
   // the type of object to search for when looking for targets
   targetObjectTypes = $TypeMasks::PlayerObjectType;
   
   // the type of object to search for when looking for items
   itemObjectTypes = $TypeMasks::itemObjectType;
   
   // some numbers for testing
   
   // distance the bot wants to be from its target when using the Ryder
   optimalRange["Ryder"] = 8;
   
   // number of milliseconds to hold the trigger down when using the Ryder
   burstLength["Ryder"] = 100;
   
   // distance the bot wants to be from its target when using the Lurker
   optimalRange["Lurker"] = 12;
   
   // number of milliseconds to hold the trigger down when using the Lurker
   burstLength["Lurker"] = 750;
   
   // +/- deviation from optimal range that is tolerated
   rangeTolerance = 3;
   
   // probability that the bot will switch from its current target to another, closer target
   switchTargetProbability = 0.1;
   
   // disable other weapons, we don't know how to use them yet
   maxInv[LurkerGrenadeLauncher] = 0;
   maxInv[LurkerGrenadeAmmo] = 0;
   maxInv[ProxMine] = 0;
   maxInv[DeployableTurret] = 0;
};


//=============================================================================
// Supporting functions for an AIPlayer driven by a behavior tree
//=============================================================================

// Spawn a bot called %name located at %startpos
function BadBot::spawn(%name, %startPos)
{
   // create the bot
   %bot = new AIPlayer(%name) {
      dataBlock = BadBotData; 
      class = "BadBot";
   };
   
   // give it a name
   if(%name !$= "")
      %bot.setShapeName(%name);
   
   // set its position, or use the default if no position is given
   if(isObject(%startPos))
   {
      %startPos = %startPos.position;
   }
   else if(%startPos $= "")
   {
      %spawnPoint = pickPlayerSpawnPoint(PlayerDropPoints);
      if(isObject(%spawnPoint))
         %startPos = %spawnPoint.getPosition();
   }
   
   %bot.setPosition(%startPos);
   
   // tetherpoint will give the bot a place to call home
   %bot.tetherPoint = %startPos;
   
   return %bot;      
}


// override getMuzzleVector so that the bots aim at where they are looking
function BadBot::getMuzzleVector(%this, %slot)
{
   return %this.getEyeVector();
}


// use onAdd to equip the bot
function BadBotData::onAdd(%data, %obj)
{
   // give him the standard player loadout
   game.loadout(%obj);
}


// Override onDisabled so we can stop running the behavior tree
function BadBotData::onDisabled(%this, %obj, %state)
{
   %obj.behaviorTree.stop();
   Parent::onDisabled(%this, %obj, %state);
}


// moveTo command, %dest can be either a location or an object
function BadBot::moveTo(%this, %dest, %slowDown)
{
   %pos = isObject(%dest) ? %dest.getPosition() : %dest;
   %this.setMoveDestination(%pos, %slowDown);
   %obj.atDestination = false;
}

// forward onReachDestination to the behavior tree as a signal
function BadBotData::onReachDestination(%data, %obj)
{
   if(isObject(%obj.behaviorTree))
      %obj.behaviorTree.postSignal("onReachDestination");
      
   %obj.atDestination = true;
}

// forward animationDone callback to the behavior tree as a signal
function BadBotData::animationDone(%data, %obj)
{
   if(isObject(%obj.behaviorTree))
      %obj.behaviorTree.postSignal("onAnimationDone");
}

// get the index of the closest node on the specified path
function BadBot::getClosestNodeOnPath(%this, %path)
{
   if(isObject(%path) && %path.isMemberOfClass("SimSet") && (%numNodes = %path.getCount()) > 0)
   {
      %bestNode = 0;
      %bestDist = VectorDist(%path.getObject(%bestNode).position, %this.position);
      
      for(%i=1; %i < %numNodes; %i++)
      {
         %node = %path.getObject(%i);
         %dist = VectorDist(%node.position, %this.position);
         
         if(%dist < %bestDist)
         {
            %bestNode = %i;
            %bestDist = %dist;  
         }
      }
      
      return %bestNode;
   }
   return -1;
}


// send a chat message from the bot
function BadBot::say(%this, %message)
{
   chatMessageAll(%this, '\c3%1: %2', %this.getShapeName(), %message);  
}


//=============================Global Utility==================================
function RandomPointOnCircle(%center, %radius)
{
   %randVec = (getRandom() - 0.5) SPC (getRandom() - 0.5) SPC "0";
   %randVec = VectorNormalize(%randVec);
   %randVec = VectorScale(%randVec, %radius);
   return VectorAdd(%center, %randVec);  
}

//===========================ScriptedBehavior Tasks=============================
/*
   ScriptedBehavior Tasks are composed of four (optional) parts:
   1) precondition - this function should return a boolean indicating whether
      or not the behavior should continue. If precondition returns true, the
      rest of the behavior is evaluated. If precondition returns false, the
      behavior will abort.
      
      There are two options for the evaluation of the precondition that can be 
      set in the editor:
      ONCE - The precondition is run the first time the behavior becomes active
      TICK - The precondition is run each time the behavior is ticked (if latent)
   
   2) onEnter - This is called the first time the behavior is run if the 
      precondition was successful. onEnter does not use a return value.
      
   3) onExit - This is called if the behavior reaches completion. onExit does
      not use a return value.
   
   4) behavior - This is the main behavior function, evaluated each tick.
      behavior must return a status (SUCCES / FAILURE / RUNNING).
*/

//==============================================================================
// wander behavior task
//==============================================================================
function wanderTask::behavior(%this, %obj)
{
   // stop aiming at things
   %obj.clearAim();
   
   // if the bot has a tetherPoint, use that as the center of his wander area,
   // otherwise use his current position
   %basePoint = %obj.tetherPoint !$= "" ? %obj.tetherPoint : %obj.position;
   
   // move   
   %obj.moveTo(RandomPointOnCircle(%basePoint, 10));
   return SUCCESS;
}


//==============================================================================
// Move to closest node task
//==============================================================================
function moveToClosestNodeTask::precondition(%this, %obj)
{
   // check that the object has a path to folow
   return isObject(%obj.path);  
}

function moveToClosestNodeTask::onEnter(%this, %obj)
{
   // stop aiming
   %obj.clearAim();  
}

function moveToClosestNodeTask::behavior(%this, %obj)
{
   // get the closest node
   %obj.currentNode = %obj.getClosestNodeOnPath(%obj.path);
   
   // move toward it
   %obj.moveTo(patrolPath.getObject(%obj.currentNode));
   return SUCCESS;
}

//==============================================================================
// Patrol behavior task
//==============================================================================
function patrolTask::precondition(%this, %obj)
{
   // the bot needs a path object
   return isObject(%obj.path);
}

function patrolTask::onEnter(%this, %obj)
{
   // stop aiming
   %obj.clearAim();
}

function patrolTask::behavior(%this, %obj)
{
   // hook into the standard AIPlayer path following
   %obj.moveToNextNode();
   return SUCCESS;
}

//=============================================================================
// findHealth task
//=============================================================================
function findHealthTask::behavior(%this, %obj)
{
   // get the objects datablock
   %db = %obj.dataBlock;
   //echo(%this.getId() @ " trying to find health!");
   // do a container search for items
   initContainerRadiusSearch( %obj.position, %db.findItemRange, %db.itemObjectTypes );
   while ( (%item = containerSearchNext()) != 0 )
   {
      // filter out irrelevant items
      if(%item.dataBlock.category !$= "Health" || !%item.isEnabled() || %item.isHidden())
         continue;
      
      %diff = VectorSub(%obj.position,%item.position);
      echo("found a health! " @ %item.position @ " distance " @ VectorLen(%diff));
      // check that the item is within the bots view cone
      //if(%obj.checkInFov(%item, %db.visionFov))
      if (true)//also don't have a checkInFov for physicsShapes yet
      {
         // set the targetItem field on the bot
         %obj.targetItem = %item;
         echo("found a health in view! " @ %item.position);
         break;
      }
   }
   
   return isObject(%obj.targetItem) ? SUCCESS : FAILURE;
}

//=============================================================================
// getHealth task
//=============================================================================
function getHealthTask::precondition(%this, %obj)
{
   // check that we have a valid health item to go for
   return (isObject(%obj.targetItem) && %obj.targetItem.isEnabled() && !%obj.targetItem.isHidden());  
}

function getHealthTask::onEnter(%this, %obj)
{
   echo(%obj.getId() @ " trying to get health!");
   // move to the item
   %obj.moveTo(%obj.targetItem);  
   //%obj.orientToPos(%obj.targetItem.position);
   //%obj.seqRun();
}

function getHealthTask::behavior(%this, %obj)
{
   // succeed when we reach the item
   %diff = VectorSub(%obj.targetItem.position ,%obj.position);
   //if(!%obj.atDestination)
   if ( VectorLen(%diff) > %obj.dataBlock.foundItemDistance )
      return RUNNING;
   
   return SUCCESS;
}

//=============================================================================
// pickTarget task
//=============================================================================
function pickTargetTask::precondition(%this, %obj)
{
   // decide if we should pick a new target or keep the old one
   if(isObject(%obj.targetObject))
   {
      return (VectorDist(%obj, %obj.targetObject) > %obj.dataBlock.visionRange ||
              getRandom() < %obj.dataBlock.switchTargetProbability);
   }
   
   return true;
}

function pickTargetTask::behavior(%this, %obj)
{
   %obj.targetObject = -1;
   %db = %obj.dataBlock;
   
   // container search for other players
   initContainerRadiusSearch( %obj.position, %db.VisionRange, %db.targetObjectTypes );
   while ( (%target = containerSearchNext()) != 0 )
   {
      // don't target ourself, dead players or god.
      if(%target == %obj || !%target.isEnabled() || %target.isGod)
         continue;
      
      // Check that the target is within the bots view cone
      if(%obj.checkInFov(%target, %db.visionFov))
      {
         // set the targetObject
         %obj.targetObject = %target;
         break;
      }
   }
   
   return SUCCESS;
}

//=============================================================================
// aimAtTargetTask
//=============================================================================
function aimAtTargetTask::precondition(%this, %obj)
{
   // need to be alive and have a target
   return isObject(%obj.targetObject) && %obj.isEnabled();
}

function aimAtTargetTask::behavior(%this, %obj)
{
   // calculate an aim offset
   %targetPos = %obj.targetObject.getWorldBoxCenter();
   %weaponImage = %obj.getMountedImage($WeaponSlot);
   %projectile = %weaponImage.projectile;
   %correction = "0 0 1";
   if(isObject(%projectile))
   {
      // simple target leading approximation (not for ballistics)
      %targetDist = VectorDist(%targetPos, %obj.position);
      %bulletVel = %projectile.muzzleVelocity;
      %targetVel = %obj.targetObject.getVelocity();      
      %correction = VectorAdd(%correction, VectorScale( %targetVel, (%targetDist / %bulletVel) ));
   }
   %obj.setAimObject(%obj.targetObject, %correction);
   
   return SUCCESS;
}

//=============================================================================
// shootAtTargetTask
//=============================================================================
function shootAtTargetTask::precondition(%this, %obj)
{
   return isObject(%obj.targetObject) && 
          %obj.checkInLos(%obj.targetObject) &&
          VectorDot(VectorNormalize(VectorSub(%obj.getAimLocation(), %obj.position)), %obj.getForwardVector()) > 0.9 &&
          %obj.getImageAmmo($WeaponSlot);  
}

function shootAtTargetTask::behavior(%this, %obj)
{
   if(!isEventPending(%obj.triggerSchedule))
   {
      %obj.setImageTrigger($WeaponSlot, true);
      %burstLength = %obj.dataBlock.burstLength[%obj.getMountedImage($WeaponSlot).item.description];
      %obj.triggerSchedule = %obj.schedule(%burstLength, setImageTrigger, $WeaponSlot, false);
   }

   return SUCCESS;
}


//=============================================================================
// combatMoveTask
//=============================================================================
function combatMoveTask::behavior(%this, %obj)
{
   %image = %obj.getMountedImage($WeaponSlot);
   %db = %obj.getDatablock();
   %optimalRange = %db.optimalRange[%image.item.description];
   %currentRange = VectorDist(%obj.position, %obj.targetObject.position);
   %rangeDelta = %currentRange - %optimalRange;

   %moveVec = "0 0 0";
   %fwd = %obj.getForwardVector();
   %right = %obj.getRightVector();
   
   // forward / back to stay in range
   if(mAbs(%rangeDelta) > %db.rangeTolerance)
      %moveVec = VectorScale(%fwd, %rangeDelta);
   
   // random side strafe
   %moveVec = VectorAdd(%moveVec, VectorScale(%right, 5 * (getRandom(0,2) - 1)));
      
   %obj.moveTo(VectorAdd(%obj.position, %moveVec));
   
   return SUCCESS;
}








//=============================================================================
//
//                 OPEN SIM EARTH
//

function PhysicsShape::onStartup(%this)
{
   echo(%this @ " calling onStartup!");
   
   %this.setAmbientSeqByName("ambient");
   %this.setIdleSeqByName("ambient");
   %this.setWalkSeqByName("walk");
   %this.setRunSeqByName("run");
   %this.setAttackSeqByName("power_punch_down");
   %this.setBlockSeqByName("tpose");
   %this.setFallSeqByName("ambient");
   %this.setGetupSeqByName("rSideGetup");
   
   %this.groundMove();
}

function PhysicsShape::orientTo(%this, %dest)
{
   %pos = isObject(%dest) ? %dest.getPosition() : %dest;
   %this.orientToPos(%pos);
}

function PhysicsShape::moveTo(%this, %dest, %slowDown)
{
   %pos = isObject(%dest) ? %dest.getPosition() : %dest;
   
   //This is how you print messages to the chat gui instead of the console:
   //%this.say("moving to " @ %pos);
   
   echo(%this.getId() @ " moving to " @ %pos);
   
   %this.orientToPos(%pos);
   
   %this.seqWalk();
   //%this.seqRun();
   
   //%obj.atDestination = false;
}



function PhysicsShape::say(%this, %message)//Testing, does this only work for AIPlayers?
{
   chatMessageAll(%this, '\c3%1: %2', %this.getid(), %message);  
}


function onStartup::precondition(%this, %obj)
{
   if (%obj.startedUp != true)
      return true;
   else
      return false;
}

function onStartup::behavior(%this, %obj)
{
   echo("calling onStartup!");   
   
   %obj.setAmbientSeqByName("ambient");
   %obj.setIdleSeqByName("ambient");
   %obj.setWalkSeqByName("walk");
   %obj.setRunSeqByName("run");
   %obj.setAttackSeqByName("power_punch_down");
   %obj.setBlockSeqByName("tpose");
   %obj.setFallSeqByName("ambient");
   %obj.setGetupSeqByName("rSideGetup");
   
   %obj.groundMove();
   
   %obj.startedUp = true;
   return SUCCESS;   
}

////////////// BEHAVIORS ///////////////////////////////

///////////////////////////////////
//[behaviorName]::precondition()
//[behaviorName]::onEnter()
//[behaviorName]::onExit()

//Do a raycast, either torque or physx, and find the ground directly below me.
//if below some threshold, then just move/interpolate us there. If above that, go to
//falling animation and/or ragdoll until we hit the ground and stop, then go to getUp task.

/* // No longer necessary... this is now done during processTick.
function goToGround::behavior(%this, %obj)
{
   %start = VectorAdd(%obj.position,"0 0 1.0");//Add a tiny bit (or, a huge amount)
                // so we don't get an error when we're actually on the ground.
                
   %contact = physx3CastGroundRay(%start);
   
   %obj.setPosition(%contact);
   echo(%this @ " is going to ground!!!!!!");
   %obj.setAmbientSeqByName("ambient");
   %obj.setIdleSeqByName("ambient");
   %obj.setWalkSeqByName("walk");
   %obj.setRunSeqByName("run");
   %obj.setAttackSeqByName("power_punch_down");
   %obj.setBlockSeqByName("tpose");
   %obj.setFallSeqByName("ambient");
   %obj.setGetupSeqByName("rSideGetup");
   
   return SUCCESS;
}
*/

///////////////////////////////////
//getUp::precondition()
//getUp::onEnter()
//getUp::onExit()

function getUp::behavior(%this, %obj)
{
   
   return SUCCESS;
}



///////////////////////////////////
//moveToPosition::precondition()
//moveToPosition::onEnter()
//moveToPosition::onExit()

function moveToPosition::behavior(%this, %obj)
{
   echo("calling move to position!");
   %obj.groundMove();
   return SUCCESS;   
}


//=============================================================================
// findTarget task
//=============================================================================
function findTarget::behavior(%this, %obj)
{
   // get the objects datablock
   %db = %obj.dataBlock;
   //echo(%this.getId() @ " trying to find target!");
   // do a container search for items
   initContainerRadiusSearch( %obj.position, %db.findItemRange, %db.itemObjectTypes );
   while ( (%item = containerSearchNext()) != 0 )
   {
      if (%item.dataBlock.category $= "Health" && %item.isEnabled() && !%item.isHidden())
      {
      
      %diff = VectorSub(%obj.position,%item.position);
      
      // check that the item is within the bots view cone
      //if(%obj.checkInFov(%item, %db.visionFov))
      if (true)// (We don't have a checkInFov for physicsShapes yet)
      {
         // set the targetItem field on the bot
         %obj.targetItem = %item;
         break;
      }
   }
   
   return isObject(%obj.targetItem) ? SUCCESS : FAILURE;
}
