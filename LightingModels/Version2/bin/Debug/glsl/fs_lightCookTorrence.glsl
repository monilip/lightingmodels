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

uniform float F0;
uniform float m;

uniform bool isTexture;

uniform vec3 Ka;
uniform vec3 Kd;
uniform vec3 Ks;

void main() 
{
	// Cook-Torrence
	// R = I * (specularColor * RS * + diffuseColor)
	// I = cos(A), A -> angle between light direction and normal => max(0.0,clamp(dot(lightDir,normal),0.0,1.0))
					
	// RS = RS_numerator / RS_denominator

	// RS_numerator = Fresnel x Roughness x Geometric

	// Fresnel (Schlick's approximation) = F0 + (1-F0) * (1 - VdotH)^5
	// F0 -> put in shader (goes from 0.0 to 1.0) 

	// Roughness (Beckmann’s distribution) = (1 / R1) * exp(R2)
	// R1 = cos(B) ^ 4 * roughnessSquare, B -> angle between normal and half vector

	// from D3DBook:(Lighting)_Cook-Torrance
	// R2 = -(tan(B)/m)^2
	// tan^2(B) = sin^2(B) / cos^2(B)
	// sin^2(B) + cos^2(B) = 1
	// tan^2(B) = (1 - cos^2(B)) / cos^2(B)
	// -(tan(B)/m)^2 = - ((1 - cos^2(B)) / cos^2(B)/m^2) = (cos^2(B) - 1)/(cos^2(B) * m^2)
	// R2 = (cos^2(B) - 1)/(cos^2(B) * m^2)

	// Geometric = min(1, min(G1, G2))
	// G1 = 2 * cos(B) * cos(C) / cos(D), C -> angle between normal and vector to viewer, D -> angle between vector to viewer and half vector
	// G2 = 2 * cos(B) * cos(A) / cos(D)

	// RS_denominator = PI * cos(C) * cos(A)
	
	// ancillary variables
	vec3 N = normalize(f_normal);
	vec3 L = normalize(lightPos - f_vertPos);
	vec3 V = normalize(-f_vertPos);	
	vec3 H = normalize(L + V);

	float NdotL = clamp(dot(N,L),0.0,1.0);  
	float NdotH = clamp(dot(N,H),0.0,1.0);
	float NdotV = clamp(dot(N,V),0.0,1.0);
	float VdotH = clamp(dot(V,H),0.0,1.0);

	// Geometric
	float G1 = 2 * NdotH * NdotV / VdotH;
	float G2 = 2 * NdotH * NdotL / VdotH;
	float Geometric = min(1.0, min(G1, G2));

	/// Roughness (Beckmann’s distribution)
	float R1 = pow(m,2.0f) * pow(NdotH,4.0f);
	float R2 = (NdotH * NdotH - 1.0f) / (NdotH * NdotH * pow(m,2.0f) );
	float Roughness = (1.0f / R1) * exp(R2);

	// Fresnel
	float Fresnel = pow((1.0f-VdotH),5.0f);
	Fresnel *= (1.0f-F0);
	Fresnel += F0;
	
	vec3 RS_numerator = vec3(Fresnel * Geometric * Roughness);
	float RS_denominator = PI * NdotV * NdotL;
	vec3 RS = RS_numerator / RS_denominator;

	// texture
	if (isTexture == true)
	{
		vec3 Dif = diffuseColor;
		Dif.r +=texture2D(maintexture, f_texcoord).r;
		Dif.g +=texture2D(maintexture, f_texcoord).g;
		Dif.b +=texture2D(maintexture, f_texcoord).b;

		vec3 lighting = max(0.0, NdotL) * (specularColor * Ks * RS + Dif * Kd);

		outputColor = vec4(lighting,texture2D(maintexture, f_texcoord).a);
	}
	else
	{
		vec3 Dif = diffuseColor;

		Dif += vec3(f_color);

		vec3 lighting = max(0.0, NdotL) * (specularColor * Ks * RS + Dif * Kd);
		outputColor = vec4(lighting,1.0);
	}
}