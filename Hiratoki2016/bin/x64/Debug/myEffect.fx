float4 MyVertexShader(float4 position : SV_Position) : SV_Position
{
    return position;
}
 
float4 MyPixelShader() : SV_Target
{
    return float4(1, 1, 1, 1);
}
 
technique10 MyTechnique
{
	pass MyPass
	{
		SetVertexShader( CompileShader( vs_5_0, MyVertexShader() ) );
		SetPixelShader( CompileShader( ps_5_0, MyPixelShader() ) );
	}
}
