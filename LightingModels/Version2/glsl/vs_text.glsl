#version 330

in vec3 vPosition;
in vec2 texcoord;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;

out vec2 f_texcoord;

void main()
{
    gl_Position = projectionMatrix * viewMatrix * modelMatrix  * vec4(vPosition, 1.0);
    f_texcoord = texcoord;
}