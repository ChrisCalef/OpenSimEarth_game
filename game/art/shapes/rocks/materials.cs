//--- okok.DAE MATERIALS BEGIN ---
singleton Material(okok_Clifford)
{
	mapTo = "Clifford";

	diffuseMap[0] = "stone2fixAA";
	normalMap[0] = "RockNormal01";
	specularMap[0] = "";

	diffuseColor[0] = "1 1 1 1";
	specular[0] = "0.9 0.9 0.9 1";
	specularPower[0] = 45;
	pixelSpecular[0] = 1;
	emissive[0] = false;

	doubleSided = 1;
	translucent = false;
	translucentBlendOp = "None";
   useAnisotropic[0] = "1";
};

//--- okok.DAE MATERIALS END ---


singleton Material(DefaultMaterial2)
{
   mapTo = "stone2fix";
   diffuseMap[0] = "stone2fixAA";
   normalMap[0] = "rock01_NRM";
   doubleSided = "1";
   specularMap[0] = "Cliffs0042_M_SPEC";
   useAnisotropic[0] = "1";
   subSurfaceColor[0] = "0.00392157 0 0 1";
};

singleton Material(sqRock_base01)
{
   mapTo = "base01";
   diffuseColor[0] = "0.588235 0.588235 0.588235 1";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "45";
   translucentBlendOp = "None";
   diffuseMap[0] = "rock01a";
   normalMap[0] = "RockNormal02";
};

singleton Material(wowRock01_Rockyface)
{
   mapTo = "Rockyface";
   diffuseMap[0] = "stone2fix";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "45";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1";
};

singleton Material(Coll_Rock01_CliffFace002)
{
   mapTo = "CliffFace002";
   diffuseMap[0] = "stone2fixAA";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "45";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1";
   doubleSided = "1";
   detailScale[0] = "1 1";
   detailNormalMap[0] = "normal_rock1";
   detailNormalMapStrength[0] = "3";
   materialTag0 = "RoadAndPath";
};

singleton Material(Coll_Erod_Rock_01_Dirt)
{
   mapTo = "Dirt";
   diffuseMap[0] = "SoilRough0066_57_S";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "45";
   translucentBlendOp = "None";
   normalMap[0] = "SoilRough0066_57_S_NRM";
   doubleSided = "1";
};

singleton Material(Coll_Erod_Rock_01_Grass)
{
   mapTo = "Grass";
   diffuseMap[0] = "Grass0032_L";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
};

singleton Material(SM_Rock_01_Rocky001)
{
   mapTo = "Rocky001";
   diffuseMap[0] = "rock01";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "116";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1";
   doubleSided = "1";
   specularMap[0] = "rock01_DISP";
   useAnisotropic[0] = "1";
};

singleton Material(_3Stones01_Stones001)
{
   mapTo = "Stones001";
   diffuseMap[0] = "rock01a.dds";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
   normalMap[0] = "rock01_NRM2.dds";
   detailNormalMap[0] = "normal_rock1";
   useAnisotropic[0] = "1";
   doubleSided = "1";
   detailScale[0] = "1 1";
   materialTag0 = "RoadAndPath";
   detailNormalMapStrength[0] = "2";
};

singleton Material(overrock_Overhang)
{
   mapTo = "Overhang";
   diffuseMap[0] = "rock01a.dds";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1.dds";
   useAnisotropic[0] = "1";
   doubleSided = "1";
   detailNormalMap[0] = "normal_rock1";
   materialTag0 = "RoadAndPath";
};

singleton Material(overrock_Overhang)
{
   mapTo = "Overhang";
   diffuseMap[0] = "rock01a";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1";
   detailNormalMap[0] = "normal_rock1";
};

singleton Material(Stone02)
{
   mapTo = "Stone02";
   diffuseMap[0] = "rock01a.dds";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1.dds";
   detailScale[0] = "1 1";
};

singleton Material(wtf_Stones002)
{
   mapTo = "Stones002";
   diffuseMap[0] = "rock01a.dds";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1.dds";
};

singleton Material(Overhang02_OverRock02)
{
   mapTo = "OverRock02";
   diffuseMap[0] = "rock01a.dds";
   specular[0] = "0.9 0.9 0.9 1";
   specularPower[0] = "10";
   translucentBlendOp = "None";
   normalMap[0] = "normal_rock1.dds";
   useAnisotropic[0] = "1";
};
