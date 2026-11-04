#nullable enable

namespace IMC;

partial class FormIMC
{
    private System.ComponentModel.IContainer? components = null;

    // ── Cabeçalho ───────────────────────────────────────────────
    private PainelBuffer pnlCabecalho = null!;
    private Label lblTitulo = null!;
    private Label lblSubtitulo = null!;

    // ── Cartão 1 (esquerda, topo): entrada de dados ─────────────
    private PainelBuffer pnlEntrada = null!;
    private Label lblPeso = null!;
    private Label lblAltura = null!;
    private Label lblDica = null!;
    private TextBox txtPeso = null!;
    private TextBox txtAltura = null!;
    private Button btnCalcular = null!;
    private Button btnLimpar = null!;

    // ── Cartão 2 (esquerda, base): resultado ────────────────────
    private PainelBuffer pnlResultado = null!;
    private Panel pnlFaixa = null!;              // tira lateral que muda de cor
    private Label lblTituloResultado = null!;
    private Label lblResultado = null!;

    // ── Cartão 3 (direita): tabela de referência ────────────────
    private PainelBuffer pnlCartaoReferencia = null!;
    private PainelBuffer pnlReferencia = null!;  // linhas desenhadas no evento Paint
    private Label lblTituloReferencia = null!;
    private Label lblReferenciaNota = null!;

    // ── Rodapé ──────────────────────────────────────────────────
    private Panel pnlRodape = null!;
    private Label lblRodape = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        // Criação de todos os controlos
        pnlCabecalho = new PainelBuffer();
        lblTitulo = new Label();
        lblSubtitulo = new Label();
        pnlEntrada = new PainelBuffer();
        lblPeso = new Label();
        lblAltura = new Label();
        lblDica = new Label();
        txtPeso = new TextBox();
        txtAltura = new TextBox();
        btnCalcular = new Button();
        btnLimpar = new Button();
        pnlResultado = new PainelBuffer();
        pnlFaixa = new Panel();
        lblTituloResultado = new Label();
        lblResultado = new Label();
        pnlCartaoReferencia = new PainelBuffer();
        pnlReferencia = new PainelBuffer();
        lblTituloReferencia = new Label();
        lblReferenciaNota = new Label();
        pnlRodape = new Panel();
        lblRodape = new Label();
        SuspendLayout();

        // ════════════════ CABEÇALHO (gradiente desenhado no Paint) ════════════════
        pnlCabecalho.Dock = DockStyle.Top;
        pnlCabecalho.Name = "pnlCabecalho";
        pnlCabecalho.Size = new Size(760, 90);
        pnlCabecalho.Paint += pnlCabecalho_Paint;
        pnlCabecalho.Controls.Add(lblTitulo);
        pnlCabecalho.Controls.Add(lblSubtitulo);

        lblTitulo.AutoSize = true;
        lblTitulo.BackColor = Color.Transparent;
        lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.White;
        lblTitulo.Location = new Point(26, 10);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Text = "Calculadora de IMC";

        lblSubtitulo.AutoSize = true;
        lblSubtitulo.BackColor = Color.Transparent;
        lblSubtitulo.Font = new Font("Segoe UI", 10F);
        lblSubtitulo.ForeColor = Color.FromArgb(200, 215, 235);
        lblSubtitulo.Location = new Point(30, 58);
        lblSubtitulo.Name = "lblSubtitulo";
        lblSubtitulo.Text = "Índice de Massa Corporal  •  Classificação da OMS";

        // ════════════════ CARTÃO 1: ENTRADA DE DADOS ════════════════
        pnlEntrada.BackColor = Color.White;
        pnlEntrada.CorBorda = Color.FromArgb(222, 229, 239);
        pnlEntrada.Location = new Point(30, 115);
        pnlEntrada.Name = "pnlEntrada";
        pnlEntrada.Size = new Size(340, 240);
        pnlEntrada.Controls.Add(lblPeso);
        pnlEntrada.Controls.Add(txtPeso);
        pnlEntrada.Controls.Add(lblAltura);
        pnlEntrada.Controls.Add(txtAltura);
        pnlEntrada.Controls.Add(btnCalcular);
        pnlEntrada.Controls.Add(btnLimpar);
        pnlEntrada.Controls.Add(lblDica);

        // Peso
        lblPeso.AutoSize = true;
        lblPeso.Font = new Font("Segoe UI Semibold", 9F);
        lblPeso.ForeColor = Color.FromArgb(90, 103, 130);
        lblPeso.Location = new Point(20, 16);
        lblPeso.Name = "lblPeso";
        lblPeso.Text = "PESO (kg)";

        txtPeso.BackColor = Color.FromArgb(248, 250, 252);
        txtPeso.BorderStyle = BorderStyle.FixedSingle;
        txtPeso.Font = new Font("Segoe UI", 13F);
        txtPeso.Location = new Point(20, 38);
        txtPeso.Name = "txtPeso";
        txtPeso.PlaceholderText = "Ex.: 70,5";
        txtPeso.Size = new Size(300, 31);
        txtPeso.TabIndex = 0;

        // Altura
        lblAltura.AutoSize = true;
        lblAltura.Font = new Font("Segoe UI Semibold", 9F);
        lblAltura.ForeColor = Color.FromArgb(90, 103, 130);
        lblAltura.Location = new Point(20, 86);
        lblAltura.Name = "lblAltura";
        lblAltura.Text = "ALTURA (m)";

        txtAltura.BackColor = Color.FromArgb(248, 250, 252);
        txtAltura.BorderStyle = BorderStyle.FixedSingle;
        txtAltura.Font = new Font("Segoe UI", 13F);
        txtAltura.Location = new Point(20, 108);
        txtAltura.Name = "txtAltura";
        txtAltura.PlaceholderText = "Ex.: 1,75";
        txtAltura.Size = new Size(300, 31);
        txtAltura.TabIndex = 1;

        // Botão principal (turquesa)
        btnCalcular.BackColor = Color.FromArgb(0, 169, 157);
        btnCalcular.Cursor = Cursors.Hand;
        btnCalcular.FlatStyle = FlatStyle.Flat;
        btnCalcular.FlatAppearance.BorderSize = 0;
        btnCalcular.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 140, 130);
        btnCalcular.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 115, 108);
        btnCalcular.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnCalcular.ForeColor = Color.White;
        btnCalcular.Location = new Point(20, 160);
        btnCalcular.Name = "btnCalcular";
        btnCalcular.Size = new Size(190, 46);
        btnCalcular.TabIndex = 2;
        btnCalcular.Text = "Calcular IMC";
        btnCalcular.UseVisualStyleBackColor = false;
        btnCalcular.Click += btnCalcular_Click;

        // Botão secundário (cinzento)
        btnLimpar.BackColor = Color.FromArgb(226, 232, 240);
        btnLimpar.Cursor = Cursors.Hand;
        btnLimpar.FlatStyle = FlatStyle.Flat;
        btnLimpar.FlatAppearance.BorderSize = 0;
        btnLimpar.FlatAppearance.MouseOverBackColor = Color.FromArgb(203, 213, 225);
        btnLimpar.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 192, 210);
        btnLimpar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLimpar.ForeColor = Color.FromArgb(51, 65, 85);
        btnLimpar.Location = new Point(220, 160);
        btnLimpar.Name = "btnLimpar";
        btnLimpar.Size = new Size(100, 46);
        btnLimpar.TabIndex = 3;
        btnLimpar.Text = "Limpar";
        btnLimpar.UseVisualStyleBackColor = false;
        btnLimpar.Click += btnLimpar_Click;

        // Dica
        lblDica.AutoSize = true;
        lblDica.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblDica.ForeColor = Color.FromArgb(148, 163, 184);
        lblDica.Location = new Point(20, 214);
        lblDica.Name = "lblDica";
        

        // ════════════════ CARTÃO 2: RESULTADO ════════════════
        pnlResultado.BackColor = Color.White;
        pnlResultado.CorBorda = Color.FromArgb(222, 229, 239);
        pnlResultado.Location = new Point(30, 375);
        pnlResultado.Name = "pnlResultado";
        pnlResultado.Size = new Size(340, 140);
        pnlResultado.Controls.Add(pnlFaixa);
        pnlResultado.Controls.Add(lblTituloResultado);
        pnlResultado.Controls.Add(lblResultado);

        // Tira lateral: muda de cor conforme a classificação
        pnlFaixa.BackColor = Color.FromArgb(203, 213, 225);
        pnlFaixa.Dock = DockStyle.Left;
        pnlFaixa.Name = "pnlFaixa";
        pnlFaixa.Size = new Size(8, 140);

        lblTituloResultado.AutoSize = true;
        lblTituloResultado.Font = new Font("Segoe UI Semibold", 9F);
        lblTituloResultado.ForeColor = Color.FromArgb(90, 103, 130);
        lblTituloResultado.Location = new Point(28, 14);
        lblTituloResultado.Name = "lblTituloResultado";
        lblTituloResultado.Text = "O SEU RESULTADO";

        // Etiqueta pedida no enunciado: mostra o IMC e a classificação
        lblResultado.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblResultado.ForeColor = Color.FromArgb(31, 42, 68);
        lblResultado.Location = new Point(28, 38);
        lblResultado.Name = "lblResultado";
        lblResultado.Size = new Size(295, 90);
        lblResultado.Text = "lblResultado";

        // ════════════════ CARTÃO 3: TABELA DE REFERÊNCIA ════════════════
        pnlCartaoReferencia.BackColor = Color.White;
        pnlCartaoReferencia.CorBorda = Color.FromArgb(222, 229, 239);
        pnlCartaoReferencia.Location = new Point(390, 115);
        pnlCartaoReferencia.Name = "pnlCartaoReferencia";
        pnlCartaoReferencia.Size = new Size(340, 400);
        pnlCartaoReferencia.Controls.Add(lblTituloReferencia);
        pnlCartaoReferencia.Controls.Add(pnlReferencia);
        pnlCartaoReferencia.Controls.Add(lblReferenciaNota);

        lblTituloReferencia.AutoSize = true;
        lblTituloReferencia.Font = new Font("Segoe UI Semibold", 9F);
        lblTituloReferencia.ForeColor = Color.FromArgb(90, 103, 130);
        lblTituloReferencia.Location = new Point(20, 16);
        lblTituloReferencia.Name = "lblTituloReferencia";
        lblTituloReferencia.Text = "TABELA DE REFERÊNCIA";

        // As 6 linhas coloridas são desenhadas no evento Paint
        pnlReferencia.BackColor = Color.White;
        pnlReferencia.Location = new Point(20, 44);
        pnlReferencia.Name = "pnlReferencia";
        pnlReferencia.Size = new Size(300, 320);
        pnlReferencia.Paint += pnlReferencia_Paint;

        lblReferenciaNota.AutoSize = true;
        lblReferenciaNota.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        lblReferenciaNota.ForeColor = Color.FromArgb(148, 163, 184);
        lblReferenciaNota.Location = new Point(20, 374);
        lblReferenciaNota.Name = "lblReferenciaNota";
        
        // ════════════════ RODAPÉ ════════════════
        pnlRodape.BackColor = Color.FromArgb(31, 42, 68);
        pnlRodape.Dock = DockStyle.Bottom;
        pnlRodape.Name = "pnlRodape";
        pnlRodape.Size = new Size(760, 40);
        pnlRodape.Controls.Add(lblRodape);

        lblRodape.Dock = DockStyle.Fill;
        lblRodape.Font = new Font("Segoe UI", 9F);
        lblRodape.ForeColor = Color.FromArgb(170, 184, 210);
        lblRodape.Name = "lblRodape";
        lblRodape.TextAlign = ContentAlignment.MiddleCenter;

        // ════════════════ FORMULÁRIO ════════════════
        AcceptButton = btnCalcular;                      // Enter = Calcular
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(238, 242, 248);
        ClientSize = new Size(760, 580);
        Controls.Add(pnlCabecalho);
        Controls.Add(pnlEntrada);
        Controls.Add(pnlResultado);
        Controls.Add(pnlCartaoReferencia);
        Controls.Add(pnlRodape);
        Font = new Font("Segoe UI", 10F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "FormIMC";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Calculadora de IMC";
        ResumeLayout(false);
    }
}
