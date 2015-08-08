#version 330

precision mediump float;

in vec3 f_normal;
in vec3 f_vertPos;
in vec2 f_texcoord;

out vec4 outputColor;

uniform sampler2D maintexture;

uniform float n;
uniform vec3 lightPos;
uniform vec3 ambientColor;
uniform vec3 diffuseColor;
uniform vec3 specularColor;
uniform vec3 lightDirection;

void main() 
{
	vec3 normal = normalize(f_normal);
    vec3 lightDir = lightDirection;
    vec3 reflectDir = reflect(lightDir, normal);
    vec3 viewDir = normalize(-f_vertPos);


	
	float lambertian = max(dot(lightDir,normal), 0.0);
    float specular = 0.0;

    if(lambertian > 0.0) {
       float specAngle = max(dot(reflectDir, viewDir), 0.0);
       specular = pow(specAngle, 4.0);
    }

	vec3 lighting = ambientColor + lambertian*diffuseColor + specular*specularColor;
	outputColor = vec4(lighting, 1.0);

	// texture
	outputColor += texture2D(maintexture, f_texcoord);
}