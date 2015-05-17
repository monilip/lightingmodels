#version 330

precision mediump float;

in vec3 normalInterp;
in vec3 vertPos;

out vec4 outputColor;

uniform float n;
uniform vec3 lightPos;
uniform vec3 ambientColor;
uniform vec3 diffuseColor;
uniform vec3 specularColor;


void main() 
{
	vec3 normal = normalize(normalInterp);
	vec3 lightDir = normalize(lightPos - vertPos);
	vec3 reflectDir = reflect(-lightDir, normal);
	vec3 viewDir = normalize(-vertPos);

	float diffuse = max(dot(lightDir,normal), 0.0);
	float specular = 0.0;

	if(diffuse > 0.0) {
		float nn = n;
		if (nn < 1.0f)
			nn = 1.0f;
       float specAngle = max(dot(reflectDir, viewDir), 0.0);
       specular = pow(specAngle, nn); 
    }
	
	vec3 lighting;

	lighting = ambientColor;
	lighting += diffuse*diffuseColor;
	lighting += specular*specularColor;

	outputColor = vec4(lighting, 1.0);
}