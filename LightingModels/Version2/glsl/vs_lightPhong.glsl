#version 330

in vec3 vPosition;
in vec3 vNormal;
in vec2 texcoord;

out vec3 f_normal;
out vec2 f_texcoord;
out vec3 f_vertPos;

uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 projectionMatrix;

void main()
{
	vec4 pos = modelMatrix * viewMatrix * vec4(vPosition, 1.0);
	f_vertPos = pos.xyz;
	gl_Position = projectionMatrix * pos;
	f_normal = vec3(modelMatrix * viewMatrix * vec4(vNormal, 0.0));
	
	f_texcoord = texcoord;
}