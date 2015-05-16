#version 330

precision mediump float;

in vec4 color;
in vec3 normalInterp;
in vec3 vertPos;

out vec4 outputColor;

const vec3 lightPos = vec3(1.0,1.0,1.0);
const vec3 ambientColor = vec3(0.3, 0.0, 0.0);
const vec3 diffuseColor = vec3(0.5, 0.0, 0.0);
const vec3 specColor = vec3(1.0, 1.0, 1.0);

void main() 
{
	vec3 normal = normalize(normalInterp);
	vec3 lightDir = normalize(lightPos - vertPos);
	vec3 reflectDir = reflect(-lightDir, normal);
	vec3 viewDir = normalize(-vertPos);

	float diffuse = max(dot(lightDir,normal), 0.0);
	float specular = 0.0;

	if(diffuse > 0.0) {
       float specAngle = max(dot(reflectDir, viewDir), 0.0);
       specular = pow(specAngle, 4.0);
    }


	vec3 lighting;

	lighting = ambientColor;
	lighting += diffuse*diffuseColor;
	lighting += specular*specColor;

	outputColor = vec4(lighting, 1.0);
}