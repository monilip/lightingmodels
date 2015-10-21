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
uniform vec3 surfColor;

uniform bool isTexture;

#define SQR(x) ((x) * (x))

float fresnel(float x) {
	const float kf = 1.12;

	float p = 1.0 / SQR(kf);
	float num = 1.0 / SQR(2*x/PI - kf) - p;
	float denom = 1.0 / SQR(1.0 - kf) - p;

	return num / denom;
}

float shadow(float x) {
	const float ks = 1.01;

	float p = 1.0 / SQR(1.0 - ks);
	float num = p - 1.0 / SQR(2*x/PI - ks);
	float denom = p - 1.0 / SQR(ks);

	return num / denom;
}
void main() 
{


	vec3 n = normalize(f_normal);
	vec3 l = normalize(lightPos - f_vertPos);
	vec3 v = normalize(-f_vertPos);	
	vec3 h = reflect(l, n);

	// Declare any aliases:
	float NdotL = dot(n, l);
	float NdotV = dot(n, v);
	float HdotV = dot(h, v);
	float fNdotL = fresnel(NdotL);
	float s_cubed = s * s * s;

	// Diffuse
	float d = (1.0 - m * s);
	float Rd = (1.0 - s_cubed) * (1.0 - t);

	vec3 c = surfColor;
	if (isTexture == true)
	{
		c.r +=texture2D(maintexture, f_texcoord).r;
		c.g +=texture2D(maintexture, f_texcoord).g;
		c.b +=texture2D(maintexture, f_texcoord).b;
	}
	else
	{
		c.r +=f_color.r;
		c.g +=f_color.g;
		c.b +=f_color.b;
	}
	
	vec3 diffuse = NdotL * Rd * d * c;

	// Specular
	float r = (1.0 - t) - Rd;

	float j = fNdotL * shadow(NdotL) * shadow(NdotV);

	const float k = 0.1;
	float reflect = min(1.0, r + j * (r + k));

	vec3 C1 = vec3(1.0, 1.0, 1.0);
	vec3 Cs = C1 + m * (1.0 - fNdotL) * (c - C1);

	vec3 specular = vec3(1) * pow(-HdotV, 3.0 / (1.0 - s)); 
	specular *= reflect * Cs;

	diffuse = max(vec3(0.0), diffuse);
	specular = max(vec3(0.0), specular);

	outputColor = vec4((diffuse+specular),1.0);
	//outputColor = vec4(1.0);
}

