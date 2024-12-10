Shader "QuadMask"
{
    SubShader
    {
        Tags { "Queue"="Geometry" }
        Pass
        {
            Stencil
            {
                Ref 1         
                Comp Always   
                Pass Replace   
            }
            ColorMask 0       
        }
    }
}
