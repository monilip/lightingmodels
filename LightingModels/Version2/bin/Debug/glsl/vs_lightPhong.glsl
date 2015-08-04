#version 330

in vec3 vPosition;
in vec3 vColor;
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
	f_texcoord = texcoord;
	f_normal =  normalize((modelMatrix * vec4(floor(vNormal), 0)).xyz);
	vec4 pos = viewMatrix * modelMatrix * vec4(vPosition, 1.0);
	f_vertPos =  pos.xyz;
	gl_Position = projectionMatrix * pos;
}
