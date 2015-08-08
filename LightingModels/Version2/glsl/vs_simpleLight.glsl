#version 330

in vec3 vPosition;
in vec3 vNormal;
in vec2 texcoord;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

out vec2 f_texcoord;
out vec3 f_normal;

void main(void)
{
    gl_Position = projectionMatrix * viewMatrix * modelMatrix  * vec4(vPosition, 1.0);
    f_texcoord = texcoord;
	f_normal =  normalize((modelMatrix * vec4(floor(vNormal), 0)).xyz);
}

