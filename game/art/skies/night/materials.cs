//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

singleton CubemapData( NightCubemap )
{
   cubeFace[0] = "./skybox_1";
   cubeFace[1] = "./skybox_2";
   cubeFace[2] = "./skybox_3";
   cubeFace[3] = "./skybox_4";
   cubeFace[4] = "./skybox_5";
   cubeFace[5] = "./skybox_6";
};

singleton Material( NightSkyMat )
{
   cubemap = NightCubemap;
   materialTag0 = "Skies";
};

singleton Material( Moon_Glow_Mat )
{
   baseTex = "./moon_wglow.png";
   emissive = true;
   translucent = true;
   vertColor[ 0 ] = true;
};

singleton Material( Moon_Mat )
{
   baseTex = "./moon_noglow.png";
   emissive = true;
   translucent = true;
   vertColor[ 0 ] = true;
};

//////// TEMP, move to its own file /////////////////
singleton CubemapData( WS_SkyboxCubemap )
{
   //cubeFace[0] = "../../terrains/WorldServer/skybox3_00";//Original versions from flightgear,
   //cubeFace[1] = "../../terrains/WorldServer/skybox3_270";//before being flipped and rotated
   //cubeFace[2] = "../../terrains/WorldServer/skybox3_180";//for Torque's weird skyboxes.
   //cubeFace[3] = "../../terrains/WorldServer/skybox3_90";
   //cubeFace[4] = "../../terrains/WorldServer/skybox3_up";
   cubeFace[0] = "./skybox_90";//"./skybox3_270";//RIGHT = EAST
   cubeFace[1] = "./skybox_270";////"./skybox3_180";//LEFT = WEST
   cubeFace[2] = "./skybox_00";//"./skybox3_00";//FORWARD = NORTH
   cubeFace[3] = "./skybox_180";//BACKWARD = SOUTH
   cubeFace[4] = "./skybox_up";//UP
   cubeFace[5] = "./skybox_6";//DOWN
};

singleton Material( WS_SkyboxMaterial )
{
   cubemap = WS_SkyboxCubemap;
   materialTag0 = "Skies";
};
