//--- OBJECT WRITE BEGIN ---
new Root(baseTree) {
   canSave = "1";
   canSaveDynamicFields = "1";

   new Sequence() {
      canSave = "1";
      canSaveDynamicFields = "1";

      //new ScriptFunc() {
         //func = "onStartup";
         //defaultReturnStatus = "SUCCESS";
         //canSave = "1";
         //canSaveDynamicFields = "1";
      //};

      new ScriptedBehavior() {
         preconditionMode = "TICK";
         internalName = "on startup";
         class = "onStartup";
         canSave = "1";
         canSaveDynamicFields = "1";
      };      
      new SubTree() {
         subTreeName = "goToTargetTree";
         internalName = "go to target";
         canSave = "1";
         canSaveDynamicFields = "1";
      };
      new ScriptEval() {
         behaviorScript = "%obj.seqIdle();";
         defaultReturnStatus = "SUCCESS";
         canSave = "1";
         canSaveDynamicFields = "1";
      };
      new RandomWait() {
         waitMinMs = "5000";
         waitMaxMs = "10000";
         canSave = "1";
         canSaveDynamicFields = "1";
      };
      new RandomSelector() {
         canSave = "1";
         canSaveDynamicFields = "1";

         new ScriptEval() {
            behaviorScript = "%obj.moveTo(%obj.getPosition()); %obj.seqRun();";
            defaultReturnStatus = "SUCCESS";
            canSave = "1";
            canSaveDynamicFields = "1";
         };
         new ScriptEval() {
            behaviorScript = "%obj.seqAttack();";
            defaultReturnStatus = "SUCCESS";
            canSave = "1";
            canSaveDynamicFields = "1";
         };
         new ScriptEval() {
            behaviorScript = "%obj.seqBlock();";
            defaultReturnStatus = "SUCCESS";
            canSave = "1";
            canSaveDynamicFields = "1";
         };
      };
      new RandomWait() {
         waitMinMs = "10000";
         waitMaxMs = "20000";
         canSave = "1";
         canSaveDynamicFields = "1";
      };
   };
};
//--- OBJECT WRITE END ---
