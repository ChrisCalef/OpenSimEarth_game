singleton TSShapeConstructor(M4Dts)
{
   baseShape = "./M4.dts";
};

function M4Dts::onLoad(%this)
{
   %this.addSequence("art/shapes/m4_optimized/sequences/TPose.dsq", "tpose", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/Root4.dsq", "ambient", "0", "-1");//
   //%this.addSequence("art/shapes/m4_optimized/sequences/test_seq.dsq", "ambient", "0", "-1");
   //%this.addSequence("art/shapes/m4_optimized/sequences/Strut.dsq", "ambient", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/CMU_16_22.dsq", "walk", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/MedRun6.dsq", "run", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/runscerd1.dsq", "runscerd", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/backGetup.dsq", "backGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/frontGetup.dsq", "frontGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/rSideGetup02.dsq", "rSideGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/lSideGetup02.dsq", "lSideGetup", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/zombiePunt2.dsq", "zombiePunt", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/Mime.dsq", "mime", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/Sneak.dsq", "sneak", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/power_punch_down.dsq", "power_punch_down", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/punch_uppercut.dsq", "punch_uppercut", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/RoundHouse.dsq", "roundhouse", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/Strut.dsq", "strut", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/soldier_march.dsq", "walk2", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/sequences/01_13_swing_under_grcap.dsq", "swingUnder", "0", "-1");
   %this.addSequence("art/shapes/m4_optimized/scene_2/shape_1.dsq", "seq_1", "0", "-1");
   //%this.addSequence("art/shapes/m4_optimized/sequences/testHeli01.dsq", "testHeli01", "0", "-1");
   //%this.addSequence("art/shapes/m4_optimized/sequences/testHeli03.dsq", "testHeli03", "0", "-1");
   
   %this.addNode("Col-1","root");
   %this.addCollisionDetail(-1,"box","bounds");
   
   %this.setSequenceCyclic("ambient", "1");
   %this.setSequenceCyclic("walk", "1");
   %this.setSequenceCyclic("strut", "1");   
   %this.setSequenceCyclic("march", "1");
   %this.setSequenceCyclic("run", "1");
   //%this.setSequenceCyclic("power_punch_down", "1");
   %this.setSequenceCyclic("tpose", "1");
   %this.setSequenceCyclic("swingUnder", "1");
   //%this.setSequenceCyclic("testHeli03", "1");
   //%this.setSequenceCyclic("seq_1", "1");
   //%this.setSequenceCyclic("seq_2", "1");
} 
