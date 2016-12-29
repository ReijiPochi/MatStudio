
matrix World, ViewProjection;
Texture2D DiffuseTexture;
float4 Color;

SamplerState mySampler
{

};

struct MatVertexDataPNT
{
	float4 position : SV_Position;
	float4 normal : NORMAL;
	float2 texCoord : TEXCOORD;
};

MatVertexDataPNT MyVertexShader(MatVertexDataPNT input)
{
	MatVertexDataPNT output = input;

	output.position = mul(output.position, World);
	output.normal = mul(output.normal, World);

	output.position = mul(output.position, ViewProjection);
	return output;
}

float4 MyPixelShader(MatVertexDataPNT input) : SV_Target
{
	return DiffuseTexture.Sample(mySampler, input.texCoord) * (0.8 - dot(float3(0.0,0.0,-1.0), input.normal) * 0.3) + Color;
}

technique10 MyTechnique
{
	pass MyPass
	{
		SetVertexShader(CompileShader(vs_5_0, MyVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, MyPixelShader()));
	}
}