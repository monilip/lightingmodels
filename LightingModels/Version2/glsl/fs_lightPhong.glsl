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
uniform vec3 lightDirection;

uniform vec3 Ka;
uniform vec3 Kd;
uniform vec3 Ks;
uniform float Ns;

uniform bool isTexture;
uniform bool enableLighting;
void main() 
{
	vec3 normal = normalize(f_normal);
	vec3 lightDir = normalize(lightPos - f_vertPos);
	vec3 reflectDir = reflect(-lightDir, normal);
	vec3 viewDir = normalize(-f_vertPos);

	float diffuse = max(dot(lightDir,normal), 0.0);
	float specular = 0.0;

	if(diffuse > 0.0) {
       float specAngle = max(dot(reflectDir, viewDir), 0.0);
       specular = pow(specAngle, Ns); 
    }
	
	vec3 lighting;

	lighting = ambientColor * Ka;
	lighting += diffuse*diffuseColor * Kd;
	lighting += specular*specularColor * Ks;

	if (enableLighting == true)
		outputColor = vec4(lighting, 1.0);

	// texture
	if (isTexture == true)
		outputColor += texture2D(maintexture, f_texcoord);
	else
		outputColor += f_color;
}