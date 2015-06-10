//--- OBJECT WRITE BEGIN ---
new Root(goToTargetTree) {
   canSave = "1";
   canSaveDynamicFields = "1";

   new Sequence() {
      canSave = "1";
      canSaveDynamicFields = "1";

      new ScriptedBehavior() {
         preconditionMode = "ONCE";
         internalName = "look for target";
         class = "findTarget";
         canSave = "1";
         canSaveDynamicFields = "1";
      };
      //new Loop() {
         //numLoops = "0";
         //terminationPolicy = "ON_SUCCESS";
         //canSave = "1";
         //canSaveDynamicFields = "1";
         
         new ScriptedBehavior() {
            preconditionMode = "TICK";
            internalName = "go to target";
            class = "goToTarget";
            canSave = "1";
            canSaveDynamicFields = "1";
         };
      //};
   };
};
//--- OBJECT WRITE END ---


//=============================================================================
// goToTarget task
//=============================================================================
function goToTarget::precondition(%this, %obj)
{
   // check that we have a valid health item to go for
   return (isObject(%obj.targetItem) && %obj.targetItem.isEnabled() && !%obj.targetItem.isHidden());  
}

function goToTarget::onEnter(%this, %obj)
{
   // move to the item
   %obj.moveTo(%obj.targetItem);  
   //%obj.orientToPos(%obj.targetItem.position);
   //%obj.seqRun();
}

function goToTarget::behavior(%this, %obj)
{
   // succeed when we reach the item
   //HERE: we need targetitem position to be on the ground, not at the actual position, 
   //or else we can never be closer than the height of the object.
   %groundPos = %obj.findGroundPosition(%obj.targetItem.position);
   %diff = VectorSub(%groundPos,%obj.getClientPosition());
   //if(!%obj.atDestination)
   
   //echo(%obj.getId() @ " is looking for target, my position " @ %obj.getClientPosition() @ 
   //" target position " @ %groundPos @  " distance = " @ VectorLen(%diff) );
   
   if ( VectorLen(%diff) > %obj.dataBlock.foundItemDistance )
      return RUNNING;
   
   return SUCCESS;
}