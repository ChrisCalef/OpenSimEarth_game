//--- OBJECT WRITE BEGIN ---
new Root(baseTree) {
   canSave = "1";
   canSaveDynamicFields = "1";


   new Sequence() {
      canSave = "1";
      canSaveDynamicFields = "1";      
      
      new ScriptEval() {
         behaviorScript = "%obj.onStartup();";
         defaultReturnStatus = "SUCCESS";
         canSave = "1";
         canSaveDynamicFields = "1";
      };

      new Loop() {
         numLoops = "0";
         terminationPolicy = "ON_SUCCESS";
         canSave = "1";
         canSaveDynamicFields = "1";
         
         new Sequence() {
            canSave = "1";
            canSaveDynamicFields = "1";      
            
            new ScriptEval() {
               behaviorScript = "%obj.setDynamic(0); %obj.actionSeq(\"idle\");";
               defaultReturnStatus = "SUCCESS";
               canSave = "1";
               canSaveDynamicFields = "1";
            };
            new SubTree() {
               subTreeName = "goToTargetTree";
               internalName = "go to target";
               canSave = "1";
               canSaveDynamicFields = "1";
            };
            new RandomWait() {
               waitMinMs = "5000";
               waitMaxMs = "7000";
               canSave = "1";
               canSaveDynamicFields = "1";
            };
            new ScriptEval() {
               behaviorScript = "%obj.actionSeq(\"attack\");";
               defaultReturnStatus = "SUCCESS";
               canSave = "1";
               canSaveDynamicFields = "1";
            };
         };
      };
   };
};
//--- OBJECT WRITE END ---
