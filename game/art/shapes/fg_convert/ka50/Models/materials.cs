
singleton Material(ka50_verttrans)
{
   mapTo = "ka50_verttrans";
   diffuseColor[0] = "0.0706408 0.714146 0.221317 0.7";
   specular[0] = "0.64 0.64 0.64 1";
   specularPower[0] = "50";
   doubleSided = "1";
   translucent = "1";
};

singleton Material(ka50_rougetrans)
{
   mapTo = "ka50_rougetrans";
   diffuseColor[0] = "0.766179 0.0508712 0.0508712 0.7";
   specular[0] = "0.64 0.64 0.64 1";
   specularPower[0] = "50";
   doubleSided = "1";
   translucent = "1";
};


singleton Material(water_mat)
{
   mapTo = "transparent";
   //diffuseColor[0] = "0.0706408 0.714146 0.221317 0.7";
   diffuseColor[0] = "0.5706408 0.614146 0.621317 0.5";
   specular[0] = "0.64 0.64 0.64 1";
   specularPower[0] = "50";
   doubleSided = "1";
   translucent = "1";
};

singleton Material(ka50_texture)
{
   mapTo = "DefaultWhite";
   diffuseMap[0] = "art/shapes/fg_convert/ka50/Models/texture";
   specular[0] = "0 0 0 1";
   specularPower[0] = "50";
   doubleSided = "1";
   translucentBlendOp = "None";
};
