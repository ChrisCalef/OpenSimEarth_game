
singleton Material(f6f_hellcat_DefaultWhite)
{
   mapTo = "DefaultWhite";
   diffuseColor[0] = "0.8 0.8 0.8 1";
   specular[0] = "0.32 0.32 0.32 1";
   specularPower[0] = "50";
   translucentBlendOp = "None";
};

singleton Material(f6f_hellcat_DefaultWhite_001)
{
   mapTo = "DefaultWhite_001";
   diffuseColor[0] = "0.8 0.8 0.8 1";
   specular[0] = "0.32 0.32 0.32 1";
   specularPower[0] = "50";
   translucentBlendOp = "None";
   diffuseMap[0] = "art/shapes/fg_convert/F6F-Hellcat/texture.png";
};

singleton Material(f6f_hellcat_texture)
{
   mapTo = "f6f_hellcat_texture";
   diffuseMap[0] = "texture.png";
   specular[0] = "1 1 1 1";
   specularPower[0] = "1";
   doubleSided = "1";
   translucentBlendOp = "None";
};
