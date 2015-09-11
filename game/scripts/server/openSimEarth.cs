

//-----------------------------------------------------------------------------
// OpenSimEarth
//-----------------------------------------------------------------------------

function startSQL(%dbname)
{//Create the sqlite object that we will use in all the scripts.
   %sqlite = new SQLiteObject(sqlite);
   
   if (%sqlite.openDatabase(%dbname))
      echo("Successfully opened database: " @ %dbname );
   else 
      echo("We had a problem involving database: " @ %dbname );
      
}

function stopSQL()
{
   sqlite.closeDatabase();
   sqlite.delete();      
}

function openSimEarthGUIs()
{
   //Eventually load up all OpenSimEarth-related GUI objects from the DB.
   //For now this just fills the DatabaseSceneList dropdown.
   %query = "SELECT id,name,description from scene;";  
	%result = sqlite.query(%query, 0);
   if (%result)
   {	   
      while (!sqlite.endOfResult(%result))
      {
         %id = sqlite.getColumn(%result, "id");        
         %name = sqlite.getColumn(%result, "name");
         %descrip = sqlite.getColumn(%result, "description"); 
         echo("Found a scene: " @ %id @ " " @ %name @ " : " @ %descrip );
         
         DatabaseSceneList.add(%name,%id);
         
         sqlite.nextRow(%result);
      }
   } 
}


function loadScene(%scene_id)
{
   //pdd(1);//physics debug draw
   
   %dyn = false;
   %grav = true;
   %ambient = true;
   
   //HERE: next step, we need to have a behavior tree name under sceneShape, and each
   //new shape needs to have its behavior assigned at create time, here.
	%query = "SELECT ss.id as ss_id,shape_id,shapeGroup_id,behaviorTree," @ 
	         "p.x as pos_x,p.y as pos_y,p.z as pos_z," @ 
	         "r.x as rot_x,r.y as rot_y,r.z as rot_z,r.angle as rot_angle " @ 
	         "FROM sceneShape ss " @ 
	         "LEFT JOIN vector3 p ON ss.pos_id=p.id " @ 
	         "LEFT JOIN rotation r ON ss.rot_id=r.id " @ 
	         "WHERE scene_id=" @ %scene_id @ ";";  
	%result = sqlite.query(%query, 0);
	//echo( %query );
   if (%result)
   {	   
      while (!sqlite.endOfResult(%result))
      {
         %sceneShape_id = sqlite.getColumn(%result, "ss_id");   
         %shape_id = sqlite.getColumn(%result, "shape_id");
         %shapeGroup_id = sqlite.getColumn(%result, "shapeGroup_id");//not used yet
         %behaviorTree = sqlite.getColumn(%result, "behaviorTree");
         
         %pos_x = sqlite.getColumn(%result, "pos_x");
         %pos_y = sqlite.getColumn(%result, "pos_y");
         %pos_z = sqlite.getColumn(%result, "pos_z");
         
         %rot_x = sqlite.getColumn(%result, "rot_x");
         %rot_y = sqlite.getColumn(%result, "rot_y");
         %rot_z = sqlite.getColumn(%result, "rot_z");
         %rot_angle = sqlite.getColumn(%result, "rot_angle");
         
         echo("Found a sceneShape: " @ %sceneShape_id @ " " @ %pos_x @ " " @ %pos_y @ " " @ %pos_z @
                " " @ %rot_x @ " " @ %rot_y @ " " @ %rot_z @ " " @ %rot_angle);
                
         %position = %pos_x @ " " @ %pos_y @ " " @ %pos_z;
         %rotation = %rot_x @ " " @ %rot_y @ " " @ %rot_z @ " " @ %rot_angle;
         %temp =  new PhysicsShape() {
            playAmbient = %ambient;
            dataBlock = "M4Physics";
            position = %position;
            rotation = %rotation;
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
            sceneShapeID = %sceneShape_id;
            sceneID = %scene_id;
            targetType = "Health";//"AmmoClip"
         };
         
         MissionGroup.add(%temp);   
         SceneShapes.add(%temp);   
                
         if (strlen(%behaviorTree)>0)
         {
            %temp.schedule(30,"setBehavior",%behaviorTree);
            echo(%temp.getId() @ " assigning behavior tree: " @ %behaviorTree );
         }
            
         sqlite.nextRow(%result);
      }
   }   
   //schedule(40, 0, "startRecording");
} 

function unloadScene(%scene_id)
{
   //HERE: look up all the sceneShapes from the scene in question, and drop them all from the current mission.
   
}


function assignBehaviors()
{//This seems arbitrary, store initial behavior tree and dynamic status in sceneShape instead.
      
   for (%i=0;%i<SceneShapes.getCount();%i++)
   {
      %shape = SceneShapes.getObject(%i);  
      
      %shape.setBehavior("baseTree");
      
      //%shape.setDynamic(1);       
   }   
}

function startRecording()
{
   for (%i=0;%i<SceneShapes.getCount();%i++)
   {
      %shape = SceneShapes.getObject(%i);  
      %shape.setIsRecording(true);
   }   
}

function stopRecording()
{
   for (%i=0;%i<SceneShapes.getCount();%i++)
   {
      %shape = SceneShapes.getObject(%i);  
      %shape.setIsRecording(false);
   }   
}

function makeSequences()
{
   //OKAY... here we go. We now need to:
   // a) find our model's home directory   
   // b) in that directory, create a new directory with a naming protocol
   //       "scene_[%scene_id].[timestamp]"?
   // c) fill it with sequences
   
   //For now, just "workSeqs", if name changes we'll have to update M4.cs every time.
   %dirPath = %shape.getPath() @ "/scenes";
   createDirectory(%dirPath);//make shape/scenes folder first, if necessary.
   %dirPath = %shape.getPath() @ "/scenes/" @ %shape.sceneID ;//then make specific scene folder.
   for (%i=0;%i<SceneShapes.getCount();%i++)
   {
      %shape = SceneShapes.getObject(%i);  
      %dirPath = %shape.getPath() @ "/scenes/" @ %shape.sceneID ;
      %shape.makeSequence(%dirPath @ "/" @ %shape.getSceneShapeID());
   }
}

function loadOSM()  // OpenStreetMap XML data
{
   //here, read lat/long for each node as we get to it, convert it to xyz coords,
   //and save it in an array, to be used in the DecalRoad declaration.    
   
   %beforeTime = getRealTime();
   
   theTP.loadOSM("min.osm");     
   //theTP.loadOSM("kincaid_map.osm");  
   //theTP.loadOSM("central_south_eug.osm");  
   //theTP.loadOSM("thirtieth_map.osm");
   //theTP.loadOSM("all_eugene.osm");  
   
   %loadTime = getRealTime() - %beforeTime;
   echo("OpenStreetMap file load time: " @ %loadTime );
}

function makeStreets()
{
   %mapDB = new SQLiteObject(mapDB);
   %dbname = "w130n40.db";//HERE: need to find this in prefs or something.
   mapDB.openDatabase(%dbname);
   
   %query = "SELECT osmId,type,name FROM osmWay;";  
	%result = mapDB.query(%query, 0);
   if (%result)
   {	   
      while (!mapDB.endOfResult(%result))
      {
         %wayId = mapDB.getColumn(%result, "osmId");
         %wayType = mapDB.getColumn(%result, "type");         
         %wayName = mapDB.getColumn(%result, "name");
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
            %result2 = mapDB.query(%node_query, 0);
            if (%result2)
            {	   
               echo("query2 results: " @ mapDB.numRows(%result2));
               %nodeString = "";
               while (!mapDB.endOfResult(%result2))
               {
                  %latitude = mapDB.getColumn(%result2, "latitude");
                  %longitude = mapDB.getColumn(%result2, "longitude");
                  %pos = theTP.convertLatLongToXYZ(%longitude @ " " @ %latitude @ " 0.0");
                  %type = mapDB.getColumn(%result2, "type");         
                  %name = mapDB.getColumn(%result2, "name");               
                  echo("node latitude " @ %latitude @ " longitude " @ %longitude @
                       " type " @ %type @ " name " @ %name );
                  %nodeString = %nodeString @ " Node = \"" @ %pos @ " " @ %roadWidth @ "\";";                      
                  mapDB.nextRow(%result2);
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
         
         mapDB.nextRow(%result);
      }
   } else echo ("no results.");
   
   mapDB.closeDatabase();
   mapDB.delete();
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
}

//Joint debugging, Chest Kinematic
function m4CK()
{
   $m4.setPartDynamic(2,0);
}