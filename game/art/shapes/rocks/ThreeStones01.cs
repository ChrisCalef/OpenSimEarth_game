
singleton TSShapeConstructor(ThreeStones01DAE)
{
   baseShape = "./ThreeStones01.DAE";
   loadLights = "0";
};

function ThreeStones01DAE::onLoad(%this)
{
   %this.removeImposter();
}
