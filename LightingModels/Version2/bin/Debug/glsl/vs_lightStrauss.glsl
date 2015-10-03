	#version 330

in vec3 vPosition;
in vec3 vNormal;
in  vec3 vColor;
in vec2 texcoord;

out vec3 f_normal;
out vec2 f_texcoord;
out vec3 f_vertPos;
out vec4 f_color;

uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform mat4 projectionMatrix;

void main()
{
	vec4 pos = viewMatrix * modelMatrix * vec4(vPosition, 1.0);
	f_vertPos = pos.xyz;
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(vPosition, 1.0);
	f_normal = vec3(modelMatrix * viewMatrix * vec4(vNormal, 0.0));
	
	f_texcoord = texcoord;

	f_color = vec4( vColor, 1.0);
}