#version 330

precision mediump float;

in vec3 f_normal;
in vec3 f_vertPos;
in vec4 f_color;
in vec2 f_texcoord;

out vec4 outputColor;

uniform sampler2D maintexture;

uniform vec3 lightPos;
uniform vec3 diffuseColor;

uniform bool isTexture;

void main() 
{
	// Lambert
	// I = diffuse * diffuseColor;
	// diffuse = cos(A), A -> angle between light direction and normal

	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);

	float lambert = max(dot(N,L), 0.0);

	vec3 Dif = diffuseColor;

	// texture or color
	if (isTexture == true)
	{
		Dif.r +=texture2D(maintexture, f_texcoord).r;
		Dif.g +=texture2D(maintexture, f_texcoord).g;
		Dif.b +=texture2D(maintexture, f_texcoord).b;
	}
	else
	{
		Dif.r +=f_color.r;
		Dif.g +=f_color.g;
		Dif.b +=f_color.b;
	}
	
	Dif = Dif * lambert;

	outputColor = vec4(Dif, 1.0f);
}