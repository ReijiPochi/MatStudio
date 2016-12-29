
matrix ViewProjection;
float4 Color;

struct MatVertexDataPNT
{
	float4 position : SV_Position;
	float4 normal : NORMAL;
	float2 texCoord : TEXCOORD;
};

MatVertexDataPNT MyVertexShader(MatVertexDataPNT input)
{
	MatVertexDataPNT output = input;
	output.position = mul(output.position, ViewProjection);
	return output;
}

float4 MyPixelShader(MatVertexDataPNT input) : SV_Target
{
	return Color;
}

technique10 MyTechnique
{
	pass MyPass
	{
		SetVertexShader(CompileShader(vs_5_0, MyVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, MyPixelShader()));
	}
}