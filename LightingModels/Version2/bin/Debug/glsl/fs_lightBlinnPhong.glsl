#version 330

precision mediump float;

in vec3 f_normal;
in vec3 f_vertPos;
in vec4 f_color;
in vec2 f_texcoord;

out vec4 outputColor;

uniform sampler2D maintexture;

uniform float n;
uniform vec3 lightPos;
uniform vec3 ambientColor;
uniform vec3 diffuseColor;
uniform vec3 specularColor;

uniform vec3 Ka;
uniform vec3 Kd;
uniform vec3 Ks;
uniform float Ns;

uniform bool isTexture;
void main() 
{
	// Blinn-Phong
	// I = ambientColor + diffuse * diffuseColor + specular*specularColor;
	// diffuse = cos(A), A -> angle between light direction and normal
	// specular = cos(B)^n, B -> angle between half vector and normal

	// ancillary variables
	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);
	vec3 V = normalize(-f_vertPos);
	vec3 H = normalize(L + V);

	float diffuse = max(dot(N,L), 0.0);
	float specular = 0.0;

	if(diffuse > 0.0) {
       float specAngle = max(dot(H,N), 0.0);
       specular = pow(specAngle, Ns * n); 
    }
	
	vec3 Amb = ambientColor;
	vec3 Dif = diffuseColor;
	vec3 Spec = specularColor;
	
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

	Amb = Amb * Ka;
	Dif = diffuse*Dif * Kd;
	Spec = specular*Spec * Ks;

	outputColor = vec4(Amb + Dif + Spec, 1.0f);
}