singleton TSShapeConstructor(M4Dts)
{
   baseShape = "./M4.dts";
};

function M4Dts::onLoad(%this)
{
   %this.addSequence("art/shapes/m4_optimized/TPose.dsq", "tpose", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/Root4.dsq", "ambient", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/CMU_16_22.dsq", "walk", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/MedRun6.dsq", "run", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/runscerd1.dsq", "runscerd", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/backGetup.dsq", "backGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/frontGetup.dsq", "frontGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/rSideGetup02.dsq", "rSideGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/lSideGetup02.dsq", "lSideGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/zombiePunt2.dsq", "zombiePunt", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/Mime.dsq", "mime", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/Sneak.dsq", "sneak", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/power_punch_down.dsq", "power_punch_down", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/punch_uppercut.dsq", "punch_uppercut", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/RoundHouse.dsq", "roundhouse", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/Strut.dsq", "strut", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/soldier_march.dsq", "walk2", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/01_13_swing_under_grcap.dsq", "swingUnder", "0", "-1");

   %this.addNode("Col-1","root");
   %this.addCollisionDetail(-1,"box","bounds");
   
   %this.setSequenceCyclic("ambient", "1");
   %this.setSequenceCyclic("walk", "1");
   %this.setSequenceCyclic("strut", "1");   
   %this.setSequenceCyclic("march", "1");
   %this.setSequenceCyclic("run", "1");
   %this.setSequenceCyclic("power_punch_down", "1");
   %this.setSequenceCyclic("tpose", "1");
   %this.setSequenceCyclic("swingUnder", "1");
} 
