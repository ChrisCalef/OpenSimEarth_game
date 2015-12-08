
function initializeOpenSimEarth()
{
   echo(" % - Initializing openSimEarth");
   
   //new ScriptObject(openSimEarthPlugin)
   //{
   //   superClass = "WorldEditorPlugin";
   //   editorGui = EWorldEditor;
   //};
   
   //Here: load all the scripts, and create all the guis.
   //Here is *not* the place to initialize SQL or anythign else that should be running
   //independently of the user opening the editor. This is only for functions related 
   //editing the world, not just running around in it.
   
   
   //exec( "./gui/EcstasyMotionAllWindow.gui" );
   
}

function destroyOpenSimEarth()
{
}


function openSimEarthPlugin::onWorldEditorStartup( %this )
{    
   echo("openSimEarth: world editor starting up!!!!");
    // Add ourselves to the window menu.
   //%accel = EditorGui.addToEditorsMenu( "openSimEarth", "", openSimEarthPlugin );   
   
   //// Add ourselves to the ToolsToolbar
   //%tooltip = "openSimEarth (" @ %accel @ ")";   
   //EditorGui.addToToolsToolbar( "openSimEarthPlugin", "openSimEarth", expandFilename("tools/worldEditor/images/toolbar/ecstasymotion"), %tooltip );
   
}

function openSimEarthPlugin::onActivated( %this )
{
   echo("openSimEarthPlugin Activated");
}