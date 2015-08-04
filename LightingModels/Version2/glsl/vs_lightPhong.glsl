#version 330

in vec3 vPosition;
in vec3 vColor;
in vec3 vNormal;
in vec2 texcoord;

out vec3 normalInterp;
out vec3 vertPos;
out vec2 f_texcoord;

uniform mat4 modelView;
uniform mat4 modelViewMatrix;
uniform mat4 projectionMatrix;

void main()
{
  vec4 pos = modelViewMatrix * vec4(vPosition, 1.0);
  vertPos = pos.xyz;
  gl_Position = projectionMatrix * pos;
  normalInterp = vec3(modelViewMatrix * vec4(vNormal, 0.0));

  // texture
  f_texcoord = texcoord;
}
