using System.Drawing.Drawing2D;
using System.Globalization;

namespace IMC;

public partial class FormIMC : Form
{
    // ════════════════════════════════════════════════════════════
    //  CONSTANTES E CAMPOS
    // ════════════════════════════════════════════════════════════

    // Paleta de cores usada na lógica (a dos controlos está no Designer)
    private static readonly Color CorTexto = Color.FromArgb(31, 42, 68);
    private static readonly Color CorTextoSuave = Color.FromArgb(148, 163, 184);
    private static readonly Color CorNeutra = Color.FromArgb(203, 213, 225);
    private static readonly Color CorErro = Color.FromArgb(220, 38, 38);

    // Fontes: grande para o resultado, menor para mensagens e erros
    private static readonly Font FonteResultado = new("Segoe UI", 16F, FontStyle.Bold);
    private static readonly Font FonteMensagem = new("Segoe UI", 11F);

    // Classificação da OMS. Cada linha da tabela de referência vem daqui.
    //   Limite    = valor a partir do qual já se passa para a faixa seguinte
    //               (ex.: IMC abaixo de 18,5 = "Abaixo do peso")
    //   Intervalo = texto mostrado na tabela
    private static readonly (double Limite, string Nome, string Intervalo, Color Cor)[] Faixas =
    {
        (18.5,            "Abaixo do peso",     "menos de 18,5",  Color.FromArgb(59, 130, 246)),
        (25.0,            "Peso normal",        "18,5 a 24,9",    Color.FromArgb(34, 197, 94)),
        (30.0,            "Sobrepeso",          "25,0 a 29,9",    Color.FromArgb(234, 179, 8)),
        (35.0,            "Obesidade grau I",   "30,0 a 34,9",    Color.FromArgb(249, 115, 22)),
        (40.0,            "Obesidade grau II",  "35,0 a 39,9",    Color.FromArgb(239, 68, 68)),
        (double.MaxValue, "Obesidade grau III", "40,0 ou mais",   Color.FromArgb(185, 28, 28)),
    };

    // Último IMC calculado (null = ainda não há resultado).
    // A tabela de referência usa este valor para destacar a linha certa.
    private double? _imcAtual;

    // ════════════════════════════════════════════════════════════
    //  CONSTRUTOR
    // ════════════════════════════════════════════════════════════

    public FormIMC()
    {
        InitializeComponent();       // cria os controlos (ficheiro Designer)
        RestaurarEstadoInicial();    // mostra a mensagem inicial
    }

    // ════════════════════════════════════════════════════════════
    //  EVENTOS DOS BOTÕES
    // ════════════════════════════════════════════════════════════

    private void btnCalcular_Click(object? sender, EventArgs e)
    {
        // 1) Conversão segura: TryParse nunca lança excepção,
        //    devolve false se o texto não for um número válido.
        if (!TentarConverter(txtPeso.Text, out double peso))
        {
            MostrarErro("Peso inválido.\nUse apenas números (ex.: 70 ou 70,5).", txtPeso);
            return;
        }

        if (!TentarConverter(txtAltura.Text, out double altura))
        {
            MostrarErro("Altura inválida.\nUse apenas números (ex.: 1,75).", txtAltura);
            return;
        }

        // 2) Valores de zero ou negativos não fazem sentido
        //    (e altura = 0 causaria divisão por zero).
        if (peso <= 0)
        {
            MostrarErro("O peso deve ser maior que zero.", txtPeso);
            return;
        }

        if (altura <= 0)
        {
            MostrarErro("A altura deve ser maior que zero.", txtAltura);
            return;
        }

        // 3) Fórmula padrão: IMC = peso (kg) / altura² (m)
        double imc = peso / (altura * altura);

        // 4) Descobrir a classificação e atualizar a interface
        var faixa = Faixas[ObterIndiceFaixa(imc)];

        lblResultado.Font = FonteResultado;
        lblResultado.ForeColor = CorTexto;
        lblResultado.Text = $"IMC: {imc:F2}{Environment.NewLine}{faixa.Nome}";

        pnlFaixa.BackColor = faixa.Cor;   // tira lateral com a cor da classificação
        _imcAtual = imc;                  // guarda o valor para a tabela
        pnlReferencia.Invalidate();       // força o redesenho da tabela
    }

    private void btnLimpar_Click(object? sender, EventArgs e)
    {
        txtPeso.Clear();
        txtAltura.Clear();
        RestaurarEstadoInicial();
        txtPeso.Focus();
    }

    // ════════════════════════════════════════════════════════════
    //  DESENHO PERSONALIZADO
    // ════════════════════════════════════════════════════════════

    /// <summary>Pinta o cabeçalho com um gradiente azul-escuro → turquesa.</summary>
    private void pnlCabecalho_Paint(object? sender, PaintEventArgs e)
    {
        if (pnlCabecalho.Width == 0 || pnlCabecalho.Height == 0) return;

        using var gradiente = new LinearGradientBrush(
            pnlCabecalho.ClientRectangle,
            Color.FromArgb(31, 42, 68),     // esquerda
            Color.FromArgb(0, 128, 140),    // direita
            LinearGradientMode.Horizontal);

        e.Graphics.FillRectangle(gradiente, pnlCabecalho.ClientRectangle);
    }

    /// <summary>
    /// Desenha a tabela de referência: uma linha por faixa da OMS.
    /// A linha correspondente ao IMC calculado aparece destacada.
    /// </summary>
    private void pnlReferencia_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;   // cantos arredondados suaves

        const float alturaLinha = 48f;
        const float espaco = 6f;

        // Índice da linha a destacar (-1 = nenhuma)
        int ativa = _imcAtual is double valor ? ObterIndiceFaixa(valor) : -1;

        // Fontes e pincéis (using => libertados automaticamente no fim do método)
        using var fonteNome = new Font("Segoe UI", 11F);
        using var fonteNomeAtiva = new Font("Segoe UI", 11F, FontStyle.Bold);
        using var fonteIntervalo = new Font("Segoe UI", 9F);
        using var fonteValor = new Font("Segoe UI", 15F, FontStyle.Bold);
        using var pincelTexto = new SolidBrush(CorTexto);
        using var pincelIntervalo = new SolidBrush(Color.FromArgb(100, 116, 139));
        using var formatoDireita = new StringFormat
        {
            Alignment = StringAlignment.Far,           // alinhado à direita
            LineAlignment = StringAlignment.Center     // centrado na vertical
        };

        for (int i = 0; i < Faixas.Length; i++)
        {
            var faixa = Faixas[i];
            bool eAtiva = i == ativa;

            float y = i * (alturaLinha + espaco);
            var linha = new RectangleF(1, y + 1, pnlReferencia.Width - 3, alturaLinha - 2);

            // a) Fundo da linha (cantos arredondados)
            using (var caminho = RetanguloArredondado(linha, 10f))
            {
                // Linha ativa: cor da faixa com transparência; as outras: cinzento muito claro
                Color corFundo = eAtiva ? Color.FromArgb(32, faixa.Cor) : Color.FromArgb(248, 250, 252);
                using var pincelFundo = new SolidBrush(corFundo);
                g.FillPath(pincelFundo, caminho);

                // Linha ativa também ganha um contorno na cor da faixa
                if (eAtiva)
                {
                    using var contorno = new Pen(faixa.Cor, 2f);
                    g.DrawPath(contorno, caminho);
                }
            }

            // b) Bolinha com a cor da faixa
            using (var pincelCor = new SolidBrush(faixa.Cor))
            {
                g.FillEllipse(pincelCor, 14f, y + (alturaLinha - 16f) / 2f, 16f, 16f);
            }

            // c) Nome da faixa (negrito se for a ativa) e intervalo em kg/m²
            g.DrawString(faixa.Nome, eAtiva ? fonteNomeAtiva : fonteNome, pincelTexto, 40f, y + 6f);
            g.DrawString($"{faixa.Intervalo} kg/m²", fonteIntervalo, pincelIntervalo, 40f, y + 27f);

            // d) Na linha ativa, mostra o valor do IMC à direita
            if (eAtiva && _imcAtual is double imc)
            {
                var areaValor = new RectangleF(0, y, linha.Right - 14f, alturaLinha);
                g.DrawString(imc.ToString("F2"), fonteValor, pincelTexto, areaValor, formatoDireita);
            }
        }
    }

    /// <summary>Cria o contorno de um retângulo com cantos arredondados.</summary>
    private static GraphicsPath RetanguloArredondado(RectangleF r, float raio)
    {
        float d = raio * 2f;   // diâmetro de cada canto
        var caminho = new GraphicsPath();
        caminho.AddArc(r.X, r.Y, d, d, 180, 90);                   // canto superior esquerdo
        caminho.AddArc(r.Right - d, r.Y, d, d, 270, 90);           // canto superior direito
        caminho.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);    // canto inferior direito
        caminho.AddArc(r.X, r.Bottom - d, d, d, 90, 90);           // canto inferior esquerdo
        caminho.CloseFigure();
        return caminho;
    }

    // ════════════════════════════════════════════════════════════
    //  MÉTODOS AUXILIARES
    // ════════════════════════════════════════════════════════════

    /// <summary>
    /// Converte texto em double sem lançar excepções.
    /// Troca a vírgula por ponto e usa a cultura invariante, para funcionar
    /// com qualquer configuração regional do Windows.
    /// </summary>
    private static bool TentarConverter(string texto, out double valor)
    {
        string normalizado = texto.Trim().Replace(',', '.');

        return double.TryParse(
                   normalizado,
                   NumberStyles.Float,
                   CultureInfo.InvariantCulture,
                   out valor)
               && double.IsFinite(valor);   // rejeita "Infinity" e "NaN"
    }

    /// <summary>Devolve a posição (0 a 5) da faixa da OMS a que o IMC pertence.</summary>
    private static int ObterIndiceFaixa(double imc)
    {
        for (int i = 0; i < Faixas.Length; i++)
        {
            if (imc < Faixas[i].Limite)
                return i;
        }
        return Faixas.Length - 1;   // segurança: última faixa
    }

    /// <summary>Mostra uma mensagem de erro no cartão de resultado e foca o campo errado.</summary>
    private void MostrarErro(string mensagem, TextBox campo)
    {
        lblResultado.Font = FonteMensagem;
        lblResultado.ForeColor = CorErro;
        lblResultado.Text = mensagem;

        pnlFaixa.BackColor = CorErro;
        _imcAtual = null;                // remove o destaque da tabela
        pnlReferencia.Invalidate();

        campo.Focus();
        campo.SelectAll();
    }

    /// <summary>Repõe o cartão de resultado como estava ao abrir o programa.</summary>
    private void RestaurarEstadoInicial()
    {
        lblResultado.Font = FonteMensagem;
        lblResultado.ForeColor = CorTextoSuave;
        lblResultado.Text = "Introduza o peso e a altura e carregue em «Calcular IMC».";

        pnlFaixa.BackColor = CorNeutra;
        _imcAtual = null;
        pnlReferencia.Invalidate();
    }
}
