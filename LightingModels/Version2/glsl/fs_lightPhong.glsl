﻿#version 330

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

void main() 
{
	vec3 normal = normalize(f_normal);
	vec3 lightDir = normalize(lightPos - f_vertPos);
	vec3 R = normalize(-reflect(lightDir, normal));
	vec3 E = normalize(-f_vertPos);

	float diffuse = max(dot(normal, lightDir), 0.0);
	float specular = 0.0;

	if(diffuse > 0.0) {
		float nn = n;
		if (nn < 1.0f)
			nn = 1.0f;
       float specAngle = max(dot(R, E), 0.0);
       specular = pow(specAngle, nn); 
    }
	
	vec3 lighting;

	lighting = ambientColor;
	lighting += clamp(diffuse*diffuseColor, 0.0, 1.0);
	lighting += clamp(specular*specularColor, 0.0, 1.0);;
			
	outputColor = vec4(lighting, 1.0);

	// texture
	outputColor += texture2D(maintexture, f_texcoord);
}