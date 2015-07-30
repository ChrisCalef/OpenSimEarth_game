
singleton TSShapeConstructor(SM_Rock_01DAE)
{
   baseShape = "./SM_Rock_01.DAE";
};

function SM_Rock_01DAE::onLoad(%this)
{
   %this.removeImposter();
}
