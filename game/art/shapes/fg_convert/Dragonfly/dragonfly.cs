
singleton TSShapeConstructor(DragonflyDts)
{
   baseShape = "./dragonfly.dts";
};

function DragonflyDts::onLoad(%this)
{
   %this.addNode("eye", "", "-0.35 -0.0 0.85 0 0 1 0", "0");
   %this.addNode("Col-1", "", "0 0 0 0 0 1 0", "0");
   %this.addNode("ColBoxI-1_drg", "Col-1", "3.72717 -0.770005 0.504398 -0.57735 -0.57735 -0.57735 2.09439", "0");
   //FIX need mesh based collisions, not a sphere
   %this.addCollisionDetail("-1", "Sphere", "Bounds", "4", "30", "30", "32", "30", "30", "30");
}
