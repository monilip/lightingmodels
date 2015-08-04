#version 330

in vec2 f_texcoord;
in vec3 f_normal;

out vec4 outputColor;

uniform sampler2D maintexture;
uniform vec3 lightDirection;
uniform bool enableLighting;

void main(void)
{
	float diffuse = max(dot(f_normal, lightDirection), 0);
    float ambient = 0.3;
    float lighting = (enableLighting ? max(diffuse, ambient) : 1);

	vec4 color = texture2D(maintexture, f_texcoord);
    outputColor = lighting * color;
}
