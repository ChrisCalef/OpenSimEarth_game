




singleton Material(ka50_prop_texture)
{
   mapTo = "DefaultWhite_015";
   diffuseMap[0] = "art/shapes/fg_convert/ka50/Models/MainRotor/prop.png";
   specular[0] = "0.32 0.32 0.32 1";
   specularPower[0] = "50";
   doubleSided = "1";
   translucentBlendOp = "None";
};


singleton Material(MainRotor_ka50RotorTexture)
{
   mapTo = "unmapped_mat";
   diffuseColor[0] = "0.8 0.8 0.8 1";
   specular[0] = "0.32 0.32 0.32 1";
   specularPower[0] = "50";
   doubleSided = "1";
   translucentBlendOp = "None";
   materialTag0 = "Miscellaneous";
};

singleton Material(ka50_rotor_texture)
{
   mapTo = "ka50RotorTexture";
   diffuseMap[0] = "art/shapes/fg_convert/ka50/Models/MainRotor/colors.png";
   specular[0] = "0.32 0.32 0.32 1";
   specularPower[0] = "50";
   translucentBlendOp = "None";
   materialTag0 = "Miscellaneous";
};
