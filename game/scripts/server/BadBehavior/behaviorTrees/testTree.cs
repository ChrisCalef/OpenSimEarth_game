//--- OBJECT WRITE BEGIN ---
new Root(testTree) {
   canSave = "1";
   canSaveDynamicFields = "1";

   new ScriptedBehavior(testBehave) {
      preconditionMode = "ONCE";
      internalName = "testing tree";
      class = "testBehaveTask";
      canSave = "1";
      canSaveDynamicFields = "1";
   };
};
//--- OBJECT WRITE END ---
