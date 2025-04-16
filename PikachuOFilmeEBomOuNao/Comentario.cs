namespace PikachuOFilmeEBomOuNao;

public class Comentario
{
    private readonly string _coment;
    private string _categoria;

    public Comentario(string coment) => Coment = coment;

    public string Coment
    {
        get => _coment;
        init => _coment = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int CountPositivos { get; set; } = 0;

    public int CountNegativos { get; set; } = 0;

    public string Categoria
    {
        get => _categoria;
        set => _categoria = value ?? throw new ArgumentNullException(nameof(value));
    }
}