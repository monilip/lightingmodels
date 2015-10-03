#version 330
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

uniform float m;
uniform float s;
uniform float t;

uniform bool isTexture;


float Fresnel(float x) {
	float kf = 1.12;

	float p = 1.0 / (kf*kf);
	float num = 1.0 / ((x - kf)*(x - kf)) - p;
	float denom = 1.0 / ((1.0 - kf) * (1.0 - kf)) - p;

	return num / denom;
}

float Shadow(float x) {
	float ks = 1.01;
	float p = 1.0 / (ks*ks);
	float num = 1.0 / ((1.0 - ks) * (1.0 - ks)) - 1.0 / ((x - ks)*(x - ks));
	float denom = 1.0 / ((1.0 - ks) * (1.0 - ks)) - p;

	return num / denom;
}

void main() 
{
	// Strauss
	// I = Diffuse + Specular
	// Diffuse = cos a * (1 - m * s) * (1 - s^3) * (1 - t) * c
	// c - diffuseColor
	// cos a = NdotL

	// Specular = cos b ^(3/(1 - s) * Reflect * Cs
	// cos b = NdotV
	// Cs = C1 + m * (1 - Fresnel(2a/PI)) * (c - C1)
	// C1 = rgb(1,1,1) - color white
	// a = acos(NdotL)


	// Reflect = min (1, r + j * (r + kj))
	// kj = 0.1;
	// r = (1 - t) - (1 - s^3) * (1-t)
	// j = Fresnel(2a/PI) * Shadow(2a/PI) * Shadow(2b/PI)
	// b = acos(NdotV)
	
	// Fresnel(x) = (1/(x - kf)^2 - 1/kf^2) / (1/(1 - kf)^2 - 1/kf^2)) 
	// Shadow(x) = (1/(1 - ks)^2 - 1/(x - ks)^2) / (1/(1 - ks)^2 - 1/ks^2)) 
	// kf = 1.12
	// ks = 1.01

	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);
	vec3 V = normalize(-f_vertPos);	
	vec3 H = normalize(L + V);
	float NdotL = dot(N,L); 
	float NdotV = dot(N,V);
	float HdotV = dot(H,V);

	vec3 c = diffuseColor;
	if (isTexture == true)
	{
		c.r +=texture2D(maintexture, f_texcoord).r;
		c.g +=texture2D(maintexture, f_texcoord).g;
		c.b +=texture2D(maintexture, f_texcoord).b;
	}
	else
	{
		c += vec3(f_color);
	}

	vec3 Diffuse = NdotL * ( 1 - m * s) * (1 - pow(s,3)) * (1 - t) * c;

	float r = (1 - t) - (1 - pow(s,3)) * (1-t);
	float a = acos(NdotL);
	float b = acos(NdotV);
	float j = Fresnel(2*a/PI) * Shadow(2*a/PI) * Shadow(2*b/PI);
	float k = 0.1;
	float Reflect = min (1.0, r + j * (r + k));

	vec3 C1 = vec3(1);
	vec3 Cs = C1 + m * (1 - Fresnel(2*a/PI)) * (c - C1);
	vec3 Specular = pow(HdotV,(3/(1 - s))) * Reflect * Cs;


	outputColor = vec4((Diffuse+Specular),1.0);
}

