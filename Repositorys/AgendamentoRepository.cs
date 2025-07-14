using System;
using System.Data.SqlClient;
using Dapper;

public class AgendamentoRepository
{
    private readonly string _connectionString;

    public AgendamentoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int CriarUsuario(string nome, string email, string telefone)
    {
        using var conn = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO Usuarios (NomeCompleto, Email, Telefone)
                    VALUES (@nome, @email, @telefone);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return conn.QuerySingle<int>(sql, new { nome, email, telefone });
    }

    public int CriarAgendamento(int usuarioId, int eventoId, int horarioId, string codigo)
    {
        using var conn = new SqlConnection(_connectionString);
        var sql = @"INSERT INTO Agendamentos (UsuarioId, EventoId, HorarioId, DataAgendamento, Codigo)
                    VALUES (@usuarioId, @eventoId, @horarioId, GETDATE(), @codigo);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return conn.QuerySingle<int>(sql, new { usuarioId, eventoId, horarioId, codigo });
    }

    public int ObterVagasRestantes(int horarioId)
    {
        using var conn = new SqlConnection(_connectionString);
        var sql = @"SELECT h.VagasTotais - COUNT(a.Id)
                    FROM Horarios h
                    LEFT JOIN Agendamentos a ON a.HorarioId = h.Id
                    WHERE h.Id = @horarioId
                    GROUP BY h.VagasTotais;";
        return conn.QuerySingleOrDefault<int>(sql, new { horarioId });
    }

    public (string Titulo, DateTime DataHora) ObterDadosEventoHorario(int eventoId, int horarioId)
    {
        using var conn = new SqlConnection(_connectionString);
        var sql = @"SELECT e.Titulo, h.DataHora
                    FROM Eventos e
                    JOIN Horarios h ON h.EventoId = e.Id
                    WHERE e.Id = @eventoId AND h.Id = @horarioId;";
        return conn.QuerySingle<(string, DateTime)>(sql, new { eventoId, horarioId });
    }
}
