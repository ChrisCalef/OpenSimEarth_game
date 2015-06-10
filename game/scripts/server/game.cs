//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
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

// Game duration in secs, no limit if the duration is set to 0
$Game::Duration = 20 * 60;

// When a client score reaches this value, the game is ended.
$Game::EndGameScore = 30;

// Pause while looking over the end game screen (in secs)
$Game::EndGamePause = 10;

//-----------------------------------------------------------------------------

function onServerCreated()
{
   // Server::GameType is sent to the master server.
   // This variable should uniquely identify your game and/or mod.
   $Server::GameType = $appName;

   // Server::MissionType sent to the master server.  Clients can
   // filter servers based on mission type.
   $Server::MissionType = "Deathmatch";

   // GameStartTime is the sim time the game started. Used to calculated
   // game elapsed time.
   $Game::StartTime = 0;

   // Create the server physics world.
   physicsInitWorld( "server", $pref::Physics::gravity );

   // Load up any objects or datablocks saved to the editor managed scripts
   %datablockFiles = new ArrayObject();
   %datablockFiles.add( "art/ribbons/ribbonExec.cs" );   
   %datablockFiles.add( "art/particles/managedParticleData.cs" );
   %datablockFiles.add( "art/particles/managedParticleEmitterData.cs" );
   %datablockFiles.add( "art/decals/managedDecalData.cs" );
   %datablockFiles.add( "art/datablocks/managedDatablocks.cs" );
   %datablockFiles.add( "art/forest/managedItemData.cs" );
   %datablockFiles.add( "art/datablocks/datablockExec.cs" );   
   loadDatablockFiles( %datablockFiles, true );

   // Run the other gameplay scripts in this folder
   exec("./scriptExec.cs");

   // Keep track of when the game started
   $Game::StartTime = $Sim::Time;
}

function onServerDestroyed()
{
   // This function is called as part of a server shutdown.

   physicsDestroyWorld( "server" );

   // Clean up the GameCore package here as it persists over the
   // life of the server.
   if (isPackage(GameCore))
   {
      deactivatePackage(GameCore);
   }
}

//-----------------------------------------------------------------------------

function onGameDurationEnd()
{
   // This "redirect" is here so that we can abort the game cycle if
   // the $Game::Duration variable has been cleared, without having
   // to have a function to cancel the schedule.

   if ($Game::Duration && !(EditorIsActive() && GuiEditorIsActive()))
      Game.onGameDurationEnd();
}

//-----------------------------------------------------------------------------

function cycleGame()
{
   // This is setup as a schedule so that this function can be called
   // directly from object callbacks.  Object callbacks have to be
   // carefull about invoking server functions that could cause
   // their object to be deleted.

   if (!$Game::Cycling)
   {
      $Game::Cycling = true;
      $Game::Schedule = schedule(0, 0, "onCycleExec");
   }
}

function onCycleExec()
{
   // End the current game and start another one, we'll pause for a little
   // so the end game victory screen can be examined by the clients.

   //endGame();
   endMission();
   $Game::Schedule = schedule($Game::EndGamePause * 1000, 0, "onCyclePauseEnd");
}

function onCyclePauseEnd()
{
   $Game::Cycling = false;

   // Just cycle through the missions for now.

   %search = $Server::MissionFileSpec;
   %oldMissionFile = makeRelativePath( $Server::MissionFile );
      
   for( %file = findFirstFile( %search ); %file !$= ""; %file = findNextFile( %search ) )
   {
      if( %file $= %oldMissionFile )
      {
         // Get the next one, back to the first if there is no next.
         %file = findNextFile( %search );
         if( %file $= "" )
            %file = findFirstFile(%search);
         break;
      }
   }

   if( %file $= "" )
      %file = findFirstFile( %search );

   loadMission(%file);
}

//-----------------------------------------------------------------------------
// GameConnection Methods
// These methods are extensions to the GameConnection class. Extending
// GameConnection makes it easier to deal with some of this functionality,
// but these could also be implemented as stand-alone functions.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------

function GameConnection::onLeaveMissionArea(%this)
{
   // The control objects invoke this method when they
   // move out of the mission area.

   messageClient(%this, 'MsgClientJoin', '\c2Now leaving the mission area!');
}

function GameConnection::onEnterMissionArea(%this)
{
   // The control objects invoke this method when they
   // move back into the mission area.

   messageClient(%this, 'MsgClientJoin', '\c2Now entering the mission area.');
}

//-----------------------------------------------------------------------------

function GameConnection::onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc)
{
   game.onDeath(%this, %sourceObject, %sourceClient, %damageType, %damLoc);
}

// ----------------------------------------------------------------------------
// weapon HUD
// ----------------------------------------------------------------------------
function GameConnection::setAmmoAmountHud(%client, %amount, %amountInClips )
{
   commandToClient(%client, 'SetAmmoAmountHud', %amount, %amountInClips);
}

function GameConnection::RefreshWeaponHud(%client, %amount, %preview, %ret, %zoomRet, %amountInClips)
{
   commandToClient(%client, 'RefreshWeaponHud', %amount, %preview, %ret, %zoomRet, %amountInClips);
}

//-----------------------------------------------------------------------------
// Physics Experimentation
//-----------------------------------------------------------------------------


function loadOSM()
{
   //here, read lat/long for each node as we get to it, convert it to xyz coords,
   //and save it in an array, to be used in the DecalRoad declaration.    
   
   %beforeTime = getRealTime();
   
   //theTP.loadOSM("min.osm");     
   //theTP.loadOSM("kincaid_map.osm");  
   theTP.loadOSM("central_south_eug.osm");  
   //theTP.loadOSM("thirtieth_map.osm");
   //theTP.loadOSM("all_eugene.osm");  
   
   %loadTime = getRealTime() - %beforeTime;
   echo("load time: " @ %loadTime );
   
}

function makeStreets()
{
   %sqlite = new SQLiteObject(sqlite);
   %dbname = "w130n40.db";
   %sqlite.openDatabase(%dbname);
   
   %query = "SELECT osmId,type,name FROM osmWay;";  
	%result = sqlite.query(%query, 0);
   if (%result)
   {	   
      while (!sqlite.endOfResult(%result))
      {
         %wayId = sqlite.getColumn(%result, "osmId");
         %wayType = sqlite.getColumn(%result, "type");         
         %wayName = sqlite.getColumn(%result, "name");
         echo("found a way: " @ %wayName);
         if ((%wayType $= "residential")||
               (%wayType $= "tertiary")||
               (%wayType $= "trunk")||
               (%wayType $= "trunk_link")||
               (%wayType $= "motorway")||
               (%wayType $= "motorway_link")||
               (%wayType $= "service")||
               (%wayType $= "footway")||
               (%wayType $= "path"))
         {   
            
            //Width
            %roadWidth = 10.0;       
            if ((%wayType $= "tertiary")||(%wayType $= "trunk_link"))
               %roadWidth = 18.0; 
            else if ((%wayType $= "trunk")||(%wayType $= "motorway_link"))
               %roadWidth = 32.0; 
            else if (%wayType $= "motorway")
               %roadWidth = 40.0; 
            else if (%wayType $= "footway")
               %roadWidth = 2.5; 
            else if (%wayType $= "path")
               %roadWidth = 5.0; 
            
            //Material
            %roadMaterial = "DefaultDecalRoadMaterial";
            if (%wayType $= "footway")
               %roadMaterial = "DefaultRoadMaterialPath";
            else if ((%wayType $= "service")||(%wayType $= "path"))
               %roadMaterial = "DefaultRoadMaterialOther";
               
            //now, query the osmWayNode and osmNode tables to get the list of points
            %node_query = "SELECT wn.nodeId,n.latitude,n.longitude,n.type,n.name from " @ 
                           "osmWayNode wn JOIN osmNode n ON wn.nodeId = n.osmId " @
                           "WHERE wn.wayID = " @ %wayId @ ";";
            %result2 = sqlite.query(%node_query, 0);
            if (%result2)
            {	   
               echo("query2 results: " @ sqlite.numRows(%result2));
               %nodeString = "";
               while (!sqlite.endOfResult(%result2))
               {
                  %latitude = sqlite.getColumn(%result2, "latitude");
                  %longitude = sqlite.getColumn(%result2, "longitude");
                  %pos = theTP.convertLatLongToXYZ(%longitude @ " " @ %latitude @ " 0.0");
                  %type = sqlite.getColumn(%result2, "type");         
                  %name = sqlite.getColumn(%result2, "name");               
                  echo("node latitude " @ %latitude @ " longitude " @ %longitude @
                       " type " @ %type @ " name " @ %name );
                  %nodeString = %nodeString @ " Node = \"" @ %pos @ " " @ %roadWidth @ "\";";                      
                  sqlite.nextRow(%result2);
               }            
            }
           // " Node = \"0.0 0.0 300.0 30.000000\";" @
            echo( %nodeString );
            //Then, do the new DecalRoad, execed in order to get a loop into the declaration.
            %roadString = "      new DecalRoad() {" @
               " Material = \"" @ %roadMaterial @ "\";" @
               " textureLength = \"25\";" @
               " breakAngle = \"3\";" @
               " renderPriority = \"10\";" @
               " position = \"-8930.98 14017.1 109.587\";" @
               " rotation = \"1 0 0 0\";" @
               " scale = \"1 1 1\";" @
               " canSave = \"1\";" @
               " canSaveDynamicFields = \"1\";" @
               %nodeString @
            "};";
         
            eval(%roadString); 
         }
         
         sqlite.nextRow(%result);
      }
   } else echo ("no results.");
   %sqlite.delete();
}

/*
function streetMap()
 {   
    %xml = new SimXMLDocument() {};
    %xml.loadFile( "only_kincaid_map.osm" );
     
    // "Get" inside of the root element, "Students".     
    %result = %xml.pushChildElement("osm");  
    %version = %xml.attribute("version");     
    %generator = %xml.attribute("generator");      
    // "Get" into the first child element    
    %xml.pushFirstChildElement("bounds"); 
    %minlat = %xml.attribute("minlat");
    %maxlat = %xml.attribute("maxlat");
    echo("result: " @ %result @ " version: " @ %version @ ", generator " @ %generator @" minlat " @ %minlat @ " maxlat " @ %maxlat );
    while  (%xml.nextSiblingElement("node"))     
    {     
       %id = %xml.attribute("id"); 
       %lat = %xml.attribute("lat");     
       %lon = %xml.attribute("lon");    
       echo("node " @ %id @ " lat " @ %lat @ " long " @ %lon);   
       //HERE: store data in sqlite, and then read it back in the makeStreets function. 
       //Need at least a "way" table and a "node" table, plus other decorators I'm sure.
    } 
    %xml.nextSiblingElement("way");    
    echo("way: " @ %xml.attribute("id"));
    %xml.pushFirstChildElement("nd");
    echo("ref: " @ %xml.attribute("ref"));
    while (%xml.nextSiblingElement("nd")) 
    {
       echo("ref: " @ %xml.attribute("ref"));
    }
    while (%xml.nextSiblingElement("tag"))
    {
       echo("k: " @ %xml.attribute("k") @ "  v: " @ %xml.attribute("v") );
    }
    
 }  */
   
   
function makeRunways()
{
    new DecalRoad() {
      Material = "DefaultDecalRoadMaterial";
      textureLength = "25";
      breakAngle = "3";
      renderPriority = "10";
      position = "-8930.98 14017.1 109.587";
      rotation = "1 0 0 0";
      scale = "1 1 1";
      canSave = "1";
      canSaveDynamicFields = "1";

      Node = "-8930.984375 14017.139648 109.587013 30.000000";
      Node = "-10035.976563 12977.502930 110.499863 30.000000";
   };  
   
   //new MeshRoad() {
      //topMaterial = "DefaultRoadMaterialTop";
      //bottomMaterial = "DefaultRoadMaterialOther";
      //sideMaterial = "DefaultRoadMaterialOther";
      //textureLength = "25";
      //breakAngle = "3";
      //widthSubdivisions = "0";
      //position = "-9546.98 15036.5 109.777";
      //rotation = "1 0 0 0";
      //scale = "1 1 1";
      //canSave = "1";
      //canSaveDynamicFields = "1";
//
      //Node = "-9546.98 15036.5 109.777 30 5 0 0 1";
      //Node = "-9522.87 12639.8 113.491 30 5 0 0 1";
   //};
}
function makeM4(%start)
{   
   //physicsSetTimeScale(0.5);
   //pdd(1);//physics debug draw
   
   %dyn = false;
   %grav = true;
   %ambient = true;
  
   //%start = "0 0 0.1";
   //%start = "415 6515 195";
   %start = "1800 1820 140";
   $m4 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 0";
      //scale = "0.5 0.5 0.5";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m4); 
   
   
   
   //%start = "3 -3 1";
   %start = "1805 1820 140";
   $m5 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 -100";
      scale = "0.1 0.1 0.1";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m5); 
   
   /*
   //%start = "-3 3 1";
   %start = "425 6515 195";
   $m6 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 70";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m6);
   
   //%start = "3 3 1";
   %start = "410 6510 195";
   $m7 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 180";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m7); 

   %start = "405 6515 195";
   $m8 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 0";
      //scale = "0.5 0.5 0.5";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m8); 
   
   //%start = "3 -3 1";
   %start = "410 6520 195";
   $m9 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 -100";
      scale = "0.1 0.1 0.1";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m9); 
   
   //%start = "-3 3 1";
   %start = "412 6518 195";
   $m10 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 70";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m10);
   
   //%start = "3 3 1";
   %start = "420 6500 195";
   $m11 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 180";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m11); 
   
   %start = "415 6495 195";
   $m12 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 0";
      //scale = "0.5 0.5 0.5";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m12); 
   
   //%start = "3 -3 1";
   %start = "420 6490 195";
   $m13 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 -100";
      scale = "0.1 0.1 0.1";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m13); 
   
   //%start = "-3 3 1";
   %start = "425 6495 195";
   $m14 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 70";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m14);
   
   //%start = "3 3 1";
   %start = "420 6510 195";
   $m15 = new PhysicsShape() {
      playAmbient = %ambient;
      dataBlock = "M4Physics";
      position = %start;
      rotation = "0 0 1 180";
      canSave = "1";
      canSaveDynamicFields = "1";
      areaImpulse = "0";
      damageRadius = "0";
      invulnerable = "0";
      minDamageAmount = "0";
      radiusDamage = "0";
      hasGravity = %grav;
      isDynamic = %dyn;
   };
   MissionGroup.add($m15); 
   */
}

function m4D()
{//TEMP, this will all be automatic

   //$m4.setDynamic(1);  
   //$m5.setDynamic(1);  
   //$m6.setDynamic(1); 
   //$m7.setDynamic(1); 
   
   $m4.setBehavior("baseTree");     
   $m5.setBehavior("baseTree");  

   /*
   $m6.setBehavior("baseTree");  
   $m7.setBehavior("baseTree");  
   $m8.setBehavior("baseTree");  
   $m9.setBehavior("baseTree");  
   $m10.setBehavior("baseTree");  
   $m11.setBehavior("baseTree");  
   $m12.setBehavior("baseTree");  
   $m13.setBehavior("baseTree");  
   $m14.setBehavior("baseTree");  
   $m15.setBehavior("baseTree");  
   */
   //$m4.aitp(2,"0 0 0");  
   //$m5.aitp(2,"0 0 0");
   //$m6.aitp(2,"0 0 0");
   //$m7.aitp(2,"0 0 0");
}

function m4P()
{
   $m4.setDynamic(1);  
   //$m5.setDynamic(1);  
   //$m6.setDynamic(1); 
   //$m7.setDynamic(1); 
   
   //$m4.aitp(2,"-30 0 30");  
   //$m5.aitp(2,"0 -30 30");
   //$m6.aitp(2,"30 0 30");
   //$m7.aitp(2,"0 30 30");
}

//Joint debugging, Chest Kinematic
function m4CK()
{
   
   $m4.setPartDynamic(2,0);
   //$m5.setPartDynamic(2,0);
   //$m6.setPartDynamic(2,0);
   //$m7.setPartDynamic(2,0);
}
