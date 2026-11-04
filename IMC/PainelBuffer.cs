using System.ComponentModel;

namespace IMC;

/// <summary>
/// Painel com duplo buffer (evita cintilação quando é redesenhado)
/// e com uma borda fina opcional, usado para criar o aspeto de "cartão".
/// </summary>
public class PainelBuffer : Panel
{
    /// <summary>Cor da borda de 1 px. Se for Color.Empty, não desenha borda.</summary>
    // O atributo diz ao designer do Visual Studio para não gerar código para esta
    // propriedade (resolve o erro WFO1000). Nós definimos o valor à mão no Designer.cs.
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color CorBorda { get; set; } = Color.Empty;

    public PainelBuffer()
    {
        DoubleBuffered = true;   // desenha primeiro em memória e só depois no ecrã
        ResizeRedraw = true;     // redesenha ao mudar de tamanho
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);   // dispara o evento Paint (desenhos personalizados)

        if (CorBorda != Color.Empty)
        {
            using var caneta = new Pen(CorBorda);
            e.Graphics.DrawRectangle(caneta, 0, 0, Width - 1, Height - 1);
        }
    }
}
