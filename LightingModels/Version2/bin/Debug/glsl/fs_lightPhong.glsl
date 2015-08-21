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
	// Phong
	// P = ambientColor + diffuse * diffuseColor + specular*specularColor;
	// diffuse = cos(A), A -> angle between light direction and normal
	// specular = cos(B)^n, B -> angle between reflected light direction and vector to viewer

	// ancillary variables
	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);
	vec3 R = -reflect(L, N);
	vec3 V = normalize(-f_vertPos);

	float diffuse = max(dot(N,L), 0.0);
	float specular = 0.0;

	if(diffuse > 0.0) {
       float specAngle = max(dot(R, V), 0.0);
       specular = pow(specAngle, Ns); 
    }
	
	vec3 lighting;

	lighting = ambientColor * Ka;
	lighting += diffuse*diffuseColor * Kd;
	lighting += specular*specularColor * Ks;

	outputColor = vec4(lighting, 1.0);

	// texture
	if (isTexture == true)
		outputColor += texture2D(maintexture, f_texcoord);
	else
		outputColor += f_color;
}