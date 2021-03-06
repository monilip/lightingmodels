﻿#version 330
#define PI 3.14159265358
precision mediump float;

in vec3 f_normal;
in vec3 f_vertPos;
in vec4 f_color;
in vec2 f_texcoord;

out vec4 outputColor;

uniform sampler2D maintexture;

uniform vec3 lightPos;
uniform vec3 diffuseColor;
uniform vec3 specularColor;

uniform float alphaX;
uniform float alphaY;

uniform bool isTexture;

void main() 
{
	// Ward
	// I = (Pd + Spec) * NdotL;

	// Spec = Ps * 1 / ( 4 * PI * alphaX * alphaY * sqrt(NdotL * NdotV)) * exp(Beta) 

	// Beta = - pow( tan( acos( HdotN ) ), 2 ) * (pow( HdotT/alphaX, 2) + pow (HdotB/alphaY, 2))/ 

	// HdotT = max(dot(H,T),0.0)
	// HdotB = max(dot(H,B),0.0)
	// HdotN = max(dot(H,N),0.0)

	// T = normalize(cross(N,epsilon));
	// B = normalize(cross(N,T));
	// epsilon = Vector3(0.0,0.0,-1.0);	

	// Pd = diffuseColor;
	// Ps = specColor;
	
	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);
	vec3 V = normalize(-f_vertPos);	
	vec3 H = normalize(L + V);

	float NdotL = clamp(dot(N,L),0.0,1.0);  
	float NdotH = clamp(dot(N,H),0.0,1.0);
	float NdotV = clamp(dot(N,V),0.0,1.0);
	float VdotH = clamp(dot(V,H),0.0,1.0);

	vec3 Pd = diffuseColor;
	
	// texture or color
	if (isTexture == true)
	{
		Pd.r +=texture2D(maintexture, f_texcoord).r;
		Pd.g +=texture2D(maintexture, f_texcoord).g;
		Pd.b +=texture2D(maintexture, f_texcoord).b;
	}
	else
	{
		Pd += vec3(f_color);
	}

	vec3 Ps = specularColor;
	vec3 epsilon = vec3(1.0f,0.0f,0.0f); 
	vec3 T = normalize(cross(N,epsilon));
	vec3 B = normalize(cross(N,T));
	float HdotT = dot(H,T);
	float HdotB = dot(H,B);
	float HdotN = dot(H,N);

	float Beta = - 2  * (pow(HdotT/alphaY,2) + pow(HdotB/alphaX,2));
	vec3 Spec = Ps * 1.0f /((4.0f * PI * alphaX * alphaY) * sqrt(NdotL * NdotV))  * exp(Beta/(1 + NdotH));

	vec3 lighting = NdotL * (Pd + Spec);

	outputColor = vec4(lighting,1.0);
}

