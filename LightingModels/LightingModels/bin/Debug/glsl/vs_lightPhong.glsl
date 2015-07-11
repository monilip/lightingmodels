#version 330

in vec3 vPosition;
in vec3 vColor;
in vec3 vNormal;

out vec3 normalInterp;
out vec3 vertPos;

uniform mat4 modelView;
uniform mat4 modelViewMatrix;
uniform mat4 projectionMatrix;

// tekstura
in vec2 texcoord;
out vec2 f_texcoord;


void main()
{
  vec4 pos = modelViewMatrix * vec4(vPosition, 1.0);
  vertPos = pos.xyz;
  gl_Position = projectionMatrix * pos;
  normalInterp = vec3(modelViewMatrix * vec4(vNormal, 0.0));

  //tekstura
   f_texcoord = texcoord;

}
