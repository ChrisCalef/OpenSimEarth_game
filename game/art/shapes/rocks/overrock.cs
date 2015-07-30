
singleton TSShapeConstructor(OverrockDAE)
{
   baseShape = "./overrock.DAE";
   loadLights = "0";
};

function OverrockDAE::onLoad(%this)
{
   %this.removeImposter();
}
