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

uniform float Nu;
uniform float Nv;
uniform vec3 Rs; //color (spectrum or RGB) that specifies the specular reflectance at normal incidence???
uniform float lighter; // PD is too dark, need's to be lighter
uniform bool isTexture;

uniform vec3 Kd;
uniform vec3 Ks;

void main() 
{
	// Ashikhmin-Shirley
	// I = Pd + Ps * specularColor;
  
	// Pd = (28.0 * Rd) / (23.0 * PI) * (1 - Rs) * Pdnl * Pdnv
	// Pdnl = 1.0 - pow(1.0 - 0.5 * NdolL, 5.0)
	// Pdnv = 1.0 - pow(1.0 - 0.5 * NdotV, 5.0)
  
	// Ps = Ps_numerator / Ps_denominator * F
	
	// Ps_numerator = Ps_sqrt  * pow(NdotH, Ps_exp) 
	// Ps_sqrt = sqrt((nu + 1) * (nv + 1))
	// Ps_exp = (nu * HdotT * HdotT + nv * HdotB * HdotB) / (1.0 - HdotN * HdotN)

	// Ps_denominator = 8.0 * PI  * HdotL * max(NdotL, NdotV)
	
	// ancillary variables
	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);
	vec3 V = normalize(-f_vertPos);	
	vec3 H = normalize(L + V);
	float NdotL = clamp(dot(N,L),0.0,1.0); 
	float NdotV = clamp(dot(N,V),0.0,1.0);

	vec3 epsilon = vec3(1.0,0.0,0.0); 
	vec3 T = normalize(cross(N,epsilon));
	vec3 B = normalize(cross(N,T));
	float HdotT = dot(H,T);
	float HdotB = dot(H,B);
	float HdotN = dot(H,N);
	float HdotL = dot(H,L);
	float HdotV = dot(H,V);

	// uniform
	vec3 Rd = diffuseColor;

	if (isTexture == true)
	{
		Rd.r +=texture2D(maintexture, f_texcoord).r;
		Rd.g +=texture2D(maintexture, f_texcoord).g;
		Rd.b +=texture2D(maintexture, f_texcoord).b;
	}
	else
	{
		Rd += vec3(f_color);
	}

	float PD_NdotL = 1.0 - pow(1.0 - 0.5 * NdotL, 5);
	float PD_NdotV = 1.0 - pow(1.0 - 0.5 * NdotV, 5);  
  
	vec3 PD = (28.0 * Rd) / (23.0 * PI) * (vec3(1.0) - Rs)* PD_NdotL * PD_NdotV;
	PD *= lighter;
  
	float PS_denominator = 8.0 * PI * HdotL * max(NdotL, NdotV);
	float PS_exp = (Nu * HdotT * HdotT + Nv * HdotB * HdotB) / (1.0 - HdotN * HdotN); 
	float PS_sqrt = sqrt((Nu + 1.0) * (Nv + 1.0));
	float PS_numerator = PS_sqrt  * pow(HdotN, PS_exp);

	float Fresnel = (1.0-HdotV) * (1.0-HdotV) * (1.0-HdotV) * (1.0-HdotV) * (1.0-HdotV);
	Fresnel *= float(vec3(1.0) - Rs);
	Fresnel += Rs;
	vec3 PS = specularColor * (PS_numerator / PS_denominator ) ;//* Fresnel;	

	PD *= Kd;
	PS *= Ks;

	outputColor = vec4(PS + PD,1.0);
	//outputColor = vec4(vec3(1),1.0);
}